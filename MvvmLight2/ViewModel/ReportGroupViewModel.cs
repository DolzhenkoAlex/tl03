using GalaSoft.MvvmLight.Command;
using MvvmLight2.Model;
using MvvmLight2.ServiceData;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmLight2.ViewModel
{
    public class ReportGroupViewModel: WorkspaceViewModel
    {
         private MvvmLight2.Helper.IContainer container = MvvmLight2.Helper.Container.Instance;

        //ObservableCollection<StudentGroup> disciplines;

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
        
        public ObservableCollection<tlsp_getGroups_Result> Groups { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the DisciplineOfChairViewModel class.
        /// </summary>
        public ReportGroupViewModel()
        {
            Groups = new ObservableCollection<tlsp_getGroups_Result>();
            base.DisplayName = "Отчет Контингент";
        }

       
        #endregion Constructor 
        
        #region CommandGetGroups

        /// <summary>
        /// Команда - Формироание данных отчета по контингенту - поле
        /// </summary>
        private ICommand getGroups;

        /// <summary>
        /// Команда - Формироание данных отчета по контингенту - Свойство
        /// </summary>
        public ICommand GetGroups
        {
            get
            {
                return getGroups ??
                    (getGroups = new RelayCommand(GetExecuteGetGroups));
            }
        }

        /// <summary>
        /// Формироание данных отчета по контингенту- метод
        /// </summary>
        public async void GetExecuteGetGroups()
        {
            var service = container.Resolve<IServiceGroup>();

            if (SelectedAcademicYear != null)
            {
                // установка IsBusy перед началом ассинхронного потока визуализации выполнения длительной операции
                IsBusyCurriculum = true;
                IsEnabledButton = false;
                BusyMessage = "Формирование данных . . .";

                Groups = await Task.Factory.StartNew(() =>
                        service.GetGroupForReport( selectedAcademicYear.Id));

                IsBusyCurriculum = false;
                IsEnabledButton = true;
            }
        }
        #endregion GetDisciplines
    }
}
