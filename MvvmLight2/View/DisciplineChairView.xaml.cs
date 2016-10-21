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
    /// Логика взаимодействия для DisciplineChairView.xaml
    /// </summary>
    public partial class DisciplineChairView : UserControl
    {
        public DisciplineChairView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxFaculty.SelectedIndex = Properties.Settings.Default.DisciplineChairComboBoxFacultyIndex;
            ComboBoxChair.SelectedIndex = Properties.Settings.Default.DisciplineChairComboBoxChairIndex;
            ComboBoxAcademicYear.SelectedIndex = Properties.Settings.Default.AcademicYearSelectedProperty; 
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.DisciplineChairComboBoxFacultyIndex = ComboBoxFaculty.SelectedIndex;
            Properties.Settings.Default.DisciplineChairComboBoxChairIndex = ComboBoxChair.SelectedIndex;
            Properties.Settings.Default.AcademicYearSelectedProperty = ComboBoxAcademicYear.SelectedIndex;
        }
    }
}
