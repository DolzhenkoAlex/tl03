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
    public class DictUnitViewModel : WorkspaceViewModel
    {
        private IContainer container = Container.Instance;

        #region SelectedUnit

        /// <summary>
        /// Поле - выделенная в списке единица измерения
        /// </summary>
        private DictUnit selectedUnit;

        /// <summary>
        /// Свойство - выделенная в списке единица измерения
        /// </summary>
        public DictUnit SelectedUnit
        {
            get { return selectedUnit; }
            set
            {
                if (value == selectedUnit)
                    return;
                else
                {
                    selectedUnit = value;
                    RaisePropertyChanged("SelectedUnit");
                }
            }
        }

        #endregion SelectedUnit

        #region Properties for binding

        /// <summary>
        /// Коллекция классов данных по единицам измерения
        /// </summary>
        public ObservableCollection<DictUnit> Units { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the DictUnitViewModel class.
        /// </summary>
        public DictUnitViewModel(IServiceUnit service)
        {
            service.GetDataUnit(
               (units, error) =>
               {
                   if (error != null)
                   {
                       // Report error here
                       return;
                   }

                   Units = units;
               });

            base.DisplayName = "Единицы измерения";
        }

        #endregion Constructor

        #region CommandGet

        /// <summary>
        /// Команда - Загрузка данных по единицам измерения - поле
        /// </summary>
        private ICommand getUnit;

        /// <summary>
        /// Команда - Загрузка данных по единицам измерения - Свойство
        /// </summary>
        public ICommand GetUnit
        {
            get
            {
                return getUnit ??
                    (getUnit = new RelayCommand(GetUnitExecute));
            }

        }

        /// <summary>
        /// Загрузка  данных по единицам измерения - метод
        /// </summary>
        private void GetUnitExecute()
        {
            var service = container.Resolve<IServiceUnit>();
            Units.Clear();
            Units = service.GetUnit(Units);
        }


        #endregion CommandGet

        #region CommandEdit

        /// <summary>
        /// Редактирование данных по единицам измерения - поле
        /// </summary>
        private ICommand editUnit;

        /// <summary>
        /// Редактирование данных по единицам измерения - Свойство
        /// </summary>
        public ICommand EditUnit
        {
            get
            {
                return editUnit ??
                    (editUnit = new RelayCommand(EditUnitExecute, CanExecute));

            }
        }

        /// <summary>
        /// Редактирование данных по единицам измерения - метод 
        /// </summary>
        private void EditUnitExecute()
        {
            var viewModel = container.Resolve<EditUnitViewModel>();

            viewModel.DictUnit = SelectedUnit;

            var viewEdit = container.Resolve<EditUnitView>();

            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Редактирование данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var unitService = container.Resolve<IServiceUnit>();
                unitService.EditItemUnit(SelectedUnit);
            }
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveUnitCommand -  Редактирование / Удаление данных по единицам измерения
        /// </summary>
        /// <returns></returns>
        private bool CanExecute()
        {
            return selectedUnit != null;
        }

        #endregion CommandEdit

        #region CommandAdd

        /// <summary>
        /// Добавление данных по единицам измерения - поле
        /// </summary>
        private ICommand addUnit;

        /// <summary>
        /// Добавление данных по единицам измерения - Свойство
        /// </summary>
        public ICommand AddUnit
        {
            get
            {
                return addUnit ??
                    (addUnit = new RelayCommand(AddUnitExecute));
            }
        }

        /// <summary>
        /// Добавление данных по единицам измерения - метод
        /// </summary>
        private void AddUnitExecute()
        {
            var viewModel = container.Resolve<EditUnitViewModel>();

            DictUnit newUnit = new DictUnit();
            newUnit.StatusDel = true;
            viewModel.DictUnit = newUnit;
            var viewEdit = container.Resolve<EditUnitView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Добавление данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceUnit>();
                service.AddItemDataUnit(newUnit);

                Units.Clear();
                Units = service.GetUnit(Units);

            }
        }

        #endregion CommandAdd

        #region CommandRemove

        /// <summary>
        /// Удаление данных по единицам измерения - поле
        /// </summary>
        private ICommand removeUnit;

        /// <summary>
        /// Удаление данных по единицам измерения - Свойство
        /// </summary>
        public ICommand RemoveUnit
        {
            get
            {
                return removeUnit ??
                    (removeUnit = new RelayCommand(RemoveUnitExecute, CanExecute));
            }
        }

        /// <summary>
        /// Удаление данных по единицам измерения - метод
        /// </summary>
        private void RemoveUnitExecute()
        {
            MessageBoxResult result = MessageBox.Show("Удалить данные по единице измерения: \n" + SelectedUnit.Unit,
                   "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var service = container.Resolve<IServiceUnit>();
                service.RemoveItemUnit(selectedUnit);

                Units.Remove(selectedUnit);
            }
        }


        #endregion CommandEdit

    }
}


