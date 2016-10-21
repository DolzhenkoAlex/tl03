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
    /// Логика взаимодействия для FixedDiscipline.xaml
    /// </summary>
    public partial class FixedDisciplineView : UserControl
    {
        public FixedDisciplineView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxAcademicYear.SelectedIndex = Properties.Settings.Default.LoadChairComboBoxAcademicYearIndex;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.LoadChairComboBoxAcademicYearIndex = ComboBoxAcademicYear.SelectedIndex;
        }
    }
}
