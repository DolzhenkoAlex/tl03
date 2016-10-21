using System.Windows;

namespace MvvmLight2.Helper
{
    interface IWindowService
    {
        void Show(string title, FrameworkElement content);
        bool? ShowDialog(string title, FrameworkElement content);
    }
}
