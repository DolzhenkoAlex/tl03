using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MvvmLight2.View
{
    /// <summary>
    /// Логика взаимодействия для ServerConnectView.xaml
    /// </summary>
    public partial class ServerConnectView : UserControl
    {
        public ServerConnectView()
        {
            InitializeComponent();
        }
        private void cbNameServer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbNameServer.SelectedItem != null)
                btOK.IsEnabled = true;
        }
    }
}
