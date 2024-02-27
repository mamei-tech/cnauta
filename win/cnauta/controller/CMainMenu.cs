using System;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;

using cnauta.view;
using cnauta.model;
using cnauta.model.schema;
using cnauta.view.ifaces;

namespace cnauta.controller
{
    public class CMainMenu
    {
        #region ============ FIELDS ==================================================

        private ushort _dCnxAttempts;                                                       // disconnection attempts | UInt16
        private bool _flagCfgJustChecked;                                                   // helps to know if we already done the first load of the config file, its very important if we are recovering from a previous connected status 
        
        private readonly IViewMainMenuCtx _view;
        private CancellationTokenSource _cTkSource;                                         // token use to send run termination signals  
        
        #endregion ===================================================================
        
        #region ============ CONSTRUCTORS ============================================

        public CMainMenu(IViewMainMenuCtx view)
        {
            _view = view;
            _cTkSource = null;

            _dCnxAttempts = 1;                                                              // disconnect attempts counter
            _flagCfgJustChecked = false;

            _view.EhComputeCfg += ChkCfgFromFile;
            
            _view.EhExit += VActionExit;
            _view.EhChkSts += VActionChkSts;
            _view.EhConnect += VActionConnect;
            _view.EhDisconnect += VActionDisconnect;
            _view.EhOpenSettings += VActionOpenSettings;
        }
        
        #endregion ===================================================================
        
        #region ============ EVENTS HANDLERS =========================================

        /// <summary>
        /// Tries to ends (logout) a connection with ETECSA captive portal
        /// </summary>
        /// <param name="sender">Sender object (eg. a windows form control)</param>
        /// <param name="e">event arguments</param>
        private async void VActionDisconnect(object sender, EventArgs e)
        {
            var credential = _view.OutGetActiveAccount();
            if (credential == null)
            {
                _view.InShowMsg(Strs.MSG_I_ACCOUNT_SELECTION, Strs.MSG_I, MessageBoxIcon.Information);
                return;
            }
            
            _cTkSource = new CancellationTokenSource();    
            var _ = Task.Run(() => _view.InSetReqSts(_cTkSource.Token), _cTkSource.Token);                 //  run async a routine on the view to mimic / indicate the requesting status
                
            var config = new MConfigMgr(true);
            var cnx = new MHttpCnx(config.Cfg.CsrfHwToken,  config.Cfg.LogIdToken, config.Cfg.UuidToken);
            
            try
            {
                if (_dCnxAttempts > 2)
                {
                    _dCnxAttempts = 0;
                    _view.InShowMsg(Strs.MSG_E_FORCE_DISCNX_INFO, Strs.MSG_W, MessageBoxIcon.Information);

                    hlp_SendStopSig();
                    goto setting_up_disconnect_state;
                }
                
                var wasOk = await cnx.TryToDisconnect(credential);                                    // requesting connection
                hlp_SendStopSig();                                                                         // send a termination signal to remove the requesting indicator status on the view
                _view.InSetCloseTrayMenu();

                if (!wasOk)
                {
                    ++_dCnxAttempts;
                    _view.InShowMsg(Strs.MSG_E_CANNOT_DISCONX, Strs.MSG_W, MessageBoxIcon.Warning);

                    return;
                }
                    
                setting_up_disconnect_state:
                    
                config.UpdateKey(nameof(SchConfigData.AreWeConnected), "false");
                config.UpdateKey(nameof(SchConfigData.ActiveAccount), (-1).ToString() );
                config.UpdateKey(nameof(SchConfigData.CsrfHwToken), String.Empty);
                config.UpdateKey(nameof(SchConfigData.UuidToken), cnx.UUIDToken);
                config.UpdateKey(nameof(SchConfigData.LogIdToken), String.Empty, true);
                        
                _view.InToggleCnxSts();
            }
            catch (HttpRequestException) { hlp_HandleConnectExp(Strs.MSG_E_CANNOT_REQUEST, true); }
            catch (TaskCanceledException) { hlp_HandleConnectExp(Strs.MSG_E_TIMEOUT, true); }
            catch (ArgumentNullException) { hlp_HandleConnectExp(Strs.MSG_E_RARE_HTML, true); }
            catch (InvalidOperationException) { hlp_HandleConnectExp(Strs.MSG_E_LANDING_PAGE_FAIL, true); }
        }

        /// <summary>
        /// Tries to start (login) a connection with ETECSA captive portal
        /// </summary>
        /// <param name="sender">Sender object (eg. a windows form control)</param>
        /// <param name="__">event arguments</param>
        private async void VActionConnect(object sender, EventArgs __)
        {
            var credential = _view.OutGetActiveAccount();
            if (credential == null)
            {
                _view.InShowMsg(Strs.MSG_I_ACCOUNT_SELECTION, Strs.MSG_I, MessageBoxIcon.Information);
                return;
            }
            
            _cTkSource = new CancellationTokenSource();    
            var _ = Task.Run(() => _view.InSetReqSts(_cTkSource.Token), _cTkSource.Token);                   //  run async a routine on the view to mimic / indicate the requesting status
                
            var cnx = new MHttpCnx();

            try
            {
                var result = await cnx.TryToConnect(credential);    // requesting connection
                
                hlp_SendStopSig();                                  // send a termination signal to remove the requesting indicator status on the view
                _view.InSetCloseTrayMenu();                         // closing the menu

                if (result != PLoginResult.OK)
                    _view.InShowMsg(hlp_GiveMeTheReason(result), Strs.MSG_W, MessageBoxIcon.Exclamation);
                else
                {
                    var config = new MConfigMgr(true);
                    config.UpdateKey(nameof(SchConfigData.AreWeConnected), "true");
                    config.UpdateKey(nameof(SchConfigData.ActiveAccount), credential.ActiveAccIndex.ToString());
                    config.UpdateKey(nameof(SchConfigData.UuidToken), cnx.UUIDToken);
                    config.UpdateKey(nameof(SchConfigData.CsrfHwToken), cnx.CsrfHwToken);
                    config.UpdateKey(nameof(SchConfigData.LogIdToken), cnx.LogIdToken, true);

                    if (config.Cfg.ExitWhenConnect)                  // !!!!!
                    {
                        _view.InNotify(Strs.MSG_NTF_CONNECTED, Strs.MSG_NTF_CONNECTED_DSC);
                        await Task.Delay(1000);
                        VActionExit(null, null);
                        return;
                    }

                    GetTimeLeft(credential);                         // getting account available time left from the server

                    _view.InToggleCnxSts();
                    _view.InSetCloseTrayMenu();                      // closing the menu
                }
            }
            catch (HttpRequestException) { hlp_HandleConnectExp(Strs.MSG_E_CANNOT_REQUEST, false);  }
            catch (TaskCanceledException) { hlp_HandleConnectExp(Strs.MSG_E_TIMEOUT, false); }
            catch (ArgumentNullException) { hlp_HandleConnectExp(Strs.MSG_E_RARE_HTML, false); }
            catch (InvalidOperationException) { hlp_HandleConnectExp(Strs.MSG_E_LANDING_PAGE_FAIL, false); }
            catch (Exception e) { hlp_HandleConnectExp(e.Message, false);  }
        }
        
        /// <summary>Try to get account balance</summary>
        /// <param name="__">Sender object (eg. a windows form control)</param>
        /// <param name="___">event arguments</param>
        private async void VActionChkSts(object __, EventArgs ___)
        {
            var credential = _view.OutGetActiveAccount();
            if (credential is null)
            {
                _view.InShowMsg(Strs.MSG_I_ACCOUNT_SELECTION, Strs.MSG_I, MessageBoxIcon.Information);
                return;
            }
            
            _cTkSource = new CancellationTokenSource();    
            var _ = Task.Run(() => _view.InSetReqSts(_cTkSource.Token), _cTkSource.Token);          //  run async a routine on the view to mimic / indicate the requesting status
            
            var cnx = new MHttpCnx();
            try
            {
                var balance = await cnx.TryToGetAccountSts(credential);    // requesting connection
                
                hlp_SendStopSig();                                        // send a termination signal to remove the requesting indicator status on the view
                _view.InSetCloseTrayMenu();                               // closing the menu
                _view.InSetRecoverSts(true);                              // recoverint the status feedback  
                
                if (balance < 0)
                    _view.InNotify(Strs.MSG_NTF_ACC_STS, Strs.MSG_NTF_ACC_STS_FAIL, ToolTipIcon.Error);

                var divider = credential.User.Contains("com.cu") ? 12.5 : 2.5;
                _view.InNotify(Strs.MSG_NTF_ACC_STS, String.Format(Strs.MSG_NTF_ACC_STS_DATA, balance.ToString(CultureInfo.InvariantCulture),  balance / divider));
                
                GetTimeLeft(credential);                                 // getting account available time left from the server
            }
            catch (HttpRequestException) { hlp_HandleConnectExp(Strs.MSG_E_CANNOT_REQUEST, true);  }
            catch (TaskCanceledException) { hlp_HandleConnectExp(Strs.MSG_E_TIMEOUT, true); }
            catch (ArgumentNullException) { hlp_HandleConnectExp(Strs.MSG_E_RARE_HTML, true); }
            catch (InvalidOperationException) { hlp_HandleConnectExp(Strs.MSG_E_LANDING_PAGE_FAIL, true); }
            catch (Exception e) { hlp_HandleConnectExp(e.Message, true);  }
        }

        /// <summary>
        /// View Action Exit from the entire application
        /// </summary>
        /// <remarks>VAction prefix means View Action</remarks>
        /// <param name="sender">Sender object (eg. a windows form control)</param>
        /// <param name="e">event arguments</param>
        private void VActionExit(object sender, EventArgs e)
        {
            _view.TrayIcon.Visible = false;
            //Application.Exit();
            Environment.Exit(0);            // kill background tasks if any
        }

        /// <summary>
        /// View Action Open Settings Form View
        /// </summary>
        /// <remarks>VAction prefix means View Action</remarks>
        /// <param name="sender">Sender object (eg. a windows form control)</param>
        /// <param name="e">event arguments</param>
        private void VActionOpenSettings(object sender, EventArgs e)
        {
            var form = new VFormConfigs();
            var _ = new CConfig(form);
            form.Show();

            _flagCfgJustChecked = false;                    // forcing next time menu (main view) was open, reload the config file, so we notice the conf update so app can react to it
            _view.InSetCloseTrayMenu();
        }

        /// <summary>
        /// Load the configure account credential from the configuration file, so it can
        /// be displayed (for user selection) on the application tray menu.
        /// Also, check if app previously close while connected during execution, if so the apps react accordingly   
        /// </summary>
        /// <remarks>VComputeCf == compute configuration file</remarks>
        /// <param name="__">Sender object (eg. a windows form control)</param>
        /// <param name="_">event arguments</param>
        private void ChkCfgFromFile(object __, EventArgs _)
        {
            if (_flagCfgJustChecked) return;
            _flagCfgJustChecked = true;

            SchConfigData config;
            try
            {
                config = (new MConfigMgr()).LoadConfig();
            }
            catch (Exception)
            {
                _view.InShowMsg(Strs.MSG_E_LOADING_CFG, Strs.MSG_C);
                VActionExit(null, null);
                return;
            }
            
            _view.InSetAccountsInMenu(config);
            if (config.AreWeConnected)
                _view.InToggleCnxSts(true, config.ActiveAccount);
        }
        
        #endregion ===================================================================
        
        #region ============ HELPERS =================================================

        /// <summary>
        /// Tries to get the account available cnx time left from the server
        /// </summary>
        /// <param name="uCred">User credential, account data so the user can be disconnected to the captive portal</param>
        private async void GetTimeLeft(SchCredential uCred)
        {
            if (uCred is null)
            {
                _view.InShowMsg(Strs.MSG_I_ACCOUNT_SELECTION, Strs.MSG_I, MessageBoxIcon.Information);
                return;
            }
            
            var config = new MConfigMgr(true);
            var cnx = new MHttpCnx(config.Cfg.CsrfHwToken,  config.Cfg.LogIdToken, config.Cfg.UuidToken);
            
            try
            {
                var tl = await cnx.TryToGetTmLeft(uCred);                      // requesting connection | tl = time left
                if(tl.Length == 3) 
                    _view.InUpdateTimeLeft(tl[0], tl[1]);
            }
            catch (HttpRequestException) { }
            catch (TaskCanceledException) { }
            catch (ArgumentNullException) { }
            catch (InvalidOperationException) { }
            catch (Exception) { }
        }

        #endregion ===================================================================
        
        #region ============ HELPERS =================================================

        /// <summary>
        /// Give the <see cref="PLoginResult"/> login result token, it will retrieve a msg string to explaining the login response reason 
        /// </summary>
        /// <param name="result">Login result token</param>
        /// <remarks>hlp means helper</remarks>
        /// <returns>Explanation msg according with the given token</returns>
        /// <exception cref="ArgumentOutOfRangeException">Unknown token was given</exception>
        private string hlp_GiveMeTheReason(PLoginResult result)
        {
            switch (result)
            {
                case PLoginResult.WRONG_PASS:
                    return Strs.PORTAL_RES_WRONG_PASS; 
                case PLoginResult.USER_INVALID:
                    return Strs.PORTAL_RES_USER_INVALID;
                case PLoginResult.MANY_ATTEMPTS:
                    return Strs.PORTAL_RES_MANY_ATTEMPTS;
                case PLoginResult.ACCOUNT_OUT_TIME:
                    return Strs.PORTAL_RES_ACCOUNT_OUT_TIME;
                case PLoginResult.ALREADY_CONN:
                    return Strs.PORTAL_RES_ALREADY_CONN;
                default:
                    throw new ArgumentOutOfRangeException(nameof(result), result, null);
            }
        }

        /// <summary>
        /// Helper method to send a cancellation / termination signal through <see cref="_cTkSource"/> cancellation token
        /// </summary>
        /// <remarks>hlp means helper</remarks>
        private void hlp_SendStopSig()
        {
            if (_cTkSource == null) return;

            _cTkSource.Cancel();
            _cTkSource.Dispose();
            _cTkSource = null;
        }

        /// <summary>
        /// Just group and encapsulate a few instruction
        /// </summary>
        /// <param name="msg">Message to be displayed on the message box</param>
        /// <param name="fromCxnSts">To know if caller coming from connected status</param>
        private void hlp_HandleConnectExp(string msg, bool fromCxnSts)
        {
            hlp_SendStopSig();                                  // send a termination signal to remove the requesting indicator status on the view
            _view.InSetCloseTrayMenu();                         // closing the menu
            _view.InShowMsg(msg);                               // showing msg alert
            _view.InSetRecoverSts(fromCxnSts);                  // restore the status indicator text & color, depending on the origin situation

            if (fromCxnSts) _dCnxAttempts++;
        }

        #endregion ===================================================================
    }
}