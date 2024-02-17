using System;
using System.Windows.Forms;

namespace cnauta.view.ifaces
{
    public interface IViewMainMenuCtx
    {
        NotifyIcon TrayIcon { get; }
        
        event EventHandler EhOpenSettings;
        event EventHandler EhExit;
    }
}