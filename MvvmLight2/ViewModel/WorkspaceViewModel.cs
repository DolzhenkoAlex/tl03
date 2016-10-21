using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace MvvmLight2.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class WorkspaceViewModel : ViewModelBase
    {
        #region DisplayName

        /// <summary>
        /// Свойство возвращает выводимое в интерфейсе имя объекта - заголовок панели.
        /// Дочерний класс может задать свое имя или переопределить свойство.
        /// </summary>
        public virtual string DisplayName { get; protected set; }

        #endregion // DisplayName

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the WorkspaceViewModel class.
        /// </summary>
        public WorkspaceViewModel()
        {
        }

        #endregion Constructor

        #region ExitCommand

        /// <summary>
        /// Возвращает команду закрытия рабочей области (вкладки)
        /// в интерфейсе пользователя
        /// Returns the command that, when invoked, attempts
        /// to remove this workspace from the user interface.
        /// </summary>
        private ICommand closeCommand;

        public ICommand CloseCommand
        {
            get
            {
                if (closeCommand == null)
                    closeCommand = new RelayCommand(OnRequestClose);

                return closeCommand;
            }
        }

        #endregion  ExitCommand

        #region RequestClose [event]

        /// <summary>
        /// Событие закрытия рабочей области (вкладки) интерфейса пользователя
        /// Raised when this workspace should be removed from the UI.
        /// </summary>
        /// 
        public event EventHandler RequestClose;

        void OnRequestClose()
        {
            EventHandler handler = this.RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        #endregion RequestClose [event]
    }
}
