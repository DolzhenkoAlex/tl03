﻿using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmLight2.Helper;
using MvvmLight2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmLight2.ViewModel
{
    public class CreateStreamPractViewModel: WorkspaceViewModel
    {
        #region Field

        IContainer container = Container.Instance;
        private readonly IMessenger messenger;

        #endregion Field

        #region Properties

        /// <summary>
        /// Свойство данных по должности
        /// </summary>
        public ObservableCollection<TeachingLoad> DisciplineStream { get; set; }

        #endregion Properties

        #region Constructor

        public CreateStreamPractViewModel(IMessenger messenger)
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
                        () => messenger.Send(new CloseViewMessage("CreateStreamPractView", true))));
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
                        () => messenger.Send(new CloseViewMessage("CreateStreamPractView", false))));
            }
        }

        #endregion
    }
}


