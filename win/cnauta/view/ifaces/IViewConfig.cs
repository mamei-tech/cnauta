using System;
using cnauta.model.schema;

namespace cnauta.view.ifaces
{
    public interface IViewConfig
    {
        event EventHandler EhSaveConfig;

        void InSetConfigData(SchConfigData data);
        void InShowErrMsg(string errorMsg);
        void InCloseConfigsForm();

        SchConfigData OutGetConfigData();
    }
}