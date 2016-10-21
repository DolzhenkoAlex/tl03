using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight2.Helper
{
    /// <summary>
    /// Вспомогательныый класс, который исполььзуется 
    /// для передечи сообщения при закрытии диалогового окна
    /// </summary>
    public sealed class CloseViewMessage
    {
        public string ViewName { get; private set; }
        public bool DialogResult { get; private set; }

        public CloseViewMessage(string viewName, bool result)
        {
            ViewName = viewName;
            DialogResult = result;
        }
    }
}
