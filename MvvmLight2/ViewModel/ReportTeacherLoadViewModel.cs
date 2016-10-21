using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ServiceData;


namespace MvvmLight2.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// Отчет по нагрузке преподавателей кафедры
    /// </summary>
    public class ReportTeacherLoadViewModel : WorkspaceViewModel
    {
        private IContainer container = Container.Instance;

        #region Selected Property
        
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

         /// <summary>
         /// Поле - выделенная в списке кафедра
         /// </summary>
         private Chair selectedChair;

         /// <summary>
         /// Выделенная в списке кафедра - свойство
         /// </summary>
         public Chair SelectedChair
         {
             get { return selectedChair; }
             set
             {
                 if (value == selectedChair)
                     return;
                 else
                 {
                     selectedChair = value;
                     RaisePropertyChanged("SelectedChair");
                 }
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

        #region SelectedTeacher

        /// <summary>
        /// Выделенный основной преподаватель в списке - поле
        /// </summary>
        private Teacher selectedTeacher;

        /// <summary>
        /// Выделенный в списке основной преподаватель - свойство 
        /// </summary>
        public Teacher SelectedTeacher
        {
            get { return selectedTeacher; }
            set
            {
                if (value == selectedTeacher)
                    return;
                else
                {
                    selectedTeacher = value;
                    RaisePropertyChanged("SelectedTeacher");
                }
            }
        }


        #endregion SelectedTeacher

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

        #endregion Selected Property

        #region IsEnabledComboBoxTeacher

        /// <summary>
        /// Признак доступности списка выбора основного преподавателя
        /// </summary>
        bool isEnabledComboBoxTeacher = false;

        /// <summary>
        /// Признак доступности списка выбора основного преподавателя
        /// </summary>
        public bool IsEnabledComboBoxTeacher
        {
            get { return isEnabledComboBoxTeacher; }
            set 
            {
                if (value == isEnabledComboBoxTeacher)
                    return;
                else
                {
                    isEnabledComboBoxTeacher = value;
                    RaisePropertyChanged("IsEnabledComboBoxTeacher");
                }
            }
        }

        #endregion IsEnabledComboBoxTeacher

        #region IsEnabledComboBoxChair

        /// <summary>
        /// Признак доступности списка выбора кафедры
        /// </summary>
        bool isEnabledComboBoxChair = false;

        /// <summary>
        /// Признак доступности списка выбора кафедры
        /// </summary>
        public bool IsEnabledComboBoxChair
        {
            get { return isEnabledComboBoxChair; }
            set 
            {
                if (value == isEnabledComboBoxChair)
                    return;
                isEnabledComboBoxChair = value;
                RaisePropertyChanged("IsEnabledComboBoxChair");
            }
        }

        #endregion IsEnabledComboBoxChair

        #region IsEnabledComboBoxYear

        /// <summary>
        /// Признак доступности списка выбора учебного года
       /// </summary>
        bool isEnabledComboBoxYear = false;

        /// <summary>
        /// Признак доступности списка выбора учебного года
        /// </summary>
        public bool IsEnabledComboBoxYear
        {
            get { return isEnabledComboBoxYear; }
            set 
            {
                if (value == isEnabledComboBoxYear)
                    return;
                isEnabledComboBoxYear = value;
                RaisePropertyChanged("IsEnabledComboBoxYear");
            }
        }

        #endregion IsEnabledComboBoxYear

        #region  IsEnabledComboBoxLoad

        bool isEnabledComboBoxLoad = false;

        public bool IsEnabledComboBoxLoad
        {
            get { return isEnabledComboBoxLoad; }
            set 
            {
                if (value == isEnabledComboBoxLoad)
                    return;
                isEnabledComboBoxLoad = value;
                RaisePropertyChanged("IsEnabledComboBoxLoad");
            }
        }


        #endregion  IsEnabledComboBoxLoad

        #region teacherAllLoad

        /// <summary>
        /// Обобщенные данные по нагрузке основного преподавателя
        /// </summary>
        TeahingLoadTeacher teacherAllLoad;

        /// <summary>
        /// Обобщенные данные по нагрузке основного преподавателя
        /// </summary>
        public TeahingLoadTeacher TeacherAllLoad
        {
            get { return teacherAllLoad; }
            set
            {
                if (value == teacherAllLoad)
                    return;
                else
                {
                    teacherAllLoad = value;
                    RaisePropertyChanged("teacherAllLoad");
                }
            }
        }

        #endregion teacherAllLoad

        #region Properties for binding

        /// <summary>
        /// Данные по кафедрам
        /// </summary>
        public ObservableCollection<Chair> Chairs { get; private set; }

        /// <summary>
        /// Обобщенные данные по нагрузке кафедры
        /// </summary>
        public TeachingLoadChair LoadChair { get; private set; }

        /// <summary>
        /// Колллекция обобщенных данных по нагрузке кафедры для заданного учебного года
        /// </summary>
        public ObservableCollection<TeachingLoadChair> AllLoadChairAcademicYear { get; private set; }

        /// <summary>
        /// Колллекция обобщенных данных по нагрузке преподавателей кафедры для заданного учебного года
        /// </summary>
        public ObservableCollection<tlsp_getAllLoadTeacher_Result> AllLoadTeacher { get; private set; } 
        

        /// <summary>
        /// Коллекция нагрузки по дисциплинам кафедры
        /// </summary>
        public ObservableCollection<TeachingLoad> LoadChairTeaching { get; private set; }

        /// <summary>
        /// Коллекция нагрузки по дисциплинам по основному преподавателю
        /// </summary>
        public ObservableCollection<LoadTeacher> TeacherLoads { get; private set; }

        /// <summary>
        /// Коллекция преподавателей
        /// </summary>
        public ObservableCollection<Teacher> TeachersChair { get; private set; }

        /// <summary>
        /// Коллекция соответствия дисциплин нагрузки преподавателя дисциплинам кафедры
        /// </summary>
        public ObservableCollection<ChairTeacherLoad> LoadChairTeacher { get; private set; }

      
        
        #endregion Properties

        #region Constructor

        public ReportTeacherLoadViewModel(IServiceLoadTeaher service)
        {

            int idChair = SetInitialChair();

            int idAcademicYear = SetInitialAcademicYear();

            // Создание свойств класса
            CreateNewObject();

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

            base.DisplayName = "Отчет Нагрузка преподавателя";

            ClearList();

        }


        #endregion Constructor()

        #region Вспомогательные методы

        /// <summary>
        /// Установить первоначальное выделение учебного года
        /// </summary>
        /// <returns></returns>
        private int SetInitialAcademicYear()
        {
            int idAcademicYear = 0;
            if (selectedAcademicYear != null)
                idAcademicYear = selectedAcademicYear.Id;
            return idAcademicYear;
        }

        /// <summary>
        /// Установить первоначальное выделение кафедры
        /// </summary>
        /// <returns></returns>
        private int SetInitialChair()
        {
            int idChair = 0;
            if (selectedChair != null)
                idChair = selectedChair.Id;
            return idChair;
        }
        
        /// <summary>
        /// Очистка списков 
        /// </summary>
        private void ClearList()
        {
            LoadChairTeaching.Clear();
            TeacherLoads.Clear();
        }
        
        /// <summary>
        /// Создание экземпляров свойств класса
        /// </summary>
        private void CreateNewObject()
        {
            TeachersChair = new ObservableCollection<Teacher>();
            Chairs = new ObservableCollection<Chair>();
            LoadChairTeaching = new ObservableCollection<TeachingLoad>();
            LoadChair = new TeachingLoadChair();
            TeacherLoads = new ObservableCollection<LoadTeacher>();
            LoadChairTeacher = new ObservableCollection<ChairTeacherLoad>();
            AllLoadTeacher = new ObservableCollection<tlsp_getAllLoadTeacher_Result>();
        }

        #endregion Вспомогательные методы

        #region Command

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
            var service = container.Resolve<IServiceChair>();
            Chairs.Clear();
            if (SelectedFaculty != null)
                Chairs = service.GetChair(Chairs, SelectedFaculty.Id);

            if (SelectedFaculty != null)
                IsEnabledComboBoxChair = true;
            else
                IsEnabledComboBoxChair = false;
        }

        #endregion CommandGetChair

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
        /// Загрузка данных по нагрузке кафедры - метод
        /// </summary>
        private void GetAllLoadChairExecute()
        {
            var service = container.Resolve<IServiceLoadTeaher>();

            if (AllLoadChairAcademicYear != null)
                AllLoadChairAcademicYear.Clear();

            if ((SelectedFaculty != null) && (SelectedChair != null) && (SelectedAcademicYear != null))
            {
                AllLoadChairAcademicYear = service.GetAllLoadChair(AllLoadChairAcademicYear, SelectedChair.Id, selectedAcademicYear.Id);

                var serviceTeacher = container.Resolve<IServiceTeacher>();
                TeachersChair.Clear();
                TeachersChair = serviceTeacher.GetTeachers(TeachersChair, SelectedChair.Id);

                if (selectedAcademicYear != null)
                {
                    IsEnabledComboBoxLoad = true;
                    IsEnabledComboBoxTeacher = true;
                }
                else
                    IsEnabledComboBoxLoad = false;
            }
        }

        #endregion CommandGetAllLoadChair

        #region CommandGetYear

        /// <summary>
        /// Команда - Загрузка обобщенных данных по нагрузке кафедры - поле
        /// </summary>
        private ICommand getYear;

        /// <summary>
        /// Команда - Загрузка обобщенных данных по нагрузке кафедры - Свойство
        /// </summary>
        public ICommand GetYear
        {
            get
            {
                return getYear ??
                    (getYear = new RelayCommand(GetYearExecute));
            }

        }

        /// <summary>
        /// Загрузка данных по нагрузке кафедры - метод
        /// </summary>
        private void GetYearExecute()
        {
            if (SelectedChair != null)
            {
                SelectedAcademicYear = null;
                SelectedNameLoadChair = null;
                IsEnabledComboBoxYear = true;
            }
            else
                IsEnabledComboBoxYear = false;
        }


        #endregion CommandGetYear

        #region CommandGetLoad

        /// <summary>
        /// Команда - Загрузка обобщенных данных по нагрузке кафедры - поле
        /// </summary>
        private ICommand getLoad;

        /// <summary>
        /// Команда - Загрузка обобщенных данных по нагрузке кафедры - Свойство
        /// </summary>
        public ICommand GetLoad
        {
            get
            {
                return getLoad ??
                    (getLoad = new RelayCommand(GetLoadExecute));
            }

        }

        /// <summary>
        /// Загрузка данных по нагрузке кафедры - метод
        /// </summary>
        private void GetLoadExecute()
        {
            if (SelectedAcademicYear != null )
                IsEnabledComboBoxLoad = true;
            else
                IsEnabledComboBoxLoad = false;
        }

        #endregion CommandGetLoad

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
                    (getLoadChair = new RelayCommand(GetLoadChairExecute));
            }
        }

        /// <summary>
        /// Загрузка данных по нагрузке кафедры - метод
        /// </summary>
        private void GetLoadChairExecute()
        {
            LoadChairTeaching.Clear();
            TeacherLoads.Clear();
            SelectedTeacher = null;
            
            var loadChairService = container.Resolve<IServiceLoadChair>();

            if (LoadChairTeaching != null)
                LoadChairTeaching.Clear();

            if ((SelectedFaculty != null) && (SelectedChair != null) && (SelectedAcademicYear != null) && (SelectedNameLoadChair != null))
            {
                LoadChairTeaching = loadChairService.GetLoadChairTeaching(LoadChairTeaching, SelectedChair.Id, selectedAcademicYear.Id, 
                    selectedNameLoadChair.NameLoadChair);
                if (LoadChairTeaching.Count == 0)
                    TeacherLoads.Clear();
                else
                    LoadChair = loadChairService.GetLoadChair(LoadChair, SelectedChair.Id, selectedAcademicYear.Id, selectedNameLoadChair.NameLoadChair);
            }

            //SelectedIndexTeacher = -1;
            isEnabledComboBoxTeacher = false;

        }

        #endregion CommandGetLoadChair

        #endregion Command
    }
        
}
