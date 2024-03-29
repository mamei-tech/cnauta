﻿
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
        public const string MSG_C = "CRITICAL";
        public const string MSG_I = "INFORMATION";

        public const string MSG_E_LOADING_CFG = "Error loading config. Please check the config file. \n\n\t\t        ❗ Program will terminate.";
        public const string MSG_E_INVALID_ACCOUNT_DATA = "Invalid user data ({0})";
        public const string MSG_E_CONFIG_NOT_LOAD = "Could not load configuration file";
        public const string MSG_E_CONFIG_NOT_SAVED = "Could not save configuration file";
        public const string MSG_E_CANNOT_REQUEST = "Request unsucessfully. Connection not stablished";
        public const string MSG_E_CANNOT_DISCONX = "Servers says can't disconnect. Please, wait a fews seconds and retry.";
        public const string MSG_E_FORCE_DISCNX_INFO = "Servers says can't disconnect again... But we are setting the app up to disconnection state anyways.";
        public const string MSG_E_TIMEOUT = "No response from server, timeout exceeded so task was canceled.";
        public const string MSG_E_RARE_HTML = "Server response an unespected HTML content.";
        public const string MSG_E_LANDING_PAGE_FAIL = "Landing page unreachable or invalid.";
        public const string MSG_I_ACCOUNT_SELECTION = "Please, select a cnx account to be used";
        public const string MSG_I_ACCOUNT_INVALID_INDEX = "Index seems invalid";

        public const string MSG_NTF_CONNECTED = "Connected";
        public const string MSG_NTF_DISCONNECTED = "Logout OK";
        public const string MSG_NTF_CONNECTED_DSC = "connection was stablished";
        public const string MSG_NTF_DISCONNECTED_DSC = "connection was terminated";
        public const string MSG_NTF_ACC_STS = "Account balance";
        public const string MSG_NTF_ACC_STS_FAIL = "can't get the data";
        public const string MSG_NTF_ACC_STS_DATA = "balance: {0} CUP\nhours: {1}";

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
        public const string TM_Placeholder = "--";
        public static string M_DEFAULT_TIME = "   {0}:{1}   -   {2}:{3}";

        public const string M_CNX = "⚡ Connect";
        public const string M_DCNX = "❌ Disconnect";
        public const string M_STATUS_DISCONNECTED = "    ⚫ Disconnected ";
        public const string M_STATUS_CONNECTED = "     🟢 Connected ";

        public const string M_TOOLS = "⛏ Tools";
        public const string M_TOOLS_CHK_STATUS = "📊 Chk Status";
        public const string M_TOOLS_AUTOCONX = "⏱ Auto Connect";
        public const string M_TOOLS_AUTODISCONX = "⏱ Auto Disconnect";

        public const string M_ACCOUNT = "💳 Active Account";
        public const string M_SETTINGS = "⚙ Settings";
        public const string M_EXIT = "🔚 Exit  (or ESC 2 hide)";

        /// <summary>
        /// Retrieve the default time information using the placeholders 
        /// </summary>
        /// <returns>--:--   -   --:--</returns>
        public static string FormatTimeInfo()
        {
            return String.Format(M_DEFAULT_TIME, TM_Placeholder, TM_Placeholder, TM_Placeholder, TM_Placeholder);
        }

        /// <summary>
        /// Retrieve a formatted time information using the given information
        /// </summary>
        /// <param name="TmLeftHH">time left - hours</param>
        /// <param name="TmLeftMM">time left - minutes</param>
        /// <param name="TmElapsedHH">time elapsed - hours</param>
        /// <param name="tmElapsedMM">time elapsed - minutes</param>
        /// <returns>03:25   -   01:35</returns>
        public static string FormatTimeInfo(string TmLeftHH, string TmLeftMM, string TmElapsedHH, string tmElapsedMM)
        {
            return String.Format(M_DEFAULT_TIME, TmLeftHH, TmLeftMM, TmElapsedHH, tmElapsedMM);
        }
    }

}
