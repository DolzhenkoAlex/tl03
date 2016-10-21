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

namespace MvvmLight2
{
    /// <summary>
    /// Логика взаимодействия для ChairsView.xaml
    /// </summary>
    public partial class ChairsView : UserControl
    {
        public ChairsView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxFaculty.SelectedIndex = Properties.Settings.Default.FacultySelectedIndexProperty;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.FacultySelectedIndexProperty = ComboBoxFaculty.SelectedIndex;
        }

        //protected override oncl
    }
}
