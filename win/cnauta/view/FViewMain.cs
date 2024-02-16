using System;
using System.Windows.Forms;
using cnauta.view.ifaces;

namespace cnauta.view
{
    public partial class FViewMain : Form, IMainView
    {
        #region ============ Fields ==================================================

        #endregion ===================================================================

        #region ============ CONSTRUCTORS ============================================
        
        public FViewMain()
        {
            InitializeComponent();
        }

        #endregion ===================================================================

        #region ============ INTERFACE ===============================================
        
        public event EventHandler IncrementChanged;
        public void SetIncrementLabel(string value)
        {
            this.labelIncrement.Text = value;
        }

        #endregion ===================================================================

        #region ============ EVENTS ==================================================
        
        private void buttonIncrement_Click(object sender, EventArgs e)
        {
            this.IncrementChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion ===================================================================
    }
}