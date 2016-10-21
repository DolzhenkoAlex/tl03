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
    /// Логика взаимодействия для TeachersView.xaml
    /// </summary>
    public partial class TeachersView : UserControl
    {
        public TeachersView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxFaculty.SelectedIndex = Properties.Settings.Default.TeacherComboBoxFacultyIndex;
            ComboBoxChair.SelectedIndex = Properties.Settings.Default.TeacherComboBoxChairIndex;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.TeacherComboBoxFacultyIndex = ComboBoxFaculty.SelectedIndex;
            Properties.Settings.Default.TeacherComboBoxChairIndex = ComboBoxChair.SelectedIndex;
        }
    }
}
