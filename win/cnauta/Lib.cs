using System;

namespace cnauta
{
    /// <summary>Static common string definitions</summary>
    static class Strs
    {
        /// <summary>config file path</summary>
        public const string CONFIG_FILE = "appconfig.json";
        public const string MSG_E = "ERROR";
        public const string MSG_W = "WARING";
        public const string MSG_I = "INFORMATION";

        public const string MSG_E_INVALID_ACCOUNT_DATA = "Invalid user data ({0})";
        public const string MSG_E_CONFIG_NOT_LOAD = "Could not load configuration file";
        public const string MSG_E_CONFIG_NOT_SAVED = "Could not save configuration file";
        public const string MSG_E_CANNOT_REQUEST = "Request unsucessfully. Connection not stablished";
        public const string MSG_E_TIMEOUT = "No response from server, timeout exceeded so task was canceled.";
        public const string MSG_E_RARE_HTML = "Server response an unespected HTML content.";
        public const string MSG_E_LANDING_PAGE_FAIL = "Landing page unreachable or invalid.";
        public const string MSG_I_ACCOUNT_SELECTION = "Please, select an account to use in the connection";

        public const char PAS_HIDE = '*'; 
        public const char PAS_SHOW = '\0';

        public const string PORTAL_RES_WRONG_PASS = "El nombre de usuario o contraseña son incorrectos.";
        public const string PORTAL_RES_USER_INVALID = "No se pudo autorizar al usuario.";
        public const string PORTAL_RES_MANY_ATTEMPTS = "Usted a realizado muchos intentos.";
        public const string PORTAL_RES_ACCOUNT_OUT_TIME = "Su tarjeta no tiene saldo disponible.";
        public const string PORTAL_RES_ALREADY_CONN = "El usuario ya está conectado.";
    }

    /// <summary>
    /// Custom token to encapsulate requests connection (login) results of the captive portal 
    /// </summary>
    public enum PLoginResult
    {
        OK,
        WRONG_PASS,
        USER_INVALID,
        MANY_ATTEMPTS,
        ACCOUNT_OUT_TIME,
        ALREADY_CONN
    }

    /// <summary>Captive portal URL interpolation strings</summary>
    static class StrsHots
    {
        public const string HOST_NAME = "secure.etecsa.net";
        public const string HOST_PORT = "8443";
        public static readonly string HOST_URL = $"https://{HOST_NAME}:{HOST_PORT}";
        public static readonly string HOST_URL_QUERY = $"{HOST_URL}/EtecsaQueryServlet";
        public static readonly string HOST_URL_LOGIN = $"{HOST_URL}//LoginServlet";
        public static readonly string HOST_URL_LOGOUT = $"{HOST_URL}/LogoutServlet";
    }

    /// <summary>Static Menus string definitions</summary>
    static class StrMenu
    {
        public const string M_STATUS = "        ▪ Standby ▪ ";
        public const string M_CNX = "⚡ Connect";
        public const string M_DCNX = "🕳 Disconnect";

        public const string M_TOOLS = "⛏ Tools";
        public const string M_TOOLS_AUTOCONX = "Auto Connect";
        public const string M_TOOLS_AUTODISCONX = "Auto Disconnect";

        public const string M_ACCOUNT = "💳 Active Account";
        public const string M_SETTINGS = "⚙ Settings";
        public const string M_EXIT = "🔚 Exit";
    }

}
