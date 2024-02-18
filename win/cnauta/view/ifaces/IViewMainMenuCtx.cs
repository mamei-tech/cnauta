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
        event EventHandler EhConnect;
        event EventHandler EhOpenSettings;
        event EventHandler EhLoadAccountSelect;

        void InSetAccountInMenu(SchConfigData data);
        void InSetCloseTrayMenu();
        void InShowMsg(string errorMsg, string caption = Strs.MSG_E, MessageBoxIcon icon = MessageBoxIcon.Error);
        Task InShowReqSts(CancellationToken tk, string cText = StrMenu.M_STATUS);
        
        SchCredential OutGetActiveAccount();
    }
}