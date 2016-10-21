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
    public class DictTypeEmploymentViewModel : WorkspaceViewModel
    {
        private IContainer container = Container.Instance;

        #region SelectedTypeEmployment

        /// <summary>
        /// Поле - выделеный в списке тип занятости
        /// </summary>
        private DictTypeEmployment selectedTypeEmployment;

        /// <summary>
        /// Свойство - выделеный в списке тип занятости
        /// </summary>
        public DictTypeEmployment SelectedTypeEmployment
        {
            get { return selectedTypeEmployment; }
            set
            {
                if (value == selectedTypeEmployment)
                    return;
                else
                {
                    selectedTypeEmployment = value;
                    RaisePropertyChanged("SelectedTypeEmployment");
                }
            }
        }

        #endregion SelectedTypeEmployment

        #region Properties for binding

        /// <summary>
        /// Коллекция классов данных по типам занятости
        /// </summary>
        public ObservableCollection<DictTypeEmployment> TypeEmployments { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the TypeEmploymentViewModel class.
        /// </summary>
        public DictTypeEmploymentViewModel(IServiceTypeEmployment service)
        {
            service.GetDataTypeEmployment(
               (typeemployment, error) =>
               {
                   if (error != null)
                   {
                       // Report error here
                       return;
                   }

                   TypeEmployments = typeemployment;
               });

            base.DisplayName = "Тип занятости";
        }

        #endregion Constructor

        #region CommandGet

        /// <summary>
        /// Команда - Загрузка данных по типам занятости - поле
        /// </summary>
        private ICommand getTypeEmployment;

        /// <summary>
        /// Команда - Загрузка данных по типам занятости - Свойство
        /// </summary>
        public ICommand GetTypeEmployment
        {
            get
            {
                return getTypeEmployment ??
                    (getTypeEmployment = new RelayCommand(GetTypeEmploymentExecute));
            }

        }

        /// <summary>
        /// Загрузка  данных по типам занятости - метод
        /// </summary>
        private void GetTypeEmploymentExecute()
        {
            var service = container.Resolve<IServiceTypeEmployment>();
            TypeEmployments.Clear();
            TypeEmployments = service.GetTypeEmployment(TypeEmployments);
        }


        #endregion CommandGet

        #region CommandEdit

        /// <summary>
        /// Редактирование данных по типам занятости - поле
        /// </summary>
        private ICommand editTypeEmployment;

        /// <summary>
        /// Редактирование данных по типам занятости - Свойство
        /// </summary>
        public ICommand EditTypeEmployment
        {
            get
            {
                return editTypeEmployment ??
                    (editTypeEmployment = new RelayCommand(EditTypeEmploymentExecute, CanExecute));

            }
        }

        /// <summary>
        /// Редактирование данных по типам занятости - метод 
        /// </summary>
        private void EditTypeEmploymentExecute()
        {
            var viewModel = container.Resolve<EditTypeEmploymentViewModel>();

            viewModel.DictTypeEmployment = SelectedTypeEmployment;

            var viewEdit = container.Resolve<EditTypeEmploymentView>();

            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Редактирование данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var formService = container.Resolve<IServiceTypeEmployment>();
                formService.EditItemTypeEmployment(SelectedTypeEmployment);
            }
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveTypeEmploymentCommand -  Редактирование / Удаление данных по типам занятости
        /// </summary>
        /// <returns></returns>
        private bool CanExecute()
        {
            return selectedTypeEmployment != null;
        }

        #endregion CommandEdit

        #region CommandAdd

        /// <summary>
        /// Добавление данных по типам занятости - поле
        /// </summary>
        private ICommand addTypeEmployment;

        /// <summary>
        /// Добавление данных по типам занятости - Свойство
        /// </summary>
        public ICommand AddTypeEmployment
        {
            get
            {
                return addTypeEmployment ??
                    (addTypeEmployment = new RelayCommand(AddTypeEmploymentExecute));
            }
        }

        /// <summary>
        /// Добавление данных по типам занятости - метод
        /// </summary>
        private void AddTypeEmploymentExecute()
        {
            var viewModel = container.Resolve<EditTypeEmploymentViewModel>();

            DictTypeEmployment newTypeEmployment = new DictTypeEmployment();
            newTypeEmployment.StatusDel = true;
            viewModel.DictTypeEmployment = newTypeEmployment;

            var viewEdit = container.Resolve<EditTypeEmploymentView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Добавление данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceTypeEmployment>();
                service.AddItemDataTypeEmployment(newTypeEmployment);

                TypeEmployments.Clear();
                TypeEmployments = service.GetTypeEmployment(TypeEmployments);

            }
        }

        #endregion CommandAdd

        #region CommandRemove

        /// <summary>
        /// Удаление данных по типам занятости - поле
        /// </summary>
        private ICommand removeTypeEmployment;

        /// <summary>
        /// Удаление данных по типам занятости - Свойство
        /// </summary>
        public ICommand RemoveTypeEmployment
        {
            get
            {
                return removeTypeEmployment ??
                    (removeTypeEmployment = new RelayCommand(RemoveTypeEmploymentExecute, CanExecute));
            }
        }

        /// <summary>
        /// Удаление данных по типам занятости - метод
        /// </summary>
        private void RemoveTypeEmploymentExecute()
        {
            MessageBoxResult result = MessageBox.Show("Удалить данные по типу занятости: \n" + SelectedTypeEmployment.TypeOfEmployment,
                   "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var service = container.Resolve<IServiceTypeEmployment>();
                service.RemoveItemTypeEmployment(selectedTypeEmployment);

                TypeEmployments.Remove(selectedTypeEmployment);
            }
        }


        #endregion CommandEdit

    }
}
