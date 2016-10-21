using GalaSoft.MvvmLight.Command;
using MvvmLight2.Model;
using MvvmLight2.ServiceData;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace MvvmLight2.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// Отчет по закрепленным дисциплинам
    /// </summary>
    public class ReportFixedDisciplinesViewModel : WorkspaceViewModel
    {
        private MvvmLight2.Helper.IContainer container = MvvmLight2.Helper.Container.Instance;

        ObservableCollection<tlsp_getAllDiscipline_Result> disciplines;

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

        #region Properties for binding

        /// <summary>
        /// Источние данных для отчета
        /// </summary>
        public ObservableCollection<tlsp_getAllDiscipline_Result> ListFixedDiscilpines { get; set; }

        #endregion Properties for binding

        #region Constructor
        
        /// <summary>
        /// Initializes a new instance of the DisciplineOfChairViewModel class.
        /// </summary>
        public ReportFixedDisciplinesViewModel()
        {
            ListFixedDiscilpines = new ObservableCollection<tlsp_getAllDiscipline_Result>();
            base.DisplayName = "Отчет о закреплении дисциплин";
        }

        #endregion Constructor

        #region CommandGetFixedDisciplines

        /// <summary>
        /// Команда - Формироание данных отчета по закрепленным дисциплинам - поле
        /// </summary>
        private ICommand getFixedDisciplines;

        /// <summary>
        /// Команда - Формироание данных отчета по закрепленным дисциплинам - Свойство
        /// </summary>
        public ICommand GetFixedDisciplines
        {
            get
            {
                return getFixedDisciplines ??
                    (getFixedDisciplines = new RelayCommand(GetExecuteFixedDisciplines));
            }

        }

        /// <summary>
        /// Формироание данных отчета по закрепленным дисциплинам- метод
        /// </summary>
        private void GetExecuteFixedDisciplines()
        {
            var service = container.Resolve<IServiceCurriculum>();

            int idAcademicYear = 0;
            if (selectedAcademicYear != null)
                idAcademicYear = selectedAcademicYear.Id;

             BackgroundWorker worker = new BackgroundWorker();
                 // Запуск длительной операции добавления данных о новом учебном плане в базу данных
             worker.DoWork += (o, ea) =>
             {
                 disciplines = service.GetAllDisciplines(idAcademicYear);
             };

             worker.RunWorkerCompleted += (o, ea) =>
             {
                 ListFixedDiscilpines.Clear();
                 foreach (var disc in disciplines)
                     ListFixedDiscilpines.Add(disc);

                 IsBusyCurriculum = false;
                 IsEnabledButton = true;
             };

             // установка IsBusy перед началом ассинхронного потока визуализации выполнения длительной операции
             IsBusyCurriculum = true;
             IsEnabledButton  = false;
             BusyMessage = "Формирование данных . . .";
             worker.RunWorkerAsync();
        }

        #endregion CommandGetFixedDisciplines
    }
}
