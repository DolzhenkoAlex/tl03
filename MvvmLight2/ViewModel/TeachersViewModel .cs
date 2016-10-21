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
    /// ViewModel - Преподаватели
    /// Этот класс содержит свойства, которые представление View 
    /// может использовать для привязки данных
    /// </summary>
    public class TeachersViewModel : WorkspaceViewModel
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
        /// Поле - Выделенная в списке кафедра
        /// </summary>
        private Chair selectedChair;

        /// <summary>
        /// Свойство - Выделенная в списке кафедра
        /// </summary>
        public Chair SelectedChair
        {
            get { return selectedChair; }
            set
            {

                if (value == selectedChair)
                    return;

                selectedChair = value;
                RaisePropertyChanged("SelectedChair");
            }
        }

        #endregion SelectedChair

        #region SelectedTeacher

        /// <summary>
        /// Поле - выделенный в списке преподаватель 
        /// </summary>
        private Teacher selectedTeacher;

        /// <summary>
        /// Свойство - выделенный в списке преподаватель
        /// </summary>
        public Teacher SelectedTeacher
        {
            get { return selectedTeacher; }
            set
            {
                if (value == selectedTeacher)
                    return;
                else
                {
                    selectedTeacher = value;
                    RaisePropertyChanged("SelectedTeacher");
                }
            }
        }

        #endregion SelectedTeacher

        #region SelectedFacultyIndex

        /// <summary>
        /// Поле - Выделенный индекс в списке факультетов
        /// </summary>
        private int selectedFacultyIndex;

        /// <summary>
        /// Свойство - Выделенный индекс в списке факультетов
        /// </summary>
        public int SelectedFacultyIndex
        {
            get { return selectedFacultyIndex; }
            set
            {

                if (value == selectedFacultyIndex)
                    return;

                selectedFacultyIndex = value;
                RaisePropertyChanged("SelectedFacultyIndex");
            }
        }

        #endregion SelectedFacultyIndex

        #region SelectedChairIndex

        /// <summary>
        /// Поле - Выделенный индекс в списке кафедр
        /// </summary>
        private int selectedChairIndex;

        /// <summary>
        /// Свойство - Выделенный индекс в списке кафедр
        /// </summary>
        public int SelectedChairIndex
        {
            get { return selectedChairIndex; }
            set
            {

                if (value == selectedChairIndex)
                    return;

                selectedChairIndex = value;
                RaisePropertyChanged("SelectedChairIndex");
            }
        }

        #endregion SelectedChairIndex

        #region Properties for binding

        /// <summary>
        /// Коллекция классов данных по факультетам
        /// </summary>
        public ObservableCollection<Faculty> Faculties { get; private set; }
        
        /// <summary>
        /// Коллекция классов данных по кафедрам
        /// </summary>
        public ObservableCollection<Chair> Chairs { get; private set; }

        /// <summary>
        /// Коллекция классов данных по преподавателям
        /// </summary>
        public ObservableCollection<Teacher> Teachers { get; private set; }

        #endregion Properties

        #region Constructor

        public TeachersViewModel(IServiceTeacher service)
        {
            if (Chairs == null)
                Chairs = new ObservableCollection<Chair>();

            int idChair = Properties.Settings.Default.TeacherComboBoxChairIndex; 

            if (SelectedChair != null)
                idChair = SelectedChair.Id;

            service.GetDataTeacher(
               (teachers, error) =>
               {
                   if (error != null)
                   {
                       // Report error here
                       return;
                   }

                   Teachers = teachers;
               }, idChair);

            base.DisplayName = "Преподаватели";
        }


        #endregion Constructor()

        #region CommandGetChair

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
                    (getChair = new RelayCommand(GetExecuteChair));
            }

        }

        /// <summary>
        /// Загрузка  данных по кафедре для выбранного факультета- метод
        /// </summary>
        private void GetExecuteChair()
        {
            var chairService = container.Resolve<IServiceChair>();
            Chairs.Clear();
            if(SelectedFaculty != null)
                Chairs = chairService.GetChair(Chairs, SelectedFaculty.Id);
            // Установка выделения кафедры 
            SelectedChairIndex = Properties.Settings.Default.TeacherComboBoxChairIndex;   
        }

        #endregion CommandGet

        #region CommandGetTeachers

        /// <summary>
        /// Команда - Загрузка данных по преподавателям - поле
        /// </summary>
        private ICommand getTeacher;

        /// <summary>
        /// Команда - Загрузка данных по преподавателям - Свойство
        /// </summary>
        public ICommand GetTeacher
        {
            get
            {
                return getTeacher ??
                    (getTeacher = new RelayCommand(GetExecute));
            }

        }

        /// <summary>
        /// Загрузка  данных по преподавателям для выбранной кафедры- метод
        /// </summary>
        private void GetExecute()
        {
            var teacherService = container.Resolve<IServiceTeacher>();
            Teachers.Clear();
            if (SelectedChair != null)
                Teachers = teacherService.GetTeachers(Teachers, SelectedChair.Id);
        }

        #endregion CommandGet

        #region CommandEdit

        /// <summary>
        /// Команда редактирования данных по преподавателю - поле
        /// </summary>
        private ICommand editTeacher;

        /// <summary>
        /// Команда редактирования данных по преподавателю - Свойство
        /// </summary>
        public ICommand EditTeacher
        {
            get
            {
                return editTeacher ??
                    (editTeacher = new RelayCommand(EditTeacherExecute, CanExecuteTeacher));
            }
        }


        /// <summary>
        /// Редактирование данных по преподавателю
        /// </summary>
        private void EditTeacherExecute()
        {
            var viewModel = container.Resolve<EditTeacherViewModel>();
            viewModel.Teacher = SelectedTeacher;

            var viewEdit = container.Resolve<EditTeacherView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Редактирование данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceTeacher>();
                service.EditItemTeacher(SelectedTeacher);

                Teachers.Clear();
                Teachers = service.GetTeachers(Teachers, SelectedChair.Id);
            }
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveCurriculumCommand -  Редактирование / Удаление данных по преподавателю
        /// </summary>
        /// <returns></returns>
        private bool CanExecuteTeacher()
        {
            return selectedTeacher != null;
        }

        #endregion CommandEdit

        #region CommandAdd

        /// <summary>
        /// Команда Добавление данных по преподавателю - поле
        /// </summary>
        private ICommand addTeacher;

        /// <summary>
        /// Команда Добавление данных по преподавателю - Свойство
        /// </summary>
        public ICommand AddTeacher
        {
            get
            {
                return addTeacher ??
                    (addTeacher = new RelayCommand(AddTeacherExecute));
            }
        }

        /// <summary>
        /// Добавление данных по преподавателю
        /// </summary>
        private void AddTeacherExecute()
        {
            var viewModel = container.Resolve<EditTeacherViewModel>();

            Teacher newTeacher = new Teacher();
            newTeacher.IdChair = selectedChair.Id;
            newTeacher.IdPost = 1;
            newTeacher.IdTypeEmployment = 1;
            newTeacher.Rate = 1;
            newTeacher.StatusDel = true;
            
            viewModel.Teacher = newTeacher;
            
            var viewEdit = container.Resolve<EditTeacherView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Добавление данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceTeacher>();
                service.AddItemDataTeacher(newTeacher);
                
                // Обновление данных по преподавателям
                Teachers.Clear();
                Teachers = service.GetTeachers(Teachers, SelectedChair.Id);
            }
        }

        #endregion CommandAdd

        #region CommandRemove

        /// <summary>
        /// Команда Удаление данных по преподавателю
        /// </summary>
        private ICommand removeTeacher;

        /// <summary>
        /// Команда Удаление данных по преподавателю = Свойство
        /// </summary>
        public ICommand RemoveTeacher
        {
            get
            {
                return removeTeacher ??
                    (removeTeacher = new RelayCommand(RemoveTeacherExecute, CanExecuteTeacher));

            }
        }

        /// <summary>
        /// Удаление данных по преподавателю
        /// </summary>
        private void RemoveTeacherExecute()
        {
            MessageBoxResult result = MessageBox.Show("Удалить данные по преподавателю: \n" + SelectedTeacher.LastName + " "
                + selectedTeacher.FirstName + " " + selectedTeacher.SecondName,
                   "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var service = container.Resolve<IServiceTeacher>();
                service.RemoveItemTeacher(selectedTeacher);
                
                Teachers.Remove(selectedTeacher);
            }
        }

        #endregion CommandEdit
    }
}
