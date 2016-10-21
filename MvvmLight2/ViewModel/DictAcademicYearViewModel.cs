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
    public class DictAcademicYearViewModel : WorkspaceViewModel
    {
        private IContainer container = Container.Instance;

        #region SelectedAcademicYear

        /// <summary>
        /// Поле - выделенный в списке учебный год
        /// </summary>
        private DictAcademicYear selectedAcademicYear;

        /// <summary>
        /// Свойство - выделенный в списке  учебный год
        /// </summary>
        public DictAcademicYear SelectedAcademicYear
        {
            get { return selectedAcademicYear; }
            set
            {
                if (value == selectedAcademicYear)
                    return;
                else
                {
                    selectedAcademicYear = value;
                    RaisePropertyChanged("SelectedAcademicYear");
                }
            }
        }

        #endregion SelectedAcademicYear

        #region Properties for binding

        /// <summary>
        /// Коллекция классов данных по учебному году
        /// </summary>
        public ObservableCollection<DictAcademicYear> AcademicYears { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the AcademicYearViewModel class.
        /// </summary>
        public DictAcademicYearViewModel(IServiceAcademicYear service)
        {
            service.GetDataAcademicYear(
               (years, error) =>
               {
                   if (error != null)
                   {
                       // Report error here
                       return;
                   }

                   AcademicYears = years;
               });

            base.DisplayName = "Учебный год";
        }

        #endregion Constructor

        #region CommandGet

        /// <summary>
        /// Команда - Загрузка данных по учебному году - поле
        /// </summary>
        private ICommand getAcademicYear;

        /// <summary>
        /// Команда - Загрузка данных по учебному году - Свойство
        /// </summary>
        public ICommand GetAcademicYear
        {
            get
            {
                return getAcademicYear ??
                    (getAcademicYear = new RelayCommand(GetAcademicYearExecute));
            }

        }

        /// <summary>
        /// Загрузка  данных по учебному году - метод
        /// </summary>
        private void GetAcademicYearExecute()
        {
            var service = container.Resolve<IServiceAcademicYear>();
            AcademicYears.Clear();
            AcademicYears = service.GetAcademicYear(AcademicYears);
        }


        #endregion CommandGet

        #region CommandEdit

        /// <summary>
        /// Редактирование данных по учебному году - поле
        /// </summary>
        private ICommand editAcademicYear;

        /// <summary>
        /// Редактирование данных по учебному году - Свойство
        /// </summary>
        public ICommand EditAcademicYear
        {
            get
            {
                return editAcademicYear ??
                    (editAcademicYear = new RelayCommand(EditAcademicYearExecute, CanExecute));

            }
        }

        /// <summary>
        /// Редактирование данных по учебному году - метод 
        /// </summary>
        private void EditAcademicYearExecute()
        {
            var viewModel = container.Resolve<EditAcademicYearViewModel>();

            viewModel.DictAcademicYear = SelectedAcademicYear;

            var viewEdit = container.Resolve<EditAcademicYearView>();

            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Редактирование данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var yearService = container.Resolve<IServiceAcademicYear>();
                yearService.EditItemAcademicYear(SelectedAcademicYear);
            }
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveAcademicYearCommand -  Редактирование / Удаление данных по учебному году
        /// </summary>
        /// <returns></returns>
        private bool CanExecute()
        {
            return selectedAcademicYear != null;
        }

        #endregion CommandEdit

        #region CommandAdd

        /// <summary>
        /// Добавление данных по учебному году - поле
        /// </summary>
        private ICommand addAcademicYear;

        /// <summary>
        /// Добавление данных по учебному году - Свойство
        /// </summary>
        public ICommand AddAcademicYear
        {
            get
            {
                return addAcademicYear ??
                    (addAcademicYear = new RelayCommand(AddAcademicYearExecute));
            }
        }

        /// <summary>
        /// Добавление данных по учебному году - метод
        /// </summary>
        private void AddAcademicYearExecute()
        {
            var viewModel = container.Resolve<EditAcademicYearViewModel>();

            DictAcademicYear newAcademicYear = new DictAcademicYear();
            newAcademicYear.StatusDel = true;
            viewModel.DictAcademicYear = newAcademicYear;

            var viewEdit = container.Resolve<EditAcademicYearView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Добавление данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceAcademicYear>();
                service.AddItemDataAcademicYear(newAcademicYear);

                AcademicYears.Clear();
                AcademicYears = service.GetAcademicYear(AcademicYears);

            }
        }

        #endregion CommandAdd

        #region CommandRemove

        /// <summary>
        /// Удаление данных по учебному году - поле
        /// </summary>
        private ICommand removeAcademicYear;

        /// <summary>
        /// Удаление данных по учебному году - Свойство
        /// </summary>
        public ICommand RemoveAcademicYear
        {
            get
            {
                return removeAcademicYear ??
                    (removeAcademicYear = new RelayCommand(RemoveAcademicYearExecute, CanExecute));
            }
        }

        /// <summary>
        /// Удаление данных по учебному году - метод
        /// </summary>
        private void RemoveAcademicYearExecute()
        {
            MessageBoxResult result = MessageBox.Show("Удалить данные по учебному году: \n" + SelectedAcademicYear.Year,
                   "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var service = container.Resolve<IServiceAcademicYear>();
                service.RemoveItemAcademicYear(selectedAcademicYear);

                AcademicYears.Remove(selectedAcademicYear);
            }
        }


        #endregion CommandEdit

    }
}
