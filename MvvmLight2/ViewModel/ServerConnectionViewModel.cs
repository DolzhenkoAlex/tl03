using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmLight2.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmLight2.ViewModel
{
    /// <summary>
    /// Формирование строки соединения с сервером базы данных 
    /// </summary>
    class ServerConnectionViewModel : WorkspaceViewModel
    {
         #region Field

        IContainer container = Container.Instance;
        private readonly IMessenger messenger;

        #endregion Field

        #region Properties

        /// <summary>
        /// Стрка подключения
        /// </summary>
        public string ConStrBuilder {get; set;}

        /// <summary>
        /// Выбранное имя  SQL сервера
        /// </summary>
        public string SelectedServerName { get; set; }

        /// <summary>
        /// Список SQL серверов
        /// </summary>
        public ObservableCollection<string> ListServerName { get; set; }

        #endregion Properties

        #region Constructor

        public ServerConnectionViewModel(IMessenger messenger)
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
                        () => messenger.Send(new CloseViewMessage("ServerConnectView", true))));
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
                        () => messenger.Send(new CloseViewMessage("ServerConnectView", false))));
            }
        }

        #endregion
    }
}
