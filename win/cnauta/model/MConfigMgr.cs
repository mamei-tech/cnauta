using System;
using System.IO;

using Newtonsoft.Json;

using cnauta.model.schema;


namespace cnauta.model
{
    /// <summary>
    /// Configuration Manager model
    /// </summary>
    class MConfigMgr
    {
        #region ============ FIELDS ==================================================

        private SchConfigData _config;

        #endregion ===================================================================

        #region ============ PROPERTIES ==============================================

        public SchConfigData Config { get => _config; }

        #endregion ===================================================================

        #region ============ CONSTRUCTORS ============================================

        public MConfigMgr()
        {}
        
        #endregion ===================================================================

        #region ============ METHODS =================================================

        /// <summary>
        /// Load configuration file into the app memory space with the help of a JSON parser.
        /// It will use the defined <see cref="SchConfigData"/> model schema
        /// </summary>
        public SchConfigData LoadConfig()
        {
            if (!File.Exists(Strs.CONFIG_FILE)) return null;
            if ((new FileInfo(Strs.CONFIG_FILE)).Length > (long)35000) return null;

            var json = File.ReadAllText(Strs.CONFIG_FILE);

            _config = JsonConvert.DeserializeObject<SchConfigData>(json);
            return _config;
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
            else if (_config != null)
                json = JsonConvert.SerializeObject(_config, Formatting.Indented);
            else return false;

            File.WriteAllText(Strs.CONFIG_FILE, json);            
            return true;
        }

        #endregion ===================================================================
    }
}
