﻿using System.Text;
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
    /// Логика взаимодействия для EditAcademicYearView.xaml
    /// </summary>
    public partial class EditAcademicYearView : UserControl
    {
        public EditAcademicYearView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            tbAcademicYear.Focus();
        }
    }
}
