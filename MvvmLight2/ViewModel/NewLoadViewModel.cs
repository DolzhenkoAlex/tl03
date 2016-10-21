using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmLight2.Helper;
using MvvmLight2.Model;

namespace MvvmLight2.ViewModel
{
    
    /// <summary>
    /// Класс ViewModel для создания данных по новому варианту нагрузки кафедры
    /// </summary>
    class NewLoadViewModel: WorkspaceViewModel
    {
         #region Field

        IContainer container = Container.Instance;
        private readonly IMessenger messenger;

        #endregion Field

        #region Properties

        /// <summary>
        /// Свойство данных по кафедрам
        /// </summary>
        public TeachingLoadChair NewLoad { get; set; }

        #endregion Properties

        #region Constructor

        public NewLoadViewModel(IMessenger messenger)
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
                        () => messenger.Send(new CloseViewMessage("NewLoadView", true))));
            }
        }

        #endregion

        #region Cancel command

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return cancelCommand ??
                    (cancelCommand = new RelayCommand(
                        () => messenger.Send(new CloseViewMessage("NewChairView", false))));
            }
        }

        #endregion
    }
}

