using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ServiceData;
using MvvmLight2.View;

namespace MvvmLight2.ViewModel
{
    public class DictQualificationViewModel : WorkspaceViewModel
    {
        private IContainer container = Container.Instance;

        #region SelectedQualification

        /// <summary>
        /// Поле - выделенная в списке квалификация
        /// </summary>
        private DictQualification selectedQualification;

        /// <summary>
        /// Свойство - выделенная в списке квалификация
        /// </summary>
        public DictQualification SelectedQualification
        {
            get { return selectedQualification; }
            set
            {
                if (value == selectedQualification)
                    return;
                else
                {
                    selectedQualification = value;
                    RaisePropertyChanged("SelectedQualification");
                }
            }
        }

        #endregion SelectedQualification

        #region Properties for binding

        /// <summary>
        /// Коллекция классов данных по калификациям
        /// </summary>
        public ObservableCollection<DictQualification> Qualifications { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the DictQualificationViewModel class.
        /// </summary>
        public DictQualificationViewModel(IServiceQualification service)
        {
            service.GetDataQualification(
               (qualifications, error) =>
               {
                   if (error != null)
                   {
                       // Report error here
                       return;
                   }

                   Qualifications = qualifications;
               });

            base.DisplayName = "Квалификация";
        }

        #endregion Constructor

        #region CommandGet

        /// <summary>
        /// Команда - Загрузка данных по калификациям - поле
        /// </summary>
        private ICommand getQualification;

        /// <summary>
        /// Команда - Загрузка данных по калификациям - Свойство
        /// </summary>
        public ICommand GetQualification
        {
            get
            {
                return getQualification ??
                    (getQualification = new RelayCommand(GetQualificationExecute));
            }

        }

        /// <summary>
        /// Загрузка  данных по калификациям - метод
        /// </summary>
        private void GetQualificationExecute()
        {
            var service = container.Resolve<IServiceQualification>();
            Qualifications.Clear();
            Qualifications = service.GetQualification(Qualifications);
        }


        #endregion CommandGet

        #region CommandEdit

        /// <summary>
        /// Редактирование данных по калификациям - поле
        /// </summary>
        private ICommand editQualification;

        /// <summary>
        /// Редактирование данных по калификациям - Свойство
        /// </summary>
        public ICommand EditQualification
        {
            get
            {
                return editQualification ??
                    (editQualification = new RelayCommand(EditQualificationExecute, CanExecute));

            }
        }

        /// <summary>
        /// Редактирование данных по калификациям - метод 
        /// </summary>
        private void EditQualificationExecute()
        {
            var viewModel = container.Resolve<EditQualificationViewModel>();

            viewModel.DictQualification = SelectedQualification;

            var viewEdit = container.Resolve<EditQualificationView>();

            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Редактирование данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var qualificationService = container.Resolve<IServiceQualification>();
                qualificationService.EditItemQualification(SelectedQualification);
            }
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveQualificationCommand -  Редактирование / Удаление данных по калификациям
        /// </summary>
        /// <returns></returns>
        private bool CanExecute()
        {
            return selectedQualification != null;
        }

        #endregion CommandEdit

        #region CommandAdd

        /// <summary>
        /// Добавление данных по калификациям - поле
        /// </summary>
        private ICommand addQualification;

        /// <summary>
        /// Добавление данных по калификациям - Свойство
        /// </summary>
        public ICommand AddQualification
        {
            get
            {
                return addQualification ??
                    (addQualification = new RelayCommand(AddQualificationExecute));
            }
        }

        /// <summary>
        /// Добавление данных по калификациям - метод
        /// </summary>
        private void AddQualificationExecute()
        {
            var viewModel = container.Resolve<EditQualificationViewModel>();

            DictQualification newQualification = new DictQualification();
            newQualification.StatusDel = true;
            viewModel.DictQualification = newQualification;

            var viewEdit = container.Resolve<EditQualificationView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Добавление данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceQualification>();
                service.AddItemDataQualification(newQualification);

                Qualifications.Clear();
                Qualifications = service.GetQualification(Qualifications);

            }
        }

        #endregion CommandAdd

        #region CommandRemove

        /// <summary>
        /// Удаление данных по калификациям - поле
        /// </summary>
        private ICommand removeQualification;

        /// <summary>
        /// Удаление данных по калификациям - Свойство
        /// </summary>
        public ICommand RemoveQualification
        {
            get
            {
                return removeQualification ??
                    (removeQualification = new RelayCommand(RemoveQualificationExecute, CanExecute));
            }
        }

        /// <summary>
        /// Удаление данных по калификациям - метод
        /// </summary>
        private void RemoveQualificationExecute()
        {
            MessageBoxResult result = MessageBox.Show("Удалить данные по квалификации: \n" + SelectedQualification.NameQualification,
                   "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var service = container.Resolve<IServiceQualification>();
                service.RemoveItemQualification(selectedQualification);

                Qualifications.Remove(selectedQualification);
            }
        }


        #endregion CommandEdit

    }
}
