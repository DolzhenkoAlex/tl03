using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ViewModel;
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

namespace MvvmLight2
{
    /// <summary>
    /// Логика взаимодействия для ReportGroupView.xaml
    /// </summary>
    public partial class ReportGroupView : UserControl
    {
        ReportGroupViewModel reportGroupViewModel;
        private IContainer container = Container.Instance;

        public ReportGroupView()
        {
            InitializeComponent();
            reportGroupViewModel = this.DataContext as ReportGroupViewModel;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //ComboBoxAcademicYear.SelectedIndex = Properties.Settings.Default.DisciplineChairComboBoxAcademicYearIndex;
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            //Properties.Settings.Default.DisciplineChairComboBoxAcademicYearIndex = ComboBoxAcademicYear.SelectedIndex;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
             Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();

                // Получение данных для отчета
                ObservableCollection<tlsp_getGroups_Result> listGroups = reportGroupViewModel.Groups;

                reportDataSource1.Name = "DataSet1"; //Name of the report dataset in our .RDLC file
                reportDataSource1.Value = listGroups;

                this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewer.LocalReport.ReportEmbeddedResource = "MvvmLight2.Report.ReportGroups.rdlc";

                
                string academiaYear = (ComboBoxAcademicYear.SelectedValue as DictAcademicYear).Year;

               
                Microsoft.Reporting.WinForms.ReportParameter p1 = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter1", academiaYear);

                this.reportViewer.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter[] { p1 });

                reportViewer.RefreshReport();

                if (listGroups.Count == 0)
                {
                    MessageBox.Show("Данные по контингенту отсутсвуют", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
        }
    }
}
