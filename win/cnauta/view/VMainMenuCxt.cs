using System;
using System.Windows.Forms;

using cnauta.view.ifaces;

namespace cnauta.view
{
    /// <summary>
    /// View Main Menu Context
    /// </summary>
    public class VMainMenuCxt : ApplicationContext, IViewMainMenuCtx
    {
        #region ============ FIELDS ==================================================
        
        private NotifyIcon _trayIcon;
        private ContextMenuStrip _contextMenuStrip;
        
        #endregion ===================================================================
        
        #region ============ PROPERTY FIELDS =========================================
        
        public NotifyIcon TrayIcon => _trayIcon;

        #endregion ===================================================================

        #region ============ DELEGATES ===============================================

        public event EventHandler EhOpenSettings;
        public event EventHandler EhExit;

        #endregion ===================================================================

        #region ============ CONSTRUCTORS ============================================

        public VMainMenuCxt()
        {
            InitializeComponents();
        }
        
        #endregion ===================================================================
        
        #region ============ INITIALIZATIONS =========================================
        
        private void InitializeComponents()
        {
            var toolsItem = new ToolStripMenuItem(StrMenu.M_TOOLS, null, null, StrMenu.M_TOOLS);
            toolsItem.DropDownItems.Add(StrMenu.M_TOOLS_AUTOCONX);
            toolsItem.DropDownItems.Add(StrMenu.M_TOOLS_AUTODISCONX);

            _contextMenuStrip = new ContextMenuStrip()
            {
                Items =
                {
                    // new ToolStripMenuItem("Settings", null, (sender, e) => EhOpenSettings?.Invoke(sender, e), "Settings"),
                    new ToolStripMenuItem(StrMenu.M_CNX, null, (sender, _) => EhOpenSettings?.Invoke(sender, EventArgs.Empty), StrMenu.M_CNX),
                    new ToolStripSeparator(),
                    toolsItem,
                    new ToolStripMenuItem(StrMenu.M_ACCOUNT, null, (sender, _) => EhOpenSettings?.Invoke(sender, EventArgs.Empty), StrMenu.M_ACCOUNT),
                    new ToolStripMenuItem(StrMenu.M_SETTINGS, null, (sender, _) => EhOpenSettings?.Invoke(sender, EventArgs.Empty), StrMenu.M_SETTINGS),
                    new ToolStripSeparator(),
                    new ToolStripMenuItem(StrMenu.M_EXIT, null, (sender, _) => EhExit?.Invoke(sender, EventArgs.Empty), StrMenu.M_EXIT)
                }
            };
            
            _trayIcon = new NotifyIcon()
            {
                Visible = true,
                ContextMenuStrip = _contextMenuStrip,
                Icon = (System.Drawing.Icon) Properties.Resources.ResourceManager.GetObject("CNauta.Icon")
            };
            
            //TrayIcon.DoubleClick += TrayIcon_DoubleClick;
        }

        #endregion ===================================================================

        #region ============ INTERFACE & METHODS =====================================
        #endregion ===================================================================
        
        #region ============ EVENTS ==================================================
        
        /*private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            // Show the context menu when the tray icon is double-clicked
            _contextMenuStrip.Show(Cursor.Position);
        }*/
        
        #endregion ===================================================================
        
    }
}