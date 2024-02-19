using System;
using System.IO;

using Newtonsoft.Json;

using cnauta.model.schema;


namespace cnauta.model
{
    /// <summary>
    /// Configuration Manager model
    /// </summary>
    public class MConfigMgr
    {
        #region ============ FIELDS ==================================================

        /// <summary>Configuration private field</summary>
        private SchConfigData _cfg;

        #endregion ===================================================================

        #region ============ PROPERTIES ==============================================

        /// <summary>Configuration access property</summary>
        public SchConfigData Cfg => _cfg;

        #endregion ===================================================================

        #region ============ CONSTRUCTORS ============================================

        public MConfigMgr(bool doWeNeed2Load = false)
        {
            if (doWeNeed2Load) LoadConfig();
        }
        
        #endregion ===================================================================

        #region ============ METHODS =================================================

        /// <summary>
        /// Load configuration file into the app memory space with the help of a JSON parser.
        /// It will use the defined <see cref="SchConfigData"/> model schema
        /// </summary>
        public SchConfigData LoadConfig()
        {
            if (!File.Exists(Strs.CONFIG_FILE)) return null;
            if ((new FileInfo(Strs.CONFIG_FILE)).Length > 35000) return null;           // if the file is more than 3500 bytes, don't opened

            var json = File.ReadAllText(Strs.CONFIG_FILE);

            _cfg = JsonConvert.DeserializeObject<SchConfigData>(json);
            return _cfg;
        }

        /// <summary>
        /// Tries to save the configurations data in to a file named as <see cref="Strs.CONFIG_FILE"/>
        /// </summary>
        /// <remarks>
        /// If parameter was given, then it will be used as data to save.
        /// Otherwise class configurations field / property will be used.
        /// If all are null, method will return false, and the save operation will fails
        /// </remarks>
        /// <param name="configData">Configuration data</param>
        /// <returns>True if was smoothly</returns>
        public bool SaveConfig(SchConfigData configData = null)
        {
            string json;

            if (configData != null)
                json = JsonConvert.SerializeObject(configData, Formatting.Indented);
            else if (_cfg != null)
                json = JsonConvert.SerializeObject(_cfg, Formatting.Indented);
            else return false;

            File.WriteAllText(Strs.CONFIG_FILE, json);            
            return true;
        }

        /// <summary>
        /// Updates an existing configuration key without losing the other configs in the json file.
        /// </summary>
        /// <param name="key">The key to update</param>
        /// <param name="value">The new value for the key</param>
        /// <param name="want2Save"></param>
        public void UpdateKey(string key, string value, bool want2Save = false)
        {
            if (_cfg == null) return;
            switch (key)
            {
                case nameof(_cfg.DefaultUser):
                    _cfg.DefaultUser = value;
                    break;
                case nameof(_cfg.DefaultUserPass):
                    _cfg.DefaultUserPass = value;
                    break;
                case nameof(_cfg.AltAUSer):
                    _cfg.AltAUSer = value;
                    break;
                case nameof(_cfg.AltAUSerPass):
                    _cfg.AltAUSerPass = value;
                    break;
                case nameof(_cfg.AltBUSer):
                    _cfg.AltBUSer = value;
                    break;
                case nameof(_cfg.AltBUSerPass):
                    _cfg.AltBUSerPass = value;
                    break;
                case nameof(_cfg.LogIdToken):
                    _cfg.LogIdToken = value;
                    break;
                case nameof(_cfg.CsrfHwToken):
                    _cfg.CsrfHwToken = value;
                    break;
                case nameof(_cfg.UuidToken):
                    _cfg.UuidToken = value;
                    break;
                case nameof(_cfg.ActiveAccount):
                    _cfg.ActiveAccount = Int32.Parse(value); 
                    break;
                case nameof(_cfg.AreWeConnected):
                    _cfg.AreWeConnected = Boolean.Parse(value); 
                    break;
                case nameof(_cfg.ExitWhenConnect):
                    _cfg.AreWeConnected = Boolean.Parse(value);
                    break;
            }
            
            if (want2Save) SaveConfig();
        }

        #endregion ===================================================================
    }
}
