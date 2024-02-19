using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using cnauta.model.schema;

namespace cnauta.view.ifaces
{
    public interface IViewMainMenuCtx
    {
        NotifyIcon TrayIcon { get; }
        ContextMenuStrip ContextMenuStrip { get; }
        
        event EventHandler EhExit;
        event EventHandler EhChkSts;
        event EventHandler EhConnect;
        event EventHandler EhDisconnect;
        event EventHandler EhComputeCfg;
        event EventHandler EhOpenSettings;

        void InSetAccountsInMenu(SchConfigData data);
        void InSetCloseTrayMenu();
        void InShowMsg(string errorMsg, string caption = Strs.MSG_E, MessageBoxIcon icon = MessageBoxIcon.Error);
        void InNotify(string title, string content, ToolTipIcon icon = ToolTipIcon.Info);
        Task InSetReqSts(CancellationToken tk);
        void InSetConnSts(bool force2Connect = false, int accIndex = -1);
        void InSetRecoverSts(bool fromConnection = true);
        
        SchCredential OutGetActiveAccount();
    }
}