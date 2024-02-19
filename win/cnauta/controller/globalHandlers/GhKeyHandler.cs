using System.Windows.Forms;
using cnauta.view;

namespace cnauta.controller.globalHandlers
{
    /// <summary>
    /// Custom message filter to capture key press events 
    /// </summary>
    /// <remarks>
    /// Our main form is an <see cref="ApplicationContext"/> type, so we can't capture key press even conventionally in this case, so we need this
    /// </remarks>
    public class GhKeyHandler : IMessageFilter
    {
        private const int WM_KEYDOWN = 0x0100;              // key pressed windows internal msg code
        private VMainMenu _view;

        public GhKeyHandler(VMainMenu view)
        {
            _view = view;
        }
        
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg != WM_KEYDOWN) return false;          // return false so we allow message continue be processed
            
            var key = (Keys)(int)m.WParam;
            if (key == Keys.Escape) _view.InSetCloseTrayMenu();
            
            return false;               
        }
    }
}