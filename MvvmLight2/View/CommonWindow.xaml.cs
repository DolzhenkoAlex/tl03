using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using MvvmLight2.Helper;

namespace MvvmLight2.View
{
    /// <summary>
    /// Логика взаимодействия для CommonWindow.xaml
    /// </summary>
    public partial class CommonWindow : Window
    {
            private readonly IMessenger messenger;

        public CommonWindow(IMessenger messenger)
        {
            this.messenger = messenger;
            this.messenger.Register<CloseViewMessage>(this, Close);

            InitializeComponent();
        }

        internal void Close(CloseViewMessage message)
        {
            if (message.ViewName != Content.GetType().Name)
                return;
            
            this.messenger.Unregister<CloseViewMessage>(this);

            DialogResult = message.DialogResult;
            Close();
        }
    }
}
