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
    /// Логика взаимодействия для EditEducationFormView.xaml
    /// </summary>
    public partial class EditEducationFormView : UserControl
    {
        public EditEducationFormView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            tbEducationForm.Focus();
        }
    }
}

