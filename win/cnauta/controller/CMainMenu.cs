using System;
using System.Windows.Forms;

using cnauta.view;
using cnauta.model;
using cnauta.view.ifaces;

namespace cnauta.controller
{
    public class CMainMenu
    {
        #region ============ FIELDS ==================================================
        
        private readonly IViewMainMenuCtx _view;
        
        #endregion ===================================================================
        
        #region ============ CONSTRUCTORS ============================================

        public CMainMenu(IViewMainMenuCtx view)
        {
            _view = view;
            
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
        private void VActionConnect(object sender, EventArgs e)
        {
            var credential = _view.OutGetActiveAccount();
            
            if (credential == null) _view.InShowMsg(Strs.MSG_I_ACCOUNT_SELECTION, Strs.MSG_I, MessageBoxIcon.Information);
            else
            {
                // TODO implement the connection request
            }
            
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

        #endregion ===================================================================
    }
}