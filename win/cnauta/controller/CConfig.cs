using System;

using cnauta.model;
using cnauta.view.ifaces;

namespace cnauta.controller
{
    public class CConfig
    {
        #region ============ FIELDS ==================================================
        
        private readonly IViewConfig _view;
        
        /// <summary>Configurations Manager model</summary>
        /// <remarks>_m prefix means Model</remarks>
        private readonly MConfigMgr _mCfgMgr;     
        private readonly MNumber _mNumber;
        
        #endregion ===================================================================
        
        #region ============ CONSTRUCTORS ============================================

        public CConfig(IViewConfig view)
        {
            _view = view;
            _mNumber = new MNumber();
            _mCfgMgr = new MConfigMgr();

            _view.IncrementChanged += VActionIncrementChanged;
            _view.EhSaveConfig += VActionSaveConfig;
            
            _view.InSetConfigData(_mCfgMgr.LoadConfig());
        }

        #endregion ===================================================================

        #region ============ EVENTS HANDLERS =========================================

        /// <summary>
        /// Save the configuration data into the filesystem configuration file
        /// </summary>
        /// <param name="sender">Sender object (eg. a windows form control)</param>
        /// <param name="e">event arguments</param>
        /// <remarks>VAction prefix means View Action</remarks>
        public void VActionSaveConfig(object sender, EventArgs e)
        {            
            var wasOk = _mCfgMgr.SaveConfig(_view.OutGetConfigData());
            if (!wasOk) _view.InShowErrMsg(Strs.MSG_E_CONFIG_NOT_SAVED);
        }

        private void VActionIncrementChanged(object sender, EventArgs e)
        {
            _mNumber.Increment();
            _view.InSetIncrementLabel(_mNumber.ToString());
        }

        #endregion ===================================================================
    }
}