using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Data;
using System.Windows.Input;
using MvvmLight2.Helper;
using GalaSoft.MvvmLight.Command;
using MvvmLight2.ServiceData;
using MvvmLight2.View;
using System.Data.SqlClient;
using System.Data;
using MvvmLight2.Model;
using System;
using System.Windows;
using System.Configuration;
using System.Data.EntityClient;
using System.Data.Sql;

namespace MvvmLight2.ViewModel
{
    public class MainViewModel : WorkspaceViewModel
    {
        //string con = string.Empty;

        //public string Con
        //{
        //    get { return con; }
        //    set { con = value; }
        //}
        
        #region Fields

        private MvvmLight2.Helper.IContainer container = MvvmLight2.Helper.Container.Instance;

        /// <summary>
        /// Коллекция рабочих областей (вкладок) главного окна приложения
        /// </summary>
        ObservableCollection<WorkspaceViewModel> workspaces;

        #endregion Fields

        #region Constructor

        public MainViewModel() { }


        #endregion // Constructor

        #region Workspaces

        /// <summary>
        /// Свойство  Workspaces- рабочая область приложения, 
        /// которое возвращает коллекцию активных рабочих областей приложения
        /// Returns the collection of available workspaces to display.
        /// A 'workspace' is a ViewModel that can request to be closed.
        /// </summary>
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (workspaces == null)
                {
                    workspaces = new ObservableCollection<WorkspaceViewModel>();
                    /// Включение прослушивания события CollectionChanged  - изменения в коллекции
                    workspaces.CollectionChanged += this.OnWorkspacesChanged;
                }
                return workspaces;
            }

        }

        /// <summary>
        /// Обработка события CollectionChanged  - изменения в коллекции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.OnWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.OnWorkspaceRequestClose;
        }

        /// <summary>
        /// Закрытие рабочей области (вкладки) интерфейса прользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnWorkspaceRequestClose(object sender, System.EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            //workspace.Dispose();
            this.Workspaces.Remove(workspace);
        }

        #endregion Workspaces

        #region Private Helpers

        /// <summary>
        /// Проверка правильности подключения к серверу базы данных
        /// </summary>
        private void ExecuteCheckConnection()
        {
            Exception error = null;

            // Создание экземпляра соединения EntityConnection.
            EntityConnection conn =
                new EntityConnection("name=DBTeachingLoadEntities");
            
            // Открытие соединения
            if (conn.State != ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch (EntityException ex)
                {
                    error = new Exception(String.Format("Ошибка подключения к серверу\n {0}\n {1}", ex.Message, ex.InnerException.Message));
                    string msg = string.Format("{0}\n\nСоздать подключение к серверу?", error.Message);
                    var result = MessageBox.Show(msg, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                    if (result == MessageBoxResult.OK)
                    {
                        ConnectionStringDBExecute();
                    }
                    else
                    {
                        App.Current.MainWindow.Close();
                    }
                }
                finally
                {
                    // Удаление экземпляра соединения
                    conn.Dispose();
                }
            }
        }


        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Преподаватели"
        /// </summary>
        private void ShowTeacher()
        {

            TeachersViewModel workspace = null;

            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is TeachersViewModel)
                {
                    workspace = ws as TeachersViewModel;
                    break;
                }
            }

            if (workspace == null)
            {
                workspace = new TeachersViewModel(new ServiceTeaher());
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Кафедры"
        /// </summary>
        private void ShowChair()
        {
            ChairsViewModel workspace = null;

            // Проверка наличия рабочей области TeachersViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is ChairsViewModel)
                {
                    workspace = ws as ChairsViewModel;
                    break;
                }
            }

            // Если рабочей области  TeachersViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new ChairsViewModel(new ServiceChair());
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Учебные группы"
        /// </summary>
        private void ShowGroup()
        {
            GroupViewModel workspace = null;

            // Проверка наличия рабочей области GroupViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is GroupViewModel)
                {
                    workspace = ws as GroupViewModel;
                    break;
                }
            }

            // Если рабочей области  GroupViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new GroupViewModel(new ServiceGroup());
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Факультеты"
        /// </summary>
        private void ShowFaculty()
        {
            FacultiesViewModel workspace = null;

            // Проверка наличия рабочей области FacultiesViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is FacultiesViewModel)
                {
                    workspace = ws as FacultiesViewModel;
                    break;
                }
            }

            // Если рабочей области  FacultiesViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new FacultiesViewModel(new ServiceFaculty());
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Университеты"
        /// </summary>
        private void ShowUniversity()
        {
            UniversitiesViewModel workspace = null;

            // Проверка наличия рабочей области UniversitiesViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is UniversitiesViewModel)
                {
                    workspace = ws as UniversitiesViewModel;
                    break;
                }
            }

            // Если рабочей области  UniversitiesViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new UniversitiesViewModel(new ServiceUniversity());
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }


        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Приказы по нормам времени"
        /// </summary>
        private void ShowOrderStandartTime()
        {
            OrderStandardTimeViewModel workspace = null;

            // Проверка наличия рабочей области OrderStandardTimeViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is OrderStandardTimeViewModel)
                {
                    workspace = ws as OrderStandardTimeViewModel;
                    break;
                }
            }

            // Если рабочей области  OrderStandardTimeViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new OrderStandardTimeViewModel(new ServiceOrderStandartTime());
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }


        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Титулы учебных планов"
        /// </summary>
        private void ShowTitleCurriculum()
        {
            CurriculumViewMode workspace = null;

            // Проверка наличия рабочей области CurriculumViewMode в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is CurriculumViewMode)
                {
                    workspace = ws as CurriculumViewMode;
                    break;
                }
            }

            // Если рабочей области  CurriculumViewMode нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new CurriculumViewMode(new ServiceCurriculum());
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Титулы учебных планов DB"
        /// </summary>
        private void ShowTitleCurriculumFromDB()
        {
            CurriculumFromDBViewMode workspace = null;

            // Проверка наличия рабочей области CurriculumFromDBViewMode в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is CurriculumFromDBViewMode)
                {
                    workspace = ws as CurriculumFromDBViewMode;
                    break;
                }
            }

            // Если рабочей области  CurriculumFromDBViewMode нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new CurriculumFromDBViewMode(new ServiceCurriculum());
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Титулы учебных планов из XML"
        /// </summary>
        private void ShowTitleCurriculumFromXml()
        {
            CurriculumFromXmlViewMode workspace = null;

            // Проверка наличия рабочей области CurriculumFromXmlViewMode в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is CurriculumFromXmlViewMode)
                {
                    workspace = ws as CurriculumFromXmlViewMode;
                    break;
                }
            }

            // Если рабочей области  CurriculumFromXmlViewMode нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new CurriculumFromXmlViewMode(new ServiceCurriculum());
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Титулы учебных планов из EXL"
        /// </summary>
        private void ShowTitleCurriculumFromExl()
        {
            CurriculumFromExlViewMode workspace = null;

            // Проверка наличия рабочей области CurriculumFromExlViewMode в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is CurriculumFromExlViewMode)
                {
                    workspace = ws as CurriculumFromExlViewMode;
                    break;
                }
            }

            // Если рабочей области  CurriculumFromExlViewMode нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new CurriculumFromExlViewMode(new ServiceCurriculum());
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Дисциплины Кафедры"
        /// </summary>
        private void ShowDisciplineChair()
        {
            DisciplineChairViewModel workspace = null;

            // Проверка наличия рабочей области DisciplineChairViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is DisciplineChairViewModel)
                {
                    workspace = ws as DisciplineChairViewModel;
                    break;
                }
            }

            // Если рабочей области  DisciplineChairViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new DisciplineChairViewModel(new ServiceDisciplineChair());
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Закрепление дисциплин"
        /// </summary>
        private void ShowFixedDiscipline()
        {
            FixedDisciplineViewModel workspace = null;

            // Проверка наличия рабочей области DisciplineChairViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is DisciplineChairViewModel)
                {
                    workspace = ws as FixedDisciplineViewModel;
                    break;
                }
            }

            // Если рабочей области  FixedDisciplineViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new FixedDisciplineViewModel(new ServiceFixedDiscipline());
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        // <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Нагрузка Кафедры"
        /// </summary>
        private void ShowLoadChair()
        {
            LoadChairViewModel workspace = null;

            // Проверка наличия рабочей области LoadChairViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is LoadChairViewModel)
                {
                    workspace = ws as LoadChairViewModel;
                    break;
                }
            }

            // Если рабочей области  LoadChairViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new LoadChairViewModel(new ServiceLoadChair());
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с отчетом "Нагрузка кафедры - очное отделение"
        /// </summary>
        private void ShowReportLoadChairFullTime()
        {
            ReportChairLoadFullTimeViewModel workspace = null;

            // Проверка наличия рабочей области ReportChairLoadFullTimeViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is ReportChairLoadFullTimeViewModel)
                {
                    workspace = ws as ReportChairLoadFullTimeViewModel;
                    break;
                }
            }

            // Если рабочей области ReportChairLoadFullTimeViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new ReportChairLoadFullTimeViewModel();
                this.Workspaces.Add(workspace);
            }
            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с отчетом "Нагрузка кафедры - заочное отделение"
        /// </summary>
        private void ShowReportLoadChairPartTime()
        {
            ReportChairLoadPartTimeViewModel workspace = null;

            // Проверка наличия рабочей области ReportChairLoadPartTimeViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is ReportChairLoadPartTimeViewModel)
                {
                    workspace = ws as ReportChairLoadPartTimeViewModel;
                    break;
                }
            }

            // Если рабочей области ReportChairLoadPartTimeViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new ReportChairLoadPartTimeViewModel(new ServiceLoadChair());
                this.Workspaces.Add(workspace);
            }
            this.SetActiveWorkspace(workspace);
        }
        
        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Нагрузка преподавателя"
        /// </summary>
        private void ShowSetLoadTeacher()
        {
            LoadTeacherViewModel workspace = null;

            // Проверка наличия рабочей области LoadTeacherViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is LoadTeacherViewModel)
                {
                    workspace = ws as LoadTeacherViewModel;
                    break;
                }
            }

            // Если рабочей области  LoadTeacherViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new LoadTeacherViewModel(new ServiceLoadTeaher());
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Справочник Направлений подготовки"
        /// </summary>
        private void ShowSpeciality()
        {
            SpecialityViewModel workspace = null;

            // Проверка наличия рабочей области SpecialityViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is SpecialityViewModel)
                {
                    workspace = ws as SpecialityViewModel;
                    break;
                }
            }

            // Если рабочей области  SpecialityViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new SpecialityViewModel(new ServiceSpeciality());
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Справочник Учебный год"
        /// </summary>
        private void ShowAcademicYear()
        {
            DictAcademicYearViewModel workspace = null;

            // Проверка наличия рабочей области AcademicYear в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is DictAcademicYearViewModel)
                {
                    workspace = ws as DictAcademicYearViewModel;
                    break;
                }
            }

            // Если рабочей области  DictAcademicYearViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new DictAcademicYearViewModel(new ServiceAcademicYear());
                this.Workspaces.Add(workspace);
            }
            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Справочник Формы обучения"
        /// </summary>
        private void ShowEducationForm()
        {
            DictEducationFormViewModel workspace = null;

            // Проверка наличия рабочей области DictEducationFormViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is DictEducationFormViewModel)
                {
                    workspace = ws as DictEducationFormViewModel;
                    break;
                }
            }

            // Если рабочей области  DictEducationFormViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new DictEducationFormViewModel(new ServiceEducationForm());
                this.Workspaces.Add(workspace);
            }
            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Справочник Должностей преподавателей"
        /// </summary>
        private void ShowPost()
        {
            DictPostViewModel workspace = null;

            // Проверка наличия рабочей области DictPostViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is DictPostViewModel)
                {
                    workspace = ws as DictPostViewModel;
                    break;
                }
            }

            // Если рабочей области  DictPostViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new DictPostViewModel(new ServicePost());
                this.Workspaces.Add(workspace);
            }
            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Справочник квалификация обучающихся"
        /// </summary>
        private void ShowQualification()
        {
            DictQualificationViewModel workspace = null;

            // Проверка наличия рабочей области DictQualificationViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is DictQualificationViewModel)
                {
                    workspace = ws as DictQualificationViewModel;
                    break;
                }
            }

            // Если рабочей области  DictQualificationViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new DictQualificationViewModel(new ServiceQualification());
                this.Workspaces.Add(workspace);
            }
            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Справочник тип занятости преподавателя"
        /// </summary>
        private void ShowTypeEmployment()
        {
            DictTypeEmploymentViewModel workspace = null;

            // Проверка наличия рабочей области DictTypeEmploymentViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is DictTypeEmploymentViewModel)
                {
                    workspace = ws as DictTypeEmploymentViewModel;
                    break;
                }
            }

            // Если рабочей области  DictTypeEmploymentViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new DictTypeEmploymentViewModel(new ServiceTypeEmployment());
                this.Workspaces.Add(workspace);
            }
            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Справочник вида учебной работы"
        /// </summary>
        private void ShowTypeTraining()
        {
            DictTypeTrainingViewModel workspace = null;

            // Проверка наличия рабочей области DictTypeTrainingViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is DictTypeTrainingViewModel)
                {
                    workspace = ws as DictTypeTrainingViewModel;
                    break;
                }
            }

            // Если рабочей области  DictTypeTrainingViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new DictTypeTrainingViewModel(new ServiceTypeTraining());
                this.Workspaces.Add(workspace);
            }
            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с функцией "Справочник единиц измерения учебной работы/нагрузки"
        /// </summary>
        private void ShowUnit()
        {
            DictUnitViewModel workspace = null;

            // Проверка наличия рабочей области DictUnitViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is DictUnitViewModel)
                {
                    workspace = ws as DictUnitViewModel;
                    break;
                }
            }

            // Если рабочей области  DictUnitViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new DictUnitViewModel(new ServiceUnit());
                this.Workspaces.Add(workspace);
            }
            this.SetActiveWorkspace(workspace);
        }


        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с отчетом "Дисциплины кафедры"
        /// </summary>
        private void ShowReportDisciplineChair()
        {
            ReportChairDisciplinesViewModel workspace = null;

            // Проверка наличия рабочей области ReportChairDisciplinesViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is ReportChairDisciplinesViewModel)
                {
                    workspace = ws as ReportChairDisciplinesViewModel;
                    break;
                }
            }

            // Если рабочей области  ReportChairDisciplinesViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                //workspace = new ReportChairDisciplinesViewModel(new ServiceDisciplineChair());
                workspace = new ReportChairDisciplinesViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с отчетом "Дисциплины кафедры - очное отделение"
        /// </summary>
        private void ShowReportDisciplineChairFullTime()
        {
            ReportChairDisciplinesFullTimeViewModel workspace = null;

            // Проверка наличия рабочей области ReportChairDisciplinesFullTimeViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is ReportChairDisciplinesFullTimeViewModel)
                {
                    workspace = ws as ReportChairDisciplinesFullTimeViewModel;
                    break;
                }
            }

            // Если рабочей области ReportChairDisciplinesFullViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new ReportChairDisciplinesFullTimeViewModel(new ServiceDisciplineChair());
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с отчетом "Дисциплины кафедры - заочное отделение"
        /// </summary>
        private void ShowReportDisciplineChairPartTime()
        {
            ReportChairDisciplinesPartTimeViewModel workspace = null;

            // Проверка наличия рабочей области ReportChairDisciplinesPartTimeViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is ReportChairDisciplinesPartTimeViewModel)
                {
                    workspace = ws as ReportChairDisciplinesPartTimeViewModel;
                    break;
                }
            }

            // Если рабочей области ReportChairDisciplinesPartViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                //workspace = new ReportChairDisciplinesPartTimeViewModel(new ServiceDisciplineChair());
                workspace = new ReportChairDisciplinesPartTimeViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с отчетом "Нагрузка кафедры"
        /// </summary>
        private void ShowReportLoadChair()
        {
            ReportChairLoadViewModel workspace = null;

            // Проверка наличия рабочей области ReportChairLoadViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is ReportChairLoadViewModel)
                {
                    workspace = ws as ReportChairLoadViewModel;
                    break;
                }
            }

            // Если рабочей области  ReportChairLoadViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new ReportChairLoadViewModel();
                this.Workspaces.Add(workspace);
            }
            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с отчетом "Нагрузка преподавателя"
        /// </summary>
        private void ShowReportLoadTeacher()
        {
            ReportTeacherLoadViewModel workspace = null;

            // Проверка наличия рабочей области ReportTeacherLoadViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is ReportTeacherLoadViewModel)
                {
                    workspace = ws as ReportTeacherLoadViewModel;
                    break;
                }
            }

            // Если рабочей области  ReportTeacherLoadViewModel нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new ReportTeacherLoadViewModel(new ServiceLoadTeaher());
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }


        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с отчетом Сводные данные по трудоемкости
        /// </summary>
        private void ShowReportSummaryCurriculumWork()
        {
            ReportSummaryCurriculumWorkViewModel workspace = null;

            // Проверка наличия рабочей области ReportManpoweCurriculumViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is ReportTeacherLoadViewModel)
                {
                    workspace = ws as ReportSummaryCurriculumWorkViewModel;
                    break;
                }
            }

            // Если рабочей области  ReportManpoweCurriculumViewModel  нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                //workspace = new ReportSummaryCurriculumWorkViewModel(new ServiceCurriculum());
                workspace = new ReportSummaryCurriculumWorkViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }


        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с отчетом Закрепление дисциплин
        /// </summary>
        private void ShowReportFixedDiscipline()
        {
            ReportFixedDisciplinesViewModel workspace = null;

            // Проверка наличия рабочей области ReportFixedDisciplinesViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is ReportTeacherLoadViewModel)
                {
                    workspace = ws as ReportFixedDisciplinesViewModel;
                    break;
                }
            }

            // Если рабочей области  ReportFixedDisciplinesViewModel  нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                //workspace = new ReportFixedDisciplinesViewModel(new ServiceCurriculum());
                workspace = new ReportFixedDisciplinesViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отрисовка рабочей области - закладки для работы с отчетом Контингент
        /// </summary>
        private void ShowReportReportGroups()
        {
            ReportGroupViewModel workspace = null;

            // Проверка наличия рабочей области ReportGroupViewModel в списке рабочих областей Workspaces
            foreach (WorkspaceViewModel ws in Workspaces)
            {
                if (ws is ReportGroupViewModel)
                {
                    workspace = ws as ReportGroupViewModel;
                    break;
                }
            }

            // Если рабочей области  ReportGroupViewModel  нет в списке Workspaces, 
            // то создаем рабочую область и добавляем её в список
            if (workspace == null)
            {
                workspace = new ReportGroupViewModel();
                this.Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        /// <summary>
        /// Отображение окна "О программе"
        /// </summary>
        private void  ShowAbout()
        {
            var viewModel = container.Resolve<AboutViewModel>();
            var viewEdit = container.Resolve<AboutView>();
            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("О программе Нагрузка преподавателя", viewEdit);
        }

        /// <summary>
        /// Создание соединения с текущим сервером
        /// </summary>
        private void ConnectionStringDBExecute()
        {
            // Коллекция имен видимых экземпляров SQL Server
            ObservableCollection<string> listServerName = new ObservableCollection<string>();

            // Перечислитель для формирования всех доступных SQL серверов
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;

            // Извлекает объект DataTable, содержащий информацию обо всех видимых экземплярах SQL Server 
            System.Data.DataTable table = instance.GetDataSources();

            // Формирование списка серверов
            foreach (System.Data.DataRow row in table.Rows)
            {
                foreach (System.Data.DataColumn col in table.Columns)
                {
                    if (row[col].ToString() != "")
                        listServerName.Add(row[col].ToString());
                }
            }

            // Подготовка диалогового окна для выбора сервера
            var viewModel = container.Resolve<ServerConnectionViewModel>();
            viewModel.ListServerName = listServerName;
            var viewEdit = container.Resolve<ServerConnectView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Подключение к серверу", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                string nameServer = viewModel.SelectedServerName;

                var conService = container.Resolve<IServiceConnectionStringDB>();
                conService.SetConnection(nameServer);

                MessageBox.Show("Необходимо перезагрузить программу!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Information);
                App.Current.MainWindow.Close();
            }
            else
            {
                App.Current.MainWindow.Close();
            }
        }


        /// <summary>
        /// Установить рабочую область активной
        /// </summary>
        /// <param name="workspace"></param>
        void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        #endregion Private Helpers

        #region Command

        #region CommandCheckConnection

        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией Университеты
        /// </summary>
        private ICommand checkConnection;

        public ICommand CheckConnection
        {
            get
            {
                if (checkConnection == null)
                    checkConnection = new RelayCommand(() => this.ExecuteCheckConnection());
                return checkConnection;
            }
        }


        #endregion CommandCheckConnection


        #region CommandUniversity

        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией Университеты
        /// </summary>
        private ICommand universityCommand;

        public ICommand UniversityCommand
        {
            get
            {
                if (universityCommand == null)
                    universityCommand = new RelayCommand(() => this.ShowUniversity());
                return universityCommand;
            }
        }

        #endregion CommandUniversity

        #region CommandFaculties

        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией Факультеты
        /// </summary>
        private ICommand facultiesCommand;

        public ICommand FacultiesCommand
        {
            get
            {
                if (facultiesCommand == null)
                    facultiesCommand = new RelayCommand(() => this.ShowFaculty());
                return facultiesCommand;
            }
        }

        #endregion CommandFaculties

        #region CommandTeacher

        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией Преподаватели
        /// </summary>
        private ICommand teachersCommand;

        /// <summary>
        /// Свойство - Открытие закладки работы с фукнцией Преподаватели
        /// </summary>
        public ICommand TeachersCommand
        {
            get
            {
                if (teachersCommand == null)
                    teachersCommand = new RelayCommand(() => this.ShowTeacher());
                return teachersCommand;
            }
        }

        #endregion CommandTeacher

        #region CommandChair

        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией Кафедры
        /// </summary>
        private ICommand chairsCommand;

        public ICommand ChairsCommand
        {
            get
            {
                if (chairsCommand == null)
                    chairsCommand = new RelayCommand(() => this.ShowChair());
                return chairsCommand;
            }
        }

        #endregion CommandChair

        #region CommandGroup

        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией учебные группы
        /// </summary>
        private ICommand groupsCommand;

        public ICommand GroupsCommand
        {
            get
            {
                if (groupsCommand == null)
                    groupsCommand = new RelayCommand(() => this.ShowGroup());
                return groupsCommand;
            }
        }

        #endregion CommandGroup

        #region LoadTeacherCommand

        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией Нагрузка преподавателя
        /// </summary>
        private ICommand loadTeacherCommand;

        public ICommand LoadTeacherCommand
        {
            get
            {
                if (loadTeacherCommand == null)
                    loadTeacherCommand = new RelayCommand(() => this.ShowSetLoadTeacher());
                return loadTeacherCommand;
            }
        }

        #endregion LoadTeacherCommand

        #region ReportDisciplineChairCommand

        /// <summary>
        /// Поле - Открытие закладки работы с отчетом по дисциплинам кафедры
        /// </summary>
        private ICommand reportDisciplineChair;

        /// <summary>
        /// Открытие закладки работы с отчетом по дисциплинам кафедры
        /// </summary>
        public ICommand ReportDisciplineChair
        {
            get
            {
                if (reportDisciplineChair == null)
                    reportDisciplineChair = new RelayCommand(() => this.ShowReportDisciplineChair());
                return reportDisciplineChair;
            }
        }

        #endregion ReportDisciplineChairCommand

        #region ReportDisciplineChairFullTimeCommand

        /// <summary>
        /// Поле - Открытие закладки работы с отчетом по дисциплинам кафедры - очное отделение
        /// </summary>
        private ICommand reportDisciplineChairFullTime;

        /// <summary>
        /// Открытие закладки работы с отчетом по дисциплинам кафедры - очное отделение
        /// </summary>
        public ICommand ReportDisciplineChairFullTime
        {
            get
            {
                if (reportDisciplineChairFullTime == null)
                    reportDisciplineChairFullTime = new RelayCommand(() => this.ShowReportDisciplineChairFullTime());
                return reportDisciplineChairFullTime;
            }
        }

        #endregion ReportDisciplineChairFullTimeCommand

        #region ReportDisciplineChairPartTimeCommand

        /// <summary>
        /// Поле - Открытие закладки работы с отчетом по дисциплинам кафедры - заочное отделение
        /// </summary>
        private ICommand reportDisciplineChairPartTime;

        /// <summary>
        /// Открытие закладки работы с отчетом по дисциплинам кафедры - заочное отделение
        /// </summary>
        public ICommand ReportDisciplineChairPartTime
        {
            get
            {
                if (reportDisciplineChairPartTime == null)
                    reportDisciplineChairPartTime = new RelayCommand(() => this.ShowReportDisciplineChairPartTime());
                return reportDisciplineChairPartTime;
            }
        }

        #endregion ReportDisciplineChairPartTimeCommand

        #region ReportLoadChairCommand

        /// <summary>
        /// Поле - Открытие закладки работы с отчетом по SpecialityViewModel кафедры
        /// </summary>
        private ICommand reportLoadChair;

        /// <summary>
        /// Открытие закладки работы с отчетом по SpecialityViewModel кафедры
        /// </summary>
        public ICommand ReportLoadChair
        {
            get
            {
                if (reportLoadChair == null)
                    reportLoadChair = new RelayCommand(() => this.ShowReportLoadChair());
                return reportLoadChair;
            }
        }

        #endregion ReportLoadChairCommand

        #region ReportLoadChairFullTimeCommand

        /// <summary>
        /// Поле - Открытие закладки работы с отчетом ReportLoadChairFullTime
        /// </summary>
        private ICommand reportLoadChairFullTime;

        /// <summary>
        /// Открытие закладки работы с отчетом ReportLoadChairFullTime
        /// </summary>
        public ICommand ReportLoadChairFullTime
        {
            get
            {
                if (reportLoadChairFullTime == null)
                    reportLoadChairFullTime = new RelayCommand(() => this.ShowReportLoadChairFullTime());
                return reportLoadChairFullTime;
            }
        }

        #endregion ReportLoadChairFullTimeCommand

        #region ReportLoadChairPartTimeCommand

        /// <summary>
        /// Поле - Открытие закладки работы с отчетом ReportLoadChairPartTime
        /// </summary>
        private ICommand reportLoadChairPartTime;

        /// <summary>
        /// Открытие закладки работы с отчетом ReportLoadChairPartTime
        /// </summary>
        public ICommand ReportLoadChairPartTime
        {
            get
            {
                if (reportLoadChairPartTime == null)
                    reportLoadChairPartTime = new RelayCommand(() => this.ShowReportLoadChairPartTime());
                return reportLoadChairPartTime;
            }
        }

        #endregion ReportLoadChairPartTimeCommand

        #region ReportLoadTeacherCommand

        /// <summary>
        /// Поле - Открытие закладки работы с отчетом по нагрузке преподавателя
        /// </summary>
        private ICommand reportLoadTeacher;

        /// <summary>
        /// Открытие закладки работы с отчетом по нагрузке преподавателя
        /// </summary>
        public ICommand ReportLoadTeacher
        {
            get
            {
                if (reportLoadTeacher == null)
                    reportLoadTeacher = new RelayCommand(() => this.ShowReportLoadTeacher());
                return reportLoadTeacher;
            }
        }

        #endregion ReportLoadTeacherCommand

        #region ReportSummaryCurriculumWork

        /// <summary>
        /// Поле - Открытие закладки работы с отчетом Сводные данные по трудоемкости
        /// </summary>
        private ICommand reportSummaryCurriculumWork;

        /// <summary>
        /// Открытие закладки работы с отчетом Сводные данные по трудоемкости
        /// </summary>
        public ICommand ReportSummaryCurriculumWork
        {
            get
            {
                if (reportSummaryCurriculumWork == null)
                    reportSummaryCurriculumWork = new RelayCommand(() => this.ShowReportSummaryCurriculumWork());
                return reportSummaryCurriculumWork;
            }
        }

        #endregion ReportLoadTeacherCommand

        #region ReportFixedDiscipline

        /// <summary>
        /// Поле - Открытие закладки работы с отчетом Закрепление дисциплин
        /// </summary>
        private ICommand reportFixedDiscipline;

        /// <summary>
        /// Открытие закладки работы с отчетом Закрепление дисциплин
        /// </summary>
        public ICommand ReportFixedDiscipline
        {
            get
            {
                if (reportFixedDiscipline == null)
                    reportFixedDiscipline = new RelayCommand(() => this.ShowReportFixedDiscipline());
                return reportFixedDiscipline;
            }
        }

        #endregion ReportFixedDiscipline

        #region ReportReportGroups

        /// <summary>
        /// Поле - Открытие закладки работы с отчетом Закрепление дисциплин
        /// </summary>
        private ICommand reportGroups;

        /// <summary>
        /// Открытие закладки работы с отчетом Закрепление дисциплин
        /// </summary>
        public ICommand ReportGroups
        {
            get
            {
                if (reportGroups == null)
                    reportGroups = new RelayCommand(() => this.ShowReportReportGroups());
                return reportGroups;
            }
        }

        #endregion ReportReportGroups

        #region OrderStandartTimeCommand

        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией Нормы времени
        /// </summary>
        private ICommand orderStandartTimeCommand;

        public ICommand OrderStandartTimeCommand
        {
            get
            {
                if (orderStandartTimeCommand == null)
                    orderStandartTimeCommand = new RelayCommand(() => this.ShowOrderStandartTime());
                return orderStandartTimeCommand;
            }
        }


        #endregion OrderStandartTimeCommand

        #region TitleCurriculumCommand

        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией Титул учебного плана
        /// </summary>
        private ICommand titleCurriculum;

        public ICommand TitleCurriculum
        {
            get
            {
                if (titleCurriculum == null)
                    titleCurriculum = new RelayCommand(() => this.ShowTitleCurriculum());
                return titleCurriculum;
            }
        }

        #endregion TitleCurriculum

        #region TitleCurriculumFromDBCommand

        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией Титул учебного плана из DB
        /// </summary>
        private ICommand titleCurriculumFromDB;

        public ICommand TitleCurriculumFromDB
        {
            get
            {
                if (titleCurriculumFromDB == null)
                    titleCurriculumFromDB = new RelayCommand(() => this.ShowTitleCurriculumFromDB());
                return titleCurriculumFromDB;
            }
        }

        #endregion TitleCurriculumFromDBCommand

        #region TitleCurriculumFromXmlCommand

        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией Титул учебного плана из XML
        /// </summary>
        private ICommand titleCurriculumFromXml;

        public ICommand TitleCurriculumFromXml
        {
            get
            {
                if (titleCurriculumFromXml == null)
                    titleCurriculumFromXml = new RelayCommand(() => this.ShowTitleCurriculumFromXml());
                return titleCurriculumFromXml;
            }
        }

        #endregion TitleCurriculumFromXmlCommand

        #region TitleCurriculumFromExlCommand

        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией Титул учебного плана из Excel
        /// </summary>
        private ICommand titleCurriculumFromExl;

        public ICommand TitleCurriculumFromExl
        {
            get
            {
                if (titleCurriculumFromExl == null)
                    titleCurriculumFromExl = new RelayCommand(() => this.ShowTitleCurriculumFromExl());
                return titleCurriculumFromExl;
            }
        }

        #endregion TitleCurriculumFromXmlCommand

        #region LoadChairCommand
        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией Нагрузка кафедры
        /// </summary>
        private ICommand loadChairCommand;

        public ICommand LoadChairCommand
        {
            get
            {
                if (loadChairCommand == null)
                    loadChairCommand = new RelayCommand(() => this.ShowLoadChair());
                return loadChairCommand;
            }
        }

        #endregion LoadChairCommand

        #region DisciplineChairCommand

        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией Дисциплины кафедры
        /// </summary>
        private ICommand disciplineChairCommand;

        public ICommand DisciplineChairCommand
        {
            get
            {
                if (disciplineChairCommand == null)
                    disciplineChairCommand = new RelayCommand(() => this.ShowDisciplineChair());
                return disciplineChairCommand;
            }
        }

        #endregion CurriculumDisciplineCommand

        #region FixedDisciplineCommand

        /// <summary>
        /// Открытие закладки работы с фукнцией Закрепление дисциплин - поле 
        /// </summary>
        private ICommand fixedDisciplineCommand;

        /// <summary>
        /// Открытие закладки работы с фукнцией Закрепление дисциплин - Свойсво
        /// </summary>
        public ICommand FixedDisciplineCommand
        {
            get
            {
                if (fixedDisciplineCommand == null)
                    fixedDisciplineCommand = new RelayCommand(() => this.ShowFixedDiscipline());
                return fixedDisciplineCommand;
            }
        }

        #endregion FixedDisciplineCommand

        #region SpecialityCommand

        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией Направления/Специальности 
        /// </summary>
        private ICommand specialityCommand;

        public ICommand SpecialityCommand
        {
            get
            {
                if (specialityCommand == null)
                    specialityCommand = new RelayCommand(() => this.ShowSpeciality());
                return specialityCommand;
            }
        }

        #endregion SpecialityCommand

        #region AcademicYearCommand

        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией Учебный год
        /// </summary>
        private ICommand academicYearCommand;

        public ICommand AcademicYearCommand
        {
            get
            {
                if (academicYearCommand == null)
                    academicYearCommand = new RelayCommand(() => this.ShowAcademicYear());
                return academicYearCommand;
            }
        }

        #endregion AcademicYearCommand

        #region EducationFormCommand

        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией Формы обучения
        /// </summary>
        private ICommand educationFormCommand;

        public ICommand EducationFormCommand
        {
            get
            {
                if (educationFormCommand == null)
                    educationFormCommand = new RelayCommand(() => this.ShowEducationForm());
                return educationFormCommand;
            }
        }

        #endregion EducationFormCommand

        #region PostCommand

        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией Формы должности
        /// </summary>
        private ICommand postCommand;

        public ICommand PostCommand
        {
            get
            {
                if (postCommand == null)
                    postCommand = new RelayCommand(() => this.ShowPost());
                return postCommand;
            }
        }

        #endregion PostCommand

        #region QualificationCommand

        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией Формы должности
        /// </summary>
        private ICommand qualificationCommand;

        public ICommand QualificationCommand
        {
            get
            {
                if (qualificationCommand == null)
                    qualificationCommand = new RelayCommand(() => this.ShowQualification());
                return qualificationCommand;
            }
        }

        #endregion QualificationCommand

        #region TypeEmploymentCommand

        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией Формы статус занятости преподавателя
        /// </summary>
        private ICommand typeEmploymentCommand;

        public ICommand TypeEmploymentCommand
        {
            get
            {
                if (typeEmploymentCommand == null)
                    typeEmploymentCommand = new RelayCommand(() => this.ShowTypeEmployment());
                return typeEmploymentCommand;
            }
        }

        #endregion TypeEmploymentCommand

        #region TypeTrainingCommand

        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией Формы вид учебной работы
        /// </summary>
        private ICommand typeTrainingCommand;

        public ICommand TypeTrainingCommand
        {
            get
            {
                if (typeTrainingCommand == null)
                    typeTrainingCommand = new RelayCommand(() => this.ShowTypeTraining());
                return typeTrainingCommand;
            }
        }

        #endregion TypeTrainingCommand

        #region UnitCommand

        /// <summary>
        /// Поле - Открытие закладки работы с фукнцией Формы Единиц измерения учебной работы/нагрузки
        /// </summary>
        private ICommand unitCommand;

        public ICommand UnitCommand
        {
            get
            {
                if (unitCommand == null)
                    unitCommand = new RelayCommand(() => this.ShowUnit());
                return unitCommand;
            }
        }

        #endregion UnitCommand

        #region CommandClose

        /// <summary>
        /// Поле - команда закрытия приложения
        /// </summary>
        private ICommand exitCommand;

        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                    exitCommand = new RelayCommand(() => this.ExitCommandExecute());
                return exitCommand;
            }
        }

        private void ExitCommandExecute()
        {
            App.Current.MainWindow.Close();
        }

        #endregion CommandLoadTeacherCommand

        #region ConnectionStringDB

        /// <summary>
        /// Поле - 
        /// </summary>
        private ICommand connectionStringDB;

        public ICommand ConnectionStringDB
        {
            get
            {
                return connectionStringDB ??
                    (connectionStringDB = new RelayCommand(ConnectionStringDBExecute));
            }
        }

        

        

        #endregion ConnectionStringDB

        #region AboutCommand

        /// <summary>
        /// Поле - Открытия окна "О программе" 
        /// </summary>
        private ICommand about;

        public ICommand About
        {
            get
            {
                if (about == null)
                    about = new RelayCommand(() => this.ShowAbout());
                return about;
            }
        }

        #endregion AboutCommand

        #endregion Command
    }
}
