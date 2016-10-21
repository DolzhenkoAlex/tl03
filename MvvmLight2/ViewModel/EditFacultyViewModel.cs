using System;
using System.Collections.Generic;
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
    /// Класс ViewModel для редактирования данных по факультету
    /// </summary>
    class EditFacultyViewModel : WorkspaceViewModel
    {
        #region Field

        IContainer container = Container.Instance;
        private readonly IMessenger messenger;

        #endregion Field

        #region Properties

        /// <summary>
        /// Свойство данных по факультетам
        /// </summary>
        public Faculty Faculty { get; set; }

        #endregion Properties

        #region Constructor

        public EditFacultyViewModel(IMessenger messenger)
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
                        () => messenger.Send(new CloseViewMessage("EditFacultyView", true))));
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
                        () => messenger.Send(new CloseViewMessage("EditFacultyView", false))));
            }
        }

        #endregion
    }
}
