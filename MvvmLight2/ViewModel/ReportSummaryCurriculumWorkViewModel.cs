using GalaSoft.MvvmLight.Command;
using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ServiceData;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace MvvmLight2.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// Отчет Сводные данные по трудоемкости
    /// </summary>
    public class ReportSummaryCurriculumWorkViewModel : WorkspaceViewModel
    {
        private MvvmLight2.Helper.IContainer container = MvvmLight2.Helper.Container.Instance;

        #region Field

        /// <summary>
        /// Количество экзаменов
        /// </summary>
        int countExamination;

        /// <summary>
        /// Количество зачетов
        /// </summary>
        int countSetOff;

        /// <summary>
        /// Количество зачетов с оценкой
        /// </summary>
        int countSetOffWithBall;

        /// <summary>
        /// Количество курсовых работ
        /// </summary>
        int countCourseWork;

        /// <summary>
        /// Количество курсовых проектов
        /// </summary>
        int countCourseProject;

        /// <summary>
        /// Количество контрольных работ
        /// </summary>
        int countControlWork;

        /// <summary>
        /// Наличие ГАК
        /// </summary>
        int countGak;

        /// <summary>
        /// Количество недель практики
        /// </summary>
        int countPractical;

        /// <summary>
        /// Наличие НИР
        /// </summary>
        int countScientificResearchWork;

        /// <summary>
        /// Общее количество часов лекция
        /// </summary>
        int totalNumberOfLectures;

        /// <summary>
        /// Общее количество часов лабораторных работ
        /// </summary>
        int totalNumberOfLaboratoryWork;

        /// <summary>
        /// Общее количество часов практических занятий
        /// </summary>
        int totalNumberOfPracticalExercises;

        #endregion Field

        #region  SelectedAcademicYear

        DictAcademicYear selectedAcademicYear;

        public DictAcademicYear SelectedAcademicYear
        {
            get { return selectedAcademicYear; }
            set
            {
                if (value == selectedAcademicYear)
                    return;

                selectedAcademicYear = value;
                RaisePropertyChanged("SelectedAcademicYear");
            }
        }

        #endregion  SelectedAcademicYear

        #region IsEnabledButton

        bool isEnabledButton = false;

        /// <summary>
        /// Признак доступности кнопки Формировать отчет
        /// </summary>
        public bool IsEnabledButton
        {
            get { return isEnabledButton; }
            set
            {
                if (value == isEnabledButton)
                    return;

                isEnabledButton = value;
                RaisePropertyChanged("IsEnabledButton");
            }
        }

        #endregion IsEnabledButton

        #region IsRadioButtonReportAllDisciplines

        bool isRadioButtonReportAllDisciplines;

        public bool IsRadioButtonReportAllDisciplines
        {
            get { return isRadioButtonReportAllDisciplines; }
            set 
            {
                if (value == isRadioButtonReportAllDisciplines)
                    return;
                isRadioButtonReportAllDisciplines = value;
                RaisePropertyChanged("IsRadioButtonReportAllDisciplines");
            }
        }

        #endregion IsRadioButtonReportAllDisciplines

        #region IsRadioButtonReportWithoutDisciplineOfChoice

        bool isRadioButtonReportWithoutDisciplineOfChoice;

        public bool IsRadioButtonReportWithoutDisciplineOfChoice
        {
            get { return isRadioButtonReportWithoutDisciplineOfChoice; }
            set
            {
                if (value == isRadioButtonReportWithoutDisciplineOfChoice)
                    return;
                isRadioButtonReportWithoutDisciplineOfChoice = value;
                RaisePropertyChanged("IsRadioButtonReportWithoutDisciplineOfChoice");
            }
        }

        #endregion IsRadioButtonReportWithoutDisciplineOfChoice

        #region Индикатор выполнения длительной операции

        bool isBusyCurriculum = false;


        public bool IsBusyCurriculum
        {
            get { return isBusyCurriculum; }
            set
            {
                if (value == IsBusyCurriculum)
                    return;

                isBusyCurriculum = value;
                RaisePropertyChanged("IsBusyCurriculum");
            }
        }

        #endregion Индикатор выполнения длительной операции

        #region Сообщение при выполнении длительной операции

        string busyMessage;

        public string BusyMessage
        {
            get { return busyMessage; }
            set
            {
                if (value == busyMessage)
                    return;

                busyMessage = value;
                RaisePropertyChanged("BusyMessage");
            }
        }

        #endregion Сообщение при выполнении длительной операции

        #region Collection

        /// <summary>
        /// Коллекция титулов учебных планов
        /// </summary>
        ObservableCollection<tlsp_getCurriculumForComplexReport_Result> listCurriculum;
        
        /// <summary>
        /// Колллекция обозначений учебных планов с контингентом обучающихся
        /// </summary>
        ObservableCollection<tlsp_getCurriculumWithStudent_Result> listCurruculumWithStudent;
        
        /// <summary>
        /// Коллекция дисциплин учебного плана
        /// </summary>
        ObservableCollection<DisciplinePlan> disciplines;
        /// <summary>
        /// Коллекция трудоемкости учебных планов
        /// </summary>
        ObservableCollection<SummaryOfCurriculumWork> summaryCurriculumWorks;

        #endregion Collection

        #region Properties for binding
        /// <summary>
        /// Коллекция данных для отчета по трудоемкости
        /// </summary>
        public ObservableCollection<SummaryOfCurriculumWork> SummaryCurriculumWorks { get; private set; }

        /// <summary>
        /// Источник данных для отчета по трудоемкости
        /// </summary>
        public ListSummaryCurriculumWork SummaryListCurriculumWorks { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the DisciplineOfChairViewModel class.
        /// </summary>
        
        public ReportSummaryCurriculumWorkViewModel()
        {
            SummaryListCurriculumWorks = new ListSummaryCurriculumWork();
            base.DisplayName = "Отчет по трудоемкости";
        }

        #endregion Constructor

        #region Method

        /// <summary>
        /// Формирование данных по трудоемкости из базы данных
        /// </summary>
        /// <param name="service"></param>
        public void GetSummaryCurriculumWork(IServiceCurriculum service, int idAcademicYear)
        {            
            ObservableCollection<tlsp_getCurriculumWithStudent_Result> curruculumWithStudent = 
                new ObservableCollection<tlsp_getCurriculumWithStudent_Result>();

            summaryCurriculumWorks = new ObservableCollection<SummaryOfCurriculumWork>();
            SummaryCurriculumWorks = new ListSummaryCurriculumWork();

            disciplines = new ObservableCollection<DisciplinePlan>();
            listCurriculum = new ObservableCollection<tlsp_getCurriculumForComplexReport_Result>();
            listCurruculumWithStudent = new ObservableCollection<tlsp_getCurriculumWithStudent_Result>();

            
            BackgroundWorker worker = new BackgroundWorker();
            // Запуск длительной операции добавления данных о новом учебном плане в базу данных
            worker.DoWork += (o, ea) =>
            {

                // Формирование титульных данных учебного плана
                listCurriculum = service.GetCurriculumForComplexReport(listCurriculum, idAcademicYear);

                // Формирование контингента  для учебного плана
                var query = service.GetCurriculumWithStudent(curruculumWithStudent, idAcademicYear);
                if (query != null)
                    foreach (var cur in query)
                        listCurruculumWithStudent.Add(cur);

                // Формирование трудоемкости для учебных планов
                if (listCurriculum != null)
                {
                    foreach (var cur in listCurriculum)
                    {
                        if ((bool)cur.Course1)
                            SetReportSoure(service, cur, 1);
                        if ((bool)cur.Course2)
                            SetReportSoure(service, cur, 2);
                        if ((bool)cur.Course3)
                            SetReportSoure(service, cur, 3);
                        if ((bool)cur.Course4)
                            SetReportSoure(service, cur, 4);
                        if ((bool)cur.Course5)
                            SetReportSoure(service, cur, 5);
                        if ((bool)cur.Course6)
                            SetReportSoure(service, cur, 6);
                    }
                }
            };

             worker.RunWorkerCompleted += (o, ea) =>
                    {
                        // Формирование источника данных для отчета по трудоемкости
                        SummaryListCurriculumWorks.Clear();
                        SummaryListCurriculumWorks.AddWork(SummaryCurriculumWorks);
                        IsBusyCurriculum = false;
                        IsEnabledButton = true;
                    };

             // установка IsBusy перед началом ассинхронного потока визуализации выполнения длительной операции
             IsBusyCurriculum = true;
             IsEnabledButton = false;
             BusyMessage = "Формирование данных . . .";
             worker.RunWorkerAsync();
        }

        /// <summary>
        /// Формирование сводных данных по трудоемкости 
        /// </summary>
        /// <param name="service"></param>
        /// <param name="cur"></param>
        /// <param name="course"></param>
        private void SetReportSoure(IServiceCurriculum service, 
                                    tlsp_getCurriculumForComplexReport_Result cur,
                                    int course)
        {
            SummaryOfCurriculumWork curriculunReport = new SummaryOfCurriculumWork();
            
            curriculunReport.IdCurriculum = cur.Id;
            curriculunReport.CurriculumName = cur.Name;
            curriculunReport.CodeSpeciality = cur.CodeSpeciality;
            curriculunReport.NameSpeciality = cur.NameSpeciality;
            curriculunReport.Profile = cur.Profile;
            curriculunReport.Faculty = cur.ShortName;
            curriculunReport.FormEducation = cur.FormEducation;
            curriculunReport.NameQualification = cur.NameQualification;
            curriculunReport.Course = course;

            // Формирование контингента для учебного плана
            var studentInCurriculum = listCurruculumWithStudent.Where(n => n.CurriculumName == cur.Name).FirstOrDefault();
            if (studentInCurriculum != null)
                curriculunReport.CountStudent = (int)studentInCurriculum.Student;

            // Формирование списка дисциплин учебного плана
            disciplines.Clear();
            disciplines = service.GetDiscipline(disciplines, cur.Id);

            ClearLoad();

            if(isRadioButtonReportAllDisciplines)
            {
                // Формирование трудоемкости по нагрузке учебного плана
                foreach (var disc in disciplines)
                {
                    GetDisciplineForSummaryWorksReport(disc);
                }
            }
            else
            {
                // Формирование трудоемкости по нагрузке учебного плана
                foreach (var disc in disciplines)
                {
                    // Проверка удаляет из списка дисциплин плана 2-ю по выбору дисциплину 
                    if (!((disc.CodePlan != null) && disc.CodePlan.Contains(".ДВ.") && disc.CodePlan.EndsWith(".2")))
                    {
                        GetDisciplineForSummaryWorksReport(disc);
                    }
                }
            }

            curriculunReport.CountExamination = countExamination;
            curriculunReport.CountSetOff = countSetOff;
            curriculunReport.CountSetOffWithBall = countSetOffWithBall;
            curriculunReport.CountCourseWork = countCourseWork;
            curriculunReport.CountCourseProject = countCourseProject;
            curriculunReport.CountControlWork = countControlWork;
            curriculunReport.CountGak = countGak;
            curriculunReport.CountPractical = countPractical;
            curriculunReport.CountScientificResearchWork = countScientificResearchWork;
            curriculunReport.TotalNumberOfLectures = totalNumberOfLectures;
            curriculunReport.TotalNumberOfLaboratoryWork = totalNumberOfLaboratoryWork;
            curriculunReport.TotalNumberOfPracticalExercises = totalNumberOfPracticalExercises;

            SummaryCurriculumWorks.Add(curriculunReport);
        }

        /// <summary>
        /// Формирование данных по трудоемкости для дисциплин плана
        /// </summary>
        /// <param name="disc"></param>
        private void GetDisciplineForSummaryWorksReport(DisciplinePlan disc)
        {
            if (disc.ExaminationPlan != null && disc.ExaminationPlan > 0)
                countExamination += 1;
            if (disc.SetOffPlan != null && disc.SetOffPlan > 0)
                countSetOff += 1;
            if (disc.SetOffWithBallPlan != null && disc.SetOffWithBallPlan > 0)
                countSetOffWithBall += 1;
            if (disc.ControlWorkPlan != null)
                countControlWork += (int)disc.ControlWorkPlan;
            if (disc.CourseProjectPlan != null)
                countCourseProject += (int)disc.CourseProjectPlan;
            if (disc.CourseWorktPlan != null)
                countCourseWork += (int)disc.CourseWorktPlan;
            if (disc.GakPlan != null && disc.GakPlan > 0)
                countGak = 1;
            if (disc.PracticalPlan != null && disc.PracticalPlan > 0)
                countPractical += (int)disc.PracticalPlan;
            if (disc.ScientificResearchWorkPlan != null && disc.ScientificResearchWorkPlan > 0)
                countScientificResearchWork += 1;
            if (disc.LecturePlan != null && disc.LecturePlan > 0)
                totalNumberOfLectures += (int)disc.LecturePlan;
            if (disc.LaboratoryWorkPlan != null && disc.LaboratoryWorkPlan > 0)
                totalNumberOfLaboratoryWork += (int)disc.LaboratoryWorkPlan;
            if (disc.PracticalExercisesPlan != null && disc.PracticalExercisesPlan > 0)
                totalNumberOfPracticalExercises += (int)disc.PracticalExercisesPlan;
        }

        /// <summary>
        /// Обнуление данных перед суммированием даных по трудоемкости
        /// </summary>
        private void ClearLoad()
        {
            countExamination = 0;
            countSetOff = 0;
            countSetOffWithBall = 0;
            countCourseWork = 0;
            countCourseProject = 0;
            countControlWork = 0;
            countGak = 0;
            countPractical = 0;
            countScientificResearchWork = 0;
            totalNumberOfLectures = 0;
            totalNumberOfLaboratoryWork = 0;
            totalNumberOfPracticalExercises = 0;
        }

        #endregion Method

        #region CommandGetSummuryWorks

        /// <summary>
        /// Команда - Формироание данных отчета по трудоемкости - поле
        /// </summary>
        private ICommand getSummuryWorks;

        /// <summary>
        /// Команда - Формироание данных отчета по трудоемкости - Свойство
        /// </summary>
        public ICommand GetSummuryWorks
        {
            get
            {
                return getSummuryWorks ??
                    (getSummuryWorks = new RelayCommand(GetExecuteSummuryWorks));
            }

        }

        /// <summary>
        /// Формироание данных отчета по трудоемкости- метод
        /// </summary>
        private void GetExecuteSummuryWorks()
        {
            var service = container.Resolve<IServiceCurriculum>();

            int idAcademicYear = 0;
            if (selectedAcademicYear != null)
                idAcademicYear = selectedAcademicYear.Id;
            GetSummaryCurriculumWork(service, idAcademicYear);
        }


        #endregion CommandGetSummuryWorks

    }
    
}
