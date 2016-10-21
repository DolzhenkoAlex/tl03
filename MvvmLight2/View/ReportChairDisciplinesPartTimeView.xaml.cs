using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MvvmLight2.ServiceData;
using MvvmLight2.ViewModel;

namespace MvvmLight2.View
{
    /// <summary>
    /// Логика взаимодействия для ReportChairDisciplinesPartTimeView.xaml
    /// </summary>
    public partial class ReportChairDisciplinesPartTimeView : UserControl
    {
        ReportChairDisciplinesPartTimeViewModel chairDisciplinesPartTimeViewModel; 

        private IContainer container = Container.Instance;

        public ReportChairDisciplinesPartTimeView()
        {
            InitializeComponent();
            chairDisciplinesPartTimeViewModel = this.DataContext as ReportChairDisciplinesPartTimeViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((ComboBoxChair.SelectedValue != null) && (ComboBoxAcademicYear.SelectedValue != null))
            {
                // Создание источника данных для отчета   
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();

                // Получение данных для отчета
                ObservableCollection<tlsp_getDisciplineChairPartTime_Result> disciplines = chairDisciplinesPartTimeViewModel.Disciplines;

                reportDataSource1.Name = "DataSet1"; //Name of the report dataset in our .RDLC file
                reportDataSource1.Value = disciplines;

                this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewer.LocalReport.ReportEmbeddedResource = "MvvmLight2.Report.ReportChairDisciplinesPartTime.rdlc";

                string nameChair = (ComboBoxChair.SelectedValue as Chair).NameChair;
                string academiaYear = (ComboBoxAcademicYear.SelectedValue as DictAcademicYear).Year;

                Microsoft.Reporting.WinForms.ReportParameter p1 = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter1", nameChair);
                Microsoft.Reporting.WinForms.ReportParameter p2 = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter2", academiaYear);

                this.reportViewer.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter[] { p1, p2 });

                reportViewer.RefreshReport();

                if (disciplines.Count == 0)
                    MessageBox.Show("Данные по дисциплинам кафедры отсутсвуют", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
                MessageBox.Show("Необходимо выбрать \n Кафедру/Учебный год", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

      
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxAcademicYear.SelectedIndex = Properties.Settings.Default.DisciplineChairComboBoxAcademicYearIndex;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.DisciplineChairComboBoxAcademicYearIndex = ComboBoxAcademicYear.SelectedIndex;
        }
    }
}
