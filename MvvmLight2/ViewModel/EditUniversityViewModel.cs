using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmLight2.Helper;
using MvvmLight2.Model;

namespace MvvmLight2.ViewModel
{
    /// <summary>
    /// Класс ViewModel для редактирования данных по Университету/филиалу
    /// </summary>
    class EditUniversityViewModel
    {
        #region Field

        IContainer container = Container.Instance;
        private readonly IMessenger messenger;

        #endregion Field

        #region Properties

        /// <summary>
        /// Свойство данных по факультетам
        /// </summary>
        public University University { get; set; }

        #endregion Properties

        #region Constructor

        public EditUniversityViewModel(IMessenger messenger)
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
                        () => messenger.Send(new CloseViewMessage("EditUniversityView", true))));
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
                        () => messenger.Send(new CloseViewMessage("EditUniversityView", false))));
            }
        }

        #endregion
    }
}
