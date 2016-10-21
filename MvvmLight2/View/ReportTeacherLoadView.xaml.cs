using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ServiceData;

namespace MvvmLight2
{
    /// <summary>
    /// Логика взаимодействия для ReportTeacherLoadView.xaml
    /// </summary>
    public partial class ReportTeacherLoadView : UserControl
    {
        public ObservableCollection<LoadTeacher> Disciplines { get; private set; }

        private IContainer container = Container.Instance;

        public ReportTeacherLoadView()
        {
            InitializeComponent();

            Disciplines = new ObservableCollection<LoadTeacher>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();

            if ((ComboBoxChair.SelectedValue != null) && 
                (ComboBoxAcademicYear.SelectedValue != null) && 
                (ComboBoxNameLoad.SelectedValue != null) &&
                (ComboBoxTeacher.SelectedValue != null))
            {
                ObservableCollection<LoadTeacher> Load = GettDiscipline();


                reportDataSource1.Name = "DataSet1"; //Name of the report dataset in our .RDLC file
                reportDataSource1.Value = Load;

                this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
                this.reportViewer.LocalReport.ReportEmbeddedResource = "MvvmLight2.Report.ReportTeacherLoad.rdlc";

                string nameTeacher = String.Empty;
                string post = String.Empty;
                string academiaYear = String.Empty;
                
                Teacher teacher = ComboBoxTeacher.SelectedValue as Teacher;
                nameTeacher = teacher.LastName + " " +
                              teacher.FirstName.Substring(0, 1) + "." +
                              teacher.SecondName.Substring(0, 1) + ". ";
                post = teacher.DictPost.Post;
                academiaYear = (ComboBoxAcademicYear.SelectedValue as DictAcademicYear).Year;



                Microsoft.Reporting.WinForms.ReportParameter p1 = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter1", nameTeacher);
                Microsoft.Reporting.WinForms.ReportParameter p2 = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter2", post);
                Microsoft.Reporting.WinForms.ReportParameter p3 = new Microsoft.Reporting.WinForms.ReportParameter("ReportParameter3", academiaYear);

                this.reportViewer.LocalReport.SetParameters(new Microsoft.Reporting.WinForms.ReportParameter[] { p1, p2, p3 });

                reportViewer.RefreshReport();

                if (Load.Count == 0)
                    MessageBox.Show("Данные по нагрузке преподавателя отсутсвуют", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            else
                MessageBox.Show("Необходимо выбрать \n Факультет/Кафедру/Учебный год/Вариант нагрузки/Преподавателя", "Предупреждение", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private ObservableCollection<LoadTeacher> GettDiscipline()
        {
            var LoadTeacherervice = container.Resolve<IServiceLoadTeaher>();
                
            // Обобщенные данные по нагрузке преподавателя
            TeahingLoadTeacher teacherAllLoad = LoadTeacherervice.GetTeahingLoadTeacher((ComboBoxTeacher.SelectedValue as Teacher).Id,
                                                                        (ComboBoxAcademicYear.SelectedValue as DictAcademicYear).Id,
                                                                        (ComboBoxNameLoad.SelectedValue as TeachingLoadChair).NameLoadChair);
                
            if (Disciplines != null)
                Disciplines.Clear();
            // Дисциплины преподавателя
            Disciplines = LoadTeacherervice.GetLoadTeacher(teacherAllLoad.Id, Disciplines);

            return Disciplines;
        }
    }
}

