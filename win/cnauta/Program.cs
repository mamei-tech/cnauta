using System;
using System.Windows.Forms;

using cnauta.view;
using cnauta.controller;

namespace cnauta
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainView = new VMainMenu();          // VMainMenu is an application context used as conceptual view
            var _ = new CMainMenu(mainView);

            Application.Run(mainView);
        }
    }
}