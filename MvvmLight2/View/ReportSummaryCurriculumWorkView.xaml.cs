using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ServiceData;
using MvvmLight2.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MvvmLight2
{
    /// <summary>
    /// Логика взаимодействия для ReportManpoweCurriculum.xaml
    /// </summary>
    public partial class ReportSummaryCurriculumWorkView : UserControl
    {
        /// <summary>
        /// ViewModel отчета
        /// </summary>
        ReportSummaryCurriculumWorkViewModel workViewModel;

        //Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1;
        
        public ReportSummaryCurriculumWorkView()
        {
            InitializeComponent();
            RadioButtonReportAllDisciplines.IsChecked = false;
            RadioButtonReportWithoutDisciplineOfChoice.IsChecked = true;
            workViewModel = this.DataContext as ReportSummaryCurriculumWorkViewModel;
           
        }

        private void ButtonReport_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxAcademicYear.SelectedValue != null)
            {
                 //Создание источника данных для отчета    
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();

                // Получение данных для отчета
                ListSummaryCurriculumWork summaryWorks = workViewModel.SummaryListCurriculumWorks;

                // Формирование свойств отчета
                reportDataSource1.Name = "DataSet1"; //Name of the report dataset in our .RDLC file
                reportDataSource1.Value = summaryWorks;
                this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewer.LocalReport.ReportEmbeddedResource = "MvvmLight2.Report.ReportSummaryCurriculumWork.rdlc";

                // Параметр для отчета
                string academiaYear = (ComboBoxAcademicYear.SelectedValue as DictAcademicYear).Year;
                Microsoft.Reporting.WinForms.ReportParameter p1 = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter1", academiaYear);
                this.reportViewer.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter[] { p1 });
                
                // обновление отчета
                this.reportViewer.RefreshReport();
                
                if (summaryWorks.Count == 0)
                {
                    MessageBox.Show("Данные по закреплению дисциплин отсутствуют", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Необходимо выбрать \nУчебный год", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RadioButtonReportAllDisciplines_Checked(object sender, RoutedEventArgs e)
        {
            this.reportViewer.LocalReport.DataSources.Clear();
            this.reportViewer.RefreshReport();

            if ((bool)RadioButtonReportAllDisciplines.IsChecked)
                RadioButtonReportWithoutDisciplineOfChoice.IsChecked = false;
        }

        private void RadioButtonReportWithoutDisciplineOfChoice_Checked(object sender, RoutedEventArgs e)
        {
            this.reportViewer.LocalReport.DataSources.Clear();
            this.reportViewer.RefreshReport();

            if ((bool)RadioButtonReportWithoutDisciplineOfChoice.IsChecked)
                RadioButtonReportAllDisciplines.IsChecked = false; 
        }    
    }
}
