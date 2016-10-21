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
    /// Логика взаимодействия для LoadTeacherView.xaml
    /// </summary>
    public partial class LoadTeacherView : UserControl
    {
        public LoadTeacherView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxFaculty.SelectedIndex = Properties.Settings.Default.FacultySelectedIndexProperty;
            ComboBoxChair.SelectedIndex = Properties.Settings.Default.ChairSelectedProperty;
            ComboBoxAcademicYear.SelectedIndex = Properties.Settings.Default.AcademicYearSelectedProperty;
        }

        //private void ComboBoxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}
    }
}
