namespace cnauta
{
    /// <summary>Static common string definitions</summary>
    static class Strs
    {
        /// <summary>config file path</summary>
        public const string CONFIG_FILE = "appconfig.json";
        public const string MSG_E = "ERROR";
        public const string MSG_I = "INFORMATION";

        public const string MSG_E_INVALID_ACCOUNT_DATA = "Invalid user data ({0})";
        public const string MSG_E_CONFIG_NOT_LOAD = "Could not load configuration file";
        public const string MSG_E_CONFIG_NOT_SAVED = "Could not save configuration file";
        public const string MSG_I_ACCOUNT_SELECTION = "Please, select an account to use in the connection";

        public const char PAS_HIDE = '*'; 
        public const char PAS_SHOW = '\0'; 
    }

    /// <summary>Static Menus string definitions</summary>
    static class StrMenu
    {
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
