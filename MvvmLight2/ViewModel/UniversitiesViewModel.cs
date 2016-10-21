using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ServiceData;

namespace MvvmLight2.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// Унивенситеты/филиалы
    /// </summary>
    public class UniversitiesViewModel:WorkspaceViewModel
    {
        private IContainer container = Container.Instance;

        #region SelectedUniversity

        /// <summary>
        /// Поле - Выделенный в списке университет
        /// </summary>
        private University selectedUniversity;

        /// <summary>
        /// Свойство - Выделенный в списке университет
        /// </summary>
        public University SelectedUniversity
        {
            get { return selectedUniversity; }
            set
            {

                if (value == selectedUniversity)
                    return;

                selectedUniversity = value;
                RaisePropertyChanged("SelectedUniversity");
            }
        }

        #endregion SelectedUniversity

        #region Property for binding
        /// <summary>
        /// Коллекция классов данных по университетам/филиалам
        /// </summary>
        public ObservableCollection<University> Universities { get; private set; }

        #endregion Property for binding

        #region Constructor

        public UniversitiesViewModel() 
        {
            base.DisplayName = "Университет";
        }

        /// <summary>
        /// Initializes a new instance of the UniversitiesViewModel class.
        /// </summary>
        public UniversitiesViewModel(IServiceUniversity service)
        {
            service.GetDataUniversity(
               (universities, error) =>
               {
                   if (error != null)
                   {
                       string msg = string.Format("{0}\n\nСоздать подключение к серверу?", error.Message);
                       var result = MessageBox.Show(msg, "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                       if (result == MessageBoxResult.OK)
                       {
                           this.Cleanup();
                           //return;
                       }
                       else
                       {
                           App.Current.MainWindow.Close();
                       }
                       
                   }

                   Universities = universities;
               });

            base.DisplayName = "Университет";
        }

        #endregion Constructor

        #region CommandEdit

        /// <summary>
        /// Команда - Редактирование данных по университету - поле
        /// </summary>
        private ICommand editUniversity;

        /// <summary>
        /// Команда - Редактирование данных по университету - Свойство
        /// </summary>
        public ICommand EditUniversity
        {
            get
            {
                return editUniversity ??
                    (editUniversity = new RelayCommand(EditUniversityExecute, CanExecute));
            }

        }

        /// <summary>
        /// Редактирование данных по университету - метод
        /// </summary>
        private void EditUniversityExecute()
        {
            var viewModel = container.Resolve<EditUniversityViewModel>();
            viewModel.University = selectedUniversity;

            var viewEdit = container.Resolve<EditUniversityView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Редактирование данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceUniversity>();
                service.EditItemUniversity(selectedUniversity);
            }
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveCurriculumCommand -  Редактирование / Удаление данных по университету
        /// </summary>
        /// <returns></returns>
        private bool CanExecute()
        {
            return selectedUniversity != null;
        }

        #endregion CommandEdit

        #region CommandAdd

        /// <summary>
        /// Команда - Добавление данных по университету - поле
        /// </summary>
        private ICommand addUniversity;

        /// <summary>
        /// Команда - Добавление данных по университету - Свойство
        /// </summary>
        public ICommand AddUniversity
        {
            get
            {
                return addUniversity ??
                    (addUniversity = new RelayCommand(AddUniversityExecute));
            }
        }

        /// <summary>
        /// Добавление данных по университету - метод
        /// </summary>
        private void AddUniversityExecute()
        {
            var viewModel = container.Resolve<EditUniversityViewModel>();

            University newUniversity = new University();
            newUniversity.StatusDel = true;

            viewModel.University = newUniversity;

            var viewEdit = container.Resolve<EditUniversityView>();

            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Добавление данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceUniversity>();
                service.AddItemDataUniversity(newUniversity);

                Universities.Add(viewModel.University);
            }
        }

        #endregion CommandAdd

        #region CommandRemove

        /// <summary>
        /// Команда - Удаление данных по университету - поле
        /// </summary>
        private ICommand removeUniversity;

        /// <summary>
        /// Команда - Удаление данных по университету - Свойство
        /// </summary>
        public ICommand RemoveUniversity
        {
            get
            {
                return removeUniversity ??
                    (removeUniversity = new RelayCommand(RemoveUniversityExecute, CanExecute));
            }
        }

        /// <summary>
        /// Удаление данных по университету - метод
        /// </summary>
        private void RemoveUniversityExecute()
        {
            MessageBoxResult result = MessageBox.Show("Удалить данные по университету/филиалу: \n" + selectedUniversity.Name,
                   "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var service = container.Resolve<IServiceUniversity>();
                service.RemoveItemUniversity(selectedUniversity);
                
                Universities.Remove(selectedUniversity);
            }
        }
        #endregion CommandRemove
    }
}
