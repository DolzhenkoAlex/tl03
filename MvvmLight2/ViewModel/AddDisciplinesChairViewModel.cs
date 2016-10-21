using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmLight2.Helper;
using MvvmLight2.Model;

namespace MvvmLight2.ViewModel
{
    /// <summary>
    /// Класс ViewModel для выбора учебных планов 
    /// при формировании дисциплин кафедры
    /// </summary>
    class AddDisciplinesChairViewModel: WorkspaceViewModel
    {
        #region Field

        IContainer container = Container.Instance;
        private readonly IMessenger messenger;

        #endregion Field

        #region Properties

        Curriculum selectedCurriculum;

        public Curriculum SelectedCurriculum
        {
            get { return selectedCurriculum; }
            set 
            { 
                if (value == selectedCurriculum)
                    return;
                
                selectedCurriculum = value;
                RaisePropertyChanged("SelectedCurriculum");
            }
        }


        /// <summary>
        /// Коллекция учебных планов
        /// </summary>
        public ObservableCollection<Curriculum> Curriculums { get;  set; }

        #endregion Properties

        #region Constructor

        public AddDisciplinesChairViewModel(IMessenger messenger)
        {
            this.messenger = messenger;
        }

        #endregion Constructor

        #region Ok command

        private ICommand okCommand;
        public ICommand OkCommand
        {
            get
            {
                return okCommand ??
                    (okCommand = new RelayCommand(
                        () => messenger.Send(new CloseViewMessage("AddDisciplineChairView", true))));
            }
        }

        #endregion

        #region Cancel command

        private ICommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return _cancelCommand ??
                    (_cancelCommand = new RelayCommand(
                        () => messenger.Send(new CloseViewMessage("AddDisciplineChairView", false))));
            }
        }

        #endregion
    }
}

