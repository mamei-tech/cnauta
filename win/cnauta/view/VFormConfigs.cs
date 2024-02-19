using System;
using System.Windows.Forms;

using cnauta.view.ifaces;
using cnauta.model.schema;


namespace cnauta.view
{
    public partial class VFormConfigs : Form, IViewConfig
    {
        #region ============ FIELDS ==================================================

        /// <summary>Configuration data. It must be filled with the configuration coming form the config file</summary>
        private SchConfigData _config;                                                

        #endregion ===================================================================

        #region ============ PROPERTY FIELDS =========================================
        #endregion ===================================================================

        #region ============ DELEGATES ===============================================

        public event EventHandler EhSaveConfig;

        #endregion ===================================================================

        #region ============ CONSTRUCTORS ============================================

        public VFormConfigs()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MinimizeBox = false;
            MaximizeBox = false;
        }

        #endregion ===================================================================

        #region ============ INITIALIZATIONS =========================================
        #endregion ===================================================================

        #region ============ INTERFACE & METHODS =====================================

        /// <summary>Clos the formulary</summary>
        public void InCloseConfigsForm()
        {
            Close();
        }

        /// <summary>
        /// Set the configuration [data] in to the windows form controls (GUI) 
        /// </summary>
        /// <remarks>IN prefix means a receiver method to process data coming from controller (see MVC term) </remarks>
        /// <param name="data">Configuration data schema object</param>
        public void InSetConfigData(SchConfigData data)
        {
            if (data == null) 
            {
                MessageBox.Show(Strs.MSG_E_CONFIG_NOT_LOAD, Strs.MSG_E, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            _config = data;

            if (!String.IsNullOrEmpty(_config.DefaultUser)) txb_defaultUser.Text = _config.DefaultUser;
            if (!String.IsNullOrEmpty(_config.DefaultUserPass)) txb_defaultUserPass.Text = _config.DefaultUserPass;

            if (!String.IsNullOrEmpty(_config.AltAUSer)) txb_alternativeAUser.Text = _config.AltAUSer;
            if (!String.IsNullOrEmpty(_config.AltAUSerPass)) txb_alternativeAUserPass.Text = _config.AltAUSerPass;

            if (!String.IsNullOrEmpty(_config.AltBUSer)) txb_alternativeBUser.Text = _config.AltBUSer;
            if (!String.IsNullOrEmpty(_config.AltBUSerPass)) txb_alternativeBUserPass.Text = _config.AltBUSerPass;
        }

        /// <summary>
        /// Display a error message box
        /// </summary>
        /// <param name="errorMsg">error message to be displayed</param>
        public void InShowErrMsg(string errorMsg)
        {            
            MessageBox.Show(errorMsg, Strs.MSG_E, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Retrieve the a configuration schema object obtaining the data from the proper Windows Form Control 
        /// </summary>  
        /// <returns>Configuration data object</returns>
        public SchConfigData OutGetConfigData()
        {
            return new SchConfigData
            {
                DefaultUser = txb_defaultUser.Text,
                DefaultUserPass = txb_defaultUserPass.Text,
                
                AltAUSer = txb_alternativeAUser.Text,
                AltAUSerPass = txb_alternativeAUserPass.Text,
                
                AltBUSer = txb_alternativeBUser.Text,
                AltBUSerPass = txb_alternativeBUserPass.Text,
                
                // passing the previously given config data, so we don't mess the config file. As if pass only the data user in the form, SchConfigData will have null in some fields and the json file will not be write (saved) properly.
                
                UuidToken = _config.UuidToken,
                CsrfHwToken = _config.CsrfHwToken,
                LogIdToken = _config.LogIdToken,
                ActiveAccount = _config.ActiveAccount,
                AreWeConnected = _config.AreWeConnected,
            };
        }

        #endregion ===================================================================

        #region ============ EVENTS ==================================================

        /// <summary>
        /// Calling the controller to invoke the action.
        /// </summary>
        /// <remarks>
        /// Event will be handled through the handled registered to <see cref="IViewConfig.EhSaveConfig"/> in the controller
        /// </remarks>
        private void btn_ConfigSave_Click(object sender, EventArgs e)
        {
            if (this.txb_defaultUser.Text.Length <= 6 || this.txb_defaultUserPass.Text.Length <= 3)
            {
                MessageBox.Show(String.Format(Strs.MSG_E_INVALID_ACCOUNT_DATA, "default user"), Strs.MSG_E, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            EhSaveConfig?.Invoke(this, e);
        }

        #endregion ===================================================================

        #region ============ LOCAL EVENTS HANDLERS ===================================

        /// <summary>
        /// Hide / un-hide the password present in the correspondent text box  
        /// </summary>
        /// <param name="__">Sender object (eg. a windows form control)</param>
        /// <param name="_">event arguments</param>
        private void btn_revealDefaultPass_Click(object __, EventArgs _)
        {
            if (txb_defaultUserPass.PasswordChar == Strs.PAS_HIDE) txb_defaultUserPass.PasswordChar = Strs.PAS_SHOW;
            else txb_defaultUserPass.PasswordChar = Strs.PAS_HIDE;
        }

        /// <summary>
        /// Hide / un-hide the password present in the correspondent text box  
        /// </summary>
        /// <param name="__">Sender object (eg. a windows form control)</param>
        /// <param name="_">event arguments</param>
        private void btn_revealAltAPass_Click(object __, EventArgs _)
        {
            if (txb_alternativeAUserPass.PasswordChar == Strs.PAS_HIDE) txb_alternativeAUserPass.PasswordChar = Strs.PAS_SHOW;
            else txb_alternativeAUserPass.PasswordChar = Strs.PAS_HIDE;
        }

        /// <summary>
        /// Hide / un-hide the password present in the correspondent text box  
        /// </summary>
        /// <param name="__">Sender object (eg. a windows form control)</param>
        /// <param name="_">event arguments</param>
        private void btn_revealAltBPass_Click(object __, EventArgs _)
        {
            if (txb_alternativeBUserPass.PasswordChar == Strs.PAS_HIDE) txb_alternativeBUserPass.PasswordChar = Strs.PAS_SHOW;
            else txb_alternativeBUserPass.PasswordChar = Strs.PAS_HIDE;
        }
        
        /// <param name="__"></param>
        /// <param name="_"></param>
        private void btn_configCancel_Click(object __, EventArgs _)
        {
            InCloseConfigsForm();
        }

        #endregion ===================================================================


        
    }
}