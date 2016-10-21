using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ViewModel;

namespace MvvmLight2.View
{
    /// <summary>
    /// Логика взаимодействия для ReportChairLoadFullTimeView.xaml
    /// </summary>
    public partial class ReportChairLoadFullTimeView : UserControl
    {
        ReportChairLoadFullTimeViewModel reportChairLoadFullTimeViewModel;

        private IContainer container = Container.Instance;

        private int indexReport;
        
        public ReportChairLoadFullTimeView()
        {
            InitializeComponent();
            RadioButtonReportChaieLoadFuLLTime.IsChecked = true;
            RadioButtonReportChairLoadFuLLTimeWithQualification.IsChecked = false;
            reportChairLoadFullTimeViewModel = this.DataContext as ReportChairLoadFullTimeViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((ComboBoxChair.SelectedValue != null) && (ComboBoxAcademicYear.SelectedValue != null) && (ComboBoxNameLoad.SelectedValue != null))
            {
                // Создание источника данных для отчета    
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();

                // Получение данных для отчета
                ObservableCollection<TeachingLoad> Load = reportChairLoadFullTimeViewModel.LoadChairTeaching;

                reportDataSource1.Name = "DataSet1"; //Name of the report dataset in our .RDLC file
                reportDataSource1.Value = Load;

                this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);

                switch (indexReport)
                {
                    case 1:
                        this.reportViewer.LocalReport.ReportEmbeddedResource = "MvvmLight2.Report.ReportChairLoadFullTime.rdlc";
                        break;
                    case 2:
                        this.reportViewer.LocalReport.ReportEmbeddedResource = "MvvmLight2.Report.ReportChairLoadFullTimeWithQualification.rdlc";
                        break;
                }

                string nameChair = (ComboBoxChair.SelectedValue as Chair).NameChair;
                string academiaYear = (ComboBoxAcademicYear.SelectedValue as DictAcademicYear).Year;
                string nameLoad = (ComboBoxNameLoad.SelectedValue as TeachingLoadChair).NameLoadChair;

                Microsoft.Reporting.WinForms.ReportParameter p1 = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter1", nameChair);
                Microsoft.Reporting.WinForms.ReportParameter p2 = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter2", academiaYear);
                Microsoft.Reporting.WinForms.ReportParameter p3 = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter3", nameLoad);

                this.reportViewer.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter[] { p1, p2, p3 });

                reportViewer.RefreshReport();

                if (Load.Count == 0)
                    MessageBox.Show("Данные по нагрузке кафедры отсутсвуют", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
                 MessageBox.Show("Необходимо выбрать \n Факультет/Кафедру/Учебный год/Вариант нагрузки", 
                     "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxAcademicYear.SelectedIndex = Properties.Settings.Default.LoadChairComboBoxAcademicYearIndex;
            ComboBoxNameLoad.SelectedIndex = Properties.Settings.Default.LoadChairComboBoxNameLoadIndex;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.LoadChairComboBoxAcademicYearIndex = ComboBoxAcademicYear.SelectedIndex;
            Properties.Settings.Default.LoadChairComboBoxNameLoadIndex = ComboBoxNameLoad.SelectedIndex;
        }

        private void RadioButtonReportChaieLoadFullTime_Checked(object sender, RoutedEventArgs e)
        {
            if(RadioButtonReportChaieLoadFuLLTime.IsChecked == true)
            {
                indexReport = 1;
                RadioButtonReportChairLoadFuLLTimeWithQualification.IsChecked = false;
            }
            else
            {
                indexReport = 2;
                RadioButtonReportChairLoadFuLLTimeWithQualification.IsChecked = true;
            }
        }

        private void RadioButtonReportChairLoadFuLLTimeWithQualification_Checked(object sender, RoutedEventArgs e)
        {
            if (RadioButtonReportChairLoadFuLLTimeWithQualification.IsChecked == true)
            {
                indexReport = 2;
                RadioButtonReportChaieLoadFuLLTime.IsChecked = false;
            }
            else
            {
                indexReport = 1;
                RadioButtonReportChaieLoadFuLLTime.IsChecked = true;
            }
        }
    }
}
