using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
using MvvmLight2.Model;

namespace MvvmLight2
{
    /// <summary>
    /// Логика взаимодействия для FacultyView.xaml
    /// </summary>
    public partial class FacultyView : UserControl
    {
        public FacultyView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxUniversity.SelectedIndex = Properties.Settings.Default.UniversitySelectedProperty;
        }

    }
}
