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
    public class DictTypeTrainingViewModel : WorkspaceViewModel
    {
        private IContainer container = Container.Instance;

        #region SelectedTypeTraining

        /// <summary>
        /// Поле - выделенный в списке тип обучения
        /// </summary>
        private DictTypeTraining selectedTypeTraining;

        /// <summary>
        /// Свойство - выделеный в списке  тип обучения
        /// </summary>
        public DictTypeTraining SelectedTypeTraining
        {
            get { return selectedTypeTraining; }
            set
            {
                if (value == selectedTypeTraining)
                    return;
                else
                {
                    selectedTypeTraining = value;
                    RaisePropertyChanged("SelectedTypeTraining");
                }
            }
        }

        #endregion SelectedTypeTraining

        #region Properties for binding

        /// <summary>
        /// Коллекция классов данных по типам обучения
        /// </summary>
        public ObservableCollection<DictTypeTraining> TypeTrainings { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the SpecialityViewModel class.
        /// </summary>
        public DictTypeTrainingViewModel(IServiceTypeTraining service)
        {
            service.GetDataTypeTraining(
               (typetraining, error) =>
               {
                   if (error != null)
                   {
                       // Report error here
                       return;
                   }

                   TypeTrainings = typetraining;
               });

            base.DisplayName = "Вид учебной работы";
        }

        #endregion Constructor

        #region CommandGet

        /// <summary>
        /// Команда - Загрузка данных по типам обучения - поле
        /// </summary>
        private ICommand getTypeTraining;

        /// <summary>
        /// Команда - Загрузка данных по типам обучения - Свойство
        /// </summary>
        public ICommand GetTypeTraining
        {
            get
            {
                return getTypeTraining ??
                    (getTypeTraining = new RelayCommand(GetTypeTrainingExecute));
            }

        }

        /// <summary>
        /// Загрузка  данных по типам обучения - метод
        /// </summary>
        private void GetTypeTrainingExecute()
        {
            var service = container.Resolve<IServiceTypeTraining>();
            TypeTrainings.Clear();
            TypeTrainings = service.GetTypeTraining(TypeTrainings);
        }


        #endregion CommandGet

        #region CommandEdit

        /// <summary>
        /// Редактирование данных по типам обучения - поле
        /// </summary>
        private ICommand editTypeTraining;

        /// <summary>
        /// Редактирование данных по типам обучения - Свойство
        /// </summary>
        public ICommand EditTypeTraining
        {
            get
            {
                return editTypeTraining ??
                    (editTypeTraining = new RelayCommand(EditTypeTrainingExecute, CanExecute));

            }
        }

        /// <summary>
        /// Редактирование данных по типам обучения - метод 
        /// </summary>
        private void EditTypeTrainingExecute()
        {
            var viewModel = container.Resolve<EditTypeTrainingViewModel>();

            viewModel.DictTypeTraining = SelectedTypeTraining;

            var viewEdit = container.Resolve<EditTypeTrainingView>();

            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Редактирование данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var formService = container.Resolve<IServiceTypeTraining>();
                formService.EditItemTypeTraining(SelectedTypeTraining);
            }
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveTypeTrainingCommand -  Редактирование / Удаление данных по типам обучения
        /// </summary>
        /// <returns></returns>
        private bool CanExecute()
        {
            return selectedTypeTraining != null;
        }

        #endregion CommandEdit

        #region CommandAdd

        /// <summary>
        /// Добавление данных по типам обучения - поле
        /// </summary>
        private ICommand addTypeTraining;

        /// <summary>
        /// Добавление данных по типам обучения - Свойство
        /// </summary>
        public ICommand AddTypeTraining
        {
            get
            {
                return addTypeTraining ??
                    (addTypeTraining = new RelayCommand(AddTypeTrainingExecute));
            }
        }

        /// <summary>
        /// Добавление данных по типам обучения - метод
        /// </summary>
        private void AddTypeTrainingExecute()
        {
            var viewModel = container.Resolve<EditTypeTrainingViewModel>();

            DictTypeTraining newTypeTraining = new DictTypeTraining();
            newTypeTraining.StatusDel = true;
            viewModel.DictTypeTraining = newTypeTraining;

            var viewEdit = container.Resolve<EditTypeTrainingView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Добавление данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceTypeTraining>();
                service.AddItemDataTypeTraining(newTypeTraining);

                TypeTrainings.Clear();
                TypeTrainings = service.GetTypeTraining(TypeTrainings);

            }
        }

        #endregion CommandAdd

        #region CommandRemove

        /// <summary>
        /// Удаление данных по типам обучения - поле
        /// </summary>
        private ICommand removeTypeTraining;

        /// <summary>
        /// Удаление данных по типам обучения - Свойство
        /// </summary>
        public ICommand RemoveTypeTraining
        {
            get
            {
                return removeTypeTraining ??
                    (removeTypeTraining = new RelayCommand(RemoveTypeTrainingExecute, CanExecute));
            }
        }

        /// <summary>
        /// Удаление данных по типам обучения - метод
        /// </summary>
        private void RemoveTypeTrainingExecute()
        {
            MessageBoxResult result = MessageBox.Show("Удалить данные по типу обучения: \n" + SelectedTypeTraining.TypeWork,
                   "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var service = container.Resolve<IServiceTypeTraining>();
                service.RemoveItemTypeTraining(selectedTypeTraining);

                TypeTrainings.Remove(selectedTypeTraining);
            }
        }


        #endregion CommandEdit

    }
}
