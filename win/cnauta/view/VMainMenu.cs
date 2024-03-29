﻿using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using cnauta.model.schema;
using cnauta.view.ifaces;
using cnauta.controller.globalHandlers;

namespace cnauta.view
{
    /// <summary>
    /// View Main Menu Context
    /// </summary>
    public class VMainMenu : ApplicationContext, IViewMainMenuCtx
    {
        #region ============ FIELDS ==================================================
        
        private NotifyIcon _trayIcon;
        private ContextMenuStrip _contextMenuStrip;

        private bool _flagAreConnected;         // tells if we already load and list (on menu) the configured users credentials 
        private string _activeU;                // active user
        private string _activeP;                // active password
        private int _activeAccI;                // active account index

        private string _tmHH;                   // account time-left hours  
        private string _tmMM;                   // account time-left minutes

        private System.Windows.Forms.Timer _timer;
        private DateTime _startTime;
        
        #endregion ===================================================================
        
        #region ============ PROPERTY FIELDS =========================================
        
        public NotifyIcon TrayIcon => _trayIcon;
        public ContextMenuStrip ContextMenuStrip => _contextMenuStrip;

        #endregion ===================================================================

        #region ============ DELEGATES ===============================================

        public event EventHandler EhExit;
        public event EventHandler EhChkSts;
        public event EventHandler EhConnect;
        public event EventHandler EhDisconnect;
        public event EventHandler EhComputeCfg;
        public event EventHandler EhOpenSettings;
        
        #endregion ===================================================================

        #region ============ CONSTRUCTORS ============================================

        public VMainMenu(bool connected = false)
        {
            InitializeComponents();

            _flagAreConnected = connected;
            _activeU = "";
            _activeP = "";

            _tmHH = StrMenu.TM_Placeholder;
            _tmMM = StrMenu.TM_Placeholder;
        }
        
        #endregion ===================================================================
        
        #region ============ INITIALIZATIONS =========================================
        
        private void InitializeComponents()
        {
            InitContextMenu();
            IniTrayIcon();
            InitTimer();
            
            Application.AddMessageFilter(new GhKeyHandler(this));
        }
        
        private void InitContextMenu()
        {
            _contextMenuStrip = new ContextMenuStrip()
            {
                Items =
                {
                    new ToolStripMenuItem(StrMenu.M_STATUS_DISCONNECTED, null, null, StrMenu.M_STATUS_DISCONNECTED)
                    {
                        Enabled = false,
                    },
                    new ToolStripMenuItem()
                    {
                        Enabled = false,
                        Text = StrMenu.FormatTimeInfo(),
                    },
                    new ToolStripSeparator(),
                    new ToolStripMenuItem(StrMenu.M_CNX, null, MenuItem_ClickConnection, StrMenu.M_CNX),
                    new ToolStripSeparator(),
                    new ToolStripMenuItem(StrMenu.M_TOOLS, null, null, StrMenu.M_TOOLS)
                    {
                        DropDownItems =
                        {
                            new ToolStripMenuItem(StrMenu.M_TOOLS_CHK_STATUS, null, (sender, _) => EhChkSts?.Invoke(sender, EventArgs.Empty), StrMenu.M_TOOLS_CHK_STATUS) {Enabled = false},
                            new ToolStripMenuItem(StrMenu.M_TOOLS_AUTOCONX) {Enabled = false},
                            new ToolStripMenuItem(StrMenu.M_TOOLS_AUTODISCONX) {Enabled = false}
                        }
                    },
                    new ToolStripMenuItem(StrMenu.M_ACCOUNT, null, null, StrMenu.M_ACCOUNT) { Enabled = false },
                    new ToolStripMenuItem(StrMenu.M_SETTINGS, null, (sender, _) => EhOpenSettings?.Invoke(sender, EventArgs.Empty), StrMenu.M_SETTINGS),
                    new ToolStripSeparator(),
                    new ToolStripMenuItem(StrMenu.M_EXIT, null, (sender, _) => EhExit?.Invoke(sender, EventArgs.Empty), StrMenu.M_EXIT)
                }
            };
            
            _contextMenuStrip.AutoClose = false;
        }

        private void IniTrayIcon()
        {
            _trayIcon = new NotifyIcon()
            {
                Visible = true,
                ContextMenuStrip = _contextMenuStrip,
                Icon = (System.Drawing.Icon) Properties.Resources.ResourceManager.GetObject("CNauta.Icon")
            };
            TrayIcon.MouseClick += TrayIcon_LoadAccounts;
        }

        private void InitTimer()
        {
            // _timer = new System.Windows.Forms.Timer { Interval = 1000 }; // 1 second
            _timer = new System.Windows.Forms.Timer { Interval = 6000 };    // 1 minute
            _timer.Tick += Timer_Tick;                                      // registering tick event
        }

        #endregion ===================================================================

        #region ============ INTERFACE & METHODS =====================================

        /// <summary>
        /// Provide a way to update the available time left
        /// </summary>
        /// <param name="HH">Hours in HH format</param>
        /// <param name="MM">Minutes in MM format</param>
        public void InUpdateTimeLeft(string HH, string MM)
        {
            _tmHH = HH;
            _tmMM = MM;
            _startTime = DateTime.Now;
            
            _timer.Stop();                                          // just in case already started
            _timer.Start();
        }

        /// <summary>
        /// Recover the status of the status indicator (first item on the menu)
        /// </summary>
        /// <param name="fromCxnSts">To know if caller coming from connected status</param>
        public void InSetRecoverSts(bool fromCxnSts = true) 
        {
            if (fromCxnSts)
            {
                _contextMenuStrip.Items[0].BackColor = Color.LimeGreen;
                _contextMenuStrip.Items[0].Text = StrMenu.M_STATUS_CONNECTED;
                return;
            }

            _contextMenuStrip.Items[0].ResetBackColor();
            _contextMenuStrip.Items[0].Text = StrMenu.M_STATUS_DISCONNECTED;
        }

        /// <summary>Display a notification</summary>
        /// <param name="title">Notification title</param>
        /// <param name="content">Notification content</param>
        /// <param name="icon">Notification icon</param>
        public void InNotify(string title, string content, ToolTipIcon icon = ToolTipIcon.Info)
        {
            _trayIcon.BalloonTipTitle = title;
            _trayIcon.BalloonTipText = content;
            _trayIcon.BalloonTipIcon = ToolTipIcon.Info;
            _trayIcon.ShowBalloonTip(6000);
        }

        /// <summary>
        /// Display in the app tray menu, the configured user connection account defined in the
        /// parameter [configuration] data 
        /// </summary>
        /// <remarks>
        /// Come in handy if for some reason, config change and we need to update the available accounts
        /// </remarks>
        /// <param name="data">Configuration data to display the available user connection account</param>
        public void InSetAccountsInMenu(SchConfigData data)
        {
            uint howManyDoWeHave = 0;               // accounts
            
            var menuItem = (ToolStripMenuItem) _contextMenuStrip.Items[6];
            if (menuItem == null) return;
            
            menuItem.DropDownItems.Clear();
            menuItem.Enabled = true;

            if (!String.IsNullOrEmpty(data.DefaultUser))
            {
                howManyDoWeHave++;
                menuItem.DropDownItems.Add(
                    new ToolStripMenuItem(data.DefaultUser, null, (sender, _) => AccountsItem_Click(sender, EventArgs.Empty)) 
                        { Name = data.DefaultUserPass });
            }

            if (!String.IsNullOrEmpty(data.AltAUSer))
            {
                howManyDoWeHave++;
                menuItem.DropDownItems.Add(
                    new ToolStripMenuItem(data.AltAUSer, null, (sender, _) => AccountsItem_Click(sender, EventArgs.Empty)) 
                        { Name = data.AltAUSerPass });
            }
            if (!String.IsNullOrEmpty(data.AltBUSer))
            {
                howManyDoWeHave++;
                menuItem.DropDownItems.Add(
                    new ToolStripMenuItem(data.AltBUSer, null, (sender, _) => AccountsItem_Click(sender, EventArgs.Empty)) 
                        { Name = data.AltBUSerPass });
            }
            

            if (howManyDoWeHave > 1)
            {
                // clearing previous info, if for some reason, config change and we need to update the available account
                _activeP = String.Empty;
                _activeU = String.Empty;
                _activeAccI = data.ActiveAccount;
            }
            else
            {
                // we have just one account available, so its no make any sense the user need to says which one is active, so
                ((ToolStripMenuItem) menuItem.DropDownItems[0]).Checked = true;
                _activeP = menuItem.DropDownItems[0].Name;
                _activeU = menuItem.DropDownItems[0].Text;
                _activeAccI = 0;
            }
        }

        /// <summary>
        /// Tries to toggle GUI controls data (visual, text, ect) so we the app can give feedback about the current connection status
        /// ❗ It can be forced to 'connected' status 
        /// </summary>
        /// <remarks>InSetConnSts == Set Up Connected Status</remarks>
        /// <param name="force2Connect">If this is true, it will tries to set the status to 'connected' no matter _flagAreConnected current value</param>
        /// <param name="accIndex">If we force force2Connect, we need an account drop down menu index to display as active</param>
        public void InToggleCnxSts(bool force2Connect = false, int accIndex = -1)
        {
            var stsItem = _contextMenuStrip.Items[0];
            
            if (_flagAreConnected && !force2Connect)                        // exiting from connected -> disconnected
            {
                _flagAreConnected = false;

                stsItem.ResetBackColor();
                stsItem.Text = StrMenu.M_STATUS_DISCONNECTED;

                ((ToolStripMenuItem) _contextMenuStrip.Items[5]).DropDownItems[0].Enabled = false;
                _contextMenuStrip.Items[1].Text = StrMenu.FormatTimeInfo();
                _contextMenuStrip.Items[3].Text = StrMenu.M_CNX;
                _contextMenuStrip.Items[7].Enabled = true;
                
                InNotify(Strs.MSG_NTF_DISCONNECTED, Strs.MSG_NTF_DISCONNECTED_DSC);
                
                // timer section
                _timer.Stop();
            }
            else                                                            // exiting from disconnected -> connected 
            {
                _flagAreConnected = true;

                stsItem.BackColor = Color.LimeGreen;
                stsItem.Text = StrMenu.M_STATUS_CONNECTED;

                ((ToolStripMenuItem) _contextMenuStrip.Items[5]).DropDownItems[0].Enabled = true;
                _contextMenuStrip.Items[3].Text = StrMenu.M_DCNX;
                _contextMenuStrip.Items[7].Enabled = false;
                
                InNotify(Strs.MSG_NTF_CONNECTED, Strs.MSG_NTF_CONNECTED_DSC);
                
                if (accIndex >= 0 && accIndex < 3) DropMenuCheckAccount(accIndex);
            }
        }

        /// <summary>
        /// The first <see cref="ToolStripItem"/> if for app status feedback.
        /// When the apps is making an HTTP request, this can help to show feedback to the user about wat is going on
        /// </summary>
        /// <remarks>SetReqSts == Set Up Request Status</remarks>
        /// <param name="tk">A cancellation token so it can notice if a caller want to terminate the execution</param>
        public async Task InSetReqSts(CancellationToken tk)
        {
            _contextMenuStrip.Items[0].ResetBackColor();
            
            try
            {
                string[] progressChars = { "⠋", "⠙", "⠹", "⠸", "⠼", "⠴", "⠦", "⠧", "⠇", "⠏" };   // for mimicking progress during http requests
                uint pointer = 0;

                for (var i = 0; i < 1024; i++, pointer++)
                {
                    tk.ThrowIfCancellationRequested();

                    if (pointer > 9) pointer = 0;
                    _contextMenuStrip.Items[0].Text = progressChars[pointer];

                    await Task.Delay(80);
                    // Thread.Sleep(80);
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            } 
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
                Pass = _activeP,
                ActiveAccIndex = _activeAccI
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
            EhComputeCfg?.Invoke(sender, EventArgs.Empty);
        }

        /// <summary>
        /// Tries to establish a connection through the captive portal
        /// </summary>
        /// <param name="sender">Sender object (eg. a windows form control)</param>
        /// <param name="_">event arguments</param>
        private void MenuItem_ClickConnection(object sender, EventArgs _)
        {
            if (!_flagAreConnected) EhConnect?.Invoke(sender, EventArgs.Empty);
            else EhDisconnect?.Invoke(sender, EventArgs.Empty);
        }

        #endregion ===================================================================
        
        #region ============ LOCAL EVENTS HANDLERS ===================================

        /// <summary>
        /// Check and account as active according to the given index.
        /// The index should be related to the DropDownItems of the account sections
        /// </summary>
        /// <param name="accountIndex">Index of the account to be marked as active</param>
        private void DropMenuCheckAccount(int accountIndex)
        {
            if (accountIndex > 2)                               // we only may have 3 account, tops, so ... 
            {
                InShowMsg(Strs.MSG_I_ACCOUNT_INVALID_INDEX);
                return;
            }
            
            var menuItem = (ToolStripMenuItem) (_contextMenuStrip.Items[6] as ToolStripMenuItem)?.DropDownItems[accountIndex];      // menuItem == account in this case
            
            if (menuItem == null) return;
            
            menuItem.Checked = true;
            _activeU = menuItem.Text;
            _activeP = menuItem.Name;
            _activeAccI = accountIndex;
        }

        /// <summary>
        /// Handles the click on an account to set it as active
        /// </summary>
        /// <param name="sender">Sender object (eg. a windows form control)</param>
        /// <param name="_">event arguments</param>
        private void AccountsItem_Click(object sender, EventArgs _)
        {
            // getting configured connections accounts
            var items = (_contextMenuStrip.Items[6] as ToolStripMenuItem)?.DropDownItems;            
            if (items == null) return;
            
            _activeU = ((ToolStripMenuItem) sender).Text;
            _activeP = ((ToolStripMenuItem) sender).Name;
            
            if (items.Count == 1)
            {
                ((ToolStripMenuItem) items[0]).Checked = true;
                return;
            }

            for (var i = 0; i < items.Count; i++)
            {
                if (((ToolStripMenuItem) items[i]).Text == ((ToolStripMenuItem) sender).Text)
                {
                    ((ToolStripMenuItem) items[i]).Checked = true;
                    _activeAccI = i;
                }
                else ((ToolStripMenuItem) items[i]).Checked = false;
            }
        }

        /// <summary>
        /// Handle the "Tick" event for the timer
        /// </summary>
        /// <param name="sender">Sender object (eg. a windows form control)</param>
        /// <param name="_">event arguments</param>
        private void Timer_Tick(object sender, EventArgs _)
        {
            var elapsedTime = DateTime.Now - _startTime;
            _contextMenuStrip.Items[1].Text = StrMenu.FormatTimeInfo(_tmHH, _tmMM,$"{elapsedTime.Hours:D2}",$"{elapsedTime.Minutes:D2}");
        }

        #endregion ===================================================================
    }
}