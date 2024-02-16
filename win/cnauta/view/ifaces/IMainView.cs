using System;

namespace cnauta.view.ifaces
{
    public interface IMainView
    {
        event EventHandler IncrementChanged;

        void SetIncrementLabel(string value);
    }
}