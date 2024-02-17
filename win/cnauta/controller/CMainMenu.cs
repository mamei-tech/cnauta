using System;

using cnauta.view;
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
            _view.EhOpenSettings += VActionOpenSettings;
        }
        
        #endregion ===================================================================
        
        #region ============ EVENTS HANDLERS =========================================

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
            Environment.Exit(0);            // kill backgound task if any
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
        }

        #endregion ===================================================================
    }
}