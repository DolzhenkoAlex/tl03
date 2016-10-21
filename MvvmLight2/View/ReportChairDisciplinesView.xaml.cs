using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ViewModel;

namespace MvvmLight2
{
    /// <summary>
    /// Логика взаимодействия для ReportChairDisciplinesView.xaml
    /// </summary>
    public partial class ReportChairDisciplinesView : UserControl
    {

        ReportChairDisciplinesViewModel disciplineViewModel;

        private IContainer container = Container.Instance;
        public ReportChairDisciplinesView()
        {
            InitializeComponent();
            disciplineViewModel = this.DataContext as ReportChairDisciplinesViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((ComboBoxChair.SelectedValue != null) && (ComboBoxAcademicYear.SelectedValue != null))
            {
                Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();

                // Получение данных для отчета
                ObservableCollection<tlsp_getDisciplineChair_Result> listDiscilpines = disciplineViewModel.Disciplines;

                reportDataSource1.Name = "DataSet1"; //Name of the report dataset in our .RDLC file
                reportDataSource1.Value = listDiscilpines;

                this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewer.LocalReport.ReportEmbeddedResource = "MvvmLight2.Report.ReportChairDisciplines.rdlc";

                string nameChair = (ComboBoxChair.SelectedValue as Chair).NameChair;
                string academiaYear = (ComboBoxAcademicYear.SelectedValue as DictAcademicYear).Year;

                Microsoft.Reporting.WinForms.ReportParameter p1 = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter1", nameChair);
                Microsoft.Reporting.WinForms.ReportParameter p2 = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter2", academiaYear);

                this.reportViewer.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter[] { p1, p2 });

                reportViewer.RefreshReport();

                if (listDiscilpines.Count == 0)
                {
                    MessageBox.Show("Данные по дисциплинам кафедры отсутсвуют", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
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
