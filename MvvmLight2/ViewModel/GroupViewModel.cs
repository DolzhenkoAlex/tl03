using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ServiceData;
using MvvmLight2.View;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight2.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// ViewModel - Студенческие группы
    /// Этот класс содержит свойства, которые представление View 
    /// может использовать для привязки данных
    /// </summary>
    public class GroupViewModel : WorkspaceViewModel
    {
        private IContainer container = Container.Instance;

        #region NameGroup

        /// <summary>
        /// Наименование группы - поле
        /// </summary>
        string nameGroup = string.Empty;

        /// <summary>
        /// Наименование группы - свойство
        /// </summary>
        public string NameGroup
        {
            get { return nameGroup; }
            set
            {
                if (value == nameGroup)
                    return;
                else
                {
                    nameGroup = value;
                    RaisePropertyChanged("NameGroup");
                }
            }
        }

        #endregion NameGroup

        #region CodeSpeciality

        /// <summary>
        /// Шифр специальности - поле
        /// </summary>
        string codeSpeciality = string.Empty;

        /// <summary>
        /// Шифр специальности - свойство
        /// </summary>
        public string CodeSpeciality
        {
            get { return codeSpeciality; }
            set
            {
                if (value == codeSpeciality)
                    return;
                else
                {
                    codeSpeciality = value;
                    RaisePropertyChanged("CodeSpeciality");
                }
            }
        }

        #endregion CodeSpeciality

        #region Course

        /// <summary>
        /// Курс - поле
        /// </summary>
        string course = string.Empty;

        /// <summary>
        /// Курс - свойство
        /// </summary>
        public string Course
        {
            get { return course; }
            set
            {
                if (value == course)
                    return;
                else
                {
                    course = value;
                    RaisePropertyChanged("Course");
                }
            }
        }

        #endregion Course

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

        #region SelectedAcademicYear

        /// <summary>
        /// Поле - Выделенный в списке академический год
        /// </summary>
        private DictAcademicYear selectedAcademicYear;

        /// <summary>
        /// Свойство - Выделенный в списке академический год
        /// </summary>
        public DictAcademicYear SelectedAcademicYear
        {
            get { return selectedAcademicYear; }
            set
            {

                if (value == selectedAcademicYear)
                    return;

                selectedAcademicYear = value;
                RaisePropertyChanged("SelectedAcademicYear");
            }
        }

        #endregion SelectedAcademicYear

        #region SelectedGroup

        /// <summary>
        /// Поле - выделенная в списке кафедра
        /// </summary>
        private StudentGroup selectedGroup;

        /// <summary>
        /// Свойство - выделенная в списке кафедра
        /// </summary>
        public StudentGroup SelectedGroup
        {
            get { return selectedGroup; }
            set
            {
                if (value == selectedGroup)
                    return;
                else
                {
                    selectedGroup = value;
                    RaisePropertyChanged("SelectedGroup");
                }
            }
        }

        #endregion SelectedGroup

        #region SelectedIndexAcademicYear

        /// <summary>
        /// Выделенный индекс в списке учебных годов - поле
        /// </summary>
        int selectedAcademicYearIndex;
        /// <summary>
        /// Выделенный индекс в списке учебных годов  - свойство
        /// </summary>
        public int SelectedIndexAcademicYear
        {
            get { return selectedAcademicYearIndex; }
            set
            {
                if (value == selectedAcademicYearIndex)
                    return;
                else
                {
                    selectedAcademicYearIndex = value;
                    RaisePropertyChanged("SelectedIndexAcademicYear");
                }
            }
        }

        #endregion SelectedAcademicYearIndex

        #region SelectedEducationForm

        /// <summary>
        /// Выделенная в списке форма обучения - поле
        /// </summary>
        DictEducationForm selectedEducationForm;
        /// <summary>
        /// Выделенная в списке форма обучения - свойство
        /// </summary>
        public DictEducationForm SelectedEducationForm
        {
            get { return selectedEducationForm; }
            set
            {
                if (value == selectedEducationForm)
                    return;
                else
                {
                    selectedEducationForm = value;
                    RaisePropertyChanged("SelectedEducationForm");
                }
            }
        }

        #endregion SelectedEducationForm

        #region SelectedIndexEducationForm

        /// <summary>
        /// Выделенный индекс в списке форм обучения - поле
        /// </summary>
        int selectedIndexEducationForm = 0;
        /// <summary>
        /// Выделенный индекс в списке форм обучения  - свойство
        /// </summary>
        public int SelectedIndexEducationForm
        {
            get { return selectedIndexEducationForm; }
            set
            {
                if (value == selectedIndexEducationForm)
                    return;
                else
                {
                    selectedIndexEducationForm = value;
                    RaisePropertyChanged("SelectedIndexEducationForm");
                }
            }
        }

        #endregion SelectedQualification

        //#region SelectedIndexFaculty

        ///// <summary>
        ///// Выделенный индекс в списке факультетов - поле
        ///// </summary>
        //int selectedIndexFaculty;
        ///// <summary>
        ///// Выделенный индекс в списке факультетов  - свойство
        ///// </summary>
        //public int SelectedIndexFaculty
        //{
        //    get { return selectedIndexFaculty; }
        //    set
        //    {
        //        if (value == selectedIndexFaculty)
        //            return;
        //        else
        //        {
        //            selectedIndexFaculty = value;
        //            RaisePropertyChanged("SelectedIndexFaculty");
        //        }
        //    }
        //}

        //#endregion SelectedIndexFaculty

        #region Индикатор выполнения длительной операции

        bool isBusyGroup = false;


        public bool IsBusyGroup
        {
            get { return isBusyGroup; }
            set
            {
                if (value == IsBusyGroup)
                    return;

                isBusyGroup = value;
                RaisePropertyChanged("IsBusyGroup");
            }
        }

        #endregion Индикатор выполнения длительной операции

        #region Сообщение при выполнении длительной операции

        string busyMessage;

        public string BusyMessage
        {
            get { return busyMessage; }
            set
            {
                if (value == busyMessage)
                    return;

                busyMessage = value;
                RaisePropertyChanged("BusyMessage");
            }
        }

        #endregion Сообщение при выполнении длительной операции

        #region Properties for binding

        /// <summary>
        /// Коллекция классов данных по факультетам
        /// </summary>
        public ObservableCollection<Faculty> Faculties { get; private set; }

        /// <summary>
        /// Коллекция классов данных по кафедрам
        /// </summary>
        public ObservableCollection<StudentGroup> Groups { get; private set; }

        /// <summary>
        /// Коллекция имен учебных планов
        /// </summary>
        public ObservableCollection<CurriculumShort> ListCurriculum { get; set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the ChairsViewModel class.
        /// </summary>
        public GroupViewModel(IServiceGroup service)
        {
            int idFaculty = 0;
            if (SelectedFaculty != null)
                idFaculty = SelectedFaculty.Id;

            int idAcademicYear = 0;
            if (SelectedAcademicYear != null)
                idAcademicYear = selectedAcademicYear.Id;

            service.GetDataGroup(
               (groups, error) =>
               {
                   if (error != null)
                   {
                       // Report error here
                       return;
                   }

                   Groups = groups;
               }, idFaculty, idAcademicYear);

            ListCurriculum = new ObservableCollection<CurriculumShort>();

            base.DisplayName = "Контингент";
        }

        #endregion Constructor

        #region Helper

        /// <summary>
        /// Очистка параметров поиска
        /// </summary>
        private void ClearFindParametrs()
        {
            NameGroup = string.Empty;
            SelectedIndexEducationForm = 0;
            Course = string.Empty;
        }

        #endregion Helper

        #region CommandGetGroup

        /// <summary>
        /// Команда - Загрузка данных по группе - поле
        /// </summary>
        private ICommand getGroup;

        /// <summary>
        /// Команда - Загрузка данных по группе - Свойство
        /// </summary>
        public ICommand GetGroup
        {
            get
            {
                return getGroup ??
                    (getGroup = new RelayCommand(GetGroupExecute));
            }

        }

        private async void GetGroupExecute()
        {
            var service = container.Resolve<IServiceGroup>();
            Groups.Clear();
            ListCurriculum.Clear();

            if ((SelectedFaculty != null) && (SelectedAcademicYear != null))
            {
                // установка IsBusy перед началом ассинхронного потока визуализации выполнения длительной операции
                IsBusyGroup = true;
                BusyMessage = "Загрузка данных . . .";

                // Аcсинхронный вызов метода для формирования данных учебным группам
                var StudentGroups = await Task.Factory.StartNew(() =>
                    {
                        ListCurriculum = service.GetCurriculum(selectedAcademicYear.Id);
                        ObservableCollection<StudentGroup> groups = new ObservableCollection<StudentGroup>();
                        if (selectedFaculty.Name == "Не задано")
                            groups = service.GetAllGroup(selectedAcademicYear.Id);
                        else
                            groups = service.GetGroup(SelectedFaculty.Id, selectedAcademicYear.Id);
                        return groups;
                    });

                foreach (var gr in StudentGroups)
                    Groups.Add(gr);
                IsBusyGroup = false;
                ClearFindParametrs();
            }
        }

        #endregion CommandGet

        #region CommandAdd

        /// <summary>
        /// Добавление данных по группе - поле
        /// </summary>
        private ICommand addGroup;

        /// <summary>
        /// Добавление данных по группе - Свойство
        /// </summary>
        public ICommand AddGroup
        {
            get
            {
                return addGroup ??
                    (addGroup = new RelayCommand(AddGroupExecute, CanExecuteAdd));
            }
        }

        private bool CanExecuteAdd()
        {
            if (SelectedFaculty != null)
                return (SelectedFaculty.Name != "Не задано");
            else
                return false;
        }

        /// <summary>
        /// Добавление данных по группе - метод
        /// </summary>
        private void AddGroupExecute()
        {
            var viewModel = container.Resolve<EditGroupViewModel>();

            StudentGroup newGroup = new StudentGroup();

            newGroup.IdFaculty = SelectedFaculty.Id;
            newGroup.IdAcademicYear = SelectedAcademicYear.Id;
            newGroup.IdFormEducation = 1;
            newGroup.IdQualification = 1;
            newGroup.IdSpeciality = 1;
            newGroup.StatusDel = true;
            newGroup.CountStudent = 0;
            newGroup.CountComStudent = 0;
            newGroup.CountForeignStudent = 0;
            newGroup.CountSubgroup = 1;

            viewModel.Group = newGroup;
            viewModel.ListCurriculum = this.ListCurriculum;

            var viewEdit = container.Resolve<EditGroupView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Добавление данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceGroup>();
                service.AddItemDataGroup(newGroup);

                //Обноление данных по группам
                Groups.Clear();
                if (selectedFaculty.Name == "Не задано")
                    Groups = service.GetAllGroup(Groups, selectedAcademicYear.Id);
                else
                    Groups = service.GetGroup(Groups, SelectedFaculty.Id, selectedAcademicYear.Id);
            }
        }

        #endregion CommandAdd

        #region CommandEdit

        /// <summary>
        /// Редактирование данных по группе - поле
        /// </summary>
        private ICommand editGroup;

        /// <summary>
        /// Редактирование данных по группе - Свойство
        /// </summary>
        public ICommand EditGroup
        {
            get
            {
                return editGroup ??
                    (editGroup = new RelayCommand(EditGroupExecute, CanExecute));

            }
        }

        /// <summary>
        /// Редактирование данных по группе - метод 
        /// </summary>
        private void EditGroupExecute()
        {
            var viewModel = container.Resolve<EditGroupViewModel>();
            viewModel.Group = selectedGroup;
            viewModel.ListCurriculum = this.ListCurriculum;

            var viewEdit = container.Resolve<EditGroupView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Редактирование данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceGroup>();
                service.EditItemGroup(selectedGroup);

                //Groups.Clear();
                //if (selectedFaculty.Name == "Не задано")
                //    Groups = service.GetAllGroup(Groups, selectedAcademicYear.Id);
                //else
                //    Groups = service.GetGroup(Groups, SelectedFaculty.Id, selectedAcademicYear.Id);
            }
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveCurriculumCommand -  Редактирование / Удаление данных по кафедре
        /// </summary>
        /// <returns></returns>
        private bool CanExecute()
        {
            return selectedGroup != null;
        }

        #endregion CommandEdit

        #region CommandRemove

        /// <summary>
        /// Удаление данных по группе - поле
        /// </summary>
        private ICommand removeGroup;

        /// <summary>
        /// Удаление данных по группе - Свойство
        /// </summary>
        public ICommand RemoveGroup
        {
            get
            {
                return removeGroup ??
                    (removeGroup = new RelayCommand(RemoveExecute, CanExecute));
            }
        }

        /// <summary>
        /// Удаление данных по группе - метод
        /// </summary>
        private void RemoveExecute()
        {
            MessageBoxResult result = MessageBox.Show("Удалить данные по группе: \n" + SelectedGroup.NameGroup,
                   "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var service = container.Resolve<IServiceGroup>();
                service.RemoveItemGroup(selectedGroup);
                Groups.Remove(selectedGroup);
            }
        }

        #endregion CommandRemove

        #region CommandRefresh

        /// <summary>
        /// Обновление данных по группам - поле
        /// </summary>
        private ICommand refresh;

        /// <summary>
        /// Обновление данных по группам - Свойство
        /// </summary>
        public ICommand Refresh
        {
            get
            {
                return refresh ??
                    (refresh = new RelayCommand(GetGroupExecute));
            }
        }

        #endregion Refresh

        #region CommandFindGroup

        /// <summary>
        /// Поиск группы по наименованию, шифру специальности и курсу - поле
        /// </summary>
        private ICommand findGroup;

        /// <summary>
        /// Поиск группы по наименованию, шифру специальности и курсу - Свойство
        /// </summary>
        public ICommand FindGroup
        {
            get
            {
                return findGroup ??
                    (findGroup = new RelayCommand(FindGroupExecute, CanExecuteFind));
            }
        }

        /// <summary>
        /// Признак доступности команды FindGroup
        /// </summary>
        /// <returns></returns>

        private bool CanExecuteFind()
        {
            return (nameGroup != "") || (selectedEducationForm.FormEducation != "не задано") || (course != "");
        }

        /// <summary>
        /// Поиск группы по наименованию, шифру специальности и курсу - метод
        /// </summary>
        private void FindGroupExecute()
        {
            // Временная коллекция для поиска/фильтрации записей по группам
            List<StudentGroup> groupListFind = Groups.ToList<StudentGroup>();

            // Выходная последовательность - результат Linq-запроса
            IEnumerable<StudentGroup> query;

            // Запросы для фильтрации данных
            if (nameGroup != "" && selectedEducationForm.FormEducation != "не задано" && course != "")
            {
                // по наименованию, шифру специальности и курсу
                query = groupListFind.Where(n => n.NameGroup.ToUpper().Contains(nameGroup.ToUpper()) &&
                                                 n.IdFormEducation == selectedEducationForm.Id &&
                                                 n.Course.ToString() == course);
            }
            else
                if (nameGroup != "" && selectedEducationForm.FormEducation != "не задано" && course == "")
                {
                    // по наименованию и шифру
                    query = groupListFind.Where(n => n.NameGroup.ToUpper().Contains(nameGroup.ToUpper()) &&
                                                     n.IdFormEducation == selectedEducationForm.Id);
                }
                else
                    if (nameGroup != "" && selectedEducationForm.FormEducation == "не задано" && course != "")
                    {
                        // по наименованию и курсу
                        query = groupListFind.Where(n => n.NameGroup.ToUpper().Contains(nameGroup.ToUpper()) &&
                                                    n.Course.ToString() == course);
                    }
                    else
                        if (nameGroup == "" && selectedEducationForm.FormEducation != "не задано" && course != "")
                        {
                            // по курсу и шифру
                            query = groupListFind.Where(n => n.IdFormEducation == selectedEducationForm.Id &&
                                                             n.Course.ToString() == course);
                        }
                        else
                            if (nameGroup != "" && selectedEducationForm.FormEducation == "не задано" && course == "")
                            {
                                // по наименованию
                                query = groupListFind.Where(n => n.NameGroup.ToUpper().Contains(nameGroup.ToUpper()));
                            }
                            else
                                if (nameGroup == "" && selectedEducationForm.FormEducation != "не задано" && course == "")
                                {
                                    // по шифру
                                    query = groupListFind.Where(n => n.IdFormEducation == selectedEducationForm.Id);
                                }
                                else
                                    // по курсу
                                    query = groupListFind.Where(n => n.Course.ToString() == course);

            /// Анализ результатов фильтрации
            if (query.Count<StudentGroup>() > 0)
            {
                // Обновление коллекции групп
                Groups.Clear();
                foreach (var sp in query)
                    Groups.Add(sp);
            }
            else
            {
                MessageBox.Show("Студенческие группы\n\nГруппа: " + nameGroup +
                    "\nШифр: " + codeSpeciality + "\nКурс: " + course + "\n\nНЕ НАЙДЕНЫ!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #endregion CommandFindGroup

        #region CommandTransferGroups

        /// <summary>
        /// Перевод групп на следующий год обучения - Поле
        /// </summary>
        private ICommand transferGroups;

        /// <summary>
        /// Перевод групп на следующий год обучения - Свойство
        /// </summary>
        public ICommand TransferGroups
        {
            get
            {
                return transferGroups ??
                    (transferGroups = new RelayCommand(TransferGroupsExecute, CanExecuteTransfer));
            }
        }

        /// <summary>
        /// Признак доступности команды TransferGroups
        /// </summary>
        /// <returns></returns>
        private bool CanExecuteTransfer()
        {
            return selectedAcademicYear != null && Groups.Count > 0;
        }

        /// <summary>
        /// Перевод групп на следующий год обучения - Метод
        /// </summary>
        private void TransferGroupsExecute()
        {

            MessageBoxResult result = MessageBox.Show("Перевести группы с " + SelectedAcademicYear.Year + " на следующий учебный год?",
                   "Предупреждение", MessageBoxButton.OKCancel);

            if (result == MessageBoxResult.OK)
            {
                var service = container.Resolve<IServiceGroup>();
                bool canTransfer;
                int countTransferGroups = 0;

                //StreamWriter sw = new StreamWriter("TransferGroupsLog.txt", true, Encoding.UTF8);
                string path = String.Format("{0}-{1}-{2} {3}-{4}-{5}.txt", DateTime.Now.Day, DateTime.Now.Month,
                    DateTime.Now.Year, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                StreamWriter sw = new StreamWriter(path, true, Encoding.UTF8);
                sw.WriteLine("Дата перевода групп: " + DateTime.Now);
                
                System.ComponentModel.BackgroundWorker worker = new System.ComponentModel.BackgroundWorker();
                // Запуск длительной операции перевода групп на следующий курс
                worker.DoWork += (o, ea) =>
                {
                    foreach (var group in Groups)
                    {
                        StudentGroup newGroup = new StudentGroup();
                        if (group.DictQualification.NameQualification == "Бакалавр" && (int)group.Course >= 4) 
                            canTransfer = false;
                        else if (group.DictQualification.NameQualification == "Магистр" && (int)group.Course >= 2) 
                            canTransfer = false;
                        else if (group.DictQualification.NameQualification == "Специалист" && group.DictEducationForm.FormEducation == "очная"
                            && (int)group.Course >= 5) 
                            canTransfer = false;
                        else if (group.DictQualification.NameQualification == "Специалист" && group.DictEducationForm.FormEducation == "заочная"
                            && (int)group.Course >= 6) 
                            canTransfer = false;
                        else canTransfer = true;            
                        
                        if (canTransfer)
                        {
                            newGroup.StatusDel = true;
                            newGroup.IdFaculty = group.IdFaculty;
                            newGroup.IdSpeciality = group.IdSpeciality;
                            newGroup.IdFormEducation = group.IdFormEducation;
                            newGroup.IdQualification = group.IdQualification;
                            newGroup.CountStudent = group.CountStudent;
                            newGroup.CountSubgroup = group.CountSubgroup;
                            newGroup.CountForeignStudent = group.CountForeignStudent;
                            newGroup.CountComStudent = group.CountComStudent;
                            
                            newGroup.Course = group.Course + 1;
                            newGroup.IdAcademicYear = group.IdAcademicYear + 1; ///////////// Возможно нужно переделать способом ниже, но выдает ошибку
                    
                           //string[] massForSplit = group.DictAcademicYear.Year.Split('-');
                           //int firstyear = int.Parse(massForSplit[0]);
                           //int secondyear = int.Parse(massForSplit[1]);
                           //firstyear++;
                           //secondyear++;
                           //newGroup.DictAcademicYear.Year = firstyear.ToString() + "-" + secondyear.ToString();

                          // Изменяем в названии группы курс
                            int tempNumber = group.NameGroup.IndexOf(Regex.Match(group.NameGroup, @"\d+").Value);
                            
                            try
                            {
                                newGroup.NameGroup = group.NameGroup.Substring(0, tempNumber + 1) +
                                    (Convert.ToInt16(group.NameGroup[tempNumber + 1].ToString()) + 1) +
                                    group.NameGroup.Substring(tempNumber + 2);
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message.ToString());
                            }
                            
                            sw.WriteLine("Старые данные: имя группы - " + group.NameGroup + ", курс - " + group.Course + ", кол-во студентов: " + group.CountStudent);
                            sw.WriteLine("Новые данные:  имя группы - " + newGroup.NameGroup + ", курс - " + newGroup.Course + ", кол-во студентов: " + newGroup.CountStudent);
                            
                            try
                            {
                                service.AddItemDataGroup(newGroup);
                                sw.WriteLine("Результат перевода: успешно \n");
                            }
                            catch
                            {
                                sw.WriteLine("Результат перевода: произошла ошибка \n");
                            }
                            countTransferGroups++;
                        }
                    }
                    
                    IsBusyGroup = false;
                };
                
                worker.RunWorkerCompleted += (o, ea) =>
                {
                    Groups.Clear();
                    if (selectedFaculty.Name == "Не задано")
                        Groups = service.GetAllGroup(Groups, selectedAcademicYear.Id);
                    else
                        Groups = service.GetGroup(Groups, SelectedFaculty.Id, selectedAcademicYear.Id);

                    if (countTransferGroups > 0)
                        MessageBox.Show("Количество переведенных на следующий учебный год групп: " + countTransferGroups, "Перевод групп", MessageBoxButton.OK, MessageBoxImage.Information);
                    else
                        MessageBox.Show("Ни одной группы не было переведено.", "Перевод групп", MessageBoxButton.OK, MessageBoxImage.Information);
                    sw.Close();

                };

                // установка IsBusy перед началом ассинхронного потока визуализации выполнения длительной операции
                IsBusyGroup = true;
                BusyMessage = "Перевод групп . . .";
                worker.RunWorkerAsync();               
            }
        }

        #endregion CommandTransferGroups

    }
}
