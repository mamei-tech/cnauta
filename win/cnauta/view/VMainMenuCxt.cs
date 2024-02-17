using System;
using System.Drawing;
using System.Windows.Forms;

using cnauta.model.schema;
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
        
        private bool _flagAccountLoaded;        // tell if we already load and list (on menu) the configured users credentials 
        private string _activeU;                // active user
        private string _activeP;                // active password
        
        #endregion ===================================================================
        
        #region ============ PROPERTY FIELDS =========================================
        
        public NotifyIcon TrayIcon => _trayIcon;
        public ContextMenuStrip ContextMenuStrip => _contextMenuStrip;

        #endregion ===================================================================

        #region ============ DELEGATES ===============================================

        public event EventHandler EhExit;
        public event EventHandler EhConnect;
        public event EventHandler EhOpenSettings;
        public event EventHandler EhLoadAccountSelect;
        
        #endregion ===================================================================

        #region ============ CONSTRUCTORS ============================================

        public VMainMenuCxt()
        {
            InitializeComponents();

            _flagAccountLoaded = false;
            _activeU = "";
            _activeP = "";
        }
        
        #endregion ===================================================================
        
        #region ============ INITIALIZATIONS =========================================
        
        private void InitializeComponents()
        {
            _contextMenuStrip = new ContextMenuStrip()
            {
                Items =
                {
                    new ToolStripMenuItem(StrMenu.M_CNX, null, (sender, _) => EhConnect?.Invoke(sender, EventArgs.Empty), StrMenu.M_CNX),
                    new ToolStripSeparator(),
                    new ToolStripMenuItem(StrMenu.M_TOOLS, null, null, StrMenu.M_TOOLS)
                    {
                        DropDownItems =
                        {
                            new ToolStripMenuItem(StrMenu.M_TOOLS_AUTOCONX),
                            new ToolStripMenuItem(StrMenu.M_TOOLS_AUTODISCONX)
                        }
                    },
                    new ToolStripMenuItem(StrMenu.M_ACCOUNT, null, null, StrMenu.M_ACCOUNT) { Enabled = false },
                    new ToolStripMenuItem(StrMenu.M_SETTINGS, null, (sender, _) => EhOpenSettings?.Invoke(sender, EventArgs.Empty), StrMenu.M_SETTINGS),
                    new ToolStripSeparator(),
                    new ToolStripMenuItem(StrMenu.M_EXIT, null, (sender, _) => EhExit?.Invoke(sender, EventArgs.Empty), StrMenu.M_EXIT)
                }
            };
            _contextMenuStrip.AutoClose = false;
            
            _trayIcon = new NotifyIcon()
            {
                Visible = true,
                ContextMenuStrip = _contextMenuStrip,
                Icon = (System.Drawing.Icon) Properties.Resources.ResourceManager.GetObject("CNauta.Icon")
            };
            
            TrayIcon.MouseClick += TrayIcon_LoadAccounts;
        }

        #endregion ===================================================================

        #region ============ INTERFACE & METHODS =====================================

        /// <summary>
        /// Display in the app tray menu, the configured user connection account defined in the
        /// parameter [configuration] data 
        /// </summary>
        /// <param name="data">Configuration data to display the available user connection account</param>
        public void InSetAccountInMenu(SchConfigData data)
        {
            if(_flagAccountLoaded) return;
            
            var menuItem = (ToolStripMenuItem) _contextMenuStrip.Items[3];
            if (menuItem == null) return;
            
            if (!String.IsNullOrEmpty(data.DefaultUser))
                menuItem.DropDownItems.Add(
                    new ToolStripMenuItem(data.DefaultUser, null, (sender, _) => AccountsItem_Click(sender, EventArgs.Empty)) 
                        { Name = data.DefaultUserPass });
            
            if(!String.IsNullOrEmpty(data.AltAUSer)) 
                menuItem.DropDownItems.Add(
                    new ToolStripMenuItem(data.AltAUSer, null, (sender, _) => AccountsItem_Click(sender, EventArgs.Empty)) 
                        { Name = data.AltAUSerPass });
            
            if(!String.IsNullOrEmpty(data.AltBUSer))
                menuItem.DropDownItems.Add(
                    new ToolStripMenuItem(data.AltBUSer, null, (sender, _) => AccountsItem_Click(sender, EventArgs.Empty)) 
                        { Name = data.AltBUSerPass });
                
            menuItem.Enabled = true;
            _flagAccountLoaded = true;
        }

        /// <summary>
        /// The app tray menus has no auto-clos (auto close to false).
        /// This method help closing the tray menu programatically
        /// </summary>
        public void InSetCloseTrayMenu()
        {
            _contextMenuStrip.Close();
        }

        /// <summary>
        /// Display a message box
        /// </summary>
        /// <param name="errorMsg">error message to be displayed</param>
        /// <param name="caption">Message box window title / caption</param>
        /// <param name="icon">Icon to be displayed on the message</param>
        public void InShowMsg(string errorMsg, string caption = Strs.MSG_E, MessageBoxIcon icon = MessageBoxIcon.Error)
        {            
            MessageBox.Show(errorMsg, caption, MessageBoxButtons.OK, icon);
        }

        /// <summary>
        /// Retrieve the active connection credential, if any
        /// </summary>
        /// <returns>Active connection credential</returns>
        public SchCredential OutGetActiveAccount()
        {
            if (String.IsNullOrEmpty(_activeU) || String.IsNullOrEmpty(_activeP)) return null;  
            
            return new SchCredential
            {
                User = _activeU,
                Pass = _activeP
            };
        }

        #endregion ===================================================================
        
        #region ============ EVENTS ==================================================

        /// <summary>
        /// Call the controller action to load the accounts credential from the configuration file
        /// </summary>
        /// <param name="sender">Sender object (eg. a windows form control)</param>
        /// <param name="_">event arguments</param>
        private void TrayIcon_LoadAccounts(object sender, EventArgs _)
        {
            EhLoadAccountSelect?.Invoke(sender, EventArgs.Empty);
        }

        #endregion ===================================================================
        
        #region ============ LOCAL MOD HANDLERS ======================================
        
        /// <summary>
        /// Handles the click on an account to set it as active
        /// </summary>
        /// <param name="sender">Sender object (eg. a windows form control)</param>
        /// <param name="_">event arguments</param>
        private void AccountsItem_Click(object sender, EventArgs _)
        {
            // getting configured connections accounts
            var items = (_contextMenuStrip.Items[3] as ToolStripMenuItem)?.DropDownItems;            
            if (items == null) return;

            foreach (var account in items)
            {
                ((ToolStripMenuItem) account).BackColor = ((ToolStripMenuItem) account).Text == ((ToolStripMenuItem) sender).Text 
                    ? Color.LightBlue 
                    : Control.DefaultBackColor;
            }

            _activeU = ((ToolStripMenuItem) sender).Text;
            _activeP = ((ToolStripMenuItem) sender).Name;
        }

        #endregion ===================================================================
    }
}