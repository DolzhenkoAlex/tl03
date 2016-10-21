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
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// Кафедры
    /// </summary>
    public class ChairsViewModel : WorkspaceViewModel
    {
        private IContainer container = Container.Instance;

        #region SelectedFaculty

        /// <summary>
        /// Поле - Выделенный в списке факультет
        /// </summary>
        private Faculty selectedFaculty;

        /// <summary>
        /// Свойство - Выделенный в списке факультет
        /// </summary>
        public Faculty SelectedFaculty
        {
            get { return selectedFaculty; }
            set
            {

                if (value == selectedFaculty)
                    return;

                selectedFaculty = value;
                RaisePropertyChanged("SelectedFaculty");
            }
        }

        #endregion SelectedFaculty

        #region SelectedChair

        /// <summary>
        /// Поле - выделенная в списке кафедра
        /// </summary>
        private Chair selectedChair;

        /// <summary>
        /// Свойство - выделенная в списке кафедра
        /// </summary>
        public Chair SelectedChair
        {
            get { return selectedChair; }
            set
            {
                if (value == selectedChair)
                    return;
                else
                {
                    selectedChair = value;
                    RaisePropertyChanged("SelectedChair");
                }
            }
        }

        #endregion SelectedChair

        #region Properties for binding

        /// <summary>
        /// Коллекция классов данных по кафедрам
        /// </summary>
        public ObservableCollection<Chair> Chairs { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the ChairsViewModel class.
        /// </summary>
        public ChairsViewModel(IServiceChair service)
        {
            //int idFaculty = 1;

            int idFaculty = Properties.Settings.Default.FacultySelectedIndexProperty -1;

            if (selectedFaculty != null)
                idFaculty = SelectedFaculty.Id;
            
            service.GetDataChair(
               (chairs, error) =>
               {
                   if (error != null)
                   {
                       // Report error here
                       return;
                   }

                   Chairs = chairs;
               }, idFaculty);

            base.DisplayName = "Кафедры";
        }

        #endregion Constructor

        #region CommandGet

        /// <summary>
        /// Команда - Загрузка данных по кафедре - поле
        /// </summary>
        private ICommand getChair;

        /// <summary>
        /// Команда - Загрузка данных по кафедре - Свойство
        /// </summary>
        public ICommand GetChair
        {
            get
            {
                return getChair ??
                    (getChair = new RelayCommand(GetChairExecute));
            }

        }

        /// <summary>
        /// Загрузка  данных по кафедре для выбранного факультета- метод
        /// </summary>
        private void GetChairExecute()
        {
            var service = container.Resolve<IServiceChair>();
            Chairs.Clear();

            if (selectedFaculty != null)
                Chairs = service.GetChair(Chairs, selectedFaculty.Id);
        }


        #endregion CommandGet

        #region CommandEdit

        /// <summary>
        /// Редактирование данных по кафедре - поле
        /// </summary>
        private ICommand editChair;

        /// <summary>
        /// Редактирование данных по кафедре - Свойство
        /// </summary>
        public ICommand EditChair
        {
            get
            {
                return editChair ??
                    (editChair = new RelayCommand(EditChairExecute, CanExecute));

            }
        }

        /// <summary>
        /// Редактирование данных по кафедре - метод 
        /// </summary>
        private void EditChairExecute()
        {
            var viewModel = container.Resolve<EditChairViewModel>();

            viewModel.Chair = SelectedChair;

            var viewEdit = container.Resolve<EditChairView>();

            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Редактирование данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var chairService = container.Resolve<IServiceChair>();
                chairService.EditItemChiar(SelectedChair);
            }
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveCurriculumCommand -  Редактирование / Удаление данных по кафедре
        /// </summary>
        /// <returns></returns>
        private bool CanExecute()
        {
            return selectedChair != null;
        }

        #endregion CommandEdit

        #region CommandAdd

        /// <summary>
        /// Добавление данных по кафедре - поле
        /// </summary>
        private ICommand addChair;

        /// <summary>
        /// Добавление данных по кафедре - Свойство
        /// </summary>
        public ICommand AddChair
        {
            get
            {
                return addChair ??
                    (addChair = new RelayCommand(AddChairExecute));
            }
        }

        /// <summary>
        /// Добавление данных по кафедре - метод
        /// </summary>
        private void AddChairExecute()
        {
            var viewModel = container.Resolve<EditChairViewModel>();

            Chair newChair = new Chair();
            newChair.IdFaculty = selectedFaculty.Id;
            newChair.StatusDel = true;
            viewModel.Chair = newChair;

            var viewEdit = container.Resolve<EditChairView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Добавление данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceChair>();
                service.AddItemDataChair(newChair);

                Chairs.Clear();
                Chairs = service.GetChair(Chairs, selectedFaculty.Id);

                /// Возможно потребуется установить фокус на созданную запись в списке преподавателей
            }
        }

        #endregion CommandAdd

        #region CommandRemove

        /// <summary>
        /// Удаление данных по кафедре - поле
        /// </summary>
        private ICommand removeChair;

        /// <summary>
        /// Удаление данных по кафедре - Свойство
        /// </summary>
        public ICommand RemoveChair
        {
            get
            {
                return removeChair ??
                    (removeChair = new RelayCommand(RemoveChairExecute, CanExecute));
            }
        }

        /// <summary>
        /// Удаление данных по кафедре - метод
        /// </summary>
        private void RemoveChairExecute()
        {
            MessageBoxResult result = MessageBox.Show("Удалить данные по кафедре: \n" + SelectedChair.NameChair,
                   "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var service = container.Resolve<IServiceChair>();
                service.RemoveItemChair(selectedChair);

                Chairs.Remove(selectedChair);
            }
        }


        #endregion CommandEdit
    }
}
