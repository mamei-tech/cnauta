using System;
using System.Net.Http;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;

using cnauta.view;
using cnauta.model;
using cnauta.view.ifaces;

namespace cnauta.controller
{
    public class CMainMenu
    {
        #region ============ FIELDS ==================================================
        
        private readonly IViewMainMenuCtx _view;
        private CancellationTokenSource _cTkSource;                                                 // token use to send run termination signals  
        
        #endregion ===================================================================
        
        #region ============ CONSTRUCTORS ============================================

        public CMainMenu(IViewMainMenuCtx view)
        {
            _view = view;
            _cTkSource = null;

            _view.EhExit += VActionExit;
            _view.EhConnect += VActionConnect;
            _view.EhOpenSettings += VActionOpenSettings;
            _view.EhLoadAccountSelect += VActionLoadCnxAccounts;
        }
        
        #endregion ===================================================================
        
        #region ============ EVENTS HANDLERS =========================================

        /// <summary>
        /// Tries to start a connection with ETECSA captive portal
        /// </summary>
        /// <param name="sender">Sender object (eg. a windows form control)</param>
        /// <param name="e">event arguments</param>
        private async void VActionConnect(object sender, EventArgs e)
        {
            var credential = _view.OutGetActiveAccount();
            
            if (credential == null) _view.InShowMsg(Strs.MSG_I_ACCOUNT_SELECTION, Strs.MSG_I, MessageBoxIcon.Information);
            else
            {
                _cTkSource = new CancellationTokenSource();    
                var _ = Task.Run(() => _view.InShowReqSts(_cTkSource.Token), _cTkSource.Token);                   //  run async a routine on the view to mimic / indicate the requesting status
                
                var cnx = new MHttpCnx();
                
                try
                {
                    var result = await cnx.TryToConnect(credential);                                                        // requesting connection
                    hlp_SendStopSig();                                                                                      // send a termination signal to remove the requesting indicator status on the view
                    
                    if (result != PLoginResult.OK)
                        _view.InShowMsg(hlp_GiveMeTheReason(result), Strs.MSG_W, MessageBoxIcon.Exclamation);
                    else
                    {
                        // TODO estamos conectados bien haz la visualización del tiempo q estas conectado. Pone un item en el menú q sea para mostrar el estado conectado, desconectado.
                    }
                }
                catch (HttpRequestException) { _view.InShowMsg(Strs.MSG_E_CANNOT_REQUEST); }
                catch (TaskCanceledException) { _view.InShowMsg(Strs.MSG_E_TIMEOUT); }
                catch (ArgumentNullException) { _view.InShowMsg(Strs.MSG_E_RARE_HTML); }
                catch (InvalidOperationException) { _view.InShowMsg(Strs.MSG_E_LANDING_PAGE_FAIL); }
            }
            
            hlp_SendStopSig();                                                                                              // send a termination signal to remove the requesting indicator status on the view                
            _view.InSetCloseTrayMenu();
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
            
            _view.InSetCloseTrayMenu();
        }

        /// <summary>
        /// Load the configure account credential from the configuration file, so it can
        /// be displayed (for user selection) on the application tray menu 
        /// </summary>
        /// <param name="sender">Sender object (eg. a windows form control)</param>
        /// <param name="e">event arguments</param>
        private void VActionLoadCnxAccounts(object sender, EventArgs e)
        {
            var config = (new MConfigMgr()).LoadConfig();
            _view.InSetAccountInMenu(config);
        }

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

        #endregion ===================================================================
    }
}