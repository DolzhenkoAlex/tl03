using System;
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
using MvvmLight2.Helper;
using MvvmLight2.Model;

namespace MvvmLight2
{
    /// <summary>
    /// Логика взаимодействия для GroupView.xaml
    /// </summary>
    public partial class GroupView : UserControl
    {
        public GroupView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxFaculty.SelectedIndex = Properties.Settings.Default.GroupComboBoxFacultyIndex;
            ComboBoxAcademicYear.SelectedIndex = Properties.Settings.Default.GroupComboBoxAcademicYearIndex;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.GroupComboBoxFacultyIndex = ComboBoxFaculty.SelectedIndex;
            Properties.Settings.Default.GroupComboBoxAcademicYearIndex = ComboBoxAcademicYear.SelectedIndex;
        }

    }
}
