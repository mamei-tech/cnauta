using System;
using cnauta.model.schema;

namespace cnauta.view.ifaces
{
    public interface IViewConfig
    {
        event EventHandler IncrementChanged;
        event EventHandler EhSaveConfig;

        void InSetIncrementLabel(string value);
        void InSetConfigData(SchConfigData data);
        void InShowErrMsg(string errorMsg);

        SchConfigData OutGetConfigData();
        
    }
}