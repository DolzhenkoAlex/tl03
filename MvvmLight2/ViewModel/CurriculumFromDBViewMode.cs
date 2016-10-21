using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Services.Client;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ClassLibraryServiceDB.Model;
using ClassLibraryServiceDB.ServiceData;
using GalaSoft.MvvmLight.Command;
using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ServiceData;
using MvvmLight2.View;

namespace MvvmLight2.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// Учебные планы, формируемые из базы данных Dekanat
    /// </summary>
    public class CurriculumFromDBViewMode: WorkspaceViewModel
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

        #region SelectedCurriculumPlan

        Планы selectedCurriculumPlan;

        public Планы SelectedCurriculumPlan
        {
            get { return selectedCurriculumPlan; }
            set 
            {
                if (value == selectedCurriculumPlan)
                    return;

                selectedCurriculumPlan = value;
                RaisePropertyChanged("SelectedCurriculumPlan");
            }
        }

        #endregion SelectedCurriculumPlan

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

        /// <summary>
        /// Коллекция титулов учебных планов
        /// </summary>
        public ObservableCollection<Планы> TitleCurriculums { get; private set; }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the ChairsViewModel class.
        /// </summary>
        public CurriculumFromDBViewMode(IServiceCurriculum service)
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
            TitleCurriculums = new ObservableCollection<Планы>();
            base.DisplayName = "Учебные планы";
        }

        #endregion Constructor

        #region Helper

        /// <summary>
        /// Проверка наличия в списке учебных планов добавляемого плана
        /// </summary>
        /// <param name="listCurriculums"></param>
        /// <param name="nameCurriculum"></param>
        /// <param name="academicYear"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        private bool ContainCurriculum(
            ObservableCollection<Curriculum> listCurriculums,
            string nameCurriculum,
            int idAcademicYear,
            Func<Curriculum, string, int, bool> predicate)
        {
            bool contain = false;
            foreach (var curr in listCurriculums)
            {
                if (predicate(curr, nameCurriculum, idAcademicYear))
                {
                    contain = true;
                    break;
                }
            }

            return contain;
        }

        /// <summary>
        /// Формирование дисциплин учебного плана по титулу
        /// </summary>
        /// <param name="curriculum"></param>
        private void SetNewDiscipoline(Curriculum newCurriculum)
        {
            DisciplinePlan disciplinePlan;

            var serviceChair = container.Resolve<IServiceChair>();
            var serviceDiscipline = container.Resolve<IServiceCurriculumFromDB>();

            ObservableCollection<ПланыСтроки> listCurriculumPlan = serviceDiscipline.GetListDiscipline(selectedCurriculumPlan.Код);
            List<ПланыЧасы> listWatch = new List<ПланыЧасы>();
            foreach (ПланыСтроки disc in listCurriculumPlan)
            {
                if (!disc.ДисциплинаКод.Contains("Б5"))
                {
                    listWatch.Clear();
                    listWatch = serviceDiscipline.GetListWatch(disc.Код);
                    foreach (var watch in listWatch)
                    {


                        disciplinePlan = new DisciplinePlan();
                        disciplinePlan.IdCurriculum = newCurriculum.Id;

                        // Наименование дисциплины
                        disciplinePlan.Discipline = disc.Дисциплина;

                        // Код дисциплины
                        disciplinePlan.CodePlan = disc.ДисциплинаКод;

                        // Код кафедры, за которой закреплена дисциплина
                        if (disc.КодКафедры != null)
                        {
                            int codeChair = (int)disc.КодКафедры;
                            Chair chair = serviceChair.GetItemChair(codeChair);
                            if ((chair != null) && (chair.Id != 0))
                                disciplinePlan.IdChair = chair.Id;
                            else
                                disciplinePlan.IdChair = 53; //не задано
                        }
                        else
                            disciplinePlan.IdChair = 53; //не задано

                        if (watch != null)
                        {
                            disciplinePlan.LecturePlan = watch.Лекций;
                            disciplinePlan.PracticalExercisesPlan = watch.Практик;
                            disciplinePlan.LaboratoryWorkPlan = watch.Лабораторных;
                            disciplinePlan.Semester = watch.Семестр;
                            disciplinePlan.Course = watch.Курс;

                            bool examine = (bool)(watch.Экзамен);
                            if (examine)
                                disciplinePlan.ExaminationPlan = 1;
                            else
                                disciplinePlan.ExaminationPlan = 0;

                            bool setOff = (bool)(watch.Зачет);
                            if (setOff)
                                disciplinePlan.SetOffPlan = 1;
                            else
                                disciplinePlan.SetOffPlan = 0;

                            bool courseWork = (bool)(watch.КурсоваяРабота);
                            if (courseWork)
                                disciplinePlan.CourseWorktPlan = 1;
                            else
                                disciplinePlan.CourseWorktPlan = 0;

                            bool courseProject = (bool)(watch.КурсовойПроект);
                            if (courseProject)
                                disciplinePlan.CourseProjectPlan = 1;
                            else
                                disciplinePlan.CourseProjectPlan = 0;

                            if (watch.КолВоКонтрРабот != null)
                                disciplinePlan.ControlWorkPlan = watch.Контрольных;
                            else
                                disciplinePlan.ControlWorkPlan = 0;
                        }

                        using (var context = new DBTeachingLoadEntities())
                        {
                            try
                            {
                                context.DisciplinePlans.AddObject(disciplinePlan);
                                context.SaveChanges();
                            }
                            catch (DataServiceRequestException ex)
                            {
                                throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Формирование практики по учебному плану
        /// </summary>
        /// <param name="curriculum"></param>
        private void SetNewPractical(Curriculum newCurriculum)
        {
            DisciplinePlan disciplinePlan;
            var serviceChair = container.Resolve<IServiceChair>();
            var serviceDiscipline = container.Resolve<IServiceCurriculumFromDB>();

            List<ПланыПрактики> listPractic = serviceDiscipline.GetListPractic(selectedCurriculumPlan.Код);
            foreach (ПланыПрактики pract in listPractic)
            {
                if (pract.ВидПрактики != "")
                {
                    disciplinePlan = new DisciplinePlan();
                    disciplinePlan.IdCurriculum = newCurriculum.Id;

                    // Наименование дисциплины (практики)
                    disciplinePlan.Discipline = pract.ВидПрактики;

                    // Код дисциплины (практики)
                    disciplinePlan.CodePlan = "Б5";

                    // Семестр
                    int semester = Convert.ToInt32(pract.Семестр);
                    disciplinePlan.Semester = semester;
                    // Курс
                    if ((semester % 2) == 0)
                        disciplinePlan.Course = semester / 2;
                    else
                        disciplinePlan.Course = semester / 2 + 1;


                    // Код кафедры, за которой закреплена дисциплина
                    if (pract.КодКафедры != null)
                    {
                        int codeChair = (int)pract.КодКафедры;
                        Chair chair = serviceChair.GetItemChair(codeChair);
                        if ((chair != null) && (chair.Id != 0))
                            disciplinePlan.IdChair = chair.Id;
                        else
                            disciplinePlan.IdChair = 53; //не задано
                    }
                    else
                        disciplinePlan.IdChair = 53; //не задано

                    // Продолжительность практики (недель)
                    disciplinePlan.PracticalPlan = Convert.ToInt32(pract.Недель);
                    
                    //Зачет
                    disciplinePlan.SetOffPlan = 1; 

                    using (var context = new DBTeachingLoadEntities())
                    {
                        try
                        {
                            context.DisciplinePlans.AddObject(disciplinePlan);
                            context.SaveChanges();
                        }
                        catch (DataServiceRequestException ex)
                        {
                            throw new ApplicationException("Ошибка добавления данных" + ex.ToString());
                        }
                    }
                }
            }
        }

        #endregion Helper

        #region CommandGetTitle

        /// <summary>
        /// Команда - Загрузка данных по титулам учебных планов - поле
        /// </summary>
        private ICommand getTitle;

        /// <summary>
        /// Команда - Загрузка данных по титулам учебных планов- Свойство
        /// </summary>
        public ICommand GetTitle
        {
            get
            {
                return getTitle ??
                    (getTitle = new RelayCommand(GetTitleExecute));
            }

        }

        /// <summary>
        /// Загрузка данных по титулам учебных планов- метод
        /// </summary>
        private void GetTitleExecute()
        {
            var service = container.Resolve<IServiceCurriculumFromDB>();

            if (TitleCurriculums != null)
            {
                TitleCurriculums.Clear();

                TitleCurriculums = service.GetCurriculum(TitleCurriculums, selectedAcademicYear.Year );
            }

            var serviceCurriculum = container.Resolve<IServiceCurriculum>();
            if (Curriculums != null)
            {
                Curriculums.Clear();

                Curriculums = serviceCurriculum.GetCurriculum(Curriculums, selectedAcademicYear.Id);
            }
        }


        #endregion CommandGetTitle

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
            // Создание титула плана
            TitleCurriculum newCurriculum = new ClassLibraryServiceDB.Model.TitleCurriculum();

            newCurriculum.Name = selectedCurriculumPlan.ИмяФайла;
            newCurriculum.Chair = selectedCurriculumPlan.КодПрофКафедры.ToString();

            // Признак типа учебного плана
            int index = newCurriculum.Name.IndexOf("pl");
            string typePlanFile = newCurriculum.Name.Substring(index, 3);

            if(typePlanFile =="plm")
                newCurriculum.EducationForm = "очная";
            if (typePlanFile == "plz")
                newCurriculum.EducationForm = "заочная";

            //int idFaculty = (int)selectedCurriculumPlan.КодФакультета;
            //var serviceFac = container.Resolve<IServiceFaculty>();
            //Faculty fac = serviceFac.GetItemFaculty(idFaculty);
            newCurriculum.Faculty = selectedCurriculumPlan.Факультет;

            // Формирование актуальных курсов
            newCurriculum.Course1 = false;
            newCurriculum.Course2 = false;
            newCurriculum.Course3 = false;
            newCurriculum.Course4 = false;
            newCurriculum.Course5 = false;
            newCurriculum.Course6 = false;

            // Формирование даты утверждения  и протокола учебного плана.
            DateTime data = (DateTime)selectedCurriculumPlan.ДатаУтверСоветом;
            newCurriculum.DataApproval = data.ToShortDateString();
            newCurriculum.Protocol = selectedCurriculumPlan.НомПротокСовета.ToString();

            var serviceDB = container.Resolve<IServiceCurriculumFromDB>();

            List<ПланыТитулы> listTitle = serviceDB.GetListTitle(selectedCurriculumPlan.Код);

            ПланыКвалификации qualificationPlan = serviceDB.GetQualification(selectedCurriculumPlan.Код);

            // Формирование квалификации
            DictQualification qualification = new DictQualification();
            ListQualification listQualification = new ListQualification();
            string qualif = qualificationPlan.Квалификация;
            newCurriculum.Qualification = qualif;

            if (qualif != null)
            {
                qualification = listQualification.FindQualification(listQualification, qualif,
                        (x, y) => x.NameQualification.Equals(y));
                if (qualification.Id != 0)
                    newCurriculum.Qualification = qualification.NameQualification;
                else
                    newCurriculum.Qualification = "не задано";
            }
            else
                newCurriculum.Qualification = "не задано";

            // Формирование направления подготовки: код/наименование/профиль
            DictSpeciality specialite = new DictSpeciality();
            ListSpeciality listSpeciality = new ListSpeciality();
            
            string spec = listTitle[listTitle.Count - 1].Титул;
            index = spec.IndexOfAny("0123456789".ToCharArray());
            if (index > 0)
                spec = spec.Substring(index, spec.Length - index);
            index = spec.LastIndexOfAny("0123456789".ToCharArray());
            string codeSpeciality = spec.Substring(0, index + 1);
            newCurriculum.CodeSpeciality = codeSpeciality;

            specialite = listSpeciality.FindSpeciality(listSpeciality, codeSpeciality, qualification.Id,
              (x, y, z) => x.CodeSpeciality.Equals(y) && x.IdQualification.Equals(z));
            newCurriculum.Speciality = specialite.NameSpeciality;
            newCurriculum.Profile = specialite.Profile;

            // Формирование срока обучения
            newCurriculum.DurationStudy = qualificationPlan.СрокОбучения; ;

            // Формирование учебного года
            DictAcademicYear year = new DictAcademicYear();
            ListAcademicYear listYear = new ListAcademicYear();
            year = listYear.FindAcademicYear(listYear, selectedCurriculumPlan.УчебныйГод,
                (x, y) => x.Year.Equals(y));
            newCurriculum.Year = year.Year;
            
            var service = container.Resolve<IServiceCurriculum>();
            var viewModel = container.Resolve<NewCurriculumFromDBViewModel>();
            viewModel.CurriculumPlan = newCurriculum;

            var viewEdit = container.Resolve<NewCurriculumFromDBView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Добавление учебных планов", viewEdit);

            if (result.HasValue && result.Value == true)
            {

                // Проверка наличия добавляемого в списке планов кафедры
                string nameFile = selectedCurriculumPlan.ИмяФайла;
                index = nameFile.IndexOf("plm");
                nameFile = nameFile.Substring(0,index+3);
                if (!ContainCurriculum(Curriculums, nameFile, year.Id,
                                    (x, y, z) => (x.Name).Equals(y) && (x.IdAcademicYear).Equals(z)))
                {

                    BackgroundWorker worker = new BackgroundWorker();
                    // Запуск длительной операции добавления данных о новом учебном плане в базу данных
                    worker.DoWork += (o, ea) =>
                    {
                        Curriculum curriculum = SetNewCurrirulum(newCurriculum, qualif, specialite, year, service, nameFile);
                        SetNewDiscipoline(curriculum);
                        SetNewPractical(curriculum);
                    };
                    worker.RunWorkerCompleted += (o, ea) =>
                    {
                        //операция добавления данных о новом учебном плане в базу данных
                        // выполнена - осуществляется возврат в основной поток UI
                        Curriculums.Clear();
                        Curriculums = service.GetCurriculum(Curriculums, selectedAcademicYear.Id);
                        IsBusyCurriculum = false;
                    };
                    // установка IsBusy перед началом ассинхронного потока визуализации выполнения длительной операции
                    IsBusyCurriculum = true;
                    BusyMessage = "Загрузка данных . . .";
                    worker.RunWorkerAsync();
                }
                else
                    MessageBox.Show("Учебный план " + selectedCurriculumPlan.ИмяФайла + "\nдля учебного года " + year.Year +
                        "\nИМЕЕТСЯ  в списке планов кафедры!","Предупреждение",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Добавление нового учебного плана в базу данных
        /// </summary>
        /// <param name="newCurriculum"></param>
        /// <param name="qualif"></param>
        /// <param name="specialite"></param>
        /// <param name="year"></param>
        /// <param name="service"></param>
        /// <param name="nameFile"></param>
        /// <returns></returns>
        private Curriculum SetNewCurrirulum(
            TitleCurriculum newCurriculum, 
            string qualif, 
            DictSpeciality specialite, 
            DictAcademicYear year, 
            IServiceCurriculum service, 
            string nameFile)
        {
            Curriculum curriculum = new Curriculum();
            curriculum.Name = nameFile;
            curriculum.Course1 = newCurriculum.Course1;
            curriculum.Course2 = newCurriculum.Course2;
            curriculum.Course3 = newCurriculum.Course3;
            curriculum.Course4 = newCurriculum.Course4;
            curriculum.Course5 = newCurriculum.Course5;
            curriculum.Course6 = newCurriculum.Course6;

            // Формирование статуса плана
            curriculum.StatusDel = true;

            // Формирование даты утверждения  и протокола учебного плана
            curriculum.Protocol = newCurriculum.Protocol;

            // Преобразование формата даты
            string dataApproval = newCurriculum.DataApproval;
            int lenght = dataApproval.Length;
            int yearData = Convert.ToInt32(dataApproval.Substring(lenght - 4, 4));
            int month = Convert.ToInt32(dataApproval.Substring(lenght - 7, 2));
            int index = dataApproval.IndexOf(".");
            int day = Convert.ToInt32(dataApproval.Substring(0, index));
            curriculum.DataApproval = new DateTime(yearData, month, day);

            // Формироание формы обучения
            DictEducationForm education = new DictEducationForm();
            ListEducationForm listEducationForm = new ListEducationForm();
            education = listEducationForm.FindEducationForm(listEducationForm, newCurriculum.EducationForm,
                (x, y) => x.FormEducation.Equals(y));
            curriculum.IdEducationForm = education.Id;

            // Формирование направления подготовки
            if (specialite.Id != 0)
                curriculum.IdSpeciality = specialite.Id;
            else
                curriculum.IdSpeciality = 1;   // не задано

            // Формирование длительности обучения
            curriculum.DurationStudy = newCurriculum.DurationStudy;

            // Формирование квалификации
            DictQualification qualification = new DictQualification();
            ListQualification listQualification = new ListQualification();
            if (qualif != null)
            {
                qualification = listQualification.FindQualification(listQualification, qualif,
                        (x, y) => x.NameQualification.Equals(y));
                if (qualification.Id != 0)
                    curriculum.IdQualification = qualification.Id;
                else
                    curriculum.IdQualification = 1;  // не задано
            }
            else
                curriculum.IdQualification = 1;  // не задано


            // Формирование учебного года (id)
            if ((year != null) && (year.Id != 0))
                curriculum.IdAcademicYear = year.Id;
            else
                curriculum.IdAcademicYear = 9; // не задано

            // Формирование профилирующей кафедры (id)
            var serviceChair = container.Resolve<IServiceChair>();
            int codeChair = Convert.ToInt32(newCurriculum.Chair);
            Chair chair = serviceChair.GetItemChair(codeChair);
            if ((chair != null) && (chair.Id != 0))
                curriculum.IdChair = chair.Id;
            else
                curriculum.IdChair = 33;   // не задано

            // Обновление списка учебных планов
            service.AddItemCurriculum(curriculum);
            
            return curriculum;
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
        private ICommand removeCurriculumCommand;

        /// <summary>
        /// Удаление данных по учебному плану - Свойство
        /// </summary>
        public ICommand RemoveCurriculumCommand
        {
            get
            {
                return removeCurriculumCommand ??
                    (removeCurriculumCommand = new RelayCommand(RemoveCurriculumExecute, CanCurriculumExecute));
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
                var service = container.Resolve<IServiceCurriculum>();

                BackgroundWorker worker = new BackgroundWorker();
                // Запуск длительной операции добавления данных о новом учебном плане в базу данных
                worker.DoWork += (o, ea) =>
                {
                    if (Disciplines.Count > 0)
                    {
                        foreach (var disc in Disciplines)
                            service.RemoveItemDiscipline(disc);
                    }

                    service.RemoveItemCurriculum(selectedCurriculum);
                };
                worker.RunWorkerCompleted += (o, ea) =>
                {
                    //операция добавления данных о новом учебном плане в базу данных
                    // выполнена - осуществляется возврат в основной поток UI
                    Curriculums.Remove(selectedCurriculum);
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
                var service = container.Resolve<IServiceCurriculum>();
                service.RemoveItemDiscipline(selectedDiscipline);

                Disciplines.Remove(selectedDiscipline);
            }
        }

        #endregion CommandRemoveDiscipline
    }
}
