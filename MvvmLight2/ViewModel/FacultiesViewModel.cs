using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Университеты с факультетами
    /// </summary>
    public class FacultiesViewModel : WorkspaceViewModel
    {
        private IContainer container = Container.Instance;

        #region SelectedUniversity

        /// <summary>
        /// Выделенный в списке университет - поле
        /// </summary>
        private University selectedUniversity;

        /// <summary>
        /// Выделенный в списке университет - Свойство
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

        #region SelectedFaculty

        /// <summary>
        /// Выделенный в списке факультет - поле
        /// </summary>
        private Faculty selectedFaculty;

        /// <summary>
        /// Выделенный в списке факультет - Свойство
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

        #region Properties


        /// <summary>
        /// Коллекция классов данных по университетам/филиалам
        /// </summary>
        public ObservableCollection<University> Universities { get; private set; }

        /// <summary>
        /// Коллекция классов данных по факультетам
        /// </summary>
        public ObservableCollection<Faculty> Faculties { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the FacultiesViewModel class.
        /// </summary>
        public FacultiesViewModel(IServiceFaculty service)
        {
            int idUniversity = 1;

            if (SelectedUniversity != null)
                idUniversity = SelectedUniversity.Id;

            service.GetDataFaculty(
               (faculties, error) =>
               {
                   if (error != null)
                   {
                       // Report error here
                       return;
                   }

                   Faculties = faculties;
               }, idUniversity);

            base.DisplayName = "Факультеты";
        }

        #endregion Constructor

        #region CommandGet

        /// <summary>
        /// Команда - Загрузка данных по факультету - поле
        /// </summary>
        private ICommand getCommand;

        /// <summary>
        /// Команда - Загрузка данных по факультету - Свойство
        /// </summary>
        public ICommand GetCommand
        {
            get
            {
                return getCommand ??
                    (getCommand = new RelayCommand(GetExecute));
            }

        }

        /// <summary>
        /// Загрузка  данных по факультету для выбранного университета - метод
        /// </summary>
        private void GetExecute()
        {
            var service = container.Resolve<IServiceFaculty>();
            Faculties.Clear();
            Faculties = service.GetFaculty(Faculties, selectedUniversity.Id);
        }

        #endregion CommandGet

        #region CommandEdit

        /// <summary>
        /// Команда - Редактирование данных по факультету - поле
        /// </summary>
        private ICommand editFaculty;

        /// <summary>
        /// Команда - Редактирование данных по факультету - Свойство
        /// </summary>
        public ICommand EditFaculty
        {
            get
            {
                return editFaculty ??
                    (editFaculty = new RelayCommand(EditFacultyExecute, CanExecute));
            }

        }

        /// <summary>
        /// Редактирование данных по факультету - метод
        /// </summary>
        private void EditFacultyExecute()
        {
            var viewModel = container.Resolve<EditFacultyViewModel>();

            viewModel.Faculty = selectedFaculty;

            var viewEdit = container.Resolve<EditFacultyView>();

            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Редактирование данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceFaculty>();
                service.EditItemFaculty(selectedFaculty);
            }
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveCurriculumCommand -  Редактирование / Удаление данных по факультету
        /// </summary>
        /// <returns></returns>
        private bool CanExecute()
        {
            return selectedFaculty != null;
        }

        #endregion CommandEdit

        #region CommandAdd

        /// <summary>
        /// Команда - Добавление данных по факультету - поле
        /// </summary>
        private ICommand addFaculty;

        /// <summary>
        /// Команда - Добавление данных по факультету - Свойство
        /// </summary>
        public ICommand AddFaculty
        {
            get
            {
                return addFaculty ??
                    (addFaculty = new RelayCommand(AddFacultyExecute));

            }
        }

        /// <summary>
        /// Добавление данных по факультету - метод
        /// </summary>
        private void AddFacultyExecute()
        {
            var viewModel = container.Resolve<EditFacultyViewModel>();

            Faculty newfac = new Faculty();
            newfac.IdUniversity = selectedUniversity.Id;
            newfac.StatusDel = true;

            viewModel.Faculty = newfac;

            var viewEdit = container.Resolve<EditFacultyView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Добавление данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceFaculty>();
                //var fac = service.AddItemDataFaculty(newfac);
                service.AddItemDataFaculty(newfac);

                Faculties.Clear();
                Faculties = service.GetFaculty(Faculties, selectedUniversity.Id);
                
                /// Возможно потребуется установить фокус на созданную запись в списке преподавателей
            }
        }

        #endregion CommandAdd

        #region CommandRemove

        /// <summary>
        /// Команда - Удаление данных по факультету - поле
        /// </summary>
        private ICommand removeFaculty;

        /// <summary>
        /// Команда - Удаление данных по факультету - Свойство
        /// </summary>
        public ICommand RemoveFaculty
        {
            get
            {
                return removeFaculty ??
                    (removeFaculty = new RelayCommand(RemoveFacultyExecute, CanExecute));

            }

        }

        /// <summary>
        /// Удаление данных по факультету - метод
        /// </summary>
        private void RemoveFacultyExecute()
        {
            MessageBoxResult result = MessageBox.Show("Удалить данные по факультету: \n" + SelectedFaculty.Name,
                   "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var facultyService = container.Resolve<IServiceFaculty>();
                facultyService.RemoveItemFaculty(selectedFaculty);

                Faculties.Remove(selectedFaculty);
            }
        }
        #endregion CommandEdit
    }
}
