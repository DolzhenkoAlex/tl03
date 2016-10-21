using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ServiceData;
using MvvmLight2.View;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;

namespace MvvmLight2.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// Учебные планы
    /// </summary>
    public class CurriculumViewMode: WorkspaceViewModel
    {
        private MvvmLight2.Helper.IContainer container = MvvmLight2.Helper.Container.Instance;

        #region NamePlan

        /// <summary>
        /// Обозначение плана - поле
        /// </summary>
        string namePlan = string.Empty;

        /// <summary>
        /// Обозначение плана - свойство
        /// </summary>
        public string NamePlan
        {
            get { return namePlan; }
            set
            {
                if (value == namePlan)
                    return;
                else
                {
                    namePlan = value;
                    RaisePropertyChanged("NamePlan");
                }
            }
        }

        #endregion NamePlan

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

        #region SelectedCurriculum

        /// <summary>
        /// Выделенный учебный план
        /// </summary>
        private Curriculum selectedCurriculum;

        /// <summary>
        /// Выделенный учебный план
        /// </summary>
        public Curriculum SelectedCurriculum
        {
            get { return selectedCurriculum; }
            set
            {

                if (value == selectedCurriculum)
                    return;

                selectedCurriculum = value;
                RaisePropertyChanged("SelectedCurriculum");
            }
        }

        #endregion SelectedCurriculum

        #region SelectedDiscipline

        /// <summary>
        /// Выделенная дисциплина учебного плана
        /// </summary>
        private DisciplinePlan selectedDiscipline;

        /// <summary>
        /// Выделенная дисциплина учебного плана
        /// </summary>
        public DisciplinePlan SelectedDiscipline
        {
            get { return selectedDiscipline; }
            set
            {

                if (value == selectedDiscipline)
                    return;

                selectedDiscipline = value;
                RaisePropertyChanged("SelectedDiscipline");
            }
        }

        #endregion SelectedDiscipline

        #region  SelectedAcademicYear

        DictAcademicYear selectedAcademicYear;

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

        #endregion  SelectedAcademicYear

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

        #region Индикатор выполнения длительной операции

        bool isBusyCurriculum = false;


        public bool IsBusyCurriculum
        {
            get { return isBusyCurriculum; }
            set
            {
                if (value == IsBusyCurriculum)
                    return;

                isBusyCurriculum = value;
                RaisePropertyChanged("IsBusyCurriculum");
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
        /// Коллекция дисциплин учебного плана
        /// </summary>
        public ObservableCollection<DisciplinePlan> Disciplines { get; private set; }

        /// <summary>
        /// Коллекция учебных планов
        /// </summary>
        public ObservableCollection<Curriculum> Curriculums { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the ChairsViewModel class.
        /// </summary>
        public CurriculumViewMode(IServiceCurriculum service)
        {
            int idAcademicYear = 1;
            if (selectedAcademicYear != null)
                idAcademicYear = selectedAcademicYear.Id;
            
            service.GetDataCurriculum(
               (curriculums, error) =>
               {
                   if (error != null)
                   {
                       // Report error here
                       return;
                   }

                   Curriculums = curriculums;
               }, idAcademicYear);

            Disciplines = new ObservableCollection<DisciplinePlan>();
            base.DisplayName = "Учебные планы";
        }

        #endregion Constructor

        #region CommandGetCurriculum

        /// <summary>
        /// Команда - Загрузка данных по учебному плану - поле
        /// </summary>
        private ICommand getCurriculum;

        /// <summary>
        /// Команда - Загрузка данных по учебному плану - Свойство
        /// </summary>
        public ICommand GetCurriculum
        {
            get
            {
                return getCurriculum ??
                    (getCurriculum = new RelayCommand(GetCurriculumExecute));
            }

        }

        /// <summary>
        /// Загрузка  данных по дисциплинам учебного плана- метод
        /// </summary>
        private void GetCurriculumExecute()
        {
            var service = container.Resolve<IServiceCurriculum>();

            if (Curriculums != null)
            {
                Curriculums.Clear();
                Curriculums = service.GetCurriculum(Curriculums, selectedAcademicYear.Id);
            }

            NamePlan = string.Empty;
            Course = string.Empty;
            SelectedIndexEducationForm = 0;
        }

        #endregion GetCommandGetCurriculums

        #region CommandGetDiscipline

        /// <summary>
        /// Команда - Загрузка данных по учебному плану - поле
        /// </summary>
        private ICommand getDiscipline;

        /// <summary>
        /// Команда - Загрузка данных по учебному плану - Свойство
        /// </summary>
        public ICommand GetDiscipline
        {
            get
            {
                return getDiscipline ??
                    (getDiscipline = new RelayCommand(GetDisciplineExecute));
            }

        }

        /// <summary>
        /// Загрузка  данных по дисциплинам учебного плана- метод
        /// </summary>
        private void GetDisciplineExecute()
        {
            var service = container.Resolve<IServiceCurriculum>();

            if (Disciplines != null)
            {
                Disciplines.Clear();
                if (SelectedCurriculum != null)
                    Disciplines = service.GetDiscipline(Disciplines, SelectedCurriculum.Id);
            }
        }


        #endregion GetCommandGetDiscipline
        
        #region CommandAddCurriculum

        /// <summary>
        /// Добавление данных по учебному плану - поле
        /// </summary>
        private ICommand addCurriculum;

        /// <summary>
        /// Добавление данных по учебному плану - Свойство
        /// </summary>
        public ICommand AddCurriculum
        {
            get
            {
                return addCurriculum ??
                    (addCurriculum = new RelayCommand(AddCurriculumExecute));
            }
        }

        /// <summary>
        /// Добавление данных по учебному плану - метод
        /// </summary>
        private void AddCurriculumExecute()
        {
            var viewModel = container.Resolve<EditCurriculumViewModel>();

            Curriculum newCurriculum = new Curriculum();
            newCurriculum.IdAcademicYear = 1;
            newCurriculum.IdChair = 1;
            newCurriculum.IdEducationForm = 1;
            newCurriculum.IdQualification = 1;
            newCurriculum.IdSpeciality = 1;
            newCurriculum.Course1 = false;
            newCurriculum.Course2 = false;
            newCurriculum.Course3 = false;
            newCurriculum.Course4 = false;
            newCurriculum.Course5 = false;
            newCurriculum.Course6 = false;
            
            viewModel.Curriculum = newCurriculum;
            viewModel.Curriculum.DataApproval = DateTime.Now;

            var viewEdit = container.Resolve<EditCurriculumView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Добавление данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {

                var service = container.Resolve<IServiceCurriculum>();
                service.AddItemCurriculum(newCurriculum);

                Curriculums.Clear();
                Curriculums = service.GetCurriculum(Curriculums, selectedAcademicYear.Id);
            }
        }

        #endregion CommandAddCurriculum

        #region CommandEditCurriculum

        /// <summary>
        /// Редактирование данных по учебному плану - поле
        /// </summary>
        private ICommand editCurriculum;

        /// <summary>
        /// Редактирование данных по учебному плану - Свойство
        /// </summary>
        public ICommand EditCurriculum
        {
            get
            {
                return editCurriculum ??
                    (editCurriculum = new RelayCommand(EditCurriculumExecute, CanCurriculumExecute));
            }
        }

        /// <summary>
        /// Редактирование данных по учебному плану - метод 
        /// </summary>
        private void EditCurriculumExecute()
        {
            var viewModel = container.Resolve<EditCurriculumViewModel>();
            viewModel.Curriculum = SelectedCurriculum;

            var viewEdit = container.Resolve<EditCurriculumView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Редактирование данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceCurriculum>();
                service.EditItemCurriculum(selectedCurriculum);

                Curriculums.Clear();
                Curriculums = service.GetCurriculum(Curriculums, selectedAcademicYear.Id);
            }
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveCurriculumCommand -  Редактирование / Удаление данных по нормам нагрузки
        /// </summary>
        /// <returns></returns>
        private bool CanCurriculumExecute()
        {
            return selectedCurriculum != null;
        }

        #endregion CommandEditCurriculum

        #region CommandRemoveCurriculum

        /// <summary>
        /// Удаление данных по учебному плану - поле
        /// </summary>
        private ICommand removeCurriculum;

        /// <summary>
        /// Удаление данных по учебному плану - Свойство
        /// </summary>
        public ICommand RemoveCurriculum
        {
            get
            {
                return removeCurriculum ??
                    (removeCurriculum = new RelayCommand(RemoveCurriculumExecute, CanCurriculumExecute));
            }
        }

        /// <summary>
        /// Удаление данных по учебному плану - метод
        /// </summary>
        private void RemoveCurriculumExecute()
        {
            MessageBoxResult result = MessageBox.Show("Удалить данные по Учебному плану\nОбозначение плана: " + selectedCurriculum.Name
                 + "\nУчебный год:              " + selectedCurriculum.DictAcademicYear.Year
                 + "\nШифр направления: " + selectedCurriculum.DictSpeciality.CodeSpeciality 
                 + "\nНаправление:             " + selectedCurriculum.DictSpeciality.NameSpeciality,
                   "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                bool resultDeleteDiscipline = false;
                string messageErrorDelete = string.Empty;

                var service = container.Resolve<IServiceCurriculum>();

                 BackgroundWorker worker = new BackgroundWorker();
                // Запуск длительной операции удаления данных о новом учебном плане в базу данных
                worker.DoWork += (o, ea) =>
                {
                    if(Disciplines.Count > 0)
                    {
                        for(int i = 0; i < Disciplines.Count; i++)
                        {
                            resultDeleteDiscipline = service.RemoveItemDiscipline(Disciplines[i], out messageErrorDelete);
                            if(resultDeleteDiscipline)
                            {
                                MessageBox.Show(messageErrorDelete, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                                break;
                            }
                        }
                    }
                };
                worker.RunWorkerCompleted += (o, ea) =>
                {
                    if (!resultDeleteDiscipline)
                    {
                        service.RemoveItemCurriculum(selectedCurriculum);
                        Curriculums.Remove(selectedCurriculum);
                        Disciplines.Clear();
                    }
                    IsBusyCurriculum = false;
                };
                // установка IsBusy перед началом ассинхронного потока визуализации выполнения длительной операции
                IsBusyCurriculum = true;
                BusyMessage = "Удаление данных . . .";
                worker.RunWorkerAsync();
            }
        }

        #endregion CommandRemoveCurriculum

        #region CommandAddDiscipline

        /// <summary>
        /// Добавление данных по дисциплинам учебного плана - поле
        /// </summary>
        private ICommand addDiscipline;

        /// <summary>
        /// Добавление данных по дисциплинам учебного плана - Свойство
        /// </summary>
        public ICommand AddDiscipline
        {
            get
            {
                return addDiscipline ??
                    (addDiscipline = new RelayCommand(AddDisciplineExecute, CanCurriculumExecute));
            }
        }

        /// <summary>
        /// Добавление данных по дисциплинам учебного плана - метод
        /// </summary>
        private void AddDisciplineExecute()
        {
            var viewModel = container.Resolve<EditCurriculumDisciplineViewModel>();

            DisciplinePlan newDiscipline = new DisciplinePlan();
            newDiscipline.IdChair = 1;
            newDiscipline.IdCurriculum = selectedCurriculum.Id;

            viewModel.Discipline = newDiscipline;

            var viewEdit = container.Resolve<EditCurriculumDisciplineView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Редактирование данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceCurriculum>();
                service.AddItemDiscipline(newDiscipline);

                Disciplines.Clear();
                Disciplines = service.GetDiscipline(Disciplines, selectedCurriculum.Id);
            }
        }

        #endregion CommandAddDiscipline

        #region CommandEditDiscipline

        /// <summary>
        /// Редактирование данных по дисциплинам учебного плана - поле
        /// </summary>
        private ICommand editDiscipline;

        /// <summary>
        /// Редактирование данных по дисциплинам учебного плана - Свойство
        /// </summary>
        public ICommand EditDiscipline
        {
            get
            {
                return editDiscipline ??
                    (editDiscipline = new RelayCommand(EditDisciplineExecute, CanDisciplineExecute));

            }
        }

        /// <summary>
        /// Редактирование данных по дисциплинам учебного плана - метод 
        /// </summary>
        private void EditDisciplineExecute()
        {
            var viewModel = container.Resolve<EditCurriculumDisciplineViewModel>();
            viewModel.Discipline = selectedDiscipline;

            var viewEdit = container.Resolve<EditCurriculumDisciplineView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Редактирование данных", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                var service = container.Resolve<IServiceCurriculum>();
                service.EditItemDiscipline(selectedDiscipline);

                Disciplines.Clear();
                Disciplines = service.GetDiscipline(Disciplines, selectedCurriculum.Id);
            }
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveDisciplineCommand -  Редактирование / Удаление данных по дисциплинам учебного плана
        /// </summary>
        /// <returns></returns>
        private bool CanDisciplineExecute()
        {
            return selectedDiscipline != null;
        }

        #endregion CommandEdit

        #region CommandRemoveDiscipline

        /// <summary>
        /// Удаление данных по дисциплине учебного плана - поле
        /// </summary>
        private ICommand removeDiscipline;

        /// <summary>
        /// Удаление данных по дисциплине учебного плана - Свойство
        /// </summary>
        public ICommand RemoveDiscipline
        {
            get
            {
                return removeDiscipline ??
                    (removeDiscipline = new RelayCommand(RemoveDisciplineExecute, CanDisciplineExecute));
            }
        }

        /// <summary>
        /// Удаление данных по дисциплине учебного плана  - метод
        /// </summary>
        private void RemoveDisciplineExecute()
        {
            MessageBoxResult result = MessageBox.Show("Удалить данные по Дисциплине\n" +
                selectedDiscipline.CodePlan + "  " + selectedDiscipline.Discipline,
                   "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                bool resultDeleteDiscipline = false;
                string messageErrorDelete = string.Empty;

                var service = container.Resolve<IServiceCurriculum>();
                resultDeleteDiscipline = service.RemoveItemDiscipline(selectedDiscipline, out messageErrorDelete);
                if (resultDeleteDiscipline)
                {
                    MessageBox.Show(messageErrorDelete, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    Disciplines.Remove(selectedDiscipline);
                }
            }
        }

        #endregion CommandRemoveDiscipline

        #region CommandRefresh

        /// <summary>
        /// Обновление данных по учебным планам - поле
        /// </summary>
        private ICommand refresh;

        /// <summary>
        /// Обновление данных по учебным планам - Свойство
        /// </summary>
        public ICommand Refresh
        {
            get
            {
                return refresh ??
                    (refresh = new RelayCommand(GetCurriculumExecute));
            }
        }

        #endregion Refresh

        #region CommandFindCurriculum

        /// <summary>
        /// Поиск учебных планов по плану, форме обучения и курсу - поле
        /// </summary>
        private ICommand findCurriculum;

        /// <summary>
        /// Поиск учебных планов по плану, форме обучения и курсу - Свойство
        /// </summary>
        public ICommand FindCurriculum
        {
            get
            {
                return findCurriculum ??
                    (findCurriculum = new RelayCommand(FindCurriculumExecute, CanExecuteFindCurriculum));
            }
        }

        /// <summary>
        /// Признак доступности команды FindCurriculum
        /// </summary>
        /// <returns></returns>

        private bool CanExecuteFindCurriculum()
        {
            return (namePlan != "") || (selectedEducationForm.FormEducation != "не задано") || (course != "");
        }

        /// <summary>
        /// Поиск учебных планов по плану, форме обучения и курсу - метод
        /// </summary>
        private void FindCurriculumExecute()
        {
            // Временная коллекция для поиска/фильтрации записей по учебным планам
            List<Curriculum> curriculumListFind = Curriculums.ToList<Curriculum>();

            // Выходная последовательность - результат Linq-запроса
            IEnumerable<Curriculum> query;

            // Запросы для фильтрации данных
            if (namePlan != "" && selectedEducationForm.FormEducation != "не задано" && course != "")
            {
                // по плану, форме обучения и курсу
                switch (course)
                {
                    case "1": query = curriculumListFind.Where(n => n.Name.ToUpper().Contains(namePlan.ToUpper()) &&
                                                               n.IdEducationForm == selectedEducationForm.Id &&
                                                               n.Course1 == true); break;
                    case "2": query = curriculumListFind.Where(n => n.Name.ToUpper().Contains(namePlan.ToUpper()) &&
                                                               n.IdEducationForm == selectedEducationForm.Id &&
                                                               n.Course2 == true); break;
                    case "3": query = curriculumListFind.Where(n => n.Name.ToUpper().Contains(namePlan.ToUpper()) &&
                                                               n.IdEducationForm == selectedEducationForm.Id &&
                                                               n.Course3 == true); break;
                    case "4": query = curriculumListFind.Where(n => n.Name.ToUpper().Contains(namePlan.ToUpper()) &&
                                                               n.IdEducationForm == selectedEducationForm.Id &&
                                                               n.Course4 == true); break;
                    case "5": query = curriculumListFind.Where(n => n.Name.ToUpper().Contains(namePlan.ToUpper()) &&
                                                               n.IdEducationForm == selectedEducationForm.Id &&
                                                               n.Course5 == true); break;
                    case "6": query = curriculumListFind.Where(n => n.Name.ToUpper().Contains(namePlan.ToUpper()) &&
                                                               n.IdEducationForm == selectedEducationForm.Id &&
                                                               n.Course6 == true); break;
                    default: query = null; break;
                }
            }
            else
                if (namePlan != "" && selectedEducationForm.FormEducation != "не задано" && course == "")
                {
                    // по плану и форме обучения
                    query = curriculumListFind.Where(n => n.Name.ToUpper().Contains(namePlan.ToUpper()) &&
                                                     n.IdEducationForm == selectedEducationForm.Id);
                }
                else
                    if (namePlan != "" && selectedEducationForm.FormEducation == "не задано" && course != "")
                    {
                        // по плану и курсу
                        switch (course)
                        {
                            case "1": query = curriculumListFind.Where(n => n.Name.ToUpper().Contains(namePlan.ToUpper()) && n.Course1 == true); break;
                            case "2": query = curriculumListFind.Where(n => n.Name.ToUpper().Contains(namePlan.ToUpper()) && n.Course2 == true); break;
                            case "3": query = curriculumListFind.Where(n => n.Name.ToUpper().Contains(namePlan.ToUpper()) && n.Course3 == true); break;
                            case "4": query = curriculumListFind.Where(n => n.Name.ToUpper().Contains(namePlan.ToUpper()) && n.Course4 == true); break;
                            case "5": query = curriculumListFind.Where(n => n.Name.ToUpper().Contains(namePlan.ToUpper()) && n.Course5 == true); break;
                            case "6": query = curriculumListFind.Where(n => n.Name.ToUpper().Contains(namePlan.ToUpper()) && n.Course6 == true); break;
                            default: query = null; break;
                        };
                    }
                    else
                        if (namePlan == "" && selectedEducationForm.FormEducation != "не задано" && course != "")
                        {
                            // по форме обучения и курсу 
                            switch (course)
                            {
                                case "1": query = curriculumListFind.Where(n => n.IdEducationForm == selectedEducationForm.Id && n.Course1 == true); break;
                                case "2": query = curriculumListFind.Where(n => n.IdEducationForm == selectedEducationForm.Id && n.Course2 == true); break;
                                case "3": query = curriculumListFind.Where(n => n.IdEducationForm == selectedEducationForm.Id && n.Course3 == true); break;
                                case "4": query = curriculumListFind.Where(n => n.IdEducationForm == selectedEducationForm.Id && n.Course4 == true); break;
                                case "5": query = curriculumListFind.Where(n => n.IdEducationForm == selectedEducationForm.Id && n.Course5 == true); break;
                                case "6": query = curriculumListFind.Where(n => n.IdEducationForm == selectedEducationForm.Id && n.Course6 == true); break;
                                default: query = null; break;
                            }
                        }
                        else
                            if (namePlan != "" && selectedEducationForm.FormEducation == "не задано" && course == "")
                            {
                                // по плану
                                query = curriculumListFind.Where(n => n.Name.ToUpper().Contains(namePlan.ToUpper()));
                            }
                            else
                                if (namePlan == "" && selectedEducationForm.FormEducation != "не задано" && course == "")
                                {
                                    // по  форме обучения
                                    query = curriculumListFind.Where(n => n.IdEducationForm == selectedEducationForm.Id);
                                }
                                else
                                {
                                    // по курсу
                                    switch (course)
                                    {
                                        case "1": query = curriculumListFind.Where(n => n.Course1 == true); break;
                                        case "2": query = curriculumListFind.Where(n => n.Course2 == true); break;
                                        case "3": query = curriculumListFind.Where(n => n.Course3 == true); break;
                                        case "4": query = curriculumListFind.Where(n => n.Course4 == true); break;
                                        case "5": query = curriculumListFind.Where(n => n.Course5 == true); break;
                                        case "6": query = curriculumListFind.Where(n => n.Course6 == true); break;
                                        default: query = null; break;
                                    }
                                }


            /// Анализ результатов фильтрации
            if (query != null && query.Count<Curriculum>() > 0)
            {
                // Обновление коллекции учебных планов
                Curriculums.Clear();
                foreach (var cur in query)
                    Curriculums.Add(cur);
            }
            else
            {
                MessageBox.Show("Учебные планы\n\nПлан: " + namePlan +
                    "\nФорма обучения: " + selectedEducationForm.FormEducation + "\nКурс: " + course + "\n\nНЕ НАЙДЕНЫ!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #endregion CommandFindGroup
    }
}