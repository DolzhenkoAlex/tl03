using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ClassLibraryServiceDB.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MvvmLight2.Helper;
using MvvmLight2.Model;

namespace MvvmLight2.ViewModel
{
    public class NewCurriculumFromXmlViewModel: WorkspaceViewModel
    {
        #region Field

        IContainer container = Container.Instance;
        private readonly IMessenger messenger;

        #endregion Field

        #region Properties

        /// <summary>
        /// Свойство данных по титулам учебных планов
        /// </summary>
        public TitleCurriculum CurriculumPlan { get; set; }

        #endregion Properties

        #region Constructor

        public NewCurriculumFromXmlViewModel(IMessenger messenger)
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
                    (okCommand = new RelayCommand(NewCurriculumExecute));
            }
        }

        /// <summary>
        /// Закрытие окна ввода данных с проверкой 
        /// ввода актуального курса
        /// </summary>
        private void NewCurriculumExecute()
        {
            if (CurriculumPlan.Course1 ||
                CurriculumPlan.Course2 ||
                CurriculumPlan.Course3 ||
                CurriculumPlan.Course4 ||
                CurriculumPlan.Course5 ||
                CurriculumPlan.Course6)
            {
                messenger.Send(new CloseViewMessage("NewCurriculumFromXmlView", true));
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Необходимо задать актуальный курс(ы) для учебного плана",
                      "Ошибка ввода данных", MessageBoxButton.OK,MessageBoxImage.Error);
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
                        () => messenger.Send(new CloseViewMessage("NewCurriculumFromXmlView", false))));
            }
        }

        #endregion
    }
}