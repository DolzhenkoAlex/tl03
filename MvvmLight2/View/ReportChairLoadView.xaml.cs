using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ViewModel;

namespace MvvmLight2
{
    /// <summary>
    /// Логика взаимодействия для ReportChairLoadView.xaml
    /// </summary>
    public partial class ReportChairLoadView : UserControl
    {
        ReportChairLoadViewModel reportChairLoadViewModel;
        private IContainer container = Container.Instance;

        private int indexReport;

        public ReportChairLoadView()
        {
            InitializeComponent();

            RadioButtonReportChairLoad.IsChecked = true;
            RadioButtonReportChairLoadWithQualification.IsChecked = false;
            reportChairLoadViewModel = this.DataContext as ReportChairLoadViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((ComboBoxChair.SelectedValue != null) && (ComboBoxAcademicYear.SelectedValue != null) && (ComboBoxNameLoad.SelectedValue != null))
            {
                // Создание источника данных для отчета    
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();

                // Получение данных для отчета
                ObservableCollection<TeachingLoad> Load = reportChairLoadViewModel.LoadChairTeaching;

                reportDataSource1.Name = "DataSet1"; //Name of the report dataset in our .RDLC file
                reportDataSource1.Value = Load;

                this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);

                switch (indexReport)
                {
                    case 1:
                        this.reportViewer.LocalReport.ReportEmbeddedResource = "MvvmLight2.Report.ReportChairLoad.rdlc";
                        break;
                    case 2:
                        this.reportViewer.LocalReport.ReportEmbeddedResource = "MvvmLight2.Report.ReportChairLoadWithQualification.rdlc";
                        break;
                }

                string nameChair = String.Empty;
                string academiaYear = String.Empty;
                string nameLoad = String.Empty;


                nameChair = (ComboBoxChair.SelectedValue as Chair).NameChair;
                academiaYear = (ComboBoxAcademicYear.SelectedValue as DictAcademicYear).Year;
                nameLoad = (ComboBoxNameLoad.SelectedValue as TeachingLoadChair).NameLoadChair;

                Microsoft.Reporting.WinForms.ReportParameter p1 = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter1", nameChair);
                Microsoft.Reporting.WinForms.ReportParameter p2 = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter2", academiaYear);
                Microsoft.Reporting.WinForms.ReportParameter p3 = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter3", nameLoad);

                this.reportViewer.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter[] { p1, p2, p3 });

                reportViewer.RefreshReport();

                if (Load.Count == 0)
                {
                    MessageBox.Show("Данные по нагрузке кафедры отсутсвуют", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Необходимо выбрать \n Факультет/Кафедру/Учебный год/Вариант нагрузки", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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

        private void RadioButtonReportChairLoad_Checked(object sender, RoutedEventArgs e)
        {
            if (RadioButtonReportChairLoad.IsChecked == true)
            {
                indexReport = 1;
                RadioButtonReportChairLoadWithQualification.IsChecked = false;
            }
            else
            {
                indexReport = 2;
                RadioButtonReportChairLoadWithQualification.IsChecked = true;
            }
        }

        private void RadioButtonReportChairLoadWithQualification_Checked(object sender, RoutedEventArgs e)
        {
            if (RadioButtonReportChairLoadWithQualification.IsChecked == true)
            {
                indexReport = 2;
                RadioButtonReportChairLoad.IsChecked = false;
            }
            else
            {
                indexReport = 1;
                RadioButtonReportChairLoad.IsChecked = true;
            }
        }
    }
}
