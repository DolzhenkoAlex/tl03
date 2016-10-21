using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MvvmLight2
{
    /// <summary>
    /// Логика взаимодействия для LoadChairView.xaml
    /// </summary>
    public partial class LoadChairView : UserControl
    {
        public LoadChairView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxFaculty.SelectedIndex = Properties.Settings.Default.LoadChairComboBoxFacultyIndex;
            ComboBoxChair.SelectedIndex = Properties.Settings.Default.LoadChairComboBoxChairIndex;
            ComboBoxAcademicYear.SelectedIndex = Properties.Settings.Default.LoadChairComboBoxAcademicYearIndex;
            ComboBoxNameLoad.SelectedIndex = Properties.Settings.Default.LoadChairComboBoxNameLoadIndex;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.LoadChairComboBoxFacultyIndex =  ComboBoxFaculty.SelectedIndex;
            Properties.Settings.Default.LoadChairComboBoxChairIndex =  ComboBoxChair.SelectedIndex;
            Properties.Settings.Default.LoadChairComboBoxAcademicYearIndex =  ComboBoxAcademicYear.SelectedIndex;
            Properties.Settings.Default.LoadChairComboBoxNameLoadIndex =  ComboBoxNameLoad.SelectedIndex;
        }
    }
}
