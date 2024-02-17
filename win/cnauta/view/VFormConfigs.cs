using System;
using System.Windows.Forms;

using cnauta.view.ifaces;
using cnauta.model.schema;


namespace cnauta.view
{
    public partial class VFormConfigs : Form, IViewConfig
    {
        #region ============ FIELDS ==================================================

        #endregion ===================================================================

        #region ============ PROPERTY FIELDS =========================================
        #endregion ===================================================================

        #region ============ DELEGATES ===============================================

        public event EventHandler IncrementChanged;
        public event EventHandler ehSaveConfig;

        #endregion ===================================================================

        #region ============ CONSTRUCTORS ============================================

        public VFormConfigs()
        {
            InitializeComponent();
        }

        #endregion ===================================================================

        #region ============ INITIALIZATIONS =========================================
        #endregion ===================================================================

        #region ============ INTERFACE & METHODS =====================================

        public void InSetIncrementLabel(string value)
        {
            this.labelIncrement.Text = value;
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

            if (!String.IsNullOrEmpty(data.DefaultUser)) txb_defaultUser.Text = data.DefaultUser;
            if (!String.IsNullOrEmpty(data.DefaultUserPass)) txb_defaultUserPass.Text = data.DefaultUserPass;

            if (!String.IsNullOrEmpty(data.AltAUSer)) txb_alternativeAUser.Text = data.AltAUSer;
            if (!String.IsNullOrEmpty(data.AltAUSerPass)) txb_alternativeAUserPass.Text = data.AltAUSerPass;

            if (!String.IsNullOrEmpty(data.AltBUSer)) txb_alternativeBUser.Text = data.AltBUSer;
            if (!String.IsNullOrEmpty(data.AltBUSerPass)) txb_alternativeBUserPass.Text = data.AltBUSerPass;
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
                DefaultUserPass = txb_defaultUserPass.Text
            };
        }

        #endregion ===================================================================

        #region ============ EVENTS ==================================================
        
        private void buttonIncrement_Click(object sender, EventArgs e)
        {
            this.IncrementChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Calling the controller to invoke the action.
        /// </summary>
        /// <remarks>
        /// Event will be handled through the handled registered to <see cref="IViewConfig.ehSaveConfig"/> in the controller
        /// </remarks>
        private void btn_ConfigSave_Click(object sender, EventArgs e)
        {
            if (this.txb_defaultUser.Text.Length <= 6 || this.txb_defaultUserPass.Text.Length <= 3)
            {
                MessageBox.Show(String.Format(Strs.MSG_E_INVALID_ACCOUNT_DATA, "default user"), Strs.MSG_E, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            this.ehSaveConfig?.Invoke(this, e);
        }

        #endregion ===================================================================

        #region ============ LOCAL HANDLERS ==========================================
        
        /// <summary>
        /// Hide / un-hide the password present in the correspondent text box  
        /// </summary>
        private void btn_revealDefaultPass_Click(object sender, EventArgs e)
        {
            if (txb_defaultUserPass.PasswordChar == Strs.PAS_HIDE) txb_defaultUserPass.PasswordChar = Strs.PAS_SHOW;
            else txb_defaultUserPass.PasswordChar = Strs.PAS_HIDE;
        }

        /// <summary>
        /// Hide / un-hide the password present in the correspondent text box  
        /// </summary>
        private void btn_revealAltAPass_Click(object sender, EventArgs e)
        {
            if (txb_alternativeAUserPass.PasswordChar == Strs.PAS_HIDE) txb_alternativeAUserPass.PasswordChar = Strs.PAS_SHOW;
            else txb_alternativeAUserPass.PasswordChar = Strs.PAS_HIDE;
        }

        /// <summary>
        /// Hide / un-hide the password present in the correspondent text box  
        /// </summary>
        private void btn_revealAltBPass_Click(object sender, EventArgs e)
        {
            if (txb_alternativeBUserPass.PasswordChar == Strs.PAS_HIDE) txb_alternativeBUserPass.PasswordChar = Strs.PAS_SHOW;
            else txb_alternativeBUserPass.PasswordChar = Strs.PAS_HIDE;
        }

        #endregion ===================================================================


    }
}