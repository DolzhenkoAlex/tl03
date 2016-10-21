using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmLight2.Helper;
using MvvmLight2.Model;

namespace MvvmLight2.ViewModel
{
    public class NewLoadChairViewModel : WorkspaceViewModel
    {
         #region Field

        IContainer container = Container.Instance;
        private readonly IMessenger messenger;

        #endregion Field

        #region Properties

        /// <summary>
        /// Свойство данных по кафедрам
        /// </summary>
        public TeachingLoad NewLoadChair { get; set; }

        #endregion Properties

        #region Constructor

        public NewLoadChairViewModel(IMessenger messenger)
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
                        () => messenger.Send(new CloseViewMessage("NewLoadChairView", true))));
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
                        () => messenger.Send(new CloseViewMessage("NewLoadChairView", false))));
            }
        }

        #endregion
    }
}
