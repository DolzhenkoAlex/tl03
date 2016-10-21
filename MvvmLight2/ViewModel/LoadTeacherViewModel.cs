using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ServiceData;

namespace MvvmLight2.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// Распределение нагрузки преподавателям кафедры
    /// </summary>
    public class LoadTeacherViewModel : WorkspaceViewModel
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

        #region SelectedChairIndex
         /// <summary>
         /// Поле - Индекс выделенная в списке кафедра
         /// </summary>
         private int selectedChairIndex;

         /// <summary>
         /// Свойство - Индекс выделенна в списке кафедра
         /// </summary>
         public int SelectedChairIndex
         {
             get { return selectedChairIndex; }
             set
             {

                 if (value == selectedChairIndex)
                     return;

                 selectedChairIndex = value;
                 RaisePropertyChanged("SelectedChairIndex");
             }
         }

         #endregion SelectedChairIndex

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

        #region SelectedIndexTeacher

        /// <summary>
        /// Выделенный основной преподаватель в списке - поле
        /// </summary>
        private int selectedIndexTeacher = -1;

        /// <summary>
        /// Выделенный в списке основной преподаватель - свойство 
        /// </summary>
        public int SelectedIndexTeacher
        {
            get { return selectedIndexTeacher; }
            set
            {
                if (value == selectedIndexTeacher)
                    return;
                else
                {
                    selectedIndexTeacher = value;
                    RaisePropertyChanged("SelectedIndexTeacher");
                }
            }
        }

        #endregion SelectedIndexTeacher

        #region SelectedIndexAssistent

        int selectedIndexAssistent = -1;

        public int SelectedIndexAssistent
        {
            get { return selectedIndexAssistent; }
            set
            {
                if (value == selectedIndexAssistent)
                    return;
                else
                {
                    selectedIndexAssistent = value;
                    RaisePropertyChanged("SelectedIndexAssistent");
                }
            }
        }

        #endregion SelectedIndexAssistent

        #region SelectedAssistent

        /// <summary>
        /// Выделенный в списке преподаватель-ассистент  - поле
        /// </summary>
        private Teacher selectedAssistent;

        /// <summary>
        /// Выделенный в списке преподаватель-ассистент
        /// </summary>
        public Teacher SelectedAssistent
        {
            get { return selectedAssistent; }
            set 
            {
                if (value == selectedAssistent)
                    return;
                else
                {
                    selectedAssistent = value;
                    RaisePropertyChanged("SelectedAssistent");
                }
            }
        }

        #endregion SelectedAssistent

        #region SelectedDisciplineTeacher

        /// <summary>
        /// Дисциплина, выбранная в списке дисциплин,  назначенных основному преподавателю
        /// </summary>
        private LoadTeacher selectedDisciplineTeacher;

        /// <summary>
        /// Дисциплина, выбранная в списке дисциплин,  назначенных основному преподавателю
        /// </summary>
        public LoadTeacher SelectedDisciplineTeacher
        {
            get { return selectedDisciplineTeacher; }
            set 
            {
                if (value == selectedDisciplineTeacher)
                    return;
                else
                {
                    selectedDisciplineTeacher = value;
                    RaisePropertyChanged("SelectedDisciplineTeacher");
                }
            }
        }

        #endregion SelectedDisciplineTeacher

        #region SelectedDisciplineChair

        /// <summary>
        /// Выделенная дисциплина с списке дисциплин кафедры для распределения между преподавателяти - поле
        /// </summary>
        private TeachingLoad selectedDisciplineChair;

        /// <summary>
        /// Выделенная дисциплина с списке дисциплин кафедры для распределения между преподавателяти
        /// </summary>
        public TeachingLoad SelectedDisciplineChair
        {
            get { return selectedDisciplineChair; }
            set 
            {
                if (value == selectedDisciplineChair)
                    return;
                else
                {
                    selectedDisciplineChair = value;
                    RaisePropertyChanged("SelectedDisciplineChair");
                }
            }
        }



        #endregion selectedDisciplineChair

        #region SelectedDisciplinesAssistent

        /// <summary>
        /// Дисциплина, выбранная в списке дисциплин,  назначенных ассистенту
        /// </summary>
        private LoadTeacher selectedDisciplinesAssistent;

        /// <summary>
        /// Дисциплина, выбранная в списке дисциплин,  назначенных ассистенту
        /// </summary>
        public LoadTeacher SelectedDisciplinesAssistent
        {
            get { return selectedDisciplinesAssistent; }
            set
            {
                if (value == selectedDisciplinesAssistent)
                    return;
                else
                {
                    selectedDisciplinesAssistent = value;
                    RaisePropertyChanged("SelectedDisciplinesAssistent");
                }
            }
        }

        #endregion SelectedDisciplinesAssistent

        #region SelectedIndexYear

        /// <summary>
        /// Выделенная учебный год в списке
        /// </summary>
        int selectedIndexYear = -1;

        /// <summary>
        /// Выделенная учебный год в списке
        /// </summary>
        public int SelectedIndexYear
        {
            get { return selectedIndexYear; }
            set 
            {
                if (value == selectedIndexYear)
                    return;
                selectedIndexYear = value;
                RaisePropertyChanged("SelectedIndexYear");
            }
        }

        #endregion SelectedIndexYear

        #region SelectedIndexDiscipline

        /// <summary>
        /// Выделенная дисциплина в списке дисциплин кафедры
        /// </summary>
        int selectedIndexDiscipline = -1;

        /// <summary>
        /// Выделенная дисциплина в списке дисциплин кафедры
        /// </summary>
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

        #region SelectedIndexFilter

        /// <summary>
        /// Индекс выделения в фильтре списка дисциплин кафедры
        /// </summary>
        int selectedIndexFilter = 0;

        /// <summary>
        /// Индекс выделения в фильтре списка дисциплин кафедры 
        /// </summary>
        public int SelectedIndexFilter
        {
            get { return selectedIndexFilter; }
            set 
            {
                if (value == selectedIndexFilter)
                    return;
                selectedIndexFilter = value;
                RaisePropertyChanged("SelectedIndexFilter");
            }
        }


        #endregion SelectedIndexFilter


        #endregion SelectedIndexDiscipline

        #region SelectedIndexDisciplineTeacher

        /// <summary>
        /// Выделенный индекс в списке дисциплин основного преподавателя
        /// </summary>
        int selectedIndexDisciplineTeacher;
        
        /// <summary>
        /// Выделенный индекс в списке дисциплин основного преподавателя
        /// </summary>
        public int SelectedIndexDisciplineTeacher
        {
            get { return selectedIndexDisciplineTeacher; }
            set
            {
                if (value == selectedIndexDisciplineTeacher)
                    return;

                selectedIndexDisciplineTeacher = value;
                RaisePropertyChanged("SelectedIndexDisciplineTeacher");
            }
        }

        #endregion SelectedIndexDisciplineTeacher

        #region SelectedIndexDisciplineAssistent

        int selectedIndexDisciplineAssistent;

        public int SelectedIndexDisciplineAssistent
        {
            get { return selectedIndexDisciplineAssistent; }
            set
            {
                if (value == selectedIndexDisciplineAssistent)
                    return;
                selectedIndexDisciplineAssistent = value;
                RaisePropertyChanged("SelectedIndexDisciplineAssistent");
            }
        }

        #endregion SelectedIndexDisciplineAssistent

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

        #region SelectedIndexNameLoadChair

        /// <summary>
        /// Выбранный индекс в списке вариантов нагрузки
        /// </summary>
        int selectedIndexNameLoadChair = -1;

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
       
        #region SelectedIndexFaculty

        /// <summary>
        /// Выбранный индекс в списке  факультетов
        /// </summary>
        int selectedIndexFaculty = -1;

        /// <summary>
        /// Выбранный индекс в списке  факультетов
        /// </summary>
        public int SelectedIndexFaculty
        {
            get { return selectedIndexFaculty; }
            set
            {
                if (value == selectedIndexFaculty)
                    return;
                selectedIndexFaculty = value;
                RaisePropertyChanged("SelectedIndexFaculty");
            }
        }

        #endregion SelectedIndexFaculty

        #region SelectedDistributedLoad

        /// <summary>
        /// Выделенная дисциплина в списке распределения дисциплин
        /// </summary>
        DistributionTeachingLoad selectedDistributedLoad;

        /// <summary>
        /// Выделенная дисциплина в списке распределения дисциплин
        /// </summary>
        public DistributionTeachingLoad SelectedDistributedLoad
        {
            get { return selectedDistributedLoad; }
            set 
            {
                if (value == selectedDistributedLoad)
                    return;
                else
                {
                    selectedDistributedLoad = value;
                    RaisePropertyChanged("SelectedDistributedLoad");
                }
            }
        }


        #endregion SelectedDistributedLoad

        #endregion Selected Property

        #region DistributionLoad
        /// <summary>
        /// Коллекция нагрузки для распределения
        /// </summary>
        //ObservableCollection<DistributionTeachingLoad> distributionLoad;

        /// <summary>
        /// Коллекция нагрузки для распределения
        /// </summary>
        public ObservableCollection<DistributionTeachingLoad> DistributionLoad { get; set; }
        //{
        //    get { return distributionLoad; }
        //    set 
        //    {
        //        if (value == distributionLoad)
        //            return;
        //        distributionLoad = value;
        //        RaisePropertyChanged("DistributionLoad");
                
        //    }
        //} 

        #endregion DistributionLoad

        #region TeacherLoads

        /// <summary>
        /// Коллекция нагрузки по дисциплинам по преподавателю
        /// </summary>
        //ObservableCollection<LoadTeacher> teacherLoads;

        /// <summary>
        /// Коллекция нагрузки по дисциплинам по преподавателю
        /// </summary>
        //public ObservableCollection<LoadTeacher> TeacherLoads
        //{
        //    get { return teacherLoads; }
        //    set 
        //    {
        //        if (value == teacherLoads)
        //            return;
        //        teacherLoads = value;
        //        RaisePropertyChanged("TeacherLoads");
        //    }
        //}

        #endregion TeacherLoads

        #region IsFaultLoad

        /// <summary>
        /// Признак превышения плановой нагрузки преподавателя
        /// </summary>
        bool isFaultLoad = true;

        /// <summary>
        /// Признак превышения плановой нагрузки преподавателя
        /// </summary>
        public bool IsFaultLoad
        {
            get { return isFaultLoad; }
            set 
            {
                if (value == isFaultLoad)
                    return;
                else
                {
                    isFaultLoad = value;
                    RaisePropertyChanged("IsFaultLoad");
                }
            }
        }

        #endregion IsFaultLoad

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

        #region IsEnabledComboBoxAssistant

        /// <summary>
        /// Признак доступности списка выбора ассистента
        /// </summary>
        bool isEnabledComboBoxAssistant = false;

        /// <summary>
        /// Признак доступности списка выбора ассистента
        /// </summary>
        public bool IsEnabledComboBoxAssistant
        {
            get { return isEnabledComboBoxAssistant; }
            set
            {
                if (value == isEnabledComboBoxAssistant)
                    return;
                else
                {
                    isEnabledComboBoxAssistant = value;
                    RaisePropertyChanged("IsEnabledComboBoxAssistant");
                }
            }
        }

        #endregion IsEnabledComboBoxAssistant

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

        #region TeacherAllLoad

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
                    RaisePropertyChanged("TeacherAllLoad");
                }
            }
        }

        #endregion teacherAllLoad

        #region AssistantAllLoad

        /// <summary>
        /// Обобщенные данные по нагрузке преподавателя ассистента
        /// </summary>
        TeahingLoadTeacher assistantAllLoad;

        /// <summary>
        /// Обобщенные данные по нагрузке  преподавателя ассистента
        /// </summary>
        public TeahingLoadTeacher AssistantAllLoad
        {
            get { return assistantAllLoad; }
            set
            {
                if (value == assistantAllLoad)
                    return;
                else
                {
                    assistantAllLoad = value;
                    RaisePropertyChanged("AssistantAllLoad");
                }
            }
        }

        #endregion AssistantAllLoad

        #region MessageSave

        /// <summary>
        /// Предупреждение о необходимости сохранения изменений
        /// </summary>
        string messageSave = String.Empty;

        /// <summary>
        /// Предупреждение о необходимости сохранения изменений
        /// </summary>
        public string MessageSave
        {
            get { return messageSave; }
            set 
            {
                if (value == messageSave)
                    return;
                else
                {
                    messageSave = value;
                    RaisePropertyChanged("MessageSave");
                }
            }
        }

        #endregion MessageSave

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
        private ObservableCollection<LoadTeacher> teacherLoads;

        /// <summary>
        /// Коллекция нагрузки по дисциплинам по основному преподавателю
        /// </summary>
        public ObservableCollection<LoadTeacher> TeacherLoads
        {
            get { return teacherLoads; }
            set { teacherLoads = value; }
        }

        /// <summary>
        /// Коллекция нагрузки по дисциплинам по преподавателю ассистенту
        /// </summary>
        //public ObservableCollection<LoadTeacher> AssistentLoads { get; private set; }
        private ObservableCollection<LoadTeacher> assistentLoads;

        /// <summary>
        /// Коллекция нагрузки по дисциплинам по преподавателю ассистенту
        /// </summary>
        public ObservableCollection<LoadTeacher> AssistentLoads
        {
            get { return assistentLoads; }
            set 
            {
                assistentLoads = value; 
            }
        } 


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

        public LoadTeacherViewModel(IServiceLoadTeaher service)
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

            base.DisplayName = "Нагрузка преподавателя";

            ClearList();

            // Регистрация сообщений об изменении суммарной нагрузки преподавателей
            RegistrationMessengerChangeLoad();
        }


        #endregion Constructor()

        #region Вспомогательные методы

        /// <summary>
        /// Установка null для суммарной нагрузки преподавателя
        /// </summary>
        private void SetSumLoadTeacherNull()
        {
            if (teacherAllLoad != null)
                TeacherAllLoad.SumLoadTeacher = null;
            if (assistantAllLoad != null)
                AssistantAllLoad.SumLoadTeacher = null;
        }

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
            DistributionLoad = new ObservableCollection<DistributionTeachingLoad>();
            TeacherLoads = new ObservableCollection<LoadTeacher>();
            AssistentLoads = new ObservableCollection<LoadTeacher>();
            LoadChairTeacher = new ObservableCollection<ChairTeacherLoad>();
            AllLoadTeacher = new ObservableCollection<tlsp_getAllLoadTeacher_Result>();
        }

        /// <summary>
        /// Регистрация сообщений об изменении суммарной нагрузки преподавателей
        /// </summary>
        private void RegistrationMessengerChangeLoad()
        {
            Messenger.Default.Register<DistributionTeachingLoad>(this,
                x =>
                {
                    if (TeacherAllLoad != null)
                    {
                        // Обновление нагрузки преподавателя по дисциплине 
                        foreach (var disc in TeacherAllLoad.LoadTeacher)
                            if (disc.Id == x.IdLoad)
                                UpdateTeacherLoad(x, disc);

                        // Обновление суммарной нагрузки для основного преподавателя
                        if (SelectedTeacher != null)
                            TeacherAllLoad.SumLoadTeacher = SetSumLoadTeacher(TeacherAllLoad);

                        // Обновление нагрузки ассистента по дисциплине 
                        if (AssistantAllLoad != null)
                        {
                            foreach (var disc in AssistantAllLoad.LoadTeacher)
                                if (disc.Id == x.IdLoad)
                                    UpdateTeacherLoad(x, disc);

                            // Обновление суммарной нагрузки для ассистента
                            if (SelectedAssistent != null)
                                AssistantAllLoad.SumLoadTeacher = SetSumLoadTeacher(AssistantAllLoad);
                        }

                            // Формирование предупредительного сообщения о сохранении изменений
                            MessageSave = "Сохраните изменения в распределении нагрузки !";
                            selectedDistributedLoad.FlagChange = "*";
                    }

                    //Формирование плановых показателей по нагрузке
                    DistributionTeachingLoad loadPlan = new DistributionTeachingLoad();
                    if (DistributionLoad.Count > 0)
                    {
                        SetPlanDisciplineForDistributing(loadPlan);
                    }

                    // Формирование начального значения полей нагрузки
                    //decimal lecture = 0;
                    //decimal practicalExercises = 0;
                    //decimal laboratoryWork = 0;
                    //decimal examination = 0;
                    //decimal setOff = 0;
                    //decimal setOffWithBall = 0;
                    //decimal consultation = 0;
                    //decimal courseProject = 0;
                    //decimal courseWorkt = 0;
                    //decimal practical = 0;
                    //decimal graduationDesign = 0;
                    //decimal controlWork = 0;
                    //decimal gac = 0;
                    //decimal dot = 0;
                    //decimal scientificResearchWork = 0;
                    //decimal others = 0;
                    //decimal sumLoad = 0;

                    //Формирование значений распределенной нагрузки по видам нагрузки
                    LoadDiscipline loadDiscipline = new LoadDiscipline();
                    loadDiscipline = SetLoadDiscipline(DistributionLoad);

                    // Установление факта (IsLoad) полного распределения нагрузки по дисциплине преподавателям кафедры
                    if (SelectedDisciplineChair != null)
                    {
                        if (loadDiscipline.SumLoad == loadPlan.SumLoad)
                            SelectedDisciplineChair.IsLoad = true;
                        else
                            SelectedDisciplineChair.IsLoad = false;
                    }

                    // Проверка превышения плана распределения нагрузки по дисциплине
                    if (SelectedDistributedLoad != null)
                    {
                        SelectedDistributedLoad.IsFaultLoad = VerificationWorkloadDistribution(loadPlan, loadDiscipline);
                    }
                });
        }

        /// <summary>
        /// Формирование значений распределенной нагрузки по видам нагрузки
        /// </summary>
        /// <param name="loadDiscipline"></param>
        private LoadDiscipline SetLoadDiscipline( ObservableCollection<DistributionTeachingLoad> distributionLoad)
        {
            LoadDiscipline loadDiscipline = new LoadDiscipline();
            int count = distributionLoad.Count;
            if (count > 1)
            {
                for (int i = 1; i < count; i++)
                {
                    loadDiscipline.Lecture += (decimal)distributionLoad[i].Lecture;
                    loadDiscipline.PracticalExercises += (decimal)distributionLoad[i].PracticalExercises;
                    loadDiscipline.LaboratoryWork += (decimal)distributionLoad[i].LaboratoryWork;
                    loadDiscipline.Examination += (decimal)distributionLoad[i].Examination;
                    loadDiscipline.SetOff += (decimal)distributionLoad[i].SetOff;
                    loadDiscipline.SetOffWithBall += (decimal)distributionLoad[i].SetOffWithBall;
                    loadDiscipline.Consultation += (decimal)distributionLoad[i].Consultation;
                    loadDiscipline.CourseProject += (decimal)distributionLoad[i].CourseProject;
                    loadDiscipline.CourseWorkt += (decimal)distributionLoad[i].CourseWorkt;
                    loadDiscipline.Practical += (decimal)distributionLoad[i].Practical;
                    loadDiscipline.GraduationDesign += (decimal)distributionLoad[i].GraduationDesign;
                    loadDiscipline.ControlWork += (decimal)distributionLoad[i].ControlWork;
                    loadDiscipline.Gac += (decimal)distributionLoad[i].Gac;
                    loadDiscipline.Dot += (decimal)distributionLoad[i].Dot;
                    loadDiscipline.ScientificResearchWork += (decimal)distributionLoad[i].ScientificResearchWork;
                    loadDiscipline.Others += (decimal)distributionLoad[i].Others;
                    loadDiscipline.SumLoad += (decimal)distributionLoad[i].SumLoad;
                }
            }

            return loadDiscipline;
        }

        /// <summary>
        /// Проверка превышения распределенной нагрузке по дисциплине
        /// </summary>
        /// <param name="loadPlan"></param>
        /// <param name="loadDiscipline"></param>
        private bool VerificationWorkloadDistribution(DistributionTeachingLoad loadPlan, LoadDiscipline loadDiscipline)
        {
                if ((loadDiscipline.Lecture <= loadPlan.Lecture)
                    && (loadDiscipline.PracticalExercises <= loadPlan.PracticalExercises)
                    && (loadDiscipline.LaboratoryWork <= loadPlan.LaboratoryWork)
                    && (loadDiscipline.PracticalExercises <= loadPlan.PracticalExercises)
                    && (loadDiscipline.Examination <= loadPlan.Examination)
                    && (loadDiscipline.SetOff <= loadPlan.SetOff)
                    && (loadDiscipline.SetOffWithBall <= loadPlan.SetOffWithBall)
                    && (loadDiscipline.Consultation <= loadPlan.Consultation)
                    && (loadDiscipline.CourseProject <= loadPlan.CourseProject)
                    && (loadDiscipline.GraduationDesign <= loadPlan.GraduationDesign)
                    && (loadDiscipline.Gac <= loadPlan.Gac)
                    && (loadDiscipline.Practical <= loadPlan.Practical)
                    && (loadDiscipline.ControlWork <= loadPlan.CourseWorkt)
                    && (loadDiscipline.Dot <= loadPlan.Dot)
                    && (loadDiscipline.ScientificResearchWork <= loadPlan.ScientificResearchWork)
                    && (loadDiscipline.Others <= loadPlan.Others))
                    return true;
                else
                    return false;
        }

        /// <summary>
        /// Установиль плановую нагрузку по дисциплине для распределения преподавателям 
        /// </summary>
        /// <param name="loadPlan"></param>
        private void SetPlanDisciplineForDistributing(DistributionTeachingLoad loadPlan)
        {
            loadPlan.Lecture = DistributionLoad[0].Lecture;
            loadPlan.PracticalExercises = DistributionLoad[0].PracticalExercises;
            loadPlan.LaboratoryWork = DistributionLoad[0].LaboratoryWork;
            loadPlan.Examination = DistributionLoad[0].Examination;
            loadPlan.SetOff = DistributionLoad[0].SetOff;
            loadPlan.SetOffWithBall = DistributionLoad[0].SetOffWithBall;
            loadPlan.Consultation = DistributionLoad[0].Consultation;
            loadPlan.CourseProject = DistributionLoad[0].CourseProject;
            loadPlan.CourseWorkt = DistributionLoad[0].CourseWorkt;
            loadPlan.Practical = DistributionLoad[0].Practical;
            loadPlan.GraduationDesign = DistributionLoad[0].GraduationDesign;
            loadPlan.ControlWork = DistributionLoad[0].ControlWork;
            loadPlan.Gac = DistributionLoad[0].Gac;
            loadPlan.Dot = DistributionLoad[0].Dot;
            loadPlan.ScientificResearchWork = DistributionLoad[0].ScientificResearchWork;
            loadPlan.Others = DistributionLoad[0].Others;
            loadPlan.SumLoad = DistributionLoad[0].SumLoad;
        }

        /// <summary>
        /// Формирование нагрузки по дисциплине для включения в нагрузку преподавателя
        /// </summary>
        /// <param name="lt"></param>
        /// <returns></returns>
        private DistributionTeachingLoad SetDistributionTeachingLoad(LoadTeacher lt, Teacher teacher)
        {
            DistributionTeachingLoad load = new DistributionTeachingLoad();

            // Сформировать плановую нагрузку

            load.IdLoad = lt.Id;
            load.NameGroup = lt.NameGroup;
            load.Student = lt.Student;
            load.Subgroup = lt.Subgroup;
            load.ForeignStudent = lt.ForeignStudent;
            load.Speciality = lt.Speciality;
            load.Profile = lt.Profile;
            load.Qualification = lt.Qualification;
            load.Discipline = lt.Discipline;
            load.FormEducation = lt.FormEducation;
            load.ShortNameFaculty = lt.ShortNameFaculty;
            load.Code = lt.Code;
            load.Semester = lt.Semester;
            load.SemesterYear = lt.SemesterYear;
            load.Course = lt.Course;
            if (lt.Lecture != null)
                load.Lecture = (decimal)lt.Lecture;
            if (lt.PracticalExercises != null)
                load.PracticalExercises = (decimal)lt.PracticalExercises;
            if (lt.LaboratoryWork != null)
                load.LaboratoryWork = (decimal)lt.LaboratoryWork;
            if (lt.Examination != null)
                load.Examination = (decimal)lt.Examination;
            if (lt.SetOff != null)
                load.SetOff = (decimal)lt.SetOff;
            if (lt.SetOffWithBall != null)
                load.SetOffWithBall = (decimal)lt.SetOffWithBall;
            if (lt.Consultation != null)
                load.Consultation = (decimal)lt.Consultation;
            if (lt.CourseProject != null)
                load.CourseProject = (decimal)lt.CourseProject;
            if (lt.CourseWorkt != null)
                load.CourseWorkt = (decimal)lt.CourseWorkt;
            if (lt.Practical != null)
                load.Practical = (decimal)lt.Practical;
            if (lt.GraduationDesign != null) 
                load.GraduationDesign = (decimal)lt.GraduationDesign;
            if (lt.ControlWork != null)
                load.ControlWork = (decimal)lt.ControlWork;
            if (lt.Gac != null)
                load.Gac = (decimal)lt.Gac;
            if (lt.Dot != null)
                load.Dot = (decimal)lt.Dot;
            if (lt.ScientificResearchWork!= null)
                load.ScientificResearchWork = (decimal)lt.ScientificResearchWork;
            if (lt.Others != null)
                load.Others = (decimal)lt.Others;
            if (lt.SumLoad != null)
                load.SumLoad = (decimal)lt.SumLoad;
            load.FlagChange = String.Empty; ;
            load.IsFaultLoad = true;

            load.SumUnload = 0;
            load.Status = teacher.LastName + " " +
                teacher.FirstName.Substring(0, 1) + "." +
                teacher .SecondName.Substring(0, 1) + ". ";
            return load;
        }

        /// <summary>
        /// Установить плановую нагрузку по дисциплине
        /// </summary>
        private void SetPlanLoad(TeachingLoad tl)
        {
            if (tl != null)
            {
                DistributionTeachingLoad load = new DistributionTeachingLoad();

                // Сформировать плановую нагрузку
                load.IdLoad = tl.Id;
                load.NameGroup = tl.NameGroup;
                load.Student = tl.Student;
                load.Subgroup = tl.Subgroup;
                load.ForeignStudent = tl.ForeignStudent;
                load.Speciality = tl.Speciality;
                load.Profile = tl.Profile;
                load.Qualification = tl.Qualification;
                load.Discipline = tl.Discipline;
                load.FormEducation = tl.FormEducation;
                load.ShortNameFaculty = tl.ShortNameFaculty;
                load.Code = tl.Code;
                load.Semester = tl.Semester;
                load.SemesterYear = tl.SemesterYear;
                load.Course = tl.Course;
                load.Lecture = (decimal)tl.Lecture;
                load.PracticalExercises = (decimal)tl.PracticalExercises;
                load.LaboratoryWork = (decimal)tl.LaboratoryWork;
                load.Examination = (decimal)tl.Examination;
                load.SetOff = (decimal)tl.SetOff;
                load.SetOffWithBall = (decimal)tl.SetOffWithBall;
                load.Consultation = (decimal)tl.Consultation;
                load.CourseProject = (decimal)tl.CourseProject;
                load.CourseWorkt = (decimal)tl.CourseWorkt;
                load.Practical = (decimal)tl.Practical;
                load.GraduationDesign = (decimal)tl.GraduationDesign;
                load.ControlWork = (decimal)tl.ControlWork;
                load.Gac = (decimal)tl.Gac;
                load.Dot = (decimal)tl.Dot;
                load.ScientificResearchWork = (decimal)tl.ScientificResearchWork;
                load.Others = (decimal)tl.Others;
                load.SumLoad = (decimal)tl.SumLoad;
                load.FlagChange = String.Empty; ;
                load.IsFaultLoad = true;
                load.IsLoad = (bool)tl.IsLoad;

                load.SumUnload = (decimal)tl.SumUnload;
                load.Status = "План";

                DistributionLoad.Add(load);
            }
            else
                DistributionLoad.Clear();
        }


        /// <summary>
        /// Обновление нагрузки по дисциплине преподавателя
        /// </summary>
        /// <param name="x"></param>
        /// <param name="qual"></param>
        private void UpdateTeacherLoad(DistributionTeachingLoad x, LoadTeacher disc)
        {
            disc.Lecture = x.Lecture;
            disc.PracticalExercises = x.PracticalExercises;
            disc.LaboratoryWork = x.LaboratoryWork;
            disc.Examination = x.Examination;
            disc.SetOff = x.SetOff;
            disc.SetOffWithBall = x.SetOffWithBall;
            disc.Consultation = x.Consultation;
            disc.CourseProject = x.CourseProject;
            disc.CourseWorkt = x.CourseWorkt;
            disc.Practical = x.Practical;
            disc.GraduationDesign = x.GraduationDesign;
            disc.ControlWork = x.ControlWork;
            disc.Gac = x.Gac;
            disc.Dot = x.Dot;
            disc.ScientificResearchWork = x.ScientificResearchWork;
            disc.Others = x.Others;
            disc.SumLoad = x.SumLoad;
        }

        /// <summary>
        /// Установить выделение преподавателя/ассистента  в списке  преподавателей/ассистентов,
        /// для которого назначена дисциплина кафедры
        /// </summary>
        private Teacher GetTeacher(int id)
        {
            // Поиск основного преподавателя, за которым закреплена выбранная дисциплина 
            Teacher teacher = new Teacher();
             teacher = FindTeacher(
                TeachersChair,
                id,
                (x, y) => x.Id.Equals(y));
             return teacher;
        }

        /// <summary>
        /// Установить выделение дисциплины в списке дисциплин  преподавателя/ассистента.
        /// Дисциплина преподавателя должна соответствовать назначеной ему дисциплины кафедры
        /// </summary>
        private LoadTeacher SetSelectedDisciplinesTeacher(int id, ObservableCollection<LoadTeacher> LoadTeacher)
        {
            
            // Поиск дисциплины, закрепленной за основным преподавателем в списке дисциплин преподавателя 
            //if (selectedTeacher != null)
            if (LoadTeacher != null)
                return FindDisciplineTeacher(
                    LoadTeacher,
                    id,
                    (x, y) => x.Id.Equals(y));
            else
                return null;
        }

        /// <summary>
        /// Поиск преподавателя с закрепленной нагрузкой
        /// </summary>
        /// <param name="teachers">Коллекция преподавателей кафедры </param>
        /// <param name="id">код преподавателя</param>
        /// <param name="predicate">делегат-предикат</param>
        /// <returns></returns>
        private Teacher FindTeacher(
            ObservableCollection<Teacher> teachers,
            int id,
            Func<Teacher, int, bool> predicate)
        {
            Teacher teach = new Teacher();
            foreach (var teacher in teachers)
            {
                if (predicate(teacher, id))
                {
                    teach = teacher;
                    break;
                }
            }
            return teach;
        }

        /// <summary>
        /// Поиск в списке дисциплин преподавателя дисциплины,
        /// выделенной в списке дисциплин кафедры.
        /// Такой дисциплины может и не быть!
        /// </summary>
        /// <param name="teacherLoads">Учебная нагрузка преподавателя</param>
        /// <param name="id">код дисциплины</param>
        /// <param name="predicate">делегат-предикат</param>
        /// <returns></returns>
        private LoadTeacher FindDisciplineTeacher(
            ObservableCollection<LoadTeacher> teacherLoads,
            int id,
            Func<LoadTeacher, int, bool> predicate)
        {
            LoadTeacher discipline = new LoadTeacher();
            foreach (var disc in teacherLoads)
            {
                if (predicate(disc, id))
                    discipline = disc;
            }
            return discipline;
        }

        /// <summary>
        /// Поиск дисциплины, назначенной преподавателю, в списке распределяемых дисциплин 
        /// </summary>
        /// <param name="loadsAllTeacher"></param>
        /// <param name="nameTeacher"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private DistributionTeachingLoad  FindDistributionDisciplineTeacher(
            ObservableCollection<DistributionTeachingLoad> disciplines,
            string nameTeacher,
            Func<DistributionTeachingLoad, string, bool> predicate)
        {
            DistributionTeachingLoad discipline = new DistributionTeachingLoad();
            foreach(var disc in disciplines)
            {
                if (predicate(disc, nameTeacher))
                    discipline = disc;
            }
            return discipline;
        }

        /// <summary>
        /// Проверка наличия дисциплины преподавателя в списке распределяетых дисциплин
        /// </summary>
        /// <param name="disciplines"></param>
        /// <param name="nameTeacher"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private bool IsDistributionDisciplineTeacher(
            ObservableCollection<DistributionTeachingLoad> disciplines,
            string nameTeacher,
            Func<DistributionTeachingLoad, string, bool> predicate)
        {
            bool isDiscipline = false;
            foreach (var disc in disciplines)
            {
                if (predicate(disc, nameTeacher))
                    isDiscipline = true;
            }
            return isDiscipline;
        }

        /// <summary>
        /// Поиск записи соответствия дисциплины плановой нагрузки кафедры и дисциплины преподавателя 
        /// </summary>
        /// <param name="chairTeacherLoads"></param>
        /// <param name="loadTeacher"></param>
        /// <param name="speciality"></param>
        /// <param name="predicat"></param>
        /// <returns></returns>
        private ChairTeacherLoad FindChairTeacherLoad(
            ObservableCollection<ChairTeacherLoad> chairTeacherLoads,
            LoadTeacher loadTeacher,
            Teacher teacher,
            Func<ChairTeacherLoad, LoadTeacher, Teacher, bool> predicat)
        {
            ChairTeacherLoad teacherChairLoad = new ChairTeacherLoad();
            foreach(var load in chairTeacherLoads)
            {
                if (predicat(load, loadTeacher, teacher))
                    teacherChairLoad = load;
            }
            return teacherChairLoad;
        }

        /// <summary>
        /// Поиск дисциплины кафедры
        /// </summary>
        /// <param name="teachingLoads"></param>
        /// <param name="chairTeacherLoad"></param>
        /// <param name="predicat"></param>
        /// <returns></returns>
        private TeachingLoad FindDisciplineChair(
            ObservableCollection<TeachingLoad> teachingLoads,
            ChairTeacherLoad chairTeacherLoad,
            Func<TeachingLoad, ChairTeacherLoad, bool> predicat)
        {
            TeachingLoad teachingload = new TeachingLoad();
            foreach (var load in teachingLoads)
            {
                if (predicat(load, chairTeacherLoad))
                    teachingload = load;
            }
            return teachingload;
        }

        /// <summary>
        /// Поиск распределенных дисциплин кафедры
        /// </summary>
        /// <param name="teachingLoads"></param>
        /// <param name="isLoad"></param>
        /// <param name="predicat"></param>
        /// <returns></returns>
        private TeachingLoad FindDisciplineIsLoad(
            ObservableCollection<TeachingLoad> teachingLoads,
            bool isLoad,
            Func<TeachingLoad, bool, bool> predicat)
        {
            TeachingLoad disciplineIsLoad = new TeachingLoad();
            foreach (var load in teachingLoads)
            {
                if (predicat(load, isLoad))
                    disciplineIsLoad = load;
            }
            return disciplineIsLoad;
        }


        /// <summary>
        /// Проверка наличия дисциплине в списке дисциплин преподавателя
        /// </summary>
        /// <param name="LoadTeacher">Учебная нагрузка преподавателя</param>
        /// <param name="mainTeacher">основной преподаватель</param>
        /// <param name="predicate">делегат-предикат</param>
        /// <returns></returns>
        private bool ContainDiscipline(
            LoadTeacher disciplineTeacher,
            TeachingLoad selectDisciplineChair,
            ChairTeacherLoad chairTeacherLoad,
            Func<LoadTeacher, TeachingLoad, ChairTeacherLoad, bool> predicate)
        {
            bool contain = false;
            if (predicate(disciplineTeacher, selectDisciplineChair, chairTeacherLoad))
            {
                contain = true;
            }
            return contain;
        }

        /// <summary>
        /// Формирование данных по связи дисциплин кафедры и дисциплин нагрузки преподавателя
        /// </summary>
        /// <param name="loadTeacher"></param>
        /// <returns></returns>
        private ChairTeacherLoad NewChairTeacherLoad(LoadTeacher loadTeacher, 
            int idTeacher, int idLoadTeacher, int idDiscipline, string nameLoadChair, int status )
        {
            ChairTeacherLoad newChairTeacherLoad = new ChairTeacherLoad();
            newChairTeacherLoad.IdTeacher = idTeacher;
            newChairTeacherLoad.IdLoadTeacher = idLoadTeacher;
            newChairTeacherLoad.IdTeachingLoad = idDiscipline;
            newChairTeacherLoad.NameLoadChair = nameLoadChair;
            newChairTeacherLoad.Status = status;
            return newChairTeacherLoad;
        }

        /// <summary>
        /// Формирование новой дисциплины для добавления в список дисциплин нагрузки преподавателя
        /// </summary>
        /// <param name="service"></param>
        private void NewDisciplineTeacher(LoadTeacher loadTeacher, int idTeacherAllLoad, TeachingLoad selectedDiscipline)
        {
            loadTeacher.IdTeachingLoadTeacher = idTeacherAllLoad;
            loadTeacher.NameGroup = selectedDiscipline.NameGroup;
            loadTeacher.Student = selectedDiscipline.Student;
            loadTeacher.Subgroup = selectedDiscipline.Subgroup;
            loadTeacher.ForeignStudent = selectedDiscipline.ForeignStudent;
            loadTeacher.Speciality = selectedDiscipline.Speciality;
            loadTeacher.Profile = selectedDiscipline.Profile;
            loadTeacher.Qualification = selectedDiscipline.Qualification;
            loadTeacher.Discipline = selectedDiscipline.Discipline;
            loadTeacher.FormEducation = selectedDiscipline.FormEducation;
            loadTeacher.ShortNameFaculty = selectedDiscipline.ShortNameFaculty;
            loadTeacher.Code = selectedDiscipline.Code;
            loadTeacher.Semester = selectedDiscipline.Semester;
            loadTeacher.SemesterYear = selectedDiscipline.SemesterYear;
            loadTeacher.Course = selectedDiscipline.Course;
        }

        /// <summary>
        /// Вычисление суммарной нагрузки преподавателя
        /// </summary>
        /// <returns></returns>
        private decimal? SetSumLoadTeacher(TeahingLoadTeacher teacherAllLoad)
        {
            decimal? sum = 0;
            if ((teacherAllLoad != null) && (teacherAllLoad.LoadTeacher.Count > 0))
            {
                foreach (var sumDisc in teacherAllLoad.LoadTeacher)
                    sum += sumDisc.SumLoad;
            }

            return sum;
        }

        /// <summary>
        /// Обновление текущих данных по дисциплинам кафедры, обобщенной нагрузке кафедры и обобщенной нагрузке преподавателя
        /// </summary>
        /// <param name="loadService"></param>
        private void UpdateCurrentData(IServiceLoadTeaher loadService)
        {
            int selIndexMainTeacher = selectedIndexTeacher;
            int selIndexAssistent = selectedIndexAssistent;
            int selIndexDisciplineChair = SelectedIndexDiscipline;
            int selIndexDisciplineMainTeacher = selectedIndexDisciplineTeacher;
            int selIndexDisciplineAssistent = selectedIndexDisciplineAssistent;


            AssistentLoads.Clear();
            SelectedAssistent = null;
            TeacherLoads.Clear();
            SelectedTeacher = null;

            if (TeacherAllLoad != null)
                TeacherAllLoad.SumLoadTeacher = null;

            SetLoadChairTeaching();
            
            SelectedIndexDiscipline = selIndexDisciplineChair;
            SelectedIndexTeacher = selIndexMainTeacher;
            
            // Обновление обобщенных данных по нагрузке преподавателя
            TeacherAllLoad = loadService.GetTeahingLoadTeacher(selectedTeacher.Id, selectedAcademicYear.Id, selectedNameLoadChair.NameLoadChair);
            // Обновление дисциплин преподавателя
            TeacherLoads.Clear();
            TeacherLoads = loadService.GetLoadTeacher(TeacherAllLoad.Id, TeacherLoads);

            SelectedIndexDisciplineTeacher = selIndexDisciplineMainTeacher;
            IsEnabledComboBoxAssistant = true;

            if (selectedIndexAssistent > -1)
            {
                AssistantAllLoad = loadService.GetTeahingLoadTeacher(selectedAssistent.Id, selectedAcademicYear.Id, selectedNameLoadChair.NameLoadChair);
                // Обновление дисциплин ассистента
                AssistentLoads.Clear();
                AssistentLoads = loadService.GetLoadTeacher(AssistantAllLoad.Id, AssistentLoads);

                SelectedIndexDisciplineAssistent = selIndexDisciplineAssistent;
            }

            SelectedIndexAssistent = selIndexAssistent;
        }

        /// <summary>
        /// Задание нагрузки по дисциплинам кафедры по фильтру (все, распределенные, нераспределенные)
        /// </summary>
        private void SetLoadChairTeaching()
        {
            var loadChairService = container.Resolve<IServiceLoadChair>();

            if (LoadChairTeaching != null)
                LoadChairTeaching.Clear();

            if ((SelectedFaculty != null) && (SelectedChair != null) && (SelectedAcademicYear != null) && (SelectedNameLoadChair != null))
            {
                if (selectedIndexFilter == 0)
                    LoadChairTeaching = loadChairService.GetLoadChairTeaching(LoadChairTeaching, SelectedChair.Id, selectedAcademicYear.Id,
                        selectedNameLoadChair.NameLoadChair);
                if (selectedIndexFilter == 1)
                    LoadChairTeaching = loadChairService.GetDisciplinesChairIsLoad(LoadChairTeaching, SelectedChair.Id, selectedAcademicYear.Id,
                        selectedNameLoadChair.NameLoadChair, true);
                if (selectedIndexFilter == 2)
                    LoadChairTeaching = loadChairService.GetDisciplinesChairIsLoad(LoadChairTeaching, SelectedChair.Id, selectedAcademicYear.Id,
                        selectedNameLoadChair.NameLoadChair, false);

                LoadChair.SumLoadChair = null;
                LoadChair.SumUnloadChair = null;
                if (LoadChairTeaching.Count == 0)
                    TeacherLoads.Clear();
                else
                    LoadChair = loadChairService.GetLoadChair(LoadChair, SelectedChair.Id, selectedAcademicYear.Id, selectedNameLoadChair.NameLoadChair);
            }
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
            //SelectedChairIndex = -1;

            SetSumLoadTeacherNull();

            if (SelectedIndexFaculty > -1)
                IsEnabledComboBoxChair = true;
            else
                IsEnabledComboBoxChair = false;

            SelectedAcademicYear = null;
            SelectedNameLoadChair = null;

            if (LoadChair != null)
            {
                LoadChair.SumLoadChair = null;
                LoadChair.SumUnloadChair = null;
            }
        }

        #endregion CommandGetCommandLoadChair

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
            if (LoadChair != null)
            {
                LoadChair.SumLoadChair = null;
                LoadChair.SumUnloadChair = null;
            }

            SetSumLoadTeacherNull();

            var service = container.Resolve<IServiceLoadTeaher>();
            var serviceTeacher = container.Resolve<IServiceTeacher>();

            if (AllLoadChairAcademicYear != null)
                AllLoadChairAcademicYear.Clear();

            if ((SelectedFaculty != null) && (SelectedChair != null) && (SelectedAcademicYear != null))
            {
                AllLoadChairAcademicYear = service.GetAllLoadChair(AllLoadChairAcademicYear, SelectedChair.Id, selectedAcademicYear.Id);

                
                TeachersChair.Clear();
                TeachersChair = serviceTeacher.GetTeachers(TeachersChair, SelectedChair.Id);

                if (selectedAcademicYear != null)
                    IsEnabledComboBoxLoad = true;
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
            if (SelectedChairIndex > -1)
            {
                //SelectedAcademicYear = null;
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
            AssistentLoads.Clear();
            SelectedAssistent = null;
            TeacherLoads.Clear();
            SelectedTeacher = null;
           
            SetSumLoadTeacherNull();
            SetLoadChairTeaching();

            SelectedIndexTeacher = -1;
            isEnabledComboBoxTeacher = false;
        }

        #endregion CommandGetLoadChair

        #region CommandGetTeachingLoadTeacher

        /// <summary>
        /// Команда - Загрузка данных по дисциплинам преподавателя - поле
        /// </summary>
        private ICommand getTeachingLoadTeacher;

        /// <summary>
        /// Команда - Загрузка данных по дисциплинам преподавателя - Свойство
        /// </summary>
        public ICommand GetTeachingLoadTeacher
        {
            get
            {
                return getTeachingLoadTeacher ??
                    (getTeachingLoadTeacher = new RelayCommand(GetTeachingLoadTeacherExecute, CanExecuteGetTeachingLoadTeacher));
            }
        }

        /// <summary>
        /// Загрузка данных по дисциплинам преподавателя - метод
        /// </summary>
        private void GetTeachingLoadTeacherExecute()
        {
            if ((SelectedTeacher != null) && (selectedAcademicYear != null))
            {
                // Формирование данных по нагрузке основного преподавателя
                SetTeacherDisciplines();
                
                IsEnabledComboBoxAssistant = true;
            }
        }

        /// <summary>
        /// Получить дисциплины для преподавателя
        /// </summary>
        private void SetTeacherDisciplines()
        {
            var LoadTeacherervice = container.Resolve<IServiceLoadTeaher>();

            // Обобщенные данные по нагрузке преподавателя
            TeacherAllLoad = LoadTeacherervice.GetTeahingLoadTeacher(selectedTeacher.Id,
                                                                          selectedAcademicYear.Id,
                                                                          selectedNameLoadChair.NameLoadChair);

            if (TeacherLoads != null)
                TeacherLoads.Clear();

            SelectedIndexDisciplineTeacher = -1;

            // Дисциплины преподавателя
            TeacherLoads = LoadTeacherervice.GetLoadTeacher(TeacherAllLoad.Id, TeacherLoads);
            
            // Данные соответствия дисциплин преподавателя и плановых дисциплин кафедры
            LoadChairTeacher = LoadTeacherervice.GetChairTeacherLoad(selectedTeacher.Id, 1,
                                                                      selectedNameLoadChair.NameLoadChair);

            int countDistridutionLoad = DistributionLoad.Count;
            if (countDistridutionLoad > 1)
                for (int i = countDistridutionLoad - 1; i > 0; i--)
                    DistributionLoad.RemoveAt(i);
            // Формирование списка распределения нагрузки для дисциплин преподавателя
                if (TeacherLoads.Count > 0)
                {
                    foreach (LoadTeacher lt in TeacherLoads)
                    {
                        foreach (ChairTeacherLoad ctl in LoadChairTeacher)
                        {
                            if (selectedDisciplineChair != null)
                            {
                                if (ContainDiscipline(lt, selectedDisciplineChair, ctl,
                                    (x, y, z) => (x.Id).Equals(z.IdLoadTeacher) && (y.Id).Equals(z.IdTeachingLoad)))
                                {
                                    DistributionTeachingLoad load = SetDistributionTeachingLoad(lt, selectedTeacher);
                                    DistributionLoad.Insert(1,load);

                                    if (ctl.IdTeachingLoad == selectedDisciplineChair.Id)
                                    {
                                        SelectedDisciplineTeacher = null;
                                        SelectedDisciplineTeacher = SetSelectedDisciplinesTeacher((int)ctl.IdLoadTeacher, TeacherLoads);

                                    }

                                }
                            }
                        }
                    }

                    //////////////////////////////////////////////////////
                    // Добавить дисциплины ассистента в список распределения дисциплин
                    #region Формирование данных по нагрузке ассистента


                    if (selectedDisciplineChair != null)
                    {
                        //bool isDisciplineAssistent = false;

                        /// Коллекция данных соответствия распределяемой дисциплины кафедры дисциплинам преподавателей
                        ObservableCollection<ChairTeacherLoad> chairTeacherLoads = new ObservableCollection<ChairTeacherLoad>();
                        chairTeacherLoads = LoadTeacherervice.GetChairAssistentLoad(selectedDisciplineChair.Id, 2);

                        if ((chairTeacherLoads != null) && (chairTeacherLoads.Count > 0))
                        {
                            IsEnabledComboBoxAssistant = true;

                            foreach (ChairTeacherLoad ctl in chairTeacherLoads)
                            {
                                Teacher assistent = GetTeacher((int)ctl.IdTeacher);
                                AssistantAllLoad = LoadTeacherervice.GetTeahingLoadTeacher(assistent.Id, selectedAcademicYear.Id, selectedNameLoadChair.NameLoadChair);
                                AssistentLoads.Clear();
                                AssistentLoads = LoadTeacherervice.GetLoadTeacher(AssistantAllLoad.Id, AssistentLoads);

                                LoadTeacher disciplinesAssistent = SetSelectedDisciplinesTeacher((int)ctl.IdLoadTeacher, AssistentLoads);
                                DistributionLoad.Add(SetDistributionTeachingLoad(disciplinesAssistent, assistent));
                            }

                            Teacher currentTeacher = new Teacher();
                            currentTeacher = selectedAssistent;

                            // Установить выделение ассистента 
                            SelectedAssistent = GetTeacher((int)chairTeacherLoads[0].IdTeacher);
                        }
                        else
                        {
                            SelectedIndexAssistent = -1;
                            if (assistantAllLoad != null)
                                AssistantAllLoad.SumLoadTeacher = null;
                        }
                    }

                    #endregion Формирование данных по нагрузке ассистента
                }
                else
                {
                    SelectedIndexDisciplineTeacher = 0;
                    SelectedIndexAssistent = -1;
                }
        }

        /// <summary>
        /// Признак доступности команды 
        /// </summary>
        /// <returns></returns>
        private bool CanExecuteGetTeachingLoadTeacher()
        {
            return selectedDisciplineChair != null ;
        }

        #endregion CommandGetTeachingLoadTeacher

        #region CommandGetTeachingLoadAssistent

        /// <summary>
        /// Команда - Загрузка данных по дисциплинам ассистента - поле
        /// </summary>
        private ICommand getTeachingLoadAssistent;

        /// <summary>
        /// Команда - Загрузка данных по дисциплинам ассистента - Свойство
        /// </summary>
        public ICommand GetTeachingLoadAssistent
        {
            get
            {
                return getTeachingLoadAssistent ??
                    (getTeachingLoadAssistent = new RelayCommand(GetTeachingLoadAssistentExecute, CanExecuteGetTeachingLoadAssistent));
            }
        }

        /// <summary>
        /// Загрузка данных по дисциплинам ассистента - метод
        /// </summary>
        private void GetTeachingLoadAssistentExecute()
        {
            if (AssistentLoads != null)
                AssistentLoads.Clear();

            if ((selectedAssistent != null) && (selectedAcademicYear != null))
            {
                SetAssistentDisciplines(selectedAssistent);
            }
        }

        /// <summary>
        /// Установить дисциплины ассистента 
        /// </summary>
        /// <param name="selAssistent"></param>
        private void SetAssistentDisciplines(Teacher selAssistent)
        {
            var LoadTeacherervice = container.Resolve<IServiceLoadTeaher>();

            if (selectedNameLoadChair != null)
            {
                // Обобщенные данные по нагрузке ассистента
                AssistantAllLoad = LoadTeacherervice.GetTeahingLoadTeacher(selAssistent.Id,
                                                                                   selectedAcademicYear.Id,
                                                                                   selectedNameLoadChair.NameLoadChair);
                // Дисциплины ассистента
                AssistentLoads = LoadTeacherervice.GetLoadTeacher(AssistantAllLoad.Id, AssistentLoads);

                // Данные соответствия дисциплин ассистента и плановых дисциплин кафедры
                LoadChairTeacher = LoadTeacherervice.GetChairTeacherLoad(selAssistent.Id, 2,
                                                                          selectedNameLoadChair.NameLoadChair);
                // Формирование списка распределения нагрузки для дисциплин ассистента
                if (AssistentLoads.Count > 0)
                {
                    foreach (LoadTeacher lt in AssistentLoads)
                    {
                        foreach (ChairTeacherLoad ctl in LoadChairTeacher)
                            if (selectedDisciplineChair != null)
                                if (ContainDiscipline(lt, selectedDisciplineChair, ctl,
                                    (x, y, z) => (x.Id).Equals(z.IdLoadTeacher) && (y.Id).Equals(z.IdTeachingLoad)))
                                    SelectedDisciplinesAssistent = lt;
                    }
                }
            }
        }

        /// <summary>
        /// Признак доступности команды 
        /// </summary>
        /// <returns></returns>
        private bool CanExecuteGetTeachingLoadAssistent()
        {
            return selectedDisciplineChair != null;
        }


        #endregion CommandGetTeachingLoadAssistent

        #region CommandSetSelectedDisciplineChair

        /// <summary>
        /// Команда - Формирование плана нагрузки по дисциплине для распределения преподавателям - поле
        /// </summary>
        private ICommand setSelectedDisciplineChair;

        /// <summary>
        /// Команда - Формирование плана нагрузки по дисциплине для распределения преподавателям - Свойство
        /// </summary>
        public ICommand SetSelectedDisciplineChair
        {
            get
            {
                return setSelectedDisciplineChair ??
                    (setSelectedDisciplineChair = new RelayCommand(SetSelectedDisciplineChairExecute));
            }
        }

        /// <summary>
        /// Формирование плана нагрузки по дисциплине для распределения преподавателям - метод
        /// </summary>
        private void SetSelectedDisciplineChairExecute()
        {
            TeacherLoads.Clear();
            SelectedTeacher = null;
            AssistentLoads.Clear();
            SelectedAssistent = null;
           

            // Очистить список дисциплин для распределения нагрузки
            DistributionLoad.Clear();

            // Добавить в список дисциплин распределения нагрузки дисциплину кафедры - План
            SetPlanLoad(selectedDisciplineChair);

            MessageSave = String.Empty;

            if (selectedDisciplineChair != null)
            {
                var service = container.Resolve<IServiceLoadTeaher>();
                
                IsEnabledComboBoxTeacher = true;

                // поле - соответствие дисциплины кафедры дисциплине, распределенной преподавателю
                ChairTeacherLoad chairTeacherLoad = new ChairTeacherLoad();
                chairTeacherLoad = service.FindChairTeacherLoad(selectedDisciplineChair.Id, 1);
                if (chairTeacherLoad != null) // дисциплина кафедры назначена основному преподавателю
                {
                    Teacher currentTeacher = new Teacher();
                    currentTeacher = selectedTeacher;

                    SelectedDisciplineTeacher = null;


                    // Установить выделение в списке основного преподавателя
                    SelectedTeacher = GetTeacher((int)chairTeacherLoad.IdTeacher);

                    /// Если вычисленное значение SelectedTeacher изменилось - отличается от текущего currentTeacher,
                    /// то добавление дисциплины преподавателя происходит при обработке события SelectionChanged для ComboBoxMainTeacher,
                    if (currentTeacher == selectedTeacher)  // в противном случае необходимо добавить дисциплину в список распределения
                    {
                        SetTeacherDisciplines();
                    }

                }
                else    // дисциплина кафедры не назначена  основному преподавателю
                {
                    SelectedIndexDisciplineTeacher = -1;
                    SelectedIndexAssistent = -1;
                    if (assistantAllLoad != null)
                        AssistantAllLoad.SumLoadTeacher = null;
                }
            }
        }

        #endregion CommandSetSelectedDisciplineChair

        #region CommandAddDisciplineMainTeacher

        /// <summary>
        /// Добавление дисциплины в список дисциплин, закрепленных за преподавателем - поле
        /// </summary>
        private ICommand addDisciplineTeacher;

        /// <summary>
        /// Добавление дисциплины в список дисциплин, закрепленных за преподавателем - Свойство
        /// </summary>
        public ICommand AddDisciplineTeacher
        {
            get
            {
                return addDisciplineTeacher ??
                    (addDisciplineTeacher = new RelayCommand(
                        AddDisciplineTeacherExecute, CanExecuteAddDisciplineTeacher));

            }
        }

        /// <summary>
        /// Добавление дисциплины в список дисциплин, закрепленных за преподавателем - метод 
        /// </summary>
        private void AddDisciplineTeacherExecute()
        {
            var LoadTeacherervice = container.Resolve<IServiceLoadTeaher>();

            
            if ((selectedDisciplineChair != null) && (selectedTeacher != null))
            {
                AddDisciplineOfTeachingLoad(LoadTeacherervice,
                                            selectedTeacher, 1,
                                            ref selectedIndexDisciplineTeacher,
                                            ref teacherAllLoad,
                                            ref teacherLoads);
            }
        }

         /// <summary>
        /// Признак доступности команды EditCommand / RemoveCommand -  Редактирование / Удаление данных по кафедре
        /// </summary>
        /// <returns></returns>
        private bool CanExecuteAddDisciplineTeacher()
        {
            return (selectedDisciplineChair != null) && (SelectedTeacher != null);
        }

        #endregion CommandAddDisciplineMainTeacher

        #region CommandRemoveDisciplineMainTeacher

        /// <summary>
        /// Удаление дисциплины из списка дисциплин, закрепленных за преподавателем - поле
        /// </summary>
        private ICommand removeDisciplineMainTeacher;

        /// <summary>
        /// Удаление дисциплины из списка дисциплин, закрепленных за преподавателем  - Свойство
        /// </summary>
        public ICommand RemoveDisciplineMainTeacher
        {
            get
            {
                return removeDisciplineMainTeacher ??
                    (removeDisciplineMainTeacher = new RelayCommand(
                        RemoveDisciplineMainTeacherExecute, CanExecuteRemoveDisciplineMainTeacher));

            }
        }

        /// <summary>
        /// Удаление дисциплины из списка дисциплин, закрепленных за преподавателем  - метод
        /// </summary>
        private void RemoveDisciplineMainTeacherExecute()
        {
            MessageBoxResult result = MessageBox.Show("Удалить данные по дисциплине: \n" + SelectedDisciplineTeacher.Discipline +
                "\nдля преподавателя:\n" + selectedTeacher.LastName + " " +
                                         selectedTeacher.FirstName.Substring(0, 1) + "." +
                                         selectedTeacher.SecondName.Substring(0, 1) + ". ",
                   "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            { 
                var disciplineService = container.Resolve<IServiceLoadTeaher>();

                // данные соответствия дисциплины нагрузки кафедры и дисциплины преподавателя
                ChairTeacherLoad delChairTeacherLoad = new ChairTeacherLoad();
                if (selectedDisciplineTeacher != null)
                {

                    // Обновление данных связи дисциплин кафедры и нагрузки преподавателя
                    //if (LoadChairTeacher.Count == 0)
                    LoadChairTeacher = disciplineService.GetChairTeacherLoad(selectedTeacher.Id, 1, selectedNameLoadChair.NameLoadChair);

                    delChairTeacherLoad = FindChairTeacherLoad(LoadChairTeacher, SelectedDisciplineTeacher, SelectedTeacher,
                        (x, y, z) => (x.IdLoadTeacher == y.Id) && (x.IdTeacher == z.Id));

                }

               
                // удаление записи из таблицы ChairTeacherLoad - соответствия дисциплин кафедры 
                // и распределенной дисциплины преподавателю
                disciplineService.RemoveItemChairTeacherLoad(delChairTeacherLoad);

                // Изменение (уменьшение) общей нагрузки преподавателя
                TeacherAllLoad.SumLoadTeacher -= SelectedDisciplineTeacher.SumLoad;
                // Изменение (уменьшение) распределенной/нераспределенной нагрузки по дисциплине кафедры
                SelectedDisciplineChair.IsLoad = false;

                // Редактирование обобщенных данных по нагрузке преподавателя в базе данных
                disciplineService.EditItemTeahingLoadTeacher(TeacherAllLoad);

                // Редактирование данных по дисциплине кафедры
                disciplineService.EditTeachingLoad(SelectedDisciplineChair);

                // Обновление обобщенных данных по нагрузке преподавателей кафедры
                AllLoadTeacher = disciplineService.GetAllLoadTeacher(selectedChair.Id, selectedAcademicYear.Id);
                // Редактирование значения суммарной распределенной/нераспределенной нагрузки по кафедре в БД
                decimal? sumLoadChair = 0;
                foreach (var disc in AllLoadTeacher)
                    sumLoadChair += disc.SumLoadTeacher;
                LoadChair.SumUnloadChair = LoadChair.SumLoadChair - sumLoadChair;
                disciplineService.EditTeachingLoadChair(LoadChair);
                
                // Удаление выбранной дисциплины преподавателя в базе данных
                disciplineService.RemoveItemLoadTeacher(SelectedDisciplineTeacher);

                // Удаление выбранной дисциплины преподавателя из списка TeacherLoads
                TeacherLoads.Remove(SelectedDisciplineTeacher);

                UpdateCurrentData(disciplineService);
                if (TeacherLoads != null)
                    TeacherLoads.Clear();
                TeacherLoads = disciplineService.GetLoadTeacher(TeacherAllLoad.Id, TeacherLoads);
                
                if (TeacherLoads.Count > 0)
                    SelectedIndexDisciplineTeacher = 0;
                else
                {
                    string nameTeacher = selectedTeacher.LastName + " " +
                                         selectedTeacher.FirstName.Substring(0, 1) + "." +
                                         selectedTeacher.SecondName.Substring(0, 1) + ". ";
                    DistributionTeachingLoad discipline = FindDistributionDisciplineTeacher(
                    DistributionLoad,
                    nameTeacher,
                    (x, y) => x.Status.Equals(y));

                    DistributionLoad.Remove(discipline);
                }

            }
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveCommand -  Редактирование / Удаление данных по кафедре
        /// </summary>
        /// <returns></returns>
        private bool CanExecuteRemoveDisciplineMainTeacher()
        {
            return (SelectedDisciplineTeacher != null);
        }

        #endregion CommandRemoveDisciplineTeacher

        #region CommandAddDisciplineAssistent

        /// <summary>
        /// Добавление дисциплины в список дисциплин, закрепленных за ассистентом - поле
        /// </summary>
        private ICommand addDisciplineAssistent;

        /// <summary>
        /// Добавление дисциплины в список дисциплин, закрепленных за ассистентом - Свойство
        /// </summary>
        public ICommand AddDisciplineAssistent
        {
            get
            {
                return addDisciplineAssistent ??
                    (addDisciplineAssistent = new RelayCommand(
                        AddDisciplineAssistentExecute, CanExecuteAddDisciplineAssistent));

            }
        }

        /// <summary>
        /// Добавление дисциплины в список дисциплин, закрепленных за преподавателем-ассистентом - метод 
        /// </summary>
        private void AddDisciplineAssistentExecute()
        {
            var LoadTeacherervice = container.Resolve<IServiceLoadTeaher>();


            if ((SelectedDisciplineTeacher != null) && (SelectedAssistent != null))
            {
                AddDisciplineOfTeachingLoad(LoadTeacherervice, 
                                            SelectedAssistent, 2,
                                            ref selectedIndexDisciplineAssistent,
                                            ref assistantAllLoad,
                                            ref assistentLoads);
            }
        }


        /// <summary>
        /// Добавление дисциплины в нагрузку преподавателя 
        /// </summary>
        /// <param name="service"></param> сервис данных 
        /// <param name="speciality"></param> преподаватель/ассистент
        /// <param name="statusTeacher"></param> статус: 1 - преподаватель, 2 - ассистент
        /// <param name="selectedIndex"></param> выделенный индекс в списке преподавателей
        /// <param name="teachingLoadTeacher"></param> данные соответствия распределенной дисциплины преподавателя
        /// дисцпилине кафедры
        /// <param name="loads"></param> нагрузка преподавателя
        private void AddDisciplineOfTeachingLoad(IServiceLoadTeaher service, 
                                                 Teacher teacher,
                                                 int statusTeacher,
                                                 ref int selectedIndex, 
                                                 ref TeahingLoadTeacher teachingLoadTeacher,
                                                 ref ObservableCollection<LoadTeacher> loads
                                                 )
        {
            // проверка возможности назначения дисциплины ассистенту
            if (!service.IsLoad(teachingLoadTeacher.Id, selectedDisciplineChair.Code,
                selectedDisciplineChair.Discipline, selectedDisciplineChair.Speciality,
                selectedDisciplineChair.NameGroup, selectedDisciplineChair.FormEducation,
                selectedDisciplineChair.Course, selectedDisciplineChair.Semester))
            {
                // Экземпляр дисциплины нагрузки преподавателя для добавления в список
                LoadTeacher loadTeacher = new LoadTeacher();
                NewDisciplineTeacher(loadTeacher, teachingLoadTeacher.Id, selectedDisciplineChair);
                loadTeacher = service.AddItemLoadTeacher(loadTeacher);

                // Формирование записи соответствия дисциплины кафедры назначенной дисциплине преподавателя
                ChairTeacherLoad newChairTeacherLoad = NewChairTeacherLoad(loadTeacher, teacher.Id, loadTeacher.Id,
                    SelectedDisciplineChair.Id, SelectedNameLoadChair.NameLoadChair, statusTeacher);
                service.AddItemChairTeacherLoad(newChairTeacherLoad, selectedNameLoadChair.NameLoadChair);

                // Обновление данных связи дисциплин кафедры и нагрузки преподавателя ????????????????
                LoadChairTeacher = service.GetChairTeacherLoad(teacher.Id, statusTeacher, selectedNameLoadChair.NameLoadChair);

                // Обновление обобщенных данных по нагрузке преподавателя ////////////////////////////////////////
                teachingLoadTeacher = service.GetTeahingLoadTeacher(teacher.Id, selectedAcademicYear.Id, selectedNameLoadChair.NameLoadChair);

                // Обновление списка дисциплин ассистента
                loads.Clear();
                loads = service.GetLoadTeacher(teachingLoadTeacher.Id, loads);

                // Добавление новой дисциплины преподавателя в список распределяемых дисциплин кафедры
                DistributionLoad.Add(SetDistributionTeachingLoad(loadTeacher, teacher));

                // Установка выделения дисциплины в списке дисциплин преподавателя
                int count = loads.Count;
                if (count > 0)
                    selectedIndex = count - 1;
            }
            else
                MessageBox.Show("Дисциплина: \n" + selectedDisciplineChair.Discipline + "\n имеется в нагрузке преподавателя/ассистента!!!",
               "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveCommand -  Редактирование / Удаление данных по кафедре
        /// </summary>
        /// <returns></returns>
        private bool CanExecuteAddDisciplineAssistent()
        {
            return (SelectedDisciplineTeacher != null) && (SelectedAssistent != null);
        }

        #endregion CommandAddDisciplineMainTeacher

        #region CommandRemoveDisciplineAssistent

        /// <summary>
        /// Удаление дисциплины из списка дисциплин, закрепленных за ассистентом - поле
        /// </summary>
        private ICommand removeDisciplineAssistent;

        /// <summary>
        /// Удаление дисциплины из списка дисциплин, закрепленных за ассистентом - Свойство
        /// </summary>
        public ICommand RemoveDisciplineAssistent
        {
            get
            {
                return removeDisciplineAssistent ??
                    (removeDisciplineAssistent = new RelayCommand(
                        RemoveDisciplineAssistentExecute, CanExecuteRemoveDisciplineAssistent));

            }
        }

        /// <summary>
        /// Удаление дисциплины из списка дисциплин, закрепленных за ассистентом - метод
        /// </summary>
        private void RemoveDisciplineAssistentExecute()
        {
            if (selectedDisciplineTeacher != null)
            {
                if (selectedDisciplineTeacher.Code == selectedDisciplinesAssistent.Code)
                {
                    MessageBoxResult result = MessageBox.Show("Удалить данные по дисциплине: \n" + selectedDisciplinesAssistent.Discipline +
                      "\nдля преподавателя:\n" + selectedAssistent.LastName + " " +
                                               selectedAssistent.FirstName.Substring(0, 1) + "." +
                                               selectedAssistent.SecondName.Substring(0, 1) + ". ",
                         "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.OK)
                    {
                        // данные соответствия дисциплины нагрузки кафедры и дисциплины преподавателя
                        ChairTeacherLoad delChairTeacherLoad = new ChairTeacherLoad();
                        if (selectedDisciplinesAssistent != null)
                        {
                            delChairTeacherLoad = FindChairTeacherLoad(LoadChairTeacher, selectedDisciplinesAssistent, selectedAssistent,
                                (x, y, z) => (x.IdLoadTeacher == y.Id) && (x.IdTeacher == z.Id));
                        }

                        var disciplineService = container.Resolve<IServiceLoadTeaher>();
                        // удаление записи из таблицы ChairTeacherLoad - соответствия дисциплин кафедры 
                        // и распределенной дисциплины преподавателю
                        disciplineService.RemoveItemChairTeacherLoad(delChairTeacherLoad);

                        // Изменение (уменьшение) общей нагрузки преподавателя
                        AssistantAllLoad.SumLoadTeacher -= SelectedDisciplinesAssistent.SumLoad;

                        SelectedDisciplineChair.IsLoad = false;

                        // Редактирование обобщенных данных по нагрузке преподавателя в базе данных
                        disciplineService.EditItemTeahingLoadTeacher(assistantAllLoad);

                        // Редактирование данных по дисциплине кафедры
                        disciplineService.EditTeachingLoad(SelectedDisciplineChair);

                        // Обновление обобщенных данных по нагрузке преподавателей кафедры
                        AllLoadTeacher = disciplineService.GetAllLoadTeacher(selectedChair.Id, selectedAcademicYear.Id);
                        // Редактирование значения суммарной распределенной/нераспределенной нагрузки по кафедре в БД
                        decimal? sumLoadChair = 0;
                        foreach (var disc in AllLoadTeacher)
                            sumLoadChair += disc.SumLoadTeacher;
                        LoadChair.SumUnloadChair = LoadChair.SumLoadChair - sumLoadChair;
                        disciplineService.EditTeachingLoadChair(LoadChair);

                        // Удаление выбранной дисциплины преподавателя в базе данных
                        disciplineService.RemoveItemLoadTeacher(selectedDisciplinesAssistent);

                        // Удаление выбранной дисциплины преподавателя из списка TeacherLoads
                        AssistentLoads.Remove(selectedDisciplinesAssistent);

                        UpdateCurrentData(disciplineService);

                        if (AssistentLoads != null)
                            AssistentLoads.Clear();
                        AssistentLoads = disciplineService.GetLoadTeacher(assistantAllLoad.Id, AssistentLoads);

                        if (AssistentLoads.Count > 0)
                            SelectedIndexDisciplineAssistent = 0;
                        else
                        {
                            string nameTeacher = selectedAssistent.LastName + " " +
                                                 selectedAssistent.FirstName.Substring(0, 1) + "." +
                                                 selectedAssistent.SecondName.Substring(0, 1) + ". ";
                            DistributionTeachingLoad discipline = FindDistributionDisciplineTeacher(
                            DistributionLoad,
                            nameTeacher,
                            (x, y) => x.Status.Equals(y));

                            DistributionLoad.Remove(discipline);
                        }
                    }
                }
                else
                    MessageBox.Show("Для удаления дисциплины ассистента \n" +
                                   "необходимо выделить дисциплину в списке дисциплин кафедры", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
                MessageBox.Show("Для удаления дисциплины ассистента \n" +
                               "необходимо выделить дисциплину в списке дисциплин кафедры", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveCommand   
        /// </summary>
        /// <returns></returns>
        private bool CanExecuteRemoveDisciplineAssistent()
        {
            return (selectedDisciplinesAssistent != null);
        }
        #endregion CommandRemoveDisciplineMainTeacher

        #region CommandSetDisciplineForSelectedTeacherDiscipline

        /// <summary>
        /// Команда - Установка выделенной дисциплины преподавателя - поле
        /// </summary>
        private ICommand setSelectedDisciplineTeacher;

        /// <summary>
        /// Команда - Установка выделенной дисциплины преподавателя - Свойство
        /// </summary>
        public ICommand SetSelectedDisciplineTeacher
        {
            get
            {
                return setSelectedDisciplineTeacher ??
                    (setSelectedDisciplineTeacher = new RelayCommand(SetSelectedDisciplineTeacherExecute));
            }
        }

        /// <summary>
        /// Установка выделенной дисциплины преподавателя - метод
        /// </summary>
        private void SetSelectedDisciplineTeacherExecute()
        {

            var service = container.Resolve<IServiceLoadTeaher>();
            if (selectedNameLoadChair != null)
            {
                // Данные соответствия дисциплин преподавателя и плановых дисциплин кафедры
                ObservableCollection<ChairTeacherLoad> loads = service.GetChairTeacherLoad(selectedTeacher.Id, 1,
                                                                          selectedNameLoadChair.NameLoadChair);
                // Данные соответствия дисциплины нагрузки кафедры и дисциплины преподавателя
                ChairTeacherLoad chairTeacherLoad = new ChairTeacherLoad();
                if ((selectedDisciplineTeacher != null) && (loads.Count > 0))
                {
                    chairTeacherLoad = FindChairTeacherLoad(loads, SelectedDisciplineTeacher, SelectedTeacher,
                        (x, y, z) => (x.IdLoadTeacher == y.Id) && (x.IdTeacher == z.Id));

                    SelectedDisciplineChair = FindDisciplineChair(LoadChairTeaching, chairTeacherLoad,
                    (x, y) => x.Id.Equals(y.IdTeachingLoad));
                }
            }

          
        }

        //private void SetAssistentDisciplines()
        //{
        //    var LoadTeacherervice = container.Resolve<IServiceLoadTeaher>();

        //    // Обобщенные данные по нагрузке преподавателя
        //    AssistantAllLoad = LoadTeacherervice.GetTeahingLoadTeacher(selectedAssistent.Id,
        //                                                                  selectedAcademicYear.Id,
        //                                                                  selectedNameLoadChair.NameLoadChair);

        //    if (AssistentLoads != null)
        //        AssistentLoads.Clear();

        //    // Дисциплины преподавателя

        //    AssistentLoads = LoadTeacherervice.GetLoadTeacher(AssistantAllLoad.Id, TeacherLoads);

        //    // Данные соответствия дисциплин преподавателя и плановых дисциплин кафедры
        //    LoadChairTeacher = LoadTeacherervice.GetChairTeacherLoad(selectedAssistent.Id, 2,
        //                                                              selectedNameLoadChair.NameLoadChair);

        //    //int countDistridutionLoad = DistributionLoad.Count;
        //    //if (countDistridutionLoad > 1)
        //    //    for (int i = 1; i < countDistridutionLoad; i++)
        //    //        distributionLoad.RemoveAt(i);

        //    // Формирование списка распределения нагрузки для дисциплин преподавателя
        //    if (AssistentLoads.Count > 0)
        //    {
        //        foreach (LoadTeacher lt in AssistentLoads)
        //        {
        //            foreach (ChairTeacherLoad ctl in LoadChairTeacher)
        //            {
        //                if (selectedDisciplineChair != null)
        //                {
        //                    if (ContainDiscipline(lt, selectedDisciplineChair, ctl,
        //                        (x, qual, z) => (x.Id).Equals(z.IdLoadTeacher) && (qual.Id).Equals(z.IdTeachingLoad)))
        //                    {
        //                        DistributionTeachingLoad load = SetDistributionTeachingLoad(lt, selectedAssistent);
        //                        DistributionLoad.Add(load);

        //                        if (ctl.IdTeachingLoad == selectedDisciplineChair.Id)
        //                        {
        //                            SelectedDisciplinesAssistent = null;
        //                            SelectedDisciplinesAssistent = SetSelectedDisciplinesTeacher((int)ctl.IdLoadTeacher, AssistentLoads);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        SelectedIndexDisciplineAssistent = 0;
        //    }
        //}

        #endregion CommandSetDisciplineForSelectedTeacherDiscipline

        #region SaveCommand

        /// <summary>
        /// Команда - Сохранение отредектированных данных распределения нагрузки преподавателя- поле
        /// </summary>
        private ICommand saveCommand;

        /// <summary>
        /// Команда - Сохранение отредектированных данных распределения нагрузки преподавателя - Свойство
        /// </summary>
        public ICommand SaveCommand
        {
            get
            {
                return saveCommand ??
                    (saveCommand = new RelayCommand(SaveCommandExecute));
            }

        }

        /// <summary>
        /// Сохранение отредектированных данных распределения нагрузки преподавателя - метод
        /// </summary>
        private void SaveCommandExecute()
        {
            /// Проверка наличия флага изменений в строке таблицы распределения нагрузки
            /// Индекс строки для сохранения данных
            int indexDistridutionLoad = 1;
            int count = DistributionLoad.Count;
            for (int i = 1; i < count; i++)
            {
                if (DistributionLoad[i].FlagChange != String.Empty)
                    break;
                else
                    indexDistridutionLoad++;
            }

            // id распределяемой дисциплины преподавателя
            int idDisciplineTeacher = DistributionLoad[indexDistridutionLoad].IdLoad;


            // Сохраняемая дисциплина преподавателя
            LoadTeacher disciplineTeacher = new LoadTeacher();
            if (indexDistridutionLoad == 1)
                // дисциплина основного преподавателя
                disciplineTeacher = FindDisciplineTeacher(
                                    TeacherLoads,
                                    idDisciplineTeacher,
                                    (x, y) => x.Id.Equals(y));
            else
                // дисциплина ассистента
                disciplineTeacher = FindDisciplineTeacher(
                                    AssistentLoads,
                                    idDisciplineTeacher,
                                    (x, y) => x.Id.Equals(y));

            // Обновление дисциплины преподавателя перед сохранением изменений
            UpdateTeacherLoad(DistributionLoad[indexDistridutionLoad], disciplineTeacher);

            if (selectedDisciplineChair.Id == DistributionLoad[0].IdLoad)
            {
                var loadService = container.Resolve<IServiceLoadTeaher>();

                #region начало транзакции сохранения изменений в БД

                // сохранение изменений дисциплины преподавателя/ассистента
                loadService.EditItemLoadTeacher(disciplineTeacher);

                // сохранение общих данных 
                if (indexDistridutionLoad == 1) // Это не лучший способ определения дисциплины преподавателя/ассистемна!!!
                    // основного преподавателя
                    loadService.EditItemTeahingLoadTeacher(TeacherAllLoad);
                else
                    // ассистента
                    loadService.EditItemTeahingLoadTeacher(AssistantAllLoad);



                // Обновление значения нераспределенной нагрузки по дисциплине
                ObservableCollection<ChairTeacherLoad> discipnineTeachers = loadService.GetChairTeacherLoadDiscipline(SelectedDisciplineChair.Id, LoadChair.NameLoadChair);

                ObservableCollection<LoadTeacher> LoadTeacher = new ObservableCollection<LoadTeacher>();

                LoadTeacher loadTeacher = new LoadTeacher();

                if (discipnineTeachers != null)
                {
                    foreach (var disc in discipnineTeachers)
                    {
                        loadTeacher = loadService.GetDisciplineTeacher(disc.IdLoadTeacher);
                        LoadTeacher.Add(loadTeacher);
                    }
                }

                decimal sum = 0;
                if (LoadTeacher != null)
                {
                    foreach (var load in LoadTeacher)
                        sum += (decimal)load.SumLoad;
                    selectedDisciplineChair.SumUnload = selectedDisciplineChair.SumLoad - sum;
                }

                loadService.EditTeachingLoad(selectedDisciplineChair);


                // Загрузка обобщенных данных по нагрузке преподавателей кафедры
                AllLoadTeacher = loadService.GetAllLoadTeacher(selectedChair.Id, selectedAcademicYear.Id);

                // Обновление значения нераспределенной нагрузки кафедры
                LoadChair.SumUnloadChair = LoadChair.SumLoadChair;
                foreach (var load in AllLoadTeacher)
                    LoadChair.SumUnloadChair -= load.SumLoadTeacher;
                loadService.EditTeachingLoadChair(LoadChair);

                // Сброс сообщения о необходимости сохранения дисциплины
                MessageSave = String.Empty; 
                // Сброс признака необходимости сохранения дисциплины
                DistributionLoad[indexDistridutionLoad].FlagChange = String.Empty; 

                //MessageSave = String.Empty;

                #endregion конец транзакции сохранения изменений в БД

                // Обновление данных нагрузке кафедры
                UpdateCurrentData(loadService);
                
            }
        }
        
        #endregion SaveCommand

        #endregion Command
    }
        
}
