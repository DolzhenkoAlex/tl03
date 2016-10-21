using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using MvvmLight2.View;

namespace MvvmLight2.Helper
{
    class WindowService: IWindowService
    {
        private readonly IMessenger messenger;

        public WindowService(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        public void Show(string title, FrameworkElement content)
        {
            var window = new Window
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Title = title,
                Content = content,
                WindowState = WindowState.Normal
            };

            if (window == Application.Current.MainWindow)
            {
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            else
                window.Owner = Application.Current.MainWindow;

            window.Show();
        }

        public bool? ShowDialog(string title, FrameworkElement content)
        {
            if (Application.Current.MainWindow == null)
                throw new Exception("WTF!?");

            var window = new CommonWindow(messenger)
            {
                Owner = Application.Current.MainWindow,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                ShowInTaskbar = false,
                WindowStyle = WindowStyle.ToolWindow,
                Title = title,
                Content = content,
                SizeToContent = SizeToContent.WidthAndHeight
            };

            return window.ShowDialog();
        }
    }
}
