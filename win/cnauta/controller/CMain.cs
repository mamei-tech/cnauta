using System;

using cnauta.model;
using cnauta.view.ifaces;

namespace cnauta.controller
{
    public class CMain
    {
        #region ============ Fields ==================================================
        private readonly IMainView view;
        private MNumber number;
        #endregion ===================================================================
        
        #region ============ CONSTRUCTORS ============================================

        public CMain(IMainView view)
        {
            this.view = view;
            this.number = new MNumber();

            this.view.IncrementChanged += View_IncrementChanged;
        }
        
        #endregion ===================================================================
        
        #region ============ EVENTS ==================================================

        private void View_IncrementChanged(object sender, EventArgs e)
        {
            this.number.Increment();
            this.view.SetIncrementLabel(number.ToString());
        }

        #endregion ===================================================================
    }
}