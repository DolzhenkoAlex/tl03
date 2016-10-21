using System.Collections.ObjectModel;
using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ServiceData;
using System.ComponentModel;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;


namespace MvvmLight2.ViewModel
{
    public class ReportChairDisciplinesViewModel : WorkspaceViewModel
    {
         private MvvmLight2.Helper.IContainer container = MvvmLight2.Helper.Container.Instance;

        ObservableCollection<tlsp_getDisciplineChair_Result> disciplines;

        #region SelectedChair

        /// <summary>
        /// Выделенная кафедра
        /// </summary>
        private Chair selectedChair;

        /// <summary>
        /// Выделенная дисциплина кафедра
        /// </summary>
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

        #region Properties for binding
        
        public ObservableCollection<tlsp_getDisciplineChair_Result> Disciplines { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the DisciplineOfChairViewModel class.
        /// </summary>
        public ReportChairDisciplinesViewModel()
        {
            Disciplines = new ObservableCollection<tlsp_getDisciplineChair_Result>();
            base.DisplayName = "Отчет Дисциплины кафедры";
        }

       
        #endregion Constructor 
        
        #region CommandGetDisciplines

        /// <summary>
        /// Команда - Формироание данных отчета по дисциплинам кафедры - поле
        /// </summary>
        private ICommand getDisciplines;

        /// <summary>
        /// Команда - Формироание данных отчета по дисциплинам кафедры - Свойство
        /// </summary>
        public ICommand GetDisciplines
        {
            get
            {
                return getDisciplines ??
                    (getDisciplines = new RelayCommand(GetExecuteDisciplines));
            }

        }

        /// <summary>
        /// Формироание данных отчета по дисциплинам кафедры- метод
        /// </summary>
        public void GetExecuteDisciplines()
        {
            var service = container.Resolve<IServiceDisciplineChair>();

            if ((SelectedChair != null) && (SelectedAcademicYear != null))
            {
                BackgroundWorker worker = new BackgroundWorker();
                // Запуск длительной операции добавления данных о новом учебном плане в базу данных
                worker.DoWork += (o, ea) =>
                {
                    disciplines = service.GetDiscipline(selectedChair.Id, selectedAcademicYear.Id);
                };

                worker.RunWorkerCompleted += (o, ea) =>
                {
                    Disciplines.Clear();

                    if (disciplines.Count > 0)
                        foreach (var disc in disciplines)
                            Disciplines.Add(disc);

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
        #endregion GetDisciplines
    }
}
