using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ServiceData;
using System.ComponentModel;

namespace MvvmLight2.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// Отсет по нагрузке кафедры
    /// </summary>
    public class ReportChairLoadViewModel : WorkspaceViewModel
    {
        private MvvmLight2.Helper.IContainer container = MvvmLight2.Helper.Container.Instance;

        private ObservableCollection<TeachingLoad> loadChairTeaching;
        private ObservableCollection<TeachingLoadChair> allLoadChairAcademicYear;

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

        #region Индикатор выполнения длительной операции

        bool isBusyCurriculum = false;

        /// <summary>
        /// Индикатор длительной операции
        /// </summary>
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

        /// <summary>
        /// Сообщение при выполнении длительной операции
        /// </summary>
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
        /// Коллекция нагрузки по дисциплинам кафедры
        /// </summary>
        public ObservableCollection<TeachingLoad> LoadChairTeaching { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the FacultiesViewModel class.
        /// </summary>
        public ReportChairLoadViewModel()
        {
            LoadChairTeaching = new ObservableCollection<TeachingLoad>();
            LoadChair = new TeachingLoadChair();
            AllLoadChairAcademicYear = new ObservableCollection<TeachingLoadChair>();

            base.DisplayName = "Отчет Нагрузка кафедры";
        }

        #endregion Constructor

        #region Command

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
        private void    GetLoadChairExecute()
        {
            var loadChairService = container.Resolve<IServiceLoadChair>();
            
            if (LoadChairTeaching != null)
                LoadChairTeaching.Clear();
            
            if ((SelectedChair != null) && 
                (SelectedAcademicYear != null) && 
                (SelectedNameLoadChair != null))
            {
               
                BackgroundWorker worker = new BackgroundWorker();
                // Запуск длительной операции добавления данных о новом учебном плане в базу данных
                worker.DoWork += (o, ea) =>
                {
                    loadChairTeaching = loadChairService.GetLoadChairTeaching(SelectedChair.Id, 
                                                                          selectedAcademicYear.Id, 
                                                                          selectedNameLoadChair.NameLoadChair);
                };

                 worker.RunWorkerCompleted += (o, ea) =>
                {
                    if(loadChairTeaching.Count > 0)
                    {
                        LoadChairTeaching.Clear();
                        foreach (var disc in loadChairTeaching)
                            LoadChairTeaching.Add(disc);
                    }
                    IsBusyCurriculum = false;
                    IsEnabledButton = true;
                };

                 // установка IsBusy перед началом ассинхронного потока визуализации выполнения длительной операции
                 IsBusyCurriculum = true;
                 IsEnabledButton = false;
                 BusyMessage = "Формирование данных . . .";
                 worker.RunWorkerAsync();
            }
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
        /// Загрузка данных по нагрузке кафедры - метод
        /// </summary>
        private void GetAllLoadChairExecute()
        {
            var loadChairService = container.Resolve<IServiceLoadChair>();

            if (AllLoadChairAcademicYear != null)
                AllLoadChairAcademicYear.Clear();
            
            if ((SelectedChair != null) && (SelectedAcademicYear != null))
            {
                allLoadChairAcademicYear = loadChairService.GetAllLoadChair(SelectedChair.Id, selectedAcademicYear.Id);

                if(allLoadChairAcademicYear.Count > 0)
                {
                    foreach (var load in allLoadChairAcademicYear)
                        AllLoadChairAcademicYear.Add(load);
                }
            }
        }

        #endregion CommandGetAllLoadChair

        #endregion Command
    }
}

