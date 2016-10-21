using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Linq;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ServiceData;
using MvvmLight2.View;
using System.Collections.Generic;


namespace MvvmLight2.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// Нагрузка кафедры
    /// </summary>
    public class LoadChairViewModel : WorkspaceViewModel
    {
        #region Поля

        private MvvmLight2.Helper.IContainer container = MvvmLight2.Helper.Container.Instance;

        private int streamLecture; // Поток лекций
        private int streamLab;     // Поток лабораторных работ
        private int streamPract;   // Поток практических занятий

        /// <summary>
        /// Коллекция нагрузки по дисциплинам для сохранения в базе данных
        /// </summary>
        ObservableCollection<TeachingLoad> disciplineLoadForSave;

        #endregion Поля

        #region Constants
        /// <summary>
        /// весенний семестр
        /// </summary>
        const string spring = "весна";

        /// <summary>
        /// осенний семестр
        /// </summary>
        const string autumn = "осень";

        #endregion Constants

        #region PlanCurriculum

        /// <summary>
        /// Учебный план направления подготовки - поле
        /// </summary>
        string planCurriculum = string.Empty;

        /// <summary>
        /// Учебный план направления подготовки - свойство
        /// </summary>
        public string PlanCurriculum
        {
            get { return planCurriculum; }
            set
            {
                if (value == planCurriculum)
                    return;
                else
                {
                    planCurriculum = value;
                    RaisePropertyChanged("PlanCurriculum");
                }
            }
        }

        #endregion PlanCurriculum

        #region NameDiscipline

        /// <summary>
        /// Наименование дисциплины - поле
        /// </summary>
        string nameDiscipline = string.Empty;

        /// <summary>
        /// Наименование дисциплины - свойство
        /// </summary>
        public string NameDiscipline
        {
            get { return nameDiscipline; }
            set
            {
                if (value == nameDiscipline)
                    return;
                else
                {
                    nameDiscipline = value;
                    RaisePropertyChanged("NameDiscipline");
                }
            }
        }

        #endregion NameDiscipline

        #region SelectedFaculty

        /// <summary>
        /// Выделенный в списке факультет - поле
        /// </summary>
        Faculty selectedFaculty;

        /// <summary>
        /// Выделенный в списке факультет - Свойство
        /// </summary>
        public Faculty SelectedFaculty
        {
            get { return selectedFaculty; }
            set
            {
                if (value == selectedFaculty)
                    return;

                selectedFaculty = value;
                RaisePropertyChanged("SelectedFaculty");
            }
        }

        #endregion SelectedFaculty

        #region SelectedChair

        Chair selectedChair;

        public Chair SelectedChair
        {
            get { return selectedChair; }
            set
            {
                if (value == selectedChair)
                    return;

                selectedChair = value;
                RaisePropertyChanged("SelectedChair");
            }
        }

        #endregion SelectedChair

        #region SelectedAcademicYear

        /// <summary>
        /// Поле - Выделенный в списке академический год
        /// </summary>
        private DictAcademicYear selectedAcademicYear;

        /// <summary>
        /// Свойство - Выделенный в списке академический год
        /// </summary>
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

        #endregion SelectedAcademicYear

        #region SelectedDisciplineLoad

        /// <summary>
        /// Выделенная дисциплина нагрузки по кафедре
        /// </summary>
        TeachingLoad selectedDisciplineLoad;

        /// <summary>
        /// Выделенная дисциплина нагрузки по кафедре
        /// </summary>
        public TeachingLoad SelectedDisciplineLoad
        {
            get { return selectedDisciplineLoad; }
            set 
            {
                if (value == selectedDisciplineLoad)
                    return;
                selectedDisciplineLoad = value;
                RaisePropertyChanged("SelectedDisciplineLoad");
            }
        }

        #endregion SelectedDisciplineLoad

        #region SelectedNameLoadChair

        /// <summary>
        /// Выбранные вариант нагрузки кафедры
        /// </summary>
        TeachingLoadChair selectedNameLoadChair;

        /// <summary>
        /// Выбранные вариант нагрузки кафедры
        /// </summary>
        public TeachingLoadChair SelectedNameLoadChair
        {
            get { return selectedNameLoadChair; }
            set
            {
                if (value == selectedNameLoadChair)
                    return;
                selectedNameLoadChair = value;
                RaisePropertyChanged("SelectedNameLoadChair");

            }
        }

        #endregion SelectedNameLoadChair

        #region SelectedEducationForm

        /// <summary>
        /// Выделенная в списке форма обучения - поле
        /// </summary>
        DictEducationForm selectedEducationForm;
        /// <summary>
        /// Выделенная в списке форма обучения - свойство
        /// </summary>
        public DictEducationForm SelectedEducationForm
        {
            get { return selectedEducationForm; }
            set
            {
                if (value == selectedEducationForm)
                    return;
                else
                {
                    selectedEducationForm = value;
                    RaisePropertyChanged("SelectedEducationForm");
                }
            }
        }

        #endregion SelectedEducationForm

        #region SelectedIndexDiscipline

        int selectedIndexDiscipline = 0;

        public int SelectedIndexDiscipline
        {
            get { return selectedIndexDiscipline; }
            set 
            {
                if (value == selectedIndexDiscipline)
                    return;
                selectedIndexDiscipline = value;
                RaisePropertyChanged("SelectedIndexDiscipline");
            }
        }

        #endregion SelectedIndexDiscipline

        #region SelectedIndexNameLoadChair

        /// <summary>
        /// Выбранный индекс в списке вариантов нагрузки
        /// </summary>
        int selectedIndexNameLoadChair;

        /// <summary>
        /// Выбранный индекс в списке вариантов нагрузки
        /// </summary>
        public int SelectedIndexNameLoadChair
        {
            get { return selectedIndexNameLoadChair; }
            set 
            {
                if (value == selectedIndexNameLoadChair)
                    return;
                selectedIndexNameLoadChair = value;
                RaisePropertyChanged("SelectedIndexNameLoadChair");
            }
        }

        #endregion SelectedIndexNameLoadChair

        #region SelectedIndexEducationForm

        /// <summary>
        /// Выделенный индекс в списке форм обучения - поле
        /// </summary>
        int selectedIndexEducationForm = 0;
        /// <summary>
        /// Выделенный индекс в списке форм обучения  - свойство
        /// </summary>
        public int SelectedIndexEducationForm
        {
            get { return selectedIndexEducationForm; }
            set
            {
                if (value == selectedIndexEducationForm)
                    return;
                else
                {
                    selectedIndexEducationForm = value;
                    RaisePropertyChanged("SelectedIndexEducationForm");
                }
            }
        }

        #endregion SelectedQualification

        #region SelectedIndexFaculty

        /// <summary>
        /// Выделенный индекс в списке факультетов - поле
        /// </summary>
        int selectedIndexFaculty;
        /// <summary>
        /// Выделенный индекс в списке факультетов  - свойство
        /// </summary>
        public int SelectedIndexFaculty
        {
            get { return selectedIndexFaculty; }
            set
            {
                if (value == selectedIndexFaculty)
                    return;
                else
                {
                    selectedIndexFaculty = value;
                    RaisePropertyChanged("SelectedIndexFaculty");
                }
            }
        }

        #endregion SelectedIndexFaculty

        #region SelectedIndexChair

        /// <summary>
        /// Выделенный индекс в списке кафедр - поле
        /// </summary>
        int selectedIndexChair;
        /// <summary>
        /// Выделенный индекс в списке кафедр  - свойство
        /// </summary>
        public int SelectedIndexChair
        {
            get { return selectedIndexChair; }
            set
            {
                if (value == selectedIndexChair)
                    return;
                else
                {
                    selectedIndexChair = value;
                    RaisePropertyChanged("SelectedIndexChair");
                }
            }
        }

        #endregion SelectedIndexChair

        #region SelectedIndexAcademicYear

        /// <summary>
        /// Выделенный индекс в списке учебных годов - поле
        /// </summary>
        int selectedAcademicYearIndex;
        /// <summary>
        /// Выделенный индекс в списке учебных годов  - свойство
        /// </summary>
        public int SelectedIndexAcademicYear
        {
            get { return selectedAcademicYearIndex; }
            set
            {
                if (value == selectedAcademicYearIndex)
                    return;
                else
                {
                    selectedAcademicYearIndex = value;
                    RaisePropertyChanged("SelectedIndexAcademicYear");
                }
            }
        }

        #endregion SelectedAcademicYearIndex

        #region MessageSave

        /// <summary>
        /// Строка предупреждения о необходимости сохранения данных
        /// </summary>
        //string messageSave = string.Empty;

        /// <summary>
        /// Строка предупреждения о необходимости сохранения данных
        /// </summary>
        //public string MessageSave
        //{
        //    get { return messageSave; }
        //    set
        //    {
        //        if (value == messageSave)
        //            return;
        //        messageSave = value;
        //        RaisePropertyChanged("MessageSave");
        //    }
        //}


        #endregion MessageSave

        #region Индикатор выполнения длительной операции

        bool isBusy = false;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (value == isBusy)
                    return;

                isBusy = value;
                RaisePropertyChanged("IsBusy");
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

        #region Properties for binding

        /// <summary>
        /// Колллекция обобщенных данных по нагрузке кафедры для заданного учебного года
        /// </summary>
        public ObservableCollection<TeachingLoadChair> AllLoadChairAcademicYear { get; private set; }

        /// <summary>
        /// Обобщенные данные по нагрузке кафедры
        /// </summary>
        public TeachingLoadChair LoadChair { get; private set; }

        /// <summary>
        /// Данные по кафедрам
        /// </summary>
        public ObservableCollection<Chair> Chairs { get; private set; }

        /// <summary>
        /// Коллекция нагрузки по дисциплинам кафедры
        /// </summary>
        public ObservableCollection<TeachingLoad> LoadChairTeaching { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the FacultiesViewModel class.
        /// </summary>
        public LoadChairViewModel(IServiceLoadChair service)
        {
            int idChair = 0;
            if (SelectedChair != null)
                idChair = SelectedChair.Id;

            int idAcademicYear = 0;
            if (SelectedAcademicYear != null)
                idAcademicYear = selectedAcademicYear.Id;

            Chairs = new ObservableCollection<Chair>();
            LoadChairTeaching = new ObservableCollection<TeachingLoad>();
            LoadChair = new TeachingLoadChair();
            disciplineLoadForSave = new ObservableCollection<TeachingLoad>();

            service.GetDataAllLoadChair(
                (loadChair, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }
                    AllLoadChairAcademicYear = loadChair;
                }, idChair, idAcademicYear);

            base.DisplayName = "Нагрузка кафедры";

            selectedIndexDiscipline = -1;

            // Регистрация сообщений об изменении суммарной нагрузки 
            Messenger.Default.Register<TeachingLoad>(this, TotalSumLoadChair);
        }

        #endregion Constructor

        #region Вспомогательные методы

        /// <summary>
        /// Перевычисление суммарной нагрузки кафедры и Формирование списка редактируемых дисциплин кафедры
        /// </summary>
        /// <param name="teach"></param>
        private void TotalSumLoadChair(TeachingLoad load)
        {
            decimal? sum = LoadChair.SumLoadChair;

            // Перевычисление суммарной нагрузки кафедры
            SetSumLoadChair();

            // Формирование списка редактируемых дисциплин кафедры
            if (SelectedDisciplineLoad != null)
            { 
                if (load.Id == SelectedDisciplineLoad.Id)
                {
                    if ((sum != LoadChair.SumLoadChair) || ((bool)load.FlagChange))
                    {
                        SelectedDisciplineLoad.SumLoad = load.SumLoad;

                        if(! disciplineLoadForSave.Contains(load))
                            disciplineLoadForSave.Add(load);
                    }
                    //MessageSave = "Данные изменены. Сохраните изменения в базе данных !";
                    //SelectedIndexDiscipline += 1;
                }
            }
        }

        /// <summary>
        /// Вычисление суммарной нагрузки кафедры 
        /// </summary>
        /// <returns></returns>
        private void SetSumLoadChair() 
        {
            decimal sumLoad = 0;
            decimal sumLoadCommerce = 0;

            if (LoadChairTeaching != null)
                if (LoadChairTeaching.Count > 0)
                {
                    foreach (TeachingLoad tl in LoadChairTeaching)
                    {
                        if (tl.SumLoad != null)
                            sumLoad += (decimal)tl.SumLoad;
                        if (tl.SumCommerce != null)
                            sumLoadCommerce += (decimal)tl.SumCommerce;
                    }
                    LoadChair.SumLoadChair = sumLoad;
                    LoadChair.SumUnloadChair = sumLoad;
                    LoadChair.SumLoadChairCommerce = sumLoadCommerce;
                }
        }

        /// <summary>
        /// Вычисление суммарной нагрузки по дисциплине
        /// </summary>
        /// <param name="teach"></param>
        private TeachingLoad SetTotalDisciplineLoad(TeachingLoad load)
        {
            load.SumLoad = 0;
            if (load.Lecture != null)
                load.SumLoad += load.Lecture;
            if (load.Consultation != null)
                load.SumLoad += load.Consultation;
            if (load.ControlWork != null)
                load.SumLoad += load.ControlWork;
            if (load.CourseProject != null)
                load.SumLoad += load.CourseProject;
            if (load.CourseWorkt != null)
                load.SumLoad += load.CourseWorkt;
            if (load.LaboratoryWork != null)
                load.SumLoad += load.LaboratoryWork;
            if (load.PracticalExercises != null)
                load.SumLoad += load.PracticalExercises;
            if (load.Examination != null)
                load.SumLoad += load.Examination;
            if (load.SetOff != null)
                load.SumLoad += load.SetOff;
            if (load.SetOffWithBall != null)
                load.SumLoad += load.SetOffWithBall;
            if (load.Gac != null)
                load.SumLoad += load.Gac;
            if (load.GraduationDesign != null)
                load.SumLoad += load.GraduationDesign;
            if (load.Practical != null)
                load.SumLoad += load.Practical;
            if (load.Dot != null)
                load.SumLoad += load.Dot;
            if (load.Others != null)
                load.SumLoad += load.Others;
            
            load.SumUnload = load.SumLoad;
            load.SumCommerce = load.SumLoad * (decimal)load.CommerceStudent / (decimal)load.Student;

            return load;
        }


        /// <summary>
        /// Глубокое клонирование экземпляра класса TeachingLoad
        /// </summary>
        /// <param name="teach"></param>
        /// <returns></returns>
        private TeachingLoad CloneLoad(TeachingLoad load)
        {
            TeachingLoad cloneLoad = new TeachingLoad();

            cloneLoad.Id = load.Id;
            cloneLoad.IdTeachingLoadChair = load.IdTeachingLoadChair;
            cloneLoad.NameGroup = load.NameGroup;
            cloneLoad.Subgroup = load.Subgroup;
            cloneLoad.Student = load.Student;
            cloneLoad.ForeignStudent = load.ForeignStudent;
            cloneLoad.CommerceStudent = load.CommerceStudent;
            cloneLoad.Speciality = load.Speciality;
            cloneLoad.Profile = load.Profile;
            cloneLoad.Qualification = load.Qualification;
            cloneLoad.Discipline = load.Discipline;
            cloneLoad.FormEducation = load.FormEducation;
            cloneLoad.ShortNameFaculty = load.ShortNameFaculty;
            cloneLoad.NameCurricilum = load.NameCurricilum;
            cloneLoad.TypeDiscipline = load.TypeDiscipline;
            cloneLoad.Code = load.Code;
            cloneLoad.Semester = load.Semester;
            cloneLoad.SemesterYear = load.SemesterYear;
            cloneLoad.Course = load.Course;
            cloneLoad.Lecture = load.Lecture;
            cloneLoad.PracticalExercises = load.PracticalExercises;
            cloneLoad.LaboratoryWork = load.LaboratoryWork;
            cloneLoad.Examination = load.Examination;
            cloneLoad.Consultation = load.Consultation;
            cloneLoad.SetOff = load.SetOff;
            cloneLoad.SetOffWithBall = load.SetOffWithBall;
            cloneLoad.CourseProject = load.CourseProject;
            cloneLoad.CourseWorkt = load.CourseWorkt;
            cloneLoad.Practical = load.Practical;
            cloneLoad.GraduationDesign = load.GraduationDesign;
            cloneLoad.ControlWork = load.ControlWork;
            cloneLoad.ScientificResearchWork= load.ScientificResearchWork;
            cloneLoad.Gac = load.Gac;
            cloneLoad.Dot = load.Dot;
            cloneLoad.Others = load.Others;
            cloneLoad.SumLoad = load.SumLoad;
            cloneLoad.SumCommerce = load.SumCommerce;
            cloneLoad.SumUnload = load.SumUnload;
            cloneLoad.Stream = load.Stream;
            cloneLoad.StreamLab = load.StreamLab;
            cloneLoad.StreamPract = load.StreamPract;
            cloneLoad.FlagChange = load.FlagChange;
            cloneLoad.IsLoad = load.IsLoad;
            cloneLoad.IsFaultLoad = load.IsFaultLoad;
            cloneLoad.Note = load.Note;

            return cloneLoad;
        }

        /// <summary>
        /// Поиск нормы времени по idTypeTraining - индексу в справочнике видов нагрузки 
        /// </summary>
        /// <param name="norms"></param>
        /// <param name="idTypeTraining"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private decimal? FindNormTime(
            ObservableCollection<tlsp_getStandartTime_Result> norms,
            int idTypeTraining,
            Func< tlsp_getStandartTime_Result, int, bool> predicate)
        {
            decimal? normTime = 0; 
            foreach(var norm in norms) 
            {
                if (predicate(norm, idTypeTraining))
                {
                    normTime = norm.NormTime;
                    break;
                }
            }

            return normTime;
        }

        /// <summary>
        /// Инициализация класса TeachingLoad
        /// </summary>
        /// <param name="newLoad"></param>
        /// <returns></returns>
        private TeachingLoad NewTeachingLoad(TeachingLoad newLoad)
        {
            newLoad.IdTeachingLoadChair = LoadChair.Id;
            newLoad.NameCurricilum = String.Empty;
            newLoad.NameGroup = String.Empty;
            newLoad.Subgroup = 0;
            newLoad.Student = 0;
            newLoad.ForeignStudent = 0;
            newLoad.CommerceStudent = 0;
            newLoad.Speciality = String.Empty;
            newLoad.Profile = String.Empty;
            newLoad.Qualification = String.Empty;
            newLoad.Discipline = String.Empty;
            newLoad.FormEducation = String.Empty;
            newLoad.ShortNameFaculty = String.Empty;
            newLoad.TypeDiscipline = true;
            newLoad.Code = String.Empty;
            newLoad.Semester = 0;
            newLoad.SemesterYear = String.Empty;
            newLoad.Course = 0;
            newLoad.Lecture = 0;
            newLoad.PracticalExercises = 0;
            newLoad.LaboratoryWork = 0;
            newLoad.Examination = 0;
            newLoad.Consultation = 0;
            newLoad.SetOff = 0;
            newLoad.SetOffWithBall = 0;
            newLoad.CourseProject = 0;
            newLoad.CourseWorkt = 0;
            newLoad.Practical = 0;
            newLoad.GraduationDesign = 0;
            newLoad.ControlWork = 0;
            newLoad.Gac = 0;
            newLoad.ScientificResearchWork = 0;
            newLoad.Dot = 0;
            newLoad.Others = 0;
            newLoad.SumLoad = 0;
            newLoad.SumCommerce = 0;
            newLoad.SumUnload = 0;
            newLoad.Stream = 0;
            newLoad.StreamLab = 0;
            newLoad.StreamPract = 0;
            newLoad.IsFaultLoad = false;
            newLoad.IsLoad = false;
            newLoad.FlagChange = false;

            return newLoad;
        }

        /// <summary>
        /// Формирование нагрузки кафедры
        /// </summary>
        /// <param name="service"></param>
        private void ShapingLoadChair(IServiceLoadChair loadService)
        {
            // Загрузка дисциплин кафедры
            ObservableCollection<tlsp_setDisciplineChairWithGroup_Result  > disciplinesChair = loadService.GetDisciplinesChair(LoadChair.IdChair, LoadChair.IdAcademicYear);
            if (disciplinesChair.Count == 0)
                MessageBox.Show("Дисциплины кафедры не сформированы.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                // Загрузка норм времени
                ObservableCollection<tlsp_getStandartTime_Result> standartTime = loadService.GetStandartTime(LoadChair.IdAcademicYear, true);
                if (standartTime.Count == 0)
                    MessageBox.Show("Нормы времени для заданного учебного года не сформированы.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {

                    BackgroundWorker worker = new BackgroundWorker();
                    // Запуск длительной операции добавления данных о новом учебном плане в базу данных
                    worker.DoWork += (o, ea) =>
                    {

                        TeachingLoad loadDiscipline;
                        decimal? normTime = 0;
                        int idTypeWork;

                            foreach (tlsp_setDisciplineChairWithGroup_Result disc in disciplinesChair)
                            {
                                #region Вычисление нагрузки по дисциплине

                                decimal? sumLoad = 0;

                                // Дисциплина нагрузки кафедры
                                loadDiscipline = new TeachingLoad();
                                loadDiscipline = NewTeachingLoad(loadDiscipline);

                                // Имя файла учебного плана
                                loadDiscipline.NameCurricilum = disc.Name;

                                // Номер группы
                                loadDiscipline.NameGroup = disc.NameGroup;

                                // Количество подгрупп
                                if (disc.CountSubgroup != null)
                                    loadDiscipline.Subgroup = disc.CountSubgroup;
                                else
                                    loadDiscipline.Subgroup = 0;

                                // Количество студентов
                                if (disc.CountStudent != null)
                                    loadDiscipline.Student = disc.CountStudent;
                                else
                                    loadDiscipline.Student = 0;

                                // Количество студентов иностранцев
                                if (disc.CountForeignStudent != null)
                                    loadDiscipline.ForeignStudent = disc.CountForeignStudent;
                                else
                                    loadDiscipline.ForeignStudent = 0;

                                // Количество студентов на коммерческой основе
                                if (disc.CountComStudent != null)
                                    loadDiscipline.CommerceStudent = disc.CountComStudent;
                                else
                                    loadDiscipline.CommerceStudent = 0;

                                loadDiscipline.Speciality = disc.CodeSpeciality;         // направление подготовки/специальность
                                loadDiscipline.Profile = disc.Profile;                   // профиль
                                loadDiscipline.Qualification = disc.NameQualification;   // квалификация
                                loadDiscipline.Discipline = disc.Discipline;             // дисциплина
                                loadDiscipline.FormEducation = disc.FormEducation;       // форма обучения
                                loadDiscipline.ShortNameFaculty = disc.ShortName;        // краткое наименование факультета
                                loadDiscipline.TypeDiscipline = true;                    // тип дисциплины: из учебного плана/внеучебдая
                                loadDiscipline.Code = disc.CodePlan;                     // код дисциплины по плану

                                if (disc.FormEducation == "очная")
                                    loadDiscipline.Semester = disc.Semester;                 // семестр
                                else
                                    loadDiscipline.Semester = 0;

                                // семестр учебного года: осень/весна
                                loadDiscipline.SemesterYear = autumn;

                                if ((loadDiscipline.Semester != null) && (loadDiscipline.Semester != 0))
                                {
                                    if (loadDiscipline.Semester % 2 == 0)
                                        loadDiscipline.SemesterYear = spring;
                                    else
                                        loadDiscipline.SemesterYear = autumn;
                                }
                                else
                                    loadDiscipline.SemesterYear = autumn;

                                loadDiscipline.Course = disc.Course;                    // курс
                                loadDiscipline.IsLoad = false;                          // признак распределения нагрузки преподавателю
                                loadDiscipline.IsFaultLoad = false;                     // признак наличия ошибки при распределения нагрузки преподавателю



                                // Лекции
                                if ((disc.LecturePlan != 0) && (disc.LecturePlan != null))
                                {
                                    idTypeWork = 5; // Чтение лекций
                                    normTime = FindNormTime(standartTime, idTypeWork, (x, y) => x.IdTypeTraining.Equals(y));
                                    loadDiscipline.Lecture = disc.LecturePlan * normTime;

                                    sumLoad += loadDiscipline.Lecture;
                                }
                                else
                                    loadDiscipline.Lecture = 0;

                                // Лабораторные занятия
                                if ((disc.LaboratoryWorkPlan != 0) && (disc.LaboratoryWorkPlan != null))
                                {
                                    idTypeWork = 6; // Лаботаторные работы
                                    normTime = FindNormTime(standartTime, idTypeWork, (x, y) => x.IdTypeTraining.Equals(y));
                                    loadDiscipline.LaboratoryWork = disc.LaboratoryWorkPlan * normTime * disc.CountSubgroup;

                                    sumLoad += loadDiscipline.LaboratoryWork;
                                }
                                else
                                    loadDiscipline.LaboratoryWork = 0;

                                // Практические занятия
                                if ((disc.PracticalExercisesPlan != 0) && (disc.PracticalExercisesPlan != null))
                                {
                                    idTypeWork = 7; // Практические занятия
                                    normTime = FindNormTime(standartTime, idTypeWork, (x, y) => x.IdTypeTraining.Equals(y));
                                    loadDiscipline.PracticalExercises = disc.PracticalExercisesPlan * normTime;

                                    sumLoad += loadDiscipline.PracticalExercises;
                                }
                                else
                                    loadDiscipline.PracticalExercises = 0;

                                // Текущие консультации 
                                loadDiscipline.Consultation = 0;
                                if ((disc.LecturePlan != 0) && (disc.LecturePlan != null) && (disc.Discipline != "Член ГАК1"))
                                {

                                    // Текущие консультации для дневной формы
                                    if (disc.FormEducation == "очная")
                                        idTypeWork = 10;
                                    else // // Текущие консультации для заочной формы
                                        idTypeWork = 11;

                                    normTime = FindNormTime(standartTime, idTypeWork, (x, y) => x.IdTypeTraining.Equals(y));
                                    loadDiscipline.Consultation = disc.LecturePlan * normTime;

                                    // Консультации перед экзаменом
                                    //if ((disc.ExaminationPlan != 0) && (disc.ExaminationPlan != null))
                                    //{
                                    //    if (disc.CountStudent >= 15)
                                    //        loadDiscipline.Consultation += 2;
                                    //    else
                                    //        loadDiscipline.Consultation += 1;
                                    //}


                                    //sumLoad += loadDiscipline.Consultation;
                                }

                                if ((disc.ExaminationPlan != 0) && (disc.ExaminationPlan != null))
                                {
                                    if (disc.CountStudent >= 15)
                                        loadDiscipline.Consultation += 2;
                                    else
                                        loadDiscipline.Consultation += 1;
                                }


                                sumLoad += loadDiscipline.Consultation;
                                //else
                                //    loadDiscipline.Consultation = 0;

                                // Прием зачетов
                                if ((disc.SetOffPlan != 0) && (disc.SetOffPlan != null))
                                {
                                    // Прием зачетов для российских обучающихся
                                    idTypeWork = 13;
                                    normTime = FindNormTime(standartTime, idTypeWork, (x, y) => x.IdTypeTraining.Equals(y));
                                    loadDiscipline.SetOff = normTime * disc.CountStudent;

                                    // Прием зачетов для иностранных обучающихся
                                    //idTypeWork = 14;
                                    //normTime = FindNormTime(standartTime, idTypeWork, (x, y) => x.IdTypeTraining.Equals(y));
                                    //loadDiscipline.SetOff += normTime * disc.CountForeignStudent;

                                    sumLoad += loadDiscipline.SetOff;
                                }
                                else
                                    loadDiscipline.SetOff = 0;

                                // Прием зачетов с оценкой
                                if ((disc.SetOffWithBallPlan != 0) && (disc.SetOffWithBallPlan != null))
                                {
                                    // Прием зачетов для российских обучающихся
                                    idTypeWork = 35;
                                    normTime = FindNormTime(standartTime, idTypeWork, (x, y) => x.IdTypeTraining.Equals(y));
                                    loadDiscipline.SetOffWithBall = normTime * disc.CountStudent;
                                    sumLoad += loadDiscipline.SetOffWithBall;
                                }
                                else
                                    loadDiscipline.SetOffWithBall = 0;


                                // Прием экзаменов
                                if ((disc.ExaminationPlan != 0) && (disc.ExaminationPlan != null))
                                {
                                    // Прием экзаменов для российских обучающихся
                                    idTypeWork = 15;
                                    normTime = FindNormTime(standartTime, idTypeWork, (x, y) => x.IdTypeTraining.Equals(y));
                                    loadDiscipline.Examination = normTime * disc.CountStudent;
                                    sumLoad += loadDiscipline.Examination;

                                    //////////////////////////////////////////////////

                                    //if (disc.CountStudent > 15)
                                    //    loadDiscipline.Consultation += 2;
                                    //else
                                    //    loadDiscipline.Consultation += 1;
                                    //sumLoad += loadDiscipline.Consultation;

                                    // Прием экзаменов для иностранных обучающихся
                                    //idTypeWork = 16;
                                    //normTime = FindNormTime(standartTime, idTypeWork, (x, y) => x.IdTypeTraining.Equals(y));
                                    //loadDiscipline.Examination += normTime * disc.CountForeignStudent;
                                    //loadDiscipline.SumLoad += loadDiscipline.Examination;
                                }
                                else
                                    loadDiscipline.Examination = 0;

                                // Руководство курсовым проектом 
                                if ((disc.CourseProjectPlan != 0) && (disc.CourseProjectPlan != null))
                                {
                                    // Руководство курсовым проектом для российских обучающихся
                                    idTypeWork = 18;
                                    normTime = FindNormTime(standartTime, idTypeWork, (x, y) => x.IdTypeTraining.Equals(y));
                                    loadDiscipline.CourseProject = normTime * disc.CountStudent;

                                    sumLoad += loadDiscipline.CourseProject;

                                    // Руководство курсовым проектом для иностранных обучающихся
                                    //idTypeWork = 19;
                                    //normTime = FindNormTime(standartTime, idTypeWork, (x, y) => x.IdTypeTraining.Equals(y));
                                    //loadDiscipline.CourseProject += normTime * disc.CountForeignStudent;
                                    //loadDiscipline.SumLoad += loadDiscipline.CourseProject;
                                }
                                else
                                    loadDiscipline.CourseProject = 0;

                                // Проведение практики учебной/производственной
                                if ((disc.PracticalPlan != 0) && (disc.PracticalPlan != null))
                                {
                                    //int idWork = 0;

                                    if (disc.FormEducation == "очная")
                                    {
                                        idTypeWork = 20;
                                        normTime = FindNormTime(standartTime, idTypeWork, (x, y) => x.IdTypeTraining.Equals(y));
                                        loadDiscipline.Practical = disc.PracticalPlan * normTime;
                                    }
                                    else
                                    {
                                        idTypeWork = 21;
                                        normTime = FindNormTime(standartTime, idTypeWork, (x, y) => x.IdTypeTraining.Equals(y));
                                        loadDiscipline.Practical = disc.CountStudent * normTime;
                                    }

                                    sumLoad += loadDiscipline.Practical;
                                }
                                else
                                    disc.PracticalPlan = 0;

                                // Руководство ВКР
                                if ((disc.GraduationDesignPlan != null) && (disc.GraduationDesignPlan != 0))
                                {
                                    idTypeWork = 24;
                                    normTime = FindNormTime(standartTime, idTypeWork, (x, y) => x.IdTypeTraining.Equals(y));
                                    loadDiscipline.GraduationDesign = normTime * disc.CountStudent;

                                    sumLoad += loadDiscipline.GraduationDesign;
                                }
                                else
                                    loadDiscipline.GraduationDesign = 0;

                                // Член ГАК/ГЭК
                                if ((disc.GakPlan != null) && (disc.GakPlan != 0))
                                {
                                    idTypeWork = 23;
                                    normTime = FindNormTime(standartTime, idTypeWork, (x, y) => x.IdTypeTraining.Equals(y));
                                    loadDiscipline.Gac = normTime * disc.CountStudent;

                                    sumLoad += loadDiscipline.Gac;
                                }
                                else
                                    loadDiscipline.Gac = 0;

                                // Руководство НИР магистров
                                if ((disc.ScientificResearchWorkPlan != null) && (disc.ScientificResearchWorkPlan != 0))
                                {
                                    idTypeWork = 34;
                                    normTime = FindNormTime(standartTime, idTypeWork, (x, y) => x.IdTypeTraining.Equals(y));
                                    loadDiscipline.ScientificResearchWork = normTime * disc.CountStudent;

                                    sumLoad += loadDiscipline.ScientificResearchWork;
                                }
                                else
                                    loadDiscipline.ScientificResearchWork = 0;



                                loadDiscipline.SumLoad = sumLoad;
                                loadDiscipline.SumUnload = sumLoad;

                                // Вычисление нагрузки по коммерческим студентам
                                if ((disc.CountComStudent != 0) && (disc.CountComStudent != null))
                                {
                                    decimal coef = (decimal)disc.CountComStudent / (decimal)disc.CountStudent;
                                    loadDiscipline.SumCommerce = sumLoad * coef;
                                }
                                else
                                    loadDiscipline.SumCommerce = 0;

                                //loadDiscipline.SumUnload = loadDiscipline.SumLoad;

                                loadDiscipline.FlagChange = false;

                                #endregion Вычисление нагрузки по дисциплине

                                loadService.AddItemLoadChair(loadDiscipline);
                            }
                    };


                    worker.RunWorkerCompleted += (o, ea) =>
                    {

                        LoadChairTeaching.Clear();
                        // !!! Целесообразно изменить метод сервиса, убрать из параметров  LoadChairTeaching и перенисти в DoWork !!!
                        LoadChairTeaching = loadService.GetLoadChairTeaching(LoadChairTeaching, SelectedChair.Id,
                                                                             selectedAcademicYear.Id,
                                                                             selectedNameLoadChair.NameLoadChair);
                        LoadChair.SumLoadChair = 0;
                        LoadChair.SumLoadChairCommerce = 0;

                        //Перевычисление суммарной нагрузки кафедры
                        SetSumLoadChair();

                        loadService.EditItemLoadChair(LoadChair);
                        IsBusy = false;
                    };

                    //установка IsBusy перед началом ассинхронного потока визуализации выполнения длительной операции
                    IsBusy = true;
                    BusyMessage = "Формирование нагрузки . . .";
                    worker.RunWorkerAsync();
                }
            }
        }

        /// <summary>
        /// Пересчитать нагрузку кафедры
        /// </summary>
        private void RewriteLoadChair()
        {
            int index = SelectedIndexNameLoadChair;
            var service = container.Resolve<IServiceLoadChair>();

            MessageBoxResult result1 = MessageBox.Show("Пересчитать нагрузку для : \n" + selectedNameLoadChair.NameLoadChair,
                "Предупреждение", MessageBoxButton.OKCancel);
            if (result1 == MessageBoxResult.OK)
            {
                // Удалить существующие записи по нагрузке
                if (LoadChairTeaching.Count > 0)
                    foreach (TeachingLoad load in LoadChairTeaching)
                    {
                        // Можно удалить только дисциплины плановые, проверив (load.Code != "")
                        service.RemoveItemLoadChair(load);
                    }

                // Очистка данных по дисциплинам нагрузки кафедры
                LoadChairTeaching.Clear();

                // Сформировать новую нагрузку для существующег IdTeachingLoadChair
                ShapingLoadChair(service);

                // Загрузка данных по дисциплинам нагрузки кафедры
                LoadChairTeaching = service.GetLoadChairTeaching(LoadChairTeaching, SelectedChair.Id, SelectedAcademicYear.Id, SelectedNameLoadChair.NameLoadChair);

                // Перерасчет общей нагрузки
                SetSumLoadChair();

                // Сохранение отредактированных данных по общей нагрузки кафедры
                service.EditItemLoadChair(LoadChair);

                // Обновление обобщенных данных по кафедрре
                AllLoadChairAcademicYear.Clear();
                AllLoadChairAcademicYear = service.GetAllLoadChair(AllLoadChairAcademicYear, SelectedChair.Id, SelectedAcademicYear.Id);
                //AllLoadChairAcademicYear = service.GetAllLoadChair(SelectedChair.Id, SelectedAcademicYear.Id);

                SelectedIndexNameLoadChair = index;
            }
        }

        /// <summary>
        /// Формирование нового варианта нагрузки кафедры
        /// </summary>
        private void NewLoadChair()
        {
            var viewModel = container.Resolve<NewLoadViewModel>();

            // Обобщенные данные по нагрузке кафедры
            TeachingLoadChair newLoadChair = new TeachingLoadChair();
            newLoadChair.IdChair = selectedChair.Id;
            newLoadChair.IdAcademicYear = selectedAcademicYear.Id;
            newLoadChair.NameLoadChair = String.Empty;
            newLoadChair.DataLoadChair = DateTime.Now;
            newLoadChair.SumLoadChair = 0;
            newLoadChair.SumLoadSemestr1 = 0;
            newLoadChair.SumLoadSemestr2 = 0;
            newLoadChair.SumUnloadChair = 0;
            newLoadChair.SumLoadChairCommerce = 0;
            newLoadChair.SumLoadCommerceSemestr1 = 0;
            newLoadChair.SumLoadCommerceSemestr2 = 0;
            newLoadChair.IndexStreamLecture = 0;
            newLoadChair.IndexStreamLab = 0;
            newLoadChair.IndexStreamPract = 0;
            newLoadChair.Note = String.Empty;

            viewModel.NewLoad = newLoadChair;

            var viewNew = container.Resolve<NewLoadView>();
            viewNew.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var resultLoad = windowService.ShowDialog("Добавление данных", viewNew);

            if (resultLoad.HasValue && resultLoad.Value == true)
            {
                var service = container.Resolve<IServiceLoadChair>();
                
                // Добавление новой записи по обобщенной нагрузки кафедры
                service.AddLoadChair(newLoadChair);
                
                // Загрузка обобщенных данных по созданной нагрузке кафедры
                LoadChair = service.GetLoadChair(LoadChair, newLoadChair.IdChair, newLoadChair.IdAcademicYear, newLoadChair.NameLoadChair);
                
                selectedNameLoadChair = LoadChair;

                // Формироание нагрузки    
                ShapingLoadChair(service);
                
                // Обновление обобщенных данных по кафедре
                AllLoadChairAcademicYear.Clear();
                AllLoadChairAcademicYear = service.GetAllLoadChair(AllLoadChairAcademicYear, SelectedChair.Id, SelectedAcademicYear.Id);
                //AllLoadChairAcademicYear = service.GetAllLoadChair(SelectedChair.Id, SelectedAcademicYear.Id);

                // Выбор сформированного варианта нагрузки кафедры
                int index = AllLoadChairAcademicYear.Count;
                SelectedIndexNameLoadChair = index - 1;
            }
        }

        #endregion Вспомогательные методы

        #region Правила проверки корректности формирования потоков

        /// <summary>
        /// Правило 1: Совпадение наименований дисциплин
        /// </summary>
        /// <param name="flagRule"></param>
        /// <param name="disciplineStream"></param>
        /// <returns></returns>
        private static int Rule1(int flagRule, ObservableCollection<TeachingLoad> disciplineStream)
        {
            if (flagRule == 0)
            {
                // Правило 1:
                // Совпадение наименований дисциплин
                string discipline = disciplineStream[0].Discipline;
                foreach (var disc in disciplineStream)
                    if (discipline != disc.Discipline)
                    {
                        flagRule = 1;
                        break;
                    }
            }
            return flagRule;
        }

        /// <summary>
        /// Правило 2: Совпадение курсов
        /// </summary>
        /// <param name="flagRule"></param>
        /// <param name="disciplineStream"></param>
        /// <returns></returns>
        private static int Rule2(int flagRule, ObservableCollection<TeachingLoad> disciplineStream)
        {
            if (flagRule == 0)
            {
                // Правило 2:
                // Совпадение курсов
                int course = (int)disciplineStream[0].Course;
                foreach (var disc in disciplineStream)
                    if (course != disc.Course)
                    {
                        flagRule = 2;
                        break;
                    }
            }
            return flagRule;
        }

        /// <summary>
        /// Правило 3: Совпадение семестров
        /// </summary>
        /// <param name="flagRule"></param>
        /// <param name="disciplineStream"></param>
        /// <returns></returns>
        private static int Rule3(int flagRule, ObservableCollection<TeachingLoad> disciplineStream)
        {
            if (flagRule == 0)
            {
                // Правило 3:
                // Совпадение семестров

                int? semester = (int?)disciplineStream[0].Semester;
                foreach (var disc in disciplineStream)
                    if (disc.Semester != null)
                        if (semester != disc.Semester)
                        {
                            flagRule = 3;
                            break;
                        }
            }
            return flagRule;
        }

        /// <summary>
        /// Правило 4: Совпадение лекционных часов
        /// </summary>
        /// <param name="flagRule"></param>
        /// <param name="disciplineStream"></param>
        /// <returns></returns>
        private static int Rule4(int flagRule, ObservableCollection<TeachingLoad> disciplineStream)
        {
            if (flagRule == 0)
            {
                // Правило 4:
                // Совпадение лекционных часов
                decimal lecture = (decimal)disciplineStream[0].Lecture;
                foreach (var disc in disciplineStream)
                    if (lecture != disc.Lecture)
                    {
                        flagRule = 4;
                        break;
                    }
            }
            return flagRule;
        }

        /// <summary>
        /// Правило 5: Совпадение часов лабораторных занятий
        /// </summary>
        /// <param name="flagRule"></param>
        /// <param name="disciplineStream"></param>
        /// <returns></returns>
        private static int Rule5(int flagRule, ObservableCollection<TeachingLoad> disciplineStream)
        {
            if (flagRule == 0)
            {
                // Правило 5:
                // Совпадение  часов лабораторных занятий
                decimal laboratory = (decimal)disciplineStream[0].LaboratoryWork;
                foreach (var disc in disciplineStream)
                    if (laboratory != disc.LaboratoryWork)
                    {
                        flagRule = 5;
                        break;
                    }
            }
            return flagRule;
        }


        /// <summary>
        /// Правило 6: Совпадение часов практических занятий
        /// </summary>
        /// <param name="flagRule"></param>
        /// <param name="disciplineStream"></param>
        /// <returns></returns>
        private static int Rule6(int flagRule, ObservableCollection<TeachingLoad> disciplineStream)
        {
            if (flagRule == 0)
            {
                // Правило 4:
                // Совпадение часов практических занятий
                decimal practical = (decimal)disciplineStream[0].PracticalExercises;
                foreach (var disc in disciplineStream)
                    if (practical != disc.PracticalExercises)
                    {
                        flagRule = 6;
                        break;
                    }
            }
            return flagRule;
        }

        /// <summary>
        /// Анализ правил проверки корректности объединения в потоки
        /// </summary>
        /// <param name="flagRule"></param>
        private static void CheckRule(int flagRule)
        {
            switch (flagRule)
            {
                case 1:
                    MessageBox.Show("Ошибка объединения потоков \n Намиенование дисциплин не совпадают",
                        "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case 2:
                    MessageBox.Show("Ошибка объединения потоков \n Курсы не совпадают",
                        "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case 3:
                    MessageBox.Show("Ошибка объединения потоков \n Семестры не совпадают",
                        "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case 4:
                    MessageBox.Show("Ошибка объединения потоков \n Лекционные часы не совпадают",
                        "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case 5:
                    MessageBox.Show("Ошибка объединения потоков \n Часы лабораторных занятий не совпадают",
                        "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case 6:
                    MessageBox.Show("Ошибка объединения потоков \n Часы практических занятий не совпадают",
                        "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
            }
        }

        #endregion Правила проверки корректности формирования потоков

        #region CommandGetLoadChair

        /// <summary>
        /// Команда - Загрузка данных по нагрузке кафедры - поле
        /// </summary>
        private ICommand getLoadChair;

        /// <summary>
        /// Команда - Загрузка данных по нагрузке кафедры - Свойство
        /// </summary>
        public ICommand GetLoadChair
        {
            get
            {
                return getLoadChair ??
                    (getLoadChair = new RelayCommand(GetCommandLoadChairExecute));
            }
        }

        /// <summary>
        /// Загрузка данных по нагрузке кафедры - метод
        /// </summary>
        private void    GetCommandLoadChairExecute()
        {
            var loadChairService = container.Resolve<IServiceLoadChair>();
            
            if (LoadChairTeaching != null)
                LoadChairTeaching.Clear();
            
            if ((SelectedFaculty != null) && 
                (SelectedChair != null) && 
                (SelectedAcademicYear != null) && 
                (SelectedNameLoadChair != null))
            {
                LoadChairTeaching = loadChairService.GetLoadChairTeaching(LoadChairTeaching, 
                                                                          SelectedChair.Id, 
                                                                          selectedAcademicYear.Id, 
                                                                          selectedNameLoadChair.NameLoadChair);
                LoadChair.SumLoadChair = 0;
                LoadChair = loadChairService.GetLoadChair(LoadChair, 
                                                          SelectedChair.Id, 
                                                          selectedAcademicYear.Id, 
                                                          selectedNameLoadChair.NameLoadChair);
            }

            // Очистка параметров поиска/фильтации
            PlanCurriculum = string.Empty;
            NameDiscipline = string.Empty;
            //MessageSave = string.Empty;
            SelectedIndexEducationForm = 0;
        }

        #endregion CommandGetLoadChair

        #region CommandGetAllLoadChair

        /// <summary>
        /// Команда - Загрузка обобщенных данных по нагрузке кафедры - поле
        /// </summary>
        private ICommand getAllLoadChair;

        /// <summary>
        /// Команда - Загрузка обобщенных данных по нагрузке кафедры - Свойство
        /// </summary>
        public ICommand GetAllLoadChair
        {
            get
            {
                return getAllLoadChair ??
                    (getAllLoadChair = new RelayCommand(GetAllLoadChairExecute));
            }

        }

        /// <summary>
        /// Загрузка обобщенных данных по нагрузке кафедры - метод
        /// </summary>
        private void GetAllLoadChairExecute()
        {
            var loadChairService = container.Resolve<IServiceLoadChair>();

            if (AllLoadChairAcademicYear != null)
                AllLoadChairAcademicYear.Clear();

            if ((SelectedChair != null) && 
                (SelectedAcademicYear != null))
            {
                AllLoadChairAcademicYear = loadChairService.GetAllLoadChair(AllLoadChairAcademicYear, SelectedChair.Id, selectedAcademicYear.Id);
                //AllLoadChairAcademicYear = loadChairService.GetAllLoadChair(SelectedChair.Id, selectedAcademicYear.Id);
            }

            SelectedIndexNameLoadChair = Properties.Settings.Default.LoadChairComboBoxNameLoadIndex;

        }

        #endregion CommandGetAllLoadChair

        #region CommandGetChair

        /// <summary>
        /// Команда - Загрузка данных по нагрузке кафедры - поле
        /// </summary>
        private ICommand getChair;

        /// <summary>
        /// Команда - Загрузка данных по нагрузке кафедры - Свойство
        /// </summary>
        public ICommand GetChair
        {
            get
            {
                return getChair ??
                    (getChair = new RelayCommand(GetChairExecute));
            }

        }

        /// <summary>
        /// Загрузка данных по нагрузке кафедры - метод
        /// </summary>
        private void GetChairExecute()
        {
            var loadChairService = container.Resolve<IServiceChair>();
            Chairs.Clear();
            if (SelectedFaculty != null)
                Chairs = loadChairService.GetChair(Chairs, SelectedFaculty.Id);

            SelectedIndexChair = Properties.Settings.Default.LoadChairComboBoxChairIndex;
        }


        #endregion CommandGetCommandLoadChair

        #region CommandLoadDisciplineChair

        /// <summary>
        /// Команда - формирования нагрузки по кафедре
        /// </summary>
        private ICommand loadDisciplineChair;

        /// <summary>
        /// Команда - формирования нагрузки по кафедре
        /// </summary>
        public ICommand LoadDisciplineChair
        {
            get
            {
                return loadDisciplineChair ??
                    (loadDisciplineChair = new RelayCommand(LoadDisciplineChairExecute, CanExecute));
            }
        }

        /// <summary>
        /// Формирование нагрузки по кафедре
        /// </summary>
        private void LoadDisciplineChairExecute()
        {
            var viewModelMode = container.Resolve<ModeSelectionLoadsViewModel>();

            // Режим формирования нагрузки
            ModeLoad mode = new ModeLoad();
            mode.NewLoad = true;
            mode.RewriteLoad = false;

            viewModelMode.Mode = mode;

            var viewMode = container.Resolve<ModeSelectionLoadsView>();

            viewMode.DataContext = viewModelMode;

            var windowServiceMode = container.Resolve<IWindowService>();
            var result = windowServiceMode.ShowDialog("Выбор режима формирования нагрузки кафедры", viewMode);

            if (result.HasValue && result.Value == true)
            {
                // Формирование нового варианта нагрузки
                if (mode.NewLoad)
                {
                    NewLoadChair();
                }

                // Перезапись/перерасчет существующего варианта нагрузки
                if (mode.RewriteLoad)
                {
                    //RewriteLoadChair();
                }
            }
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveGroup -  Редактирование / Удаление данных по факультету
        /// </summary>
        /// <returns></returns>
        private bool CanExecute()
        {
            return SelectedChair != null;
        }

        #endregion CommandLoadDisciplineOfChair

        #region CommandSave

        /// <summary>
        /// Команда - Сохранение данных по дисциплине нагрузки кафедры - поле
        /// </summary>
        private ICommand saveCommand;

        /// <summary>
        /// Команда - Сохранение данных по дисциплине нагрузки кафедры - Свойство
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(SaveExecute, CanSaveExecute));
            }

        }

        /// <summary>
        /// Сохранение данных по дисциплине нагрузки кафедры- метод
        /// </summary>
        private void SaveExecute()
        {
            if (disciplineLoadForSave != null)
            {
                // Копия коллекции обновляемых нагрузок по дисциплинам
                ObservableCollection<TeachingLoad> disciplinesSaving = new ObservableCollection<TeachingLoad>();

                int count = LoadChairTeaching.Count;

                int index = SelectedIndexDiscipline;
                SelectedIndexDiscipline = -1;


                //if (SelectedIndexDiscipline >= count - 1)
                    SelectedIndexDiscipline = -1;
                
                // Создание копии коллекции сохраняемых дисциплин
                foreach (TeachingLoad tl in disciplineLoadForSave)
                {
                    tl.FlagChange = false;
                    TeachingLoad load = CloneLoad(tl);

                    if (!disciplinesSaving.Contains(load))
                        disciplinesSaving.Add(load);
                }
                
                var loadService = container.Resolve<IServiceLoadChair>();
             
                //Сохранение отредактированных данных по нагрузке для отдельных дисциплин
                foreach (TeachingLoad tl in disciplinesSaving)
                    loadService.EditItemTeachingLoadChair(tl);

                // Сохранение отредактированных данных по общей нагрузки кафедры
                loadService.EditItemLoadChair(LoadChair);

                disciplineLoadForSave.Clear();

                //MessageSave = string.Empty;

                SelectedIndexDiscipline = index;
            }
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveGroup -  Редактирование / Удаление данных по факультету
        /// </summary>
        /// <returns></returns>
        private bool CanSaveExecute()
        {
            //return messageSave != string.Empty;
            return false;
        }

        #endregion CommandSave

        #region CommandAdd

        /// <summary>
        /// Команда - Добавление данных по отдельной дисциплине нагрузки - поле
        /// </summary>
        private ICommand addCommand;

        /// <summary>
        /// Команда - Добавление данных по отдельной дисциплине нагрузки  свойство
        /// </summary>
        public ICommand AddCommand
        {
            get
            {
                return addCommand ??
                    (addCommand = new RelayCommand(AddExecute));

            }
        }

        /// <summary>
        /// Добавление данных по отдельной дисциплине нагрузки  - метод
        /// </summary>
        private void AddExecute()
        {
            var viewModel = container.Resolve<NewLoadChairViewModel>();

            TeachingLoad newLoad = new TeachingLoad();
            newLoad = NewTeachingLoad(newLoad);

            viewModel.NewLoadChair = newLoad;

            var viewNew = container.Resolve<NewLoadChairView>();
            viewNew.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Добавление данных", viewNew);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceLoadChair>();
                newLoad = SetTotalDisciplineLoad(newLoad);
                service.AddItemLoadChair(newLoad);

                LoadChairTeaching.Clear();
                LoadChairTeaching = service.GetLoadChairTeaching(LoadChairTeaching, SelectedChair.Id, SelectedAcademicYear.Id, selectedNameLoadChair.NameLoadChair);

                // Перевычисление суммарной нагрузки кафедры
                SetSumLoadChair();
                // Сохранение отредактированных данных по общей нагрузки кафедры
                service.EditItemLoadChair(LoadChair);

                disciplineLoadForSave.Clear();
            }
        }

       

        #endregion CommandAdd

        #region CommandRemoveDiscipline

        /// <summary>
        /// Команда - Удаление данных по по отдельной дисциплине нагрузки - поле
        /// </summary>
        private ICommand removeCommand;

        /// <summary>
        /// Команда - Удаление данных по по отдельной дисциплине нагрузки - Свойство
        /// </summary>
        public ICommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                    (removeCommand = new RelayCommand(RemoveExecute, CanRemoveExecute));
            }
        }

        /// <summary>
        /// Удаление данных по по отдельной дисциплине нагрузки - метод
        /// </summary>
        private void RemoveExecute()
        {
            MessageBoxResult result = MessageBox.Show("Удалить данные по дисциплине нагрузки: \n" + SelectedDisciplineLoad.Code +"  "+ SelectedDisciplineLoad.Discipline,
                   "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var service = container.Resolve<IServiceLoadChair>();
                bool resultDeleteDiscipline = false;
                string messageErrorDelete = string.Empty;

                /////////////////////////////////////////////////////
                // Индикация длительной операции

                BackgroundWorker worker = new BackgroundWorker();
                // Запуск длительной операции добавления данных о новом учебном плане в базу данных
                worker.DoWork += (o, ea) =>
                {
                    
                    resultDeleteDiscipline = service.RemoveItemLoadChair(SelectedDisciplineLoad, out messageErrorDelete);
                    if (resultDeleteDiscipline)
                    {
                        MessageBox.Show(messageErrorDelete, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                };
                worker.RunWorkerCompleted += (o, ea) =>
                {
                    if (!resultDeleteDiscipline)
                    {
                        LoadChairTeaching.Clear();
                        LoadChairTeaching = service.GetLoadChairTeaching(LoadChairTeaching, SelectedChair.Id, SelectedAcademicYear.Id, selectedNameLoadChair.NameLoadChair);

                        // Перевычисление суммарной нагрузки кафедры
                        SetSumLoadChair();
                        // Сохранение отредактированных данных по общей нагрузки кафедры
                        service.EditItemLoadChair(LoadChair);

                        //MessageSave = String.Empty;
                        disciplineLoadForSave.Clear();
                    }
                    IsBusy = false;
                };


                // установка IsBusy перед началом ассинхронного потока визуализации выполнения длительной операции
                IsBusy = true;
                BusyMessage = "Удаление данных . . .";
                worker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Признак доступности команды RemoveUniversityExecute -  Удаление данных по отдельной дисциплине нагрузки
        /// </summary>
        /// <returns></returns>
        private bool CanRemoveExecute()
        {
            return SelectedDisciplineLoad != null;
        }
       
        #endregion CommandRemoveDiscipline

        #region CommandRemoveLoad

        /// <summary>
        /// Команда - Удаление данных по нагрузке - поле
        /// </summary>
        private ICommand removeLoadChair;

        /// <summary>
        /// Команда - Удаление данных по нагрузке - Свойство
        /// </summary>
        public ICommand RemoveLoadChair
        {
            get
            {
                return removeLoadChair ??
                    (removeLoadChair = new RelayCommand(RemoveLoadExecute, CanRemoveLoadExecute));
            }
        }

        /// <summary>
        /// Удаление данных по нагрузке - метод
        /// </summary>
        private void RemoveLoadExecute()
        {
            // Обновление списка нагрузки перед удалением
            GetCommandLoadChairExecute();
            
            MessageBoxResult result = MessageBox.Show("Удалить данные по нагрузке кафедры: \n" + SelectedNameLoadChair.NameLoadChair,
                   "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                bool resultDeleteDiscipline = false;
                string messageErrorDelete = string.Empty;

                var loadService = container.Resolve<IServiceLoadChair>();

                // Индикация длительной операции

                BackgroundWorker worker = new BackgroundWorker();
                // Запуск длительной операции добавления данных о новом учебном плане в базу данных
                worker.DoWork += (o, ea) =>
                {

                    if (LoadChairTeaching.Count > 0)


                    //    foreach (TeachingLoad load in LoadChairTeaching)
                    //        loadService.RemoveItemLoadChair(load);
                    

                        for (int i = 0; i < LoadChairTeaching.Count; i++)
                        {
                            resultDeleteDiscipline = loadService.RemoveItemLoadChair(LoadChairTeaching[i], out messageErrorDelete);
                            if (resultDeleteDiscipline)
                            {
                                MessageBox.Show(messageErrorDelete, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                                break;
                            }
                        }

                    if (!resultDeleteDiscipline)
                        if (LoadChair != null)
                            loadService.RemoveLoad(LoadChair);
                    
                };

                worker.RunWorkerCompleted += (o, ea) =>
                {
                    if (!resultDeleteDiscipline)
                    {
                        AllLoadChairAcademicYear.Clear();
                        if ((SelectedFaculty != null) && (SelectedChair != null) && (SelectedAcademicYear != null))
                        {
                            AllLoadChairAcademicYear = loadService.GetAllLoadChair(AllLoadChairAcademicYear, SelectedChair.Id, selectedAcademicYear.Id);
                            //AllLoadChairAcademicYear = loadService.GetAllLoadChair(SelectedChair.Id, selectedAcademicYear.Id);
                        }
                        SelectedIndexNameLoadChair = -1;

                        //MessageSave = String.Empty;
                        disciplineLoadForSave.Clear();
                        LoadChair.SumLoadChair = 0;
                    }
                    IsBusy = false;
                };
                
                // установка IsBusy перед началом ассинхронного потока визуализации выполнения длительной операции
                IsBusy = true;
                BusyMessage = "Удаление данных . . .";
                worker.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveGroup -  Редактирование / Удаление данных по отдельной дисциплине нагрузки
        /// </summary>
        /// <returns></returns>
        private bool CanRemoveLoadExecute()
        {
            return (SelectedChair != null) && (SelectedNameLoadChair != null) && (SelectedAcademicYear != null);
        }

        #endregion CommandRemoveLoad

        #region CommandFindDiscipline

        /// <summary>
        /// Поиск дисциплин кафедры по учебному плану, наименованию дисциплины и форме обучения  - поле
        /// </summary>
        private ICommand findDiscipline;

        /// <summary>
        /// Поиск дисциплин кафедры по учебному плану, наименованию дисциплины и форме обучения - Свойство
        /// </summary>
        public ICommand FindDiscipline
        {
            get
            {
                return findDiscipline ??
                    (findDiscipline = new RelayCommand(FindfindDisciplineExecute, CanExecuteFind));
            }
        }

        /// <summary>
        /// Признак доступности команды FindDiscipline
        /// </summary>
        /// <returns></returns>

        private bool CanExecuteFind()
        {
            return  (planCurriculum != "") || (nameDiscipline != "") || (selectedEducationForm.FormEducation != "не задано");
        }

        /// <summary>
        /// Поиск дисциплин кафедры по учебному плану, коду и наименованию дисциплины - метод
        /// </summary>
        private void FindfindDisciplineExecute()
        {
            // Временная коллекция для поиска/фильтрации записей по дисциплинам кафедры
            List<TeachingLoad> disciplineListFind = LoadChairTeaching.ToList<TeachingLoad>();

            // Выходная последовательность - результат Linq-запроса
            IEnumerable<TeachingLoad> query;

            // Запросы для фильтрации данных
            if (planCurriculum != "" && nameDiscipline != "" && selectedEducationForm.FormEducation != "не задано")
            {
                // по коду учебного плана, наименованию дисциплины, форме обучения
                query = disciplineListFind.Where(n => n.NameCurricilum.ToUpper().Contains(planCurriculum.ToUpper()) &&
                                                      n.Discipline.ToUpper().Contains(nameDiscipline.ToUpper()) && 
                                                      n.FormEducation == selectedEducationForm.FormEducation);
            }
            else
                if (planCurriculum != "" && nameDiscipline != "" && selectedEducationForm.FormEducation == "не задано")
                {
                    // по коду учебного плана, наименованию дисциплины
                    query = disciplineListFind.Where(n => n.NameCurricilum.ToUpper().Contains(planCurriculum.ToUpper()) &&
                                                          n.Discipline.ToUpper().Contains(nameDiscipline.ToUpper()));
                }
                else
                    if (planCurriculum != "" && nameDiscipline == "" && selectedEducationForm.FormEducation != "не задано")
                    {
                        // по коду учебного плана,  форме обучения
                        query = disciplineListFind.Where(n => n.NameCurricilum.ToUpper().Contains(planCurriculum.ToUpper()) &&
                                                              n.FormEducation == selectedEducationForm.FormEducation);
                    }
                    else
                        if (planCurriculum == "" && nameDiscipline != "" && selectedEducationForm.FormEducation != "не задано")
                        {
                            // наименованию дисциплины, форме обучения
                            query = disciplineListFind.Where(n => n.Discipline.ToUpper().Contains(nameDiscipline.ToUpper()) &&
                                                                  n.FormEducation == selectedEducationForm.FormEducation);
                        }
                        else
                            if (planCurriculum != "" && nameDiscipline == "" && selectedEducationForm.FormEducation == "не задано")
                            {
                                // по коду учебного плана
                                query = disciplineListFind.Where(n => n.NameCurricilum.ToUpper().Contains(planCurriculum.ToUpper()));
                            }
                            else
                                if (planCurriculum == "" && nameDiscipline != "" && selectedEducationForm.FormEducation == "не задано")
                                {
                                    // по наименованию дисцеплины
                                    query = disciplineListFind.Where(n => n.Discipline.ToUpper().Contains(nameDiscipline.ToUpper()));
                                }
                                else
                                {
                                    // по форме обучения    
                                    query = disciplineListFind.Where(n => n.FormEducation == selectedEducationForm.FormEducation);
                                }

                   

            /// Анализ результатов фильтрации
            if (query.Count<TeachingLoad>() > 0)
            {
                // Обновление коллекции направлений подготовки
                LoadChairTeaching.Clear();
                foreach (var sp in query)
                    LoadChairTeaching.Add(sp);
            }
            else
            {
                MessageBox.Show("Дисциплины кафедры\nУчебный план: " + planCurriculum +
                    "\nДисциплина:    " + nameDiscipline + "\nФорма обучения:    " + selectedEducationForm.FormEducation +"\n НЕ НАЙДЕНЫ!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }

        #endregion CommandFindDiscipline

        #region RefreshDiscipline

        /// <summary>
        /// Команда - Обновление данных по дисциплинам кафедры - поле
        /// </summary>
        private ICommand refreshDiscipline;

        /// <summary>
        /// Команда - Обновление данных по дисциплинам кафедры - Свойство
        /// </summary>
        public ICommand RefreshDiscipline
        {
            get
            {
                return refreshDiscipline ??
                    (refreshDiscipline = new RelayCommand(GetCommandLoadChairExecute));
            }

        }

        #endregion RefreshDiscipline

        #region CommanCreateStreamLecture

        /// <summary>
        /// Команда - Формирование потока лекций - поле
        /// </summary>
        private ICommand createStreamLecture;

        /// <summary>
        /// Команда - Формирование потока лекций -   свойство
        /// </summary>
        public ICommand CreateStreamLecture
        {
            get
            {
                return createStreamLecture ??
                    (createStreamLecture = new RelayCommand(CreateStreamLectureExecute));
            }
        }

        /// <summary>
        /// Формирование потока лекций  - метод
        /// </summary>
        private void CreateStreamLectureExecute()
        {
            // Флаг выполнения правил формирования потока лекций
            int flagRule = 0;

            // Формирование списка выделенных дисциплин
            ObservableCollection<TeachingLoad> disciplineStream = new ObservableCollection<TeachingLoad>();
            foreach (var disc in LoadChairTeaching)
            {
                if((bool)disc.FlagChange)
                    disciplineStream.Add(disc);
            }

            if (disciplineStream.Count > 1)
            {

                // Проверка правил формирования потока лекций
                flagRule = Rule1(flagRule, disciplineStream);
                flagRule = Rule2(flagRule, disciplineStream);
                flagRule = Rule3(flagRule, disciplineStream);
                flagRule = Rule4(flagRule, disciplineStream);


                // Если правила выполнены
                if(flagRule == 0)
                {
                    // Задание номера потока
                    streamLecture = (int)LoadChair.IndexStreamLecture +1;
                    foreach (var disc in disciplineStream)
                        disc.Stream = streamLecture;

                    // Обнуление лекционных часов для второй и последующих дисциплпн потока
                    for (int i = 1; i < disciplineStream.Count; i++)
                        disciplineStream[i].Lecture = 0;

                    // Подготовка диалогового окна Формирование потока лекций
                    var viewModel = container.Resolve<CreateStreamViewModel>();
                    viewModel.DisciplineStream = disciplineStream;
                    var viewNew = container.Resolve<CreateStreamView>();
                    viewNew.DataContext = viewModel;
                    var windowService = container.Resolve<IWindowService>();
                    var result = windowService.ShowDialog("Формирование потока лекций", viewNew);
                    if (result.HasValue && result.Value == true)
                    {
                        // Копия коллекции обновляемых нагрузок по дисциплинам
                        ObservableCollection<TeachingLoad> disciplinesSaving = new ObservableCollection<TeachingLoad>();

                        int count = LoadChairTeaching.Count;

                        int index = SelectedIndexDiscipline;
                        SelectedIndexDiscipline = -1;

                        // Создание копии коллекции сохраняемых дисциплин
                        foreach (TeachingLoad tl in disciplineStream)
                        {
                            tl.FlagChange = false;
                            TeachingLoad load = CloneLoad(tl);

                            if (!disciplinesSaving.Contains(load))
                                disciplinesSaving.Add(load);
                        }

                        var loadService = container.Resolve<IServiceLoadChair>();

                        //Сохранение отредактированных данных по нагрузке для отдельных дисциплин
                        foreach (TeachingLoad tl in disciplinesSaving)
                            loadService.EditItemTeachingLoadChair(tl);

                        // Сохранение отредактированных данных по общей нагрузки кафедры
                        LoadChair.IndexStreamLecture = streamLecture; // Запоминание последнего номера потока
                        loadService.EditItemLoadChair(LoadChair);

                        SelectedIndexDiscipline = index;
                        //Properties.Settings.Default.StreamLecrure = streamLecture;
                    } 
                }
                else
                {
                    // Анализ нарушений формирования потока лекция
                    CheckRule(flagRule);
                }
            }
            else
                MessageBox.Show("Необходими выделить несколько дисциплин", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        #endregion CommanCreateStreamLecture

        #region CommanCreateStreamLab

        /// <summary>
        /// Команда - Формирование потока лабораторных работ - поле
        /// </summary>
        private ICommand createStreamLab;

        /// <summary>
        /// Команда - Формирование потока лабораторных работ -   свойство
        /// </summary>
        public ICommand CreateStreamLab
        {
            get
            {
                return createStreamLab ??
                    (createStreamLab = new RelayCommand(CreateStreamLabExecute));

            }
        }

        /// <summary>
        /// Формирование потока лабораторных работ  - метод
        /// </summary>
        private void CreateStreamLabExecute()
        {
            // Флаг выполнения правил формирования потока лекций
            int flagRule = 0;

            // Формирование списка выделенных дисциплин
            ObservableCollection<TeachingLoad> disciplineStream = new ObservableCollection<TeachingLoad>();
            foreach (var disc in LoadChairTeaching)
            {
                if ((bool)disc.FlagChange)
                    disciplineStream.Add(disc);
            }

            if (disciplineStream.Count > 1)
            {
                // Проверка правил формирования потока лекций
                flagRule = Rule1(flagRule, disciplineStream);
                flagRule = Rule2(flagRule, disciplineStream);
                flagRule = Rule3(flagRule, disciplineStream);
                flagRule = Rule5(flagRule, disciplineStream);

                // Если правила выполнены
                if (flagRule == 0)
                {
                    // Задание номера потока
                    streamLab = (int)LoadChair.IndexStreamLab +1;;
                    foreach (var disc in disciplineStream)
                        disc.StreamLab = streamLab;

                    // Обнуление часов лабораторных работ для второй и последующих дисциплин потока
                    for (int i = 1; i < disciplineStream.Count; i++)
                        disciplineStream[i].LaboratoryWork = 0;

                    // Подготовка диалогового окна Формирование потока лабораторных работ
                    var viewModel = container.Resolve<CreateStreamLabViewModel>();
                    viewModel.DisciplineStream = disciplineStream;
                    var viewNew = container.Resolve<CreateStreamLabView>();
                    viewNew.DataContext = viewModel;
                    var windowService = container.Resolve<IWindowService>();
                    var result = windowService.ShowDialog("Формирование потока лабораторных работ", viewNew);
                    if (result.HasValue && result.Value == true)
                    {
                        // Копия коллекции обновляемых нагрузок по дисциплинам
                        ObservableCollection<TeachingLoad> disciplinesSaving = new ObservableCollection<TeachingLoad>();

                        int count = LoadChairTeaching.Count;

                        int index = SelectedIndexDiscipline;
                        SelectedIndexDiscipline = -1;

                        // Создание копии коллекции сохраняемых дисциплин
                        foreach (TeachingLoad tl in disciplineStream)
                        {
                            tl.FlagChange = false;
                            TeachingLoad load = CloneLoad(tl);

                            if (!disciplinesSaving.Contains(load))
                                disciplinesSaving.Add(load);
                        }

                        var loadService = container.Resolve<IServiceLoadChair>();

                        //Сохранение отредактированных данных по нагрузке для отдельных дисциплин
                        foreach (TeachingLoad tl in disciplinesSaving)
                            loadService.EditItemTeachingLoadChair(tl);

                        // Сохранение отредактированных данных по общей нагрузки кафедры
                        LoadChair.IndexStreamLab = streamLab; // Запоминание последнего номера потока
                        loadService.EditItemLoadChair(LoadChair);

                        SelectedIndexDiscipline = index;
                        //Properties.Settings.Default.StreamLecrure = streamLecture;
                    }
                }
                else
                {
                    // Анализ нарушений формирования потока лекция
                    CheckRule(flagRule);
                }
            }
            else
                MessageBox.Show("Необходими выделить несколько дисциплин", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }


        #endregion CommandCommanCreateStreamLab

        #region CommanCreateStreamPract

        /// <summary>
        /// Команда - Формирование потока практических занятий - поле
        /// </summary>
        private ICommand createStreamPract;

        /// <summary>
        /// Команда - Формирование потока практических занятий -   свойство
        /// </summary>
        public ICommand CreateStreamPract
        {
            get
            {
                return createStreamPract ??
                    (createStreamPract = new RelayCommand(CreateStreamPractExecute));

            }
        }

        /// <summary>
        /// Формирование потока практических занятий - метод
        /// </summary>
        private void CreateStreamPractExecute()
        {
            // Флаг выполнения правил формирования потока лекций
            int flagRule = 0;

            // Формирование списка выделенных дисциплин
            ObservableCollection<TeachingLoad> disciplineStream = new ObservableCollection<TeachingLoad>();
            foreach (var disc in LoadChairTeaching)
            {
                if ((bool)disc.FlagChange)
                    disciplineStream.Add(disc);
            }

            if (disciplineStream.Count > 1)
            {
                // Проверка правил формирования потока лекций
                flagRule = Rule1(flagRule, disciplineStream);
                flagRule = Rule2(flagRule, disciplineStream);
                flagRule = Rule3(flagRule, disciplineStream);
                flagRule = Rule5(flagRule, disciplineStream);

                // Если правила выполнены
                if (flagRule == 0)
                {
                    // Задание номера потока
                    streamPract = (int)LoadChair.IndexStreamPract +1;
                    foreach (var disc in disciplineStream)
                        disc.StreamPract = streamPract;

                     // Обнуление часов практических занятий для второй и последующих дисциплин потока
                    for (int i = 1; i < disciplineStream.Count; i++)
                        disciplineStream[i].PracticalExercises = 0;
                    
                    // Подготовка диалогового окна Формирование потока практических занятий
                    var viewModel = container.Resolve<CreateStreamPractViewModel>();
                    viewModel.DisciplineStream = disciplineStream;
                    var viewNew = container.Resolve<CreateStreamPractView>();
                    viewNew.DataContext = viewModel;
                    var windowService = container.Resolve<IWindowService>();
                    var result = windowService.ShowDialog("Формирование потока практических занятий", viewNew);
                    if (result.HasValue && result.Value == true)
                    {
                        // Копия коллекции обновляемых нагрузок по дисциплинам
                        ObservableCollection<TeachingLoad> disciplinesSaving = new ObservableCollection<TeachingLoad>();
                        int count = LoadChairTeaching.Count;
                        int index = SelectedIndexDiscipline;
                        SelectedIndexDiscipline = -1;
                        
                        // Создание копии коллекции сохраняемых дисциплин
                        foreach (TeachingLoad tl in disciplineStream)
                        {
                            tl.FlagChange = false;
                            TeachingLoad load = CloneLoad(tl);
                            
                            if (!disciplinesSaving.Contains(load))
                                disciplinesSaving.Add(load);
                        }
                        
                        var loadService = container.Resolve<IServiceLoadChair>();
                        
                        //Сохранение отредактированных данных по нагрузке для отдельных дисциплин
                        foreach (TeachingLoad tl in disciplinesSaving)
                            loadService.EditItemTeachingLoadChair(tl);
                        
                        // Сохранение отредактированных данных по общей нагрузки кафедры
                        LoadChair.IndexStreamPract = streamPract; // Запоминание последнего номера потока
                        loadService.EditItemLoadChair(LoadChair);
                        
                        SelectedIndexDiscipline = index;
                        //Properties.Settings.Default.StreamLecrure = streamLecture;
                    }
                }
                else
                {
                    // Анализ нарушений формирования потока лекция
                    CheckRule(flagRule);
                }
            }
            else
                MessageBox.Show("Необходими выделить несколько дисциплин", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        #endregion CommandCommanCreateStreamPract
    }
}

