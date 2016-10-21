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
    /// Логика взаимодействия для ReportFixedDisciplines.xaml
    /// </summary>
    public partial class ReportFixedDisciplinesView : UserControl
    {
        ReportFixedDisciplinesViewModel fixedDisciplineViewModel;
        public ReportFixedDisciplinesView()
        {
            InitializeComponent();

            fixedDisciplineViewModel = this.DataContext as ReportFixedDisciplinesViewModel;
        }
        private void ButtonReport_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxAcademicYear.SelectedValue != null)
            {
                // Создание источника данных для отчета    
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();

                // Получение данных для отчета
                ObservableCollection<tlsp_getAllDiscipline_Result> listFixedDiscilpines = fixedDisciplineViewModel.ListFixedDiscilpines;

                // Формирование свойств отчета
                reportDataSource1.Name = "DataSet1"; //Name of the report dataset in our .RDLC file
                reportDataSource1.Value = listFixedDiscilpines;
                this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewer.LocalReport.ReportEmbeddedResource = "MvvmLight2.Report.ReportFixedAllDisciplines.rdlc";

                // Параметр для отчета
                string academiaYear = (ComboBoxAcademicYear.SelectedValue as DictAcademicYear).Year;
                Microsoft.Reporting.WinForms.ReportParameter p1 = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameterAcademicYear", academiaYear);
                this.reportViewer.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter[] { p1 });

                // обновление отчета
                reportViewer.RefreshReport();

                if (listFixedDiscilpines.Count == 0)
                {
                    MessageBox.Show("Данные по трудоемкости отсутсвуют", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Необходимо выбрать \nУчебный год", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
