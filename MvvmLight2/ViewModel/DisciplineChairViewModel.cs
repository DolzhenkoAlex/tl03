using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Linq;
using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ServiceData;
using MvvmLight2.View;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MvvmLight2.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// Дисциплины кафедры
    /// </summary>
    public class DisciplineChairViewModel : WorkspaceViewModel
    {
        private MvvmLight2.Helper.IContainer container = MvvmLight2.Helper.Container.Instance;

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
        /// Выделенная кафедра
        /// </summary>
        private Chair selectedChair;

        /// <summary>
        /// Выделенная дисциплина кафедра
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

        #region SelectedDisciplineChair

        /// <summary>
        /// Выделенная дисциплина кафедры
        /// </summary>
        tlsp_getDisciplineChair_Result selectedDisciplineChair;

        /// <summary>
        /// Выделенная дисциплина кафедры
        /// </summary>
        public tlsp_getDisciplineChair_Result SelectedDisciplineChair
        {
            get { return selectedDisciplineChair; }
            set
            {

                if (value == selectedDisciplineChair)
                    return;

                selectedDisciplineChair = value;
                RaisePropertyChanged("SelectedDisciplineChair");
            }
        }

        #endregion SelectedDisciplinePlan

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

        #region SelectedIndexFaculty

        /// <summary>
        /// Выделенный индекс в списке факультетов - поле
        /// </summary>
        int selectedIndexFaculty;
        /// <summary>
        /// Выделенный индекс в списке факультетов  - свойство
        /// </summary>
        public int SelectedIndexFaculty
        {
            get { return selectedIndexFaculty; }
            set
            {
                if (value == selectedIndexFaculty)
                    return;
                else
                {
                    selectedIndexFaculty = value;
                    RaisePropertyChanged("SelectedIndexFaculty");
                }
            }
        }

        #endregion SelectedIndexFaculty

        #region SelectedIndexChair

        /// <summary>
        /// Выделенный индекс в списке кафедр - поле
        /// </summary>
        int selectedIndexChair;
        /// <summary>
        /// Выделенный индекс в списке кафедр  - свойство
        /// </summary>
        public int SelectedIndexChair
        {
            get { return selectedIndexChair; }
            set
            {
                if (value == selectedIndexChair)
                    return;
                else
                {
                    selectedIndexChair = value;
                    RaisePropertyChanged("SelectedIndexChair");
                }
            }
        }

        #endregion SelectedIndexChair

        #region SelectedIndexAcademicYear

        /// <summary>
        /// Выделенный индекс в списке учебных годов - поле
        /// </summary>
        int selectedIndexAcademicYear;
        /// <summary>
        /// Выделенный индекс в списке учебных годов  - свойство
        /// </summary>
        public int SelectedIndexAcademicYear
        {
            get { return selectedIndexAcademicYear; }
            set
            {
                if (value == selectedIndexAcademicYear)
                    return;
                else
                {
                    selectedIndexAcademicYear = value;
                    RaisePropertyChanged("SelectedIndexAcademicYear");
                }
            }
        }

        #endregion SelectedIndexAcademicYear

        #region PlanCurriculum

        /// <summary>
        /// Учебный план направления подготовки - поле
        /// </summary>
        string planCurriculum = string.Empty;

        /// <summary>
        /// Учебный план направления подготовки - свойство
        /// </summary>
        public string PlanCurriculum
        {
            get { return planCurriculum; }
            set
            {
                if (value == planCurriculum)
                    return;
                else
                {
                    planCurriculum = value;
                    RaisePropertyChanged("PlanCurriculum");
                }
            }
        }

        #endregion PlanCurriculum

        #region CodeDiscipline

        /// <summary>
        /// Код дисциплины - поле
        /// </summary>
        string codeDiscipline = string.Empty;

        /// <summary>
        /// Код дисциплины - свойство
        /// </summary>
        public string CodeDiscipline
        {
            get { return codeDiscipline; }
            set
            {
                if (value == codeDiscipline)
                    return;
                else
                {
                    codeDiscipline = value;
                    RaisePropertyChanged("CodeDiscipline");
                }
            }
        }

        #endregion CodeDiscipline

        #region NameDiscipline

        /// <summary>
        /// Наименование дисциплины - поле
        /// </summary>
        string nameDiscipline = string.Empty;

        /// <summary>
        /// Наименование дисциплины - свойство
        /// </summary>
        public string NameDiscipline
        {
            get { return nameDiscipline; }
            set
            {
                if (value == nameDiscipline)
                    return;
                else
                {
                    nameDiscipline = value;
                    RaisePropertyChanged("NameDiscipline");
                }
            }
        }

        #endregion NameDiscipline

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
        /// Коллекция классов данных по кафедрам
        /// </summary>
        public ObservableCollection<Chair> Chairs { get; private set; }

        /// <summary>
        /// Коллекция дисциплин кафедры по учебным планам
        /// </summary>
        public ObservableCollection<DisciplineChair> PlanDisciplines { get; private set; }

        /// <summary>
        /// Коллекция дисциплин кафедры
        /// </summary>
        public ObservableCollection<tlsp_getDisciplineChair_Result> Disciplines { get; private set; }

        /// <summary>
        /// Коллекция учебных планов
        /// </summary>
        public ObservableCollection<Curriculum> Curriculums { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the DisciplineOfChairViewModel class.
        /// </summary>
        public DisciplineChairViewModel(IServiceDisciplineChair service)
        {
            //int idSelectedChair = 0;
            int idSelectedChair;
            if (SelectedChair != null)
                idSelectedChair = SelectedChair.Id;
            else
                idSelectedChair = 0;

            int idAcademicYear = 0;
            if (SelectedAcademicYear != null)
                idAcademicYear = selectedAcademicYear.Id;

            if (Chairs == null)
                Chairs = new ObservableCollection<Chair>();

            PlanDisciplines = new ObservableCollection<DisciplineChair>();

            service.GetDataDiscipline(
               (disciplines, error) =>
               {
                   if (error != null)
                   {
                       // Report error here
                       return;
                   }

                   Disciplines = disciplines;
               }, idSelectedChair, idAcademicYear);

            base.DisplayName = "Дисциплины кафедры";
        }

        #endregion Constructor

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
            if (SelectedFaculty != null)
                Chairs = chairService.GetChair(Chairs, SelectedFaculty.Id);

            SelectedIndexChair = Properties.Settings.Default.DisciplineChairComboBoxChairIndex;
            SelectedIndexAcademicYear = Properties.Settings.Default.DisciplineChairComboBoxAcademicYearIndex;
        }


        #endregion CommandGetChair

        #region CommandGetDisciplineChair

        /// <summary>
        /// Команда - Загрузка данных по дисциплинам кафедры - поле
        /// </summary>
        private ICommand getDiscipline;

        /// <summary>
        /// Команда - Загрузка данных по по дисциплинам кафедры - Свойство
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
        /// Загрузка  данных по по дисциплинам кафедры - метод
        /// </summary>
        private void GetDisciplineExecute()
        {
            var service = container.Resolve<IServiceDisciplineChair>();

            Disciplines.Clear();
            if ((selectedChair != null) && (selectedAcademicYear != null ))
                Disciplines = service.GetDiscipline(Disciplines, selectedChair.Id, selectedAcademicYear.Id);

            // Очистка параметров поиска/фильтрации
            PlanCurriculum = string.Empty;
            CodeDiscipline = string.Empty;
            NameDiscipline = string.Empty;
        }

        #endregion CommandGetDisciplineChair

        #region CommandEdit

        /// <summary>
        /// Редактирование данных по кафедре - поле
        /// </summary>
        private ICommand editCommand;

        /// <summary>
        /// Редактирование данных по дисциплинам кафедры - Свойство
        /// </summary>
        public ICommand EditCommand
        {
            get
            {
                return editCommand ??
                    (editCommand = new RelayCommand(EditExecute, CanExecute));

            }
        }

        /// <summary>
        /// Редактирование данных по дисциплинам кафедры - метод 
        /// </summary>
        private void EditExecute()
        {
            //var viewModel = container.Resolve<EditDisciplineOfChairViewModel>();

            //viewModel.DisciplineOfChair = new DataDisciplineOfChair(selectedDisciplineChair);

            //var viewEdit = container.Resolve<EditDisciplineOfChairView>();

            //viewEdit.DataContext = viewModel;

            //var windowService = container.Resolve<IWindowService>();
            //var result = windowService.ShowDialog("Редактирование данных", viewEdit);

            //if (result.HasValue && result.Value == true)
            //{
            //    selectedDisciplineChair.Code = viewModel.DisciplineOfChair.Code;
            //    selectedDisciplineChair.CodePlan = viewModel.DisciplineOfChair.CodePlan;
            //    selectedDisciplineChair.CodeChair = viewModel.DisciplineOfChair.CodeChair;
            //    selectedDisciplineChair.CodeOfSpeciality = viewModel.DisciplineOfChair.CodeOfSpeciality;
            //    selectedDisciplineChair.Chair = viewModel.DisciplineOfChair.Chair;
            //    selectedDisciplineChair.Faculty = viewModel.DisciplineOfChair.Faculty;
            //    selectedDisciplineChair.Qualific = viewModel.DisciplineOfChair.Qualific;
            //    selectedDisciplineChair.FormEducation = viewModel.DisciplineOfChair.FormEducation;
            //    selectedDisciplineChair.Course = viewModel.DisciplineOfChair.Course;
            //    selectedDisciplineChair.Semester = viewModel.DisciplineOfChair.Semester;
            //    selectedDisciplineChair.DisciplinePlan = viewModel.DisciplineOfChair.DisciplinePlan;
            //    selectedDisciplineChair.LecturePlan = viewModel.DisciplineOfChair.LecturePlan;
            //    selectedDisciplineChair.PracticalExercisesPlan = viewModel.DisciplineOfChair.PracticalExercisesPlan;
            //    selectedDisciplineChair.SetOffPlan = viewModel.DisciplineOfChair.SetOffPlan;
            //    selectedDisciplineChair.CourseProjectPlan = viewModel.DisciplineOfChair.CourseProjectPlan;
            //    selectedDisciplineChair.CourseWorktPlan = viewModel.DisciplineOfChair.CourseWorktPlan;
            //    selectedDisciplineChair.ControlWorkPlan = viewModel.DisciplineOfChair.ControlWorkPlan;
            //}
        }

        /// <summary>
        /// Признак доступности команды EditCommand / RemoveCommand -  Редактирование / Удаление данных по нормам нагрузки
        /// </summary>
        /// <returns></returns>
        private bool CanExecute()
        {
            return selectedDisciplineChair != null;
        }

        #endregion CommandEdit

        #region CommandAddDisciplinesFromAllCurriculums

        /// <summary>
        /// Добавление данных по дисциплинам учебных планов - поле
        /// </summary>
        private ICommand addDisciplinesFromAllCurriculums;

        /// <summary>
        /// Добавление данных по дисциплинам учебных планов - Свойство
        /// </summary>
        public ICommand AddDisciplinesFromAllCurriculums
        {
            get
            {
                return addDisciplinesFromAllCurriculums ??
                    (addDisciplinesFromAllCurriculums = new RelayCommand(AddDisciplinesFromAllCurriculumsExecute));
            }
        }

        /// <summary>
        /// Формирование списка дисциплин кафедры по учебным планам - метод
        /// </summary>
        private async void AddDisciplinesFromAllCurriculumsExecute()
        {
            
            MessageBoxResult result = MessageBox.Show("Сформировать дисциплины кафедры \n" +
                "по учебному году "+ SelectedAcademicYear.Year,
                  "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                var service =  container.Resolve<IServiceDisciplineChair>();
                Curriculums = service.GetCurriculum(selectedAcademicYear.Id);

                if ((Curriculums.Count > 0) && (selectedChair != null) && (selectedAcademicYear != null))
                {
                    // установка IsBusy перед началом ассинхронного потока визуализации выполнения длительной операции
                    IsBusyCurriculum = true;
                    BusyMessage = "Загрузка данных . . .";

                    // Аcсинхронный вызов метода 
                    var disciplines = await Task.Factory.StartNew(() =>
                        {
                            foreach (var cur in Curriculums)
                            {
                                PlanDisciplines.Clear();
                                PlanDisciplines = service.SetDisciplineOfCurriculum(PlanDisciplines, selectedChair.Id, selectedAcademicYear.Id, cur.Id);
                                /// Сформировать в логфайл результат формирования дисциплин
                                if (PlanDisciplines.Count > 0)
                                {
                                    foreach (DisciplineChair disc in PlanDisciplines)
                                    {
                                        service.AddItemDataDiscipline(disc);
                                    }
                                }
                            }

                            return service.GetDiscipline(selectedChair.Id, selectedAcademicYear.Id);
                        });

                    Disciplines.Clear();
                    foreach (var disc in disciplines)
                        Disciplines.Add(disc);

                    IsBusyCurriculum = false;


                    
                    // BackgroundWorker worker = new BackgroundWorker();
                    //// Запуск длительной операции добавления данных о новом учебном плане в базу данных
                    // worker.DoWork += (o, ea) =>
                    // {

                    //     foreach (var cur in Curriculums)
                    //     {
                    //         PlanDisciplines.Clear();
                    //         PlanDisciplines = service.SetDisciplineOfCurriculum(PlanDisciplines, selectedChair.Id, selectedAcademicYear.Id, cur.Id);
                    //         /// Сформировать в логфайл результат формирования дисциплин
                    //         if (PlanDisciplines.Count > 0)
                    //         {
                    //             foreach (DisciplineChair disc in PlanDisciplines)
                    //             {
                    //                 service.AddItemDataDiscipline(disc);
                    //             }
                    //         }
                    //     }
                    // };

                    // worker.RunWorkerCompleted += (o, ea) =>
                    //{
                    //    Disciplines.Clear();
                    //    Disciplines = service.GetDiscipline(Disciplines, selectedChair.Id, selectedAcademicYear.Id);
                    //    IsBusyCurriculum = false;
                    //};

                    // // установка IsBusy перед началом ассинхронного потока визуализации выполнения длительной операции
                    // IsBusyCurriculum = true;
                    // BusyMessage = "Загрузка данных . . .";
                    // worker.RunWorkerAsync();
                }
            }
        }

        #endregion CommandAddDisciplinesFromAllCurriculums

        #region CommandRemoveDiscipline

        /// <summary>
        /// Удаление данных по дисциплине кафедры - поле
        /// </summary>
        private ICommand removeDiscipline;

        /// <summary>
        /// Удаление данных по дисциплине кафедры - Свойство
        /// </summary>
        public ICommand RemoveDiscipline
        {
            get
            {
                return removeDiscipline ??
                    (removeDiscipline = new RelayCommand(RemoveDisciplineExecute, CanExecute));
            }
        }

        private async void RemoveDisciplineExecute()
        {
            tlsp_getDisciplineChair_Result disc = selectedDisciplineChair as tlsp_getDisciplineChair_Result;

            MessageBoxResult result = MessageBox.Show("Удалить данные по Дисциплине\n" +
                disc.CodePlan.ToString() + "  " + disc.Discipline.ToString(),
                "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                string messageErrorDelete = string.Empty;
                var service = container.Resolve<IServiceDisciplineChair>();

                IsBusyCurriculum = true;
                BusyMessage = "Удаление данных . . .";

                var resultRemove = await Task.Factory.StartNew(() =>
                {
                    return service.RemoveItemDiscipline(disc, out messageErrorDelete);
                });

                IsBusyCurriculum = false;

                if (resultRemove)
                {
                    MessageBox.Show(messageErrorDelete, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    Disciplines.Remove(selectedDisciplineChair);
                }
            }
        }

        #endregion CommandRemoveDiscipline

        #region RemoveAllDiscipline

        /// <summary>
        /// Удаление данных по всем дисциплинам кафедры - поле
        /// </summary>
        private ICommand removeAllDiscipline;

        /// <summary>
        /// Удаление данных по всем дисциплинам кафедры - Свойство
        /// </summary>
        public ICommand RemoveAllDiscipline
        {
            get
            {
                return removeAllDiscipline ??
                    (removeAllDiscipline = new RelayCommand(RemoveAllDisciplineExecute));
            }
        }

        /// <summary>
        /// Удаление данных по всем дисциплинам кафедры - метод
        /// </summary>
        //private void RemoveAllDisciplineExecute()
        //{
        //    //tlsp_getDisciplineChair_Result disc = selectedDisciplineChair as tlsp_getDisciplineChair_Result;
        //    MessageBoxResult result = MessageBox.Show("Удалить все данные по Дисциплинам кафедры",
        //        "Предупреждение", MessageBoxButton.OKCancel);
        //    if (result == MessageBoxResult.OK)
        //    {
        //        var service = container.Resolve<IServiceDisciplineChair>();

        //         BackgroundWorker worker = new BackgroundWorker();
        //         // Запуск длительной операции добавления данных о новом учебном плане в базу данных
        //         worker.DoWork += (o, ea) =>
        //         {
        //             service.RemoveALLDiscipline(selectedAcademicYear.Id, selectedChair.Id);
        //         };

        //         worker.RunWorkerCompleted += (o, ea) =>
        //            {
        //                Disciplines.Clear();
        //                IsBusyCurriculum = false;
        //            };

        //         // установка IsBusy перед началом ассинхронного потока визуализации выполнения длительной операции
        //         IsBusyCurriculum = true;
        //         BusyMessage = "Удаление данных . . .";
        //         worker.RunWorkerAsync();

        //    }
        //}

        private async void RemoveAllDisciplineExecute()
        {
            MessageBoxResult result = MessageBox.Show("Удалить все данные по Дисциплинам кафедры",
                "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                string messageErrorDelete = string.Empty;

                var service = container.Resolve<IServiceDisciplineChair>();

                IsBusyCurriculum = true;
                BusyMessage = "Удаление данных . . .";

                var resultRemove = await Task.Factory.StartNew(() =>
                {
                   return service.RemoveALLDiscipline(selectedAcademicYear.Id, selectedChair.Id, out messageErrorDelete);
                });
                
                IsBusyCurriculum = false;

                if (resultRemove)
                {
                    MessageBox.Show(messageErrorDelete, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                     Disciplines.Clear();
                }
            }
        }

        #endregion CommandRemoveDiscipline

        #region RefreshDiscipline

        /// <summary>
        /// Команда - Обновление данных по дисциплинам кафедры - поле
        /// </summary>
        private ICommand refreshDiscipline;

        /// <summary>
        /// Команда - Обновление данных по дисциплинам кафедры - Свойство
        /// </summary>
        public ICommand RefreshDiscipline
        {
            get
            {
                return refreshDiscipline ??
                    (refreshDiscipline = new RelayCommand(GetDisciplineExecute));
            }

        }

        #endregion RefreshDiscipline

        #region CommandAddDisciplinesFromCurriculum

        /// <summary>
        /// Добавление данных по дисциплинам кафедры - поле
        /// для выбранного учебного плана
        /// </summary>
        private ICommand addDisciplinesFromCurriculum;

        /// <summary>
        /// Добавление данных по дисциплинам кафедры - Свойство
        /// для выбранного учебного плана
        /// </summary>
        public ICommand AddDisciplinesFromCurriculum
        {
            get
            {
                return addDisciplinesFromCurriculum ??
                    (addDisciplinesFromCurriculum = new RelayCommand(AddDisciplinesFromCurriculumExecute));
            }
        }

        /// <summary>
        /// Добавление дисциплин кафедры  - метод
        /// для выбранного учебного плана
        /// </summary>
        private void AddDisciplinesFromCurriculumExecute()
        {
            var service = container.Resolve<IServiceDisciplineChair>();
            Curriculums = service.GetCurriculum(selectedAcademicYear.Id);

            var viewModel = container.Resolve<AddDisciplinesChairViewModel>();
            viewModel.Curriculums = Curriculums;

            var viewEdit = container.Resolve<AddDisciplineChairView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Выбор учебного плана", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                Curriculum curriculum = viewModel.SelectedCurriculum;

                PlanDisciplines.Clear();
                if ((selectedChair != null) && (selectedAcademicYear != null) && (curriculum != null))
                    PlanDisciplines = service.SetDisciplineOfCurriculum(PlanDisciplines, selectedChair.Id, selectedAcademicYear.Id,curriculum.Id);

                if (PlanDisciplines.Count > 0)
                {
                    foreach (DisciplineChair disc in PlanDisciplines)
                    {
                            service.AddItemDataDiscipline(disc);
                    }

                    Disciplines.Clear();
                    Disciplines = service.GetDiscipline(Disciplines, selectedChair.Id, selectedAcademicYear.Id);
                }
                else
                    MessageBox.Show("В учебном плане  " + curriculum.Name + "\nдисциплины кафедры отсутствуют!!!",
                "Предупреждение", MessageBoxButton.OK);

            }
        }

        #endregion CommandAddDisciplinesFromCurriculum

        #region CommandFindDiscipline

        /// <summary>
        /// Поиск дисциплин кафедры по учебному плану, коду и наименованию дисциплины   - поле
        /// </summary>
        private ICommand findDiscipline;

        /// <summary>
        /// Поиск дисциплин кафедры по учебному плану, коду и наименованию дисциплины - Свойство
        /// </summary>
        public ICommand FindDiscipline
        {
            get
            {
                return findDiscipline ??
                    (findDiscipline = new RelayCommand(FindfindDisciplineExecute, CanExecuteFind));
            }
        }

        /// <summary>
        /// Признак доступности команды FindDiscipline
        /// </summary>
        /// <returns></returns>

        private bool CanExecuteFind()
        {
            return (codeDiscipline != "") || (planCurriculum != "") || (nameDiscipline != "");
        }

        /// <summary>
        /// Поиск дисциплин кафедры по учебному плану, коду и наименованию дисциплины - метод
        /// </summary>
        private void FindfindDisciplineExecute()
        {
            // Временная коллекция для поиска/фильтрации записей по дисциплинам кафедры
            List<tlsp_getDisciplineChair_Result> disciplineListFind = Disciplines.ToList<tlsp_getDisciplineChair_Result>();

            // Выходная последовательность - результат Linq-запроса
            IEnumerable<tlsp_getDisciplineChair_Result> query;

            // Запросы для фильтрации данных
            if (planCurriculum != "" && codeDiscipline != "" && nameDiscipline != "")
            {
                // по коду учебного плана, коду и наименованию дисциплины
                query = disciplineListFind.Where(n => n.Name.ToUpper().Contains(planCurriculum.ToUpper()) &&
                                                      n.CodePlan != null && 
                                                      n.CodePlan.ToUpper().Contains(codeDiscipline.ToUpper()) &&
                                                      n.Discipline.ToUpper().Contains(nameDiscipline.ToUpper()));
            }
            else
                if (planCurriculum != "" && codeDiscipline != "" && nameDiscipline == "")
                {
                    // по коду учебного плана, коду дисциплины
                    query = disciplineListFind.Where(n => n.Name.ToUpper().Contains(planCurriculum.ToUpper()) &&
                                                          n.CodePlan != null && 
                                                          n.CodePlan.ToUpper().Contains(codeDiscipline.ToUpper()));
                }
                else
                    if (planCurriculum != "" && codeDiscipline == "" && nameDiscipline != "")
                    {
                        // по коду учебного плана и наименованию дисциплины
                        query = disciplineListFind.Where(n => n.Name.ToUpper().Contains(planCurriculum.ToUpper()) &&
                                                              n.Discipline.ToUpper().Contains(nameDiscipline.ToUpper()));
                    }
                    else
                        if (planCurriculum == "" && codeDiscipline != "" && nameDiscipline != "")
                        {
                            // по  коду и наименованию дисциплины
                            query = disciplineListFind.Where(n => n.CodePlan != null && 
                                                                  n.CodePlan.ToUpper().Contains(codeDiscipline.ToUpper()) &&
                                                                  n.Discipline.ToUpper().Contains(nameDiscipline.ToUpper()));
                        }
                        else
                            if (planCurriculum != "" && codeDiscipline == "" && nameDiscipline == "")
                            {
                                // по коду учебного плана
                                query = disciplineListFind.Where(n => n.Name.ToUpper().Contains(planCurriculum.ToUpper()));
                            }
                            else
                                if (planCurriculum == "" && codeDiscipline != "" && nameDiscipline == "")
                                {
                                    // по коду  дисциплины
                                    query = disciplineListFind.Where(n => n.CodePlan != null && 
                                                                          n.CodePlan.ToUpper().Contains(codeDiscipline.ToUpper()));
                                }
                                else
                                    // по наименованию дисциплины
                                    query = disciplineListFind.Where(n => n.Discipline.ToUpper().Contains(nameDiscipline.ToUpper()));


            /// Анализ результатов поиска/фильтрации
            if (query.Count<tlsp_getDisciplineChair_Result>() > 0)
            {
                // Обновление коллекции дисциплин кафедры
                Disciplines.Clear();
                foreach (var sp in query)
                    Disciplines.Add(sp);
            }
            else
            {
                MessageBox.Show("Дисциплины кафедры\nУчебный план: " + planCurriculum +
                    "\nКод дисциплины:    " + codeDiscipline + "\nДисциплина:    " + nameDiscipline + "\n НЕ НАЙДЕНЫ!", "Предупреждение",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #endregion CommandFindDiscipline
    }
}
