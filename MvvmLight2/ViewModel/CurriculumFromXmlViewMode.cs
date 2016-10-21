using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Services.Client;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using ClassLibraryServiceDB.Model;
using ClassLibraryServiceDB.ServiceData;
using GalaSoft.MvvmLight.Command;
using MvvmLight2.Helper;
using MvvmLight2.Model;
using MvvmLight2.ServiceData;
using MvvmLight2.View;
using System.Linq;

namespace MvvmLight2.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// Учебные планы, формируемые из XML-файлов
    /// </summary>
    public class CurriculumFromXmlViewMode: WorkspaceViewModel
    {  
        #region Fields

        private MvvmLight2.Helper.IContainer container = MvvmLight2.Helper.Container.Instance;

        /// <summary>
        /// Полныый путь к папке с XML-файлам учебных планов
        /// </summary>
        string fullNamePath = String.Empty;

        /// <summary>
        /// Имя папки с с XML-файлам учебных планов
        /// </summary>
        string nameDir = String.Empty;

        /// <summary>
        /// Список дисциплин - ГАК/ГЭК
        /// </summary>
        List<Discipline> listDisciplineGak = new List<Discipline>();

        /// <summary>
        /// весенний семестр
        /// </summary>
        const string spring = "весна";

        /// <summary>
        /// осенний семестр
        /// </summary>
        const string autumn = "осень";

        #endregion Fields

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

        #region SelectedItemTree

        object selectedItemTree;

        public object SelectedItemTree
        {
            get { return selectedItemTree; }
            set 
            {
                if (value == selectedItemTree)
                    return;

                selectedItemTree = value;
                RaisePropertyChanged("SelectedItemTree");
            }
        }

        #endregion SelectedItemTree

        #region SelectedFileInfo

        /// <summary>
        /// Выделенный учебный план
        /// </summary>
        private FileInfo selectedFileInfo;

        /// <summary>
        /// Выделенный учебный план
        /// </summary>
        public FileInfo SelectedFileInfo
        {
            get { return selectedFileInfo; }
            set
            {

                if (value == selectedFileInfo)
                    return;

                selectedFileInfo = value;
                RaisePropertyChanged("SelectedFileInfo");
            }
        }

        #endregion SelectedFileInfo

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
        /// Коллекция файлов планов
        /// </summary>
        public ObservableCollection<FileInfo> ListFileTitle { get; set; }

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
        public CurriculumFromXmlViewMode(IServiceCurriculum service)
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

            ListFileTitle = new ObservableCollection<FileInfo>();
            Disciplines = new ObservableCollection<DisciplinePlan>();
            TitleCurriculums = new ObservableCollection<Планы>();
            base.DisplayName = "Учебные планы-XML";
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
        /// Загрузка данных по титулам учебных планов- метод
        /// </summary>
        private void GetTitle()
        {
            var service = container.Resolve<IServiceCurriculum>();
            if (Curriculums != null)
            {
                Curriculums.Clear();
                Curriculums = service.GetCurriculum(Curriculums, selectedAcademicYear.Id);
            }
        }

        /// <summary>
        /// Загрузка XML-документа
        /// </summary>
        /// <param name="pathFile"></param>
        /// <returns></returns>
        private XDocument GetXmlTitle(string pathFile)
        {
            try
            {
                XDocument titleDoc = XDocument.Load(pathFile);
                return titleDoc;
            }
            catch (System.IO.FileNotFoundException ex)
            {
                throw new InvalidOperationException(string.Format(
                       "XML-файл '{0}' не может быть загружен.\n", pathFile), ex);
            }
        }

        /// <summary>
        /// Формироание дисциплин и практик учебного плана
        /// </summary>
        /// <param name="newCurriculum"></param> - новый учебный план
        /// <param name="titleDoc"></param> XML-документ плана
        /// <param name="flagDisc"></param> - Признак: 1 - дисциплины; 2 - практики
        /// <param name="typePlanFile"></param> - тип плана PLM - бакалавры/магистры очники, PLZ = заочники, PLI = специалисты
        private void SetNewDiscipline(Curriculum newCurriculum, XDocument titleDoc, int flagDisc, string typePlanFile)
        {
            DisciplinePlan disciplinePlan;

            var serviceDiscipline = container.Resolve<IServiceCurriculumFromXml>();

            List<Discipline> listDiscipline = new List<Discipline>();
            
            List<Discipline> listCurriculumPlan = new List<Discipline>();

            Discipline discipline;

            switch (typePlanFile)
            {
                case "PLM":   // бакалавры/магистры очники 
                    if (flagDisc == 1)   // дисциплины и ВКР 
                    {
                        // Формирование дисциплин
                        listCurriculumPlan = serviceDiscipline.GetListDiscipline(titleDoc);

                        #region Бакалавры - очное

                        // Выпускная квалификационныя работа бакалавра 
                        if ((newCurriculum.IdQualification == 2) && ((bool)newCurriculum.Course4))
                        {
                            // Руководство ВКР
                            discipline = serviceDiscipline.GetGraduationDesign(titleDoc);
                            if (discipline != null)
                            {
                                AddFinishDisciplineToPlan(listCurriculumPlan, discipline, "Б6", "4", "8");
                            }

                            // Член ГАК
                            listDisciplineGak = serviceDiscipline.GetListGak(titleDoc);
                            if (listDisciplineGak.Count > 0)
                                foreach (Discipline disc in listDisciplineGak)
                                {
                                    AddFinishDisciplineToPlan(listCurriculumPlan, disc, "Б6", "4", "8");
                                }
                            
                            // Член ГЭК
                            listDisciplineGak.Clear();
                            listDisciplineGak = serviceDiscipline.GetListGek(titleDoc);
                            if (listDisciplineGak.Count > 0)
                                foreach (Discipline disc in listDisciplineGak)
                                {
                                    AddFinishDisciplineToPlan(listCurriculumPlan, disc, "Б6", "4", "8");
                                }
                        }

                        #endregion Бакалавры - очное

                        #region Магистры - очное

                        // Выпускная квалификационныя работа магистра
                        if ((newCurriculum.IdQualification == 3) && ((bool)newCurriculum.Course2))
                        {
                            // Выпускная квалификационныя работа магистра
                            discipline = serviceDiscipline.GetGraduationDesign(titleDoc);
                            if (discipline != null)
                            {
                                AddFinishDisciplineToPlan(listCurriculumPlan, discipline, "М4", "2", "4");
                            }

                            // Член ГАК
                            listDisciplineGak = serviceDiscipline.GetListGak(titleDoc);
                            if (listDisciplineGak.Count > 0)
                                foreach (Discipline disc in listDisciplineGak)
                                {
                                    AddFinishDisciplineToPlan(listCurriculumPlan, disc, "М4", "2", "4");
                                }

                            // Член ГЭК
                            listDisciplineGak.Clear();
                            listDisciplineGak = serviceDiscipline.GetListGek(titleDoc);
                            if (listDisciplineGak.Count > 0)
                                foreach (Discipline disc in listDisciplineGak)
                                {
                                    AddFinishDisciplineToPlan(listCurriculumPlan, disc, "М4", "2", "4");
                                }
                        }
                        
                        #endregion Магистры - очное
                    }

                    #region Практики- очное
                    
                    if (flagDisc == 2) // практики
                        listCurriculumPlan = serviceDiscipline.GetListPractic(titleDoc);

                    #endregion Практики- очное

                    break;

                case "PLZ":   // бакалавры/магистры заочники
                    if (flagDisc == 1) // дисциплины
                    {
                        List<Discipline> listDisc;
                        listDisc = serviceDiscipline.GetListDisciplineDistanceLearning(titleDoc);

                        // Формирование дисциплин плана
                        foreach (var disc in listDisc)
                        {
                            listCurriculumPlan.Add(disc);
                        }

                        #region Бакалавры - заочное

                        // Выпускная квалификационныя работа бакалавра 
                        if ((newCurriculum.IdQualification == 2) && 
                            (newCurriculum.IdEducationForm == 3) && 
                            ((bool)newCurriculum.Course5))
                        {
                            // Руководство ВКР
                            discipline = serviceDiscipline.GetGraduationDesign(titleDoc);
                            if (discipline != null)
                            {
                                AddFinishDisciplineToPlan(listCurriculumPlan, discipline, "Б6", "5", "");
                            }

                            // Член ГАК
                            listDisciplineGak = serviceDiscipline.GetListGak(titleDoc);
                            if (listDisciplineGak.Count > 0)
                                foreach (Discipline disc in listDisciplineGak)
                                {
                                    AddFinishDisciplineToPlan(listCurriculumPlan, disc, "Б6", "5", "");
                                }

                            // Член ГЭК
                            listDisciplineGak.Clear();
                            listDisciplineGak = serviceDiscipline.GetListGek(titleDoc);
                            if (listDisciplineGak.Count > 0)
                                foreach (Discipline disc in listDisciplineGak)
                                {
                                    AddFinishDisciplineToPlan(listCurriculumPlan, disc, "Б6", "5", "");
                                }
                        }

                        #endregion Бакалавры - заочное

                        #region Бакалавры - заочное сокращенное/ 2-е высшее

                        // Выпускная квалификационныя работа бакалавра 
                        if ((newCurriculum.IdQualification == 2) &&
                            ((newCurriculum.IdEducationForm == 4) || 
                            (newCurriculum.IdEducationForm == 5)) &&
                            ((bool)newCurriculum.Course4))
                        {
                            // Руководство ВКР
                            discipline = serviceDiscipline.GetGraduationDesign(titleDoc);
                            if (discipline != null)
                            {
                                AddFinishDisciplineToPlan(listCurriculumPlan, discipline, "Б6", "4", "");
                            }

                            // Член ГАК
                            listDisciplineGak = serviceDiscipline.GetListGak(titleDoc);
                            if (listDisciplineGak.Count > 0)
                                foreach (Discipline disc in listDisciplineGak)
                                {
                                    AddFinishDisciplineToPlan(listCurriculumPlan, disc, "Б6", "4", "");
                                }

                            // Член ГЭК
                            listDisciplineGak.Clear();
                            listDisciplineGak = serviceDiscipline.GetListGek(titleDoc);
                            if (listDisciplineGak.Count > 0)
                                foreach (Discipline disc in listDisciplineGak)
                                {
                                    AddFinishDisciplineToPlan(listCurriculumPlan, disc, "Б6", "4", "");
                                }
                        }

                        #endregion Бакалавры - заочное сокращенное/ 2-е высшее

                        #region Магистры - заочное

                        // Выпускная квалификационныя работа бакалавра 
                        if ((newCurriculum.IdQualification == 3) &&
                            (newCurriculum.IdEducationForm == 3) &&
                            ((bool)newCurriculum.Course3))
                        {
                            // Руководство ВКР
                            discipline = serviceDiscipline.GetGraduationDesign(titleDoc);
                            if (discipline != null)
                            {
                                AddFinishDisciplineToPlan(listCurriculumPlan, discipline, "М4", "3", "");
                            }

                            // Член ГАК
                            listDisciplineGak = serviceDiscipline.GetListGak(titleDoc);
                            if (listDisciplineGak.Count > 0)
                                foreach (Discipline disc in listDisciplineGak)
                                {
                                    AddFinishDisciplineToPlan(listCurriculumPlan, disc, "М4", "3", "");
                                }

                            // Член ГЭК
                            listDisciplineGak.Clear();
                            listDisciplineGak = serviceDiscipline.GetListGek(titleDoc);
                            if (listDisciplineGak.Count > 0)
                                foreach (Discipline disc in listDisciplineGak)
                                {
                                    AddFinishDisciplineToPlan(listCurriculumPlan, disc, "М4", "3", "");
                                }
                        }

                        #endregion Магистры - заочное

                    }

                    if (flagDisc == 2)   // практики
                    {
                        var practics = serviceDiscipline.GetListPracticTrainingDistanceLearning(titleDoc);
                        if(practics.Count() > 0)
                        {
                            foreach (Discipline disc in practics)
                                listCurriculumPlan.Add(disc);
                        }

                        practics = serviceDiscipline.GetListPracticProductionDistanceLearning(titleDoc);
                        if (practics.Count() > 0)
                        {
                            foreach (Discipline disc in practics)
                                listCurriculumPlan.Add(disc);
                        }
                    }
                    break;
                
                case "PLI":  // специалисты
                    if (flagDisc == 1)  // дисциплины
                    {
                        listCurriculumPlan = serviceDiscipline.GetListDiscipline(titleDoc);

                        #region Специалисты - дневное

                        // Выпускная квалификационныя работа 
                        if ((newCurriculum.IdQualification == 4) && ((bool)newCurriculum.Course5))
                        {
                            // Руководство ВКР
                            discipline = serviceDiscipline.GetGraduationDesignSpecialist(titleDoc);
                            if (discipline != null)
                            {
                                AddFinishDisciplineToPlan(listCurriculumPlan, discipline, "С6", "5", "10");
                            }

                            // Член ГАК
                            listDisciplineGak = serviceDiscipline.GetListGakSpecialist(titleDoc);
                            if (listDisciplineGak.Count > 0)
                                foreach (Discipline disc in listDisciplineGak)
                                {
                                    AddFinishDisciplineToPlan(listCurriculumPlan, disc, "С6", "5", "10");
                                }

                            // Член ГЭК
                            listDisciplineGak.Clear();
                            listDisciplineGak = serviceDiscipline.GetListGekSpecialist(titleDoc);
                            if (listDisciplineGak.Count > 0)
                                foreach (Discipline disc in listDisciplineGak)
                                {
                                    AddFinishDisciplineToPlan(listCurriculumPlan, disc, "С6", "5", "10");
                                }
                        }

                        #endregion Бакалавры - дневное

                    }
                    if (flagDisc == 2)   // практики
                        listCurriculumPlan = serviceDiscipline.GetListPractic(titleDoc);
                    break;
            }

            foreach (Discipline disc in listCurriculumPlan)
            {

                if ((typePlanFile == "PLM") || (typePlanFile == "PLI"))
                {
                    if ((bool)newCurriculum.Course1 && ((disc.Semester == "1") || (disc.Semester == "2")))
                        disciplinePlan = AddDisciplineToPlanCourse(newCurriculum, flagDisc, typePlanFile, disc);
                    if ((bool)newCurriculum.Course2 && ((disc.Semester == "3") || (disc.Semester == "4")))
                        disciplinePlan = AddDisciplineToPlanCourse(newCurriculum, flagDisc, typePlanFile, disc);
                    if ((bool)newCurriculum.Course3 && ((disc.Semester == "5") || (disc.Semester == "6")))
                        disciplinePlan = AddDisciplineToPlanCourse(newCurriculum, flagDisc, typePlanFile, disc);
                    if ((bool)newCurriculum.Course4 && ((disc.Semester == "7") || (disc.Semester == "8")))
                        disciplinePlan = AddDisciplineToPlanCourse(newCurriculum, flagDisc, typePlanFile, disc);
                    if ((bool)newCurriculum.Course5 && ((disc.Semester == "9") || (disc.Semester == "10")))
                        disciplinePlan = AddDisciplineToPlanCourse(newCurriculum, flagDisc, typePlanFile, disc);
                }

                  if (typePlanFile == "PLZ")
                  {
                      if ((bool)newCurriculum.Course1 && (disc.Course == "1"))
                            disciplinePlan = AddDisciplineToPlanCourse(newCurriculum, flagDisc, typePlanFile, disc);
                      if ((bool)newCurriculum.Course2 && (disc.Course == "2"))
                          disciplinePlan = AddDisciplineToPlanCourse(newCurriculum, flagDisc, typePlanFile, disc);
                      if ((bool)newCurriculum.Course3 && (disc.Course == "3"))
                          disciplinePlan = AddDisciplineToPlanCourse(newCurriculum, flagDisc, typePlanFile, disc);
                      if ((bool)newCurriculum.Course4 && (disc.Course == "4"))
                          disciplinePlan = AddDisciplineToPlanCourse(newCurriculum, flagDisc, typePlanFile, disc);
                      if ((bool)newCurriculum.Course5 && (disc.Course == "5"))
                          disciplinePlan = AddDisciplineToPlanCourse(newCurriculum, flagDisc, typePlanFile, disc);
                      if ((bool)newCurriculum.Course6 && (disc.Course == "6"))
                          disciplinePlan = AddDisciplineToPlanCourse(newCurriculum, flagDisc, typePlanFile, disc);
                  }
            }
        }

        /// <summary>
        /// Добавление в дисциплины учебного плана руководства ВКР, членов ГАК/ГЭК
        /// </summary>
        /// <param name="listCurriculumPlan"></param>
        /// <param name="discipline"></param>
        /// <param name="code"></param>
        /// <param name="course"></param>
        /// <param name="semester"></param>
        private static void AddFinishDisciplineToPlan(List<Discipline> listCurriculumPlan, Discipline discipline,string code, string course, string semester)
        {
            discipline.CodeDiscipline = code;
            discipline.Course = course;
            discipline.Semester = semester;
            listCurriculumPlan.Add(discipline);
        }

        /// <summary>
        /// Добавление дисциплины в план заданного курса
        /// </summary>
        /// <param name="newCurriculum"></param>
        /// <param name="flagDisc"></param>
        /// <param name="typePlanFile"></param>
        /// <param name="disc"></param>
        /// <returns></returns>
        private DisciplinePlan AddDisciplineToPlanCourse(Curriculum newCurriculum, int flagDisc, string typePlanFile, Discipline disc)
        {
            DisciplinePlan disciplinePlan;
            disciplinePlan = new DisciplinePlan();
            disciplinePlan.IdCurriculum = newCurriculum.Id;


            // Наименование и код дисциплины
            if (flagDisc == 1)
            {
                disciplinePlan.Discipline = disc.Discipl;
                disciplinePlan.CodePlan = disc.CodeDiscipline;
            }
            switch (typePlanFile)
            {
                case "PLM":    // бакалавры/магистры очники
                    if (flagDisc == 2)
                    {
                        disciplinePlan.Discipline = disc.Discipl + " практика";
                        disciplinePlan.CodePlan = "Б5";
                    }
                    // Семестр
                    int semester = Convert.ToInt32(disc.Semester);
                    disciplinePlan.Semester = semester;
                    // Курс
                    if ((semester % 2) == 0)
                    {
                        disciplinePlan.SemesterYear = spring;
                        disciplinePlan.Course = semester / 2;
                    }
                    else
                    {
                        disciplinePlan.SemesterYear = autumn;
                        disciplinePlan.Course = semester / 2 + 1;
                    }
                    break;

                case "PLZ":  // заочники
                    if (flagDisc == 2)
                    {
                        disciplinePlan.Discipline = disc.Discipl + " практика";
                        disciplinePlan.CodePlan = "Б5";
                        //disciplinePlan.SetOffPlan = 1;   // добавлено 11.03.2015
                    }
                    // Курс
                    disciplinePlan.Course = Convert.ToInt32(disc.Course);

                    ///////////////////////////////////////////////////////
                    // Формально относим заочников к осеннему семестру!!!
                    disciplinePlan.SemesterYear = autumn;
                    break;

                case "PLI":  // специалисты
                    if (flagDisc == 2)
                    {
                        disciplinePlan.Discipline = disc.Discipl + " практика";
                        disciplinePlan.CodePlan = "С5";
                    }
                    // Семестр
                    semester = Convert.ToInt32(disc.Semester);
                    disciplinePlan.Semester = semester;
                    // Курс
                    if ((semester % 2) == 0)
                    {
                        disciplinePlan.SemesterYear = spring;
                        disciplinePlan.Course = semester / 2;
                    }
                    else
                    {
                        disciplinePlan.SemesterYear = autumn;
                        disciplinePlan.Course = semester / 2 + 1;
                    }
                    break;
            }


            // Формирование кода кафедры, за которой закреплена дисциплина
            var serviceChair = container.Resolve<IServiceChair>();
            int codeChair = Convert.ToInt32(disc.CodeChair);
            Chair chair = serviceChair.GetItemChair(codeChair);
            if ((chair != null) && (chair.Id != 0))
                disciplinePlan.IdChair = chair.Id;
            else
                disciplinePlan.IdChair = 53; //не задано

            #region Формирование нагрузки

            // Лекции
            disciplinePlan.LecturePlan = Convert.ToDecimal(disc.Lecture);

            // Лабораторные занятия
            disciplinePlan.LaboratoryWorkPlan = Convert.ToDecimal(disc.Lab);

            // Практические занятия
            disciplinePlan.PracticalExercisesPlan = Convert.ToDecimal(disc.PracticalExercise);

            // Курсовой проект
            disciplinePlan.CourseProjectPlan = Convert.ToInt32(disc.CourseProject);

            // Курсовая работа
            disciplinePlan.CourseWorktPlan = Convert.ToInt32(disc.CourseWork);

            // Контрольная работа
            disciplinePlan.ControlWorkPlan = Convert.ToInt32(disc.ControlWork);

            // Практика
            disciplinePlan.PracticalPlan = Convert.ToInt32(disc.Pract);

            //Экзамен
            if (disc.Exam != string.Empty)
                disciplinePlan.ExaminationPlan = Convert.ToInt32(disc.Exam);

            // Зачет
            if (disc.SetOff != string.Empty)
                disciplinePlan.SetOffPlan = Convert.ToInt32(disc.SetOff);

            // Зачет с оценкой
            if (disc.SetOffWithBall != string.Empty)
                disciplinePlan.SetOffWithBallPlan = Convert.ToInt32(disc.SetOffWithBall);

            // Руководство дипломным проектированием
            if ((disc.GraduationDesign != string.Empty) && (disc.GraduationDesign != null))
                disciplinePlan.GraduationDesignPlan = Convert.ToDecimal(disc.GraduationDesign);
            else
                disciplinePlan.GraduationDesignPlan = 0;

            // ГАК/ГЭК
            if ((disc.Gak != string.Empty) && (disc.Gak != null))
            {
                string d = disc.Gak.Replace(".", ",");

                disciplinePlan.GakPlan = Convert.ToDecimal(d);
            }
            else
                disciplinePlan.GakPlan = 0;

            // НИР
            if (disc.ScientificResearchWork != 0)
                disciplinePlan.ScientificResearchWorkPlan = 1;
            else
                disciplinePlan.ScientificResearchWorkPlan = 0;

            #endregion Формирование нагрузки

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
            return disciplinePlan;
        }

        #endregion Helper

        #region GetPathFile

        private ICommand getPathFile;

        public ICommand GetPathFile
        {
            get 
            { 
                return getPathFile ??
                    (getPathFile = new RelayCommand<object>(GetPathFileExecute<object>)); 
            }
        }

        private void GetPathFileExecute<Object>(object path)
        {
            if (path is DirectoryInfo)
            {
                DirectoryInfo dir = (DirectoryInfo)path;
                nameDir = dir.Name;
                fullNamePath = dir.FullName;

                GetTitleFileExecute();
            }
        }

        #endregion GetPathFile

        #region CommandGetTitle

        /// <summary>
        /// Команда - Формирование списка XML-файлов титулов учебных планов - поле
        /// </summary>
        private ICommand getTitleFile;

        /// <summary>
        /// Команда - Формирование списка XML-файлов титулов учебных планов- Свойство
        /// </summary>
        public ICommand GetTitleFile
        {
            get
            {
                return getTitleFile ??
                    (getTitleFile = new RelayCommand(GetTitleFileExecute));
            }
        }

        /// <summary>
        /// Формирование списка XML-файлов титулов учебных планов- метод
        /// </summary>
        private void GetTitleFileExecute()
        {
            if (Curriculums != null)
                Curriculums.Clear();
            ListFileTitle.Clear();
            if((nameDir != String.Empty) && (selectedAcademicYear != null))
                if (nameDir.Contains(selectedAcademicYear.Year))
                {
                    if (Directory.Exists(fullNamePath) == true)
                    {
                        DirectoryInfo dir = new DirectoryInfo(fullNamePath);
                        foreach (FileInfo fi in dir.GetFiles("*.xml"))
                            ListFileTitle.Add(fi);
                    }
                    GetTitle();

                    NamePlan = string.Empty;
                    Course = string.Empty;
                    SelectedIndexEducationForm = 0;
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
                    (addCurriculum = new RelayCommand(AddCurriculumExecute, CanAddCurriculumExecute ));
            }
        }

        /// <summary>
        /// Добавление данных по учебному плану - метод
        /// </summary>
        private void AddCurriculumExecute()
        {
            //Загрузка XML-документа учебного плана
            string dir = selectedFileInfo.DirectoryName;
            string fileName = selectedFileInfo.Name;
            string path = dir + @"\" + fileName;
            XDocument titleDoc = GetXmlTitle(path);

            // Формирование данных для титула учебного плана
            var serviceTitle = container.Resolve<IServiceCurriculumFromXml>();
            TitleCurriculum newCurriculum = serviceTitle.GetTitleCurriculum(titleDoc);

            // Формирование имени учебного плана
            int index = fileName.IndexOf(".xml");
            string nameTitle = fileName.Substring(0, index );
            newCurriculum.Name = nameTitle;

            // Признак типа учебного плана
            //index = nameTitle.IndexOf("pl");
            //string typePlanFile = nameTitle.Substring(index, 3);

            string typePlanFile = newCurriculum.Code;
 
            // Формирование учебного года
            newCurriculum.Year = selectedAcademicYear.Year;

            var viewModel = container.Resolve<NewCurriculumFromXmlViewModel>();
            viewModel.CurriculumPlan = newCurriculum;

            var viewEdit = container.Resolve<NewCurriculumFromXmlView>();
            viewEdit.DataContext = viewModel;

            var windowService = container.Resolve<IWindowService>();
            var result = windowService.ShowDialog("Добавление учебных планов", viewEdit);

            if (result.HasValue && result.Value == true)
            {
                //Проверка наличия добавляемого в списке планов кафедры
                if (!ContainCurriculum(Curriculums, newCurriculum.Name, selectedAcademicYear.Id,
                                    (x, y, z) => (x.Name).Equals(y) && (x.IdAcademicYear).Equals(z)))
                {
                    var service = container.Resolve<IServiceCurriculum>();

                    BackgroundWorker worker = new BackgroundWorker();
                    // Запуск длительной операции добавления данных о новом учебном плане в базу данных
                    worker.DoWork += (o, ea) =>
                    {
                        Curriculum curriculum = SetNewCurriculum(newCurriculum, nameTitle, selectedAcademicYear, service);

                        // Формирование дисциплин учебного плана и добавление их в базу данных
                        SetNewDiscipline(curriculum, titleDoc, 1, typePlanFile);

                        // Формирование практик учебного плана и добавление их в базу данных
                        SetNewDiscipline(curriculum, titleDoc, 2, typePlanFile);
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
                    MessageBox.Show("Учебный план " + newCurriculum.Name + "\nдля учебного года " + selectedAcademicYear.Year +
                        "\nИМЕЕТСЯ  в списке планов кафедры!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Добавление нового учебного плана в базу данных
        /// </summary>
        /// <param name="newCurriculum"></param>
        /// <param name="nameTitle"></param>
        /// <param name="year"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        private Curriculum SetNewCurriculum(
            TitleCurriculum newCurriculum, 
            string nameTitle,
            DictAcademicYear year, 
            IServiceCurriculum service)
        {
            Curriculum curriculum = new Curriculum();
            
            // Формирование имени плана
            //curriculum.Name = newCurriculum.Name;
            curriculum.Name = nameTitle;

            // Формирование статуса плана
            curriculum.StatusDel = true;

            // Формирование актуального курса для плана 
            curriculum.Course1 = newCurriculum.Course1;
            curriculum.Course2 = newCurriculum.Course2;
            curriculum.Course3 = newCurriculum.Course3;
            curriculum.Course4 = newCurriculum.Course4;
            curriculum.Course5 = newCurriculum.Course5;
            curriculum.Course6 = newCurriculum.Course6;

            // Преобразование формата даты утверждения учебного плана
            if(newCurriculum.DataApproval != "")
            {
                string data = newCurriculum.DataApproval;
                int lenght = data.Length;
                int yearData = Convert.ToInt32(data.Substring(lenght - 4, 4));
                int month = Convert.ToInt32(data.Substring(lenght - 7, 2));
                int index = data.IndexOf(".");
                int day = Convert.ToInt32(data.Substring(0, index));
                curriculum.DataApproval = new DateTime(yearData, month, day);
            }
            

            // Формирование номера протокола утверждения учебного плана
            curriculum.Protocol = newCurriculum.Protocol;

            // Формирование срока обучения
            curriculum.DurationStudy = newCurriculum.DurationStudy;

            // Формирование учебного года (id)
            if ((year != null) && (year.Id != 0))
                curriculum.IdAcademicYear = year.Id;
            else
                curriculum.IdAcademicYear = 9; // не задано

            // Формирование квалификации
            DictQualification qualification = new DictQualification();
            ListQualification listQualification = new ListQualification();
            string qual = newCurriculum.Qualification;
            qualification = listQualification.FindQualification(listQualification, qual,
                    (x, y) => (x.NameQualification).ToUpper() .Equals(y.ToUpper()));
            if (qualification.Id != 0)
                curriculum.IdQualification = qualification.Id;
            else
                curriculum.IdQualification = 1;  // не задано

            // Формирование направления подготовки: кода/направления/профиля
            DictSpeciality specialite = new DictSpeciality();
            ListSpeciality listSpeciality = new ListSpeciality();

            specialite = listSpeciality.FindSpeciality(listSpeciality, newCurriculum.CodeSpeciality, curriculum.IdQualification,
                (x, y, z) => x.CodeSpeciality.Equals(y) && x.IdQualification.Equals(z));
            if (specialite.Id != 0)
                curriculum.IdSpeciality = specialite.Id;
            else
                curriculum.IdSpeciality = 1;   // не задано

            newCurriculum.Speciality = specialite.NameSpeciality;
            newCurriculum.Profile = specialite.Profile;

            // Формироание формы обучения
            DictEducationForm education = new DictEducationForm();
            ListEducationForm listEducationForm = new ListEducationForm();

            education = listEducationForm.FindEducationForm(listEducationForm, newCurriculum.EducationForm,
                (x, y) => x.FormEducation.Equals(y));
            curriculum.IdEducationForm = education.Id;

            // Формирование выпускающей кафедры
            var serviceChair = container.Resolve<IServiceChair>();
            int codeChair = Convert.ToInt32(newCurriculum.Chair);
            Chair ch = serviceChair.GetItemChair(codeChair);
            if ((ch != null) && (ch.Id != 0))
                curriculum.IdChair = ch.Id;
            else
                curriculum.IdChair = 33;   // не задано

            // Добавление нового титула учебного плана в базу данных
            //var service = container.Resolve<IServiceCurriculum>();
            service.AddItemCurriculum(curriculum);
            
            return curriculum;
        }

        /// <summary>
        /// Признак доступности команды RemoveUniversityExecute - Удаление данных по отдельной дисциплине нагрузки
        /// </summary>
        /// <returns></returns>
        private bool CanAddCurriculumExecute()
        {
            return SelectedFileInfo != null;
        }

        #endregion CommandAddCurriculum


        #region CommandAddAllCurriculum

        /// <summary>
        /// Добавление данных по учебному плану - поле
        /// </summary>
        private ICommand addAllCurriculum;

        /// <summary>
        /// Добавление данных по учебному плану - Свойство
        /// </summary>
        public ICommand AddAllCurriculum
        {
            get
            {
                return addAllCurriculum ??
                    (addAllCurriculum = new RelayCommand(AddAllCurriculumExecute, CanAddAllCurriculumExecute));
            }
        }

        /// <summary>
        /// Добавление данных по учебному плану - метод
        /// </summary>
        private void AddAllCurriculumExecute()
        {
            MessageBoxResult result = MessageBox.Show("Добавить данные по всем Учебным планам\nВы уверены?",
                  "Предупреждение", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {


                ////Загрузка XML-документа учебного плана
                //string dir = selectedFileInfo.DirectoryName;
                //string fileName = selectedFileInfo.Name;
                //string path = dir + @"\" + fileName;
                //XDocument titleDoc = GetXmlTitle(path);

                //// Формирование данных для титула учебного плана
                //var serviceTitle = container.Resolve<IServiceCurriculumFromXml>();
                //TitleCurriculum newCurriculum = serviceTitle.GetTitleCurriculum(titleDoc);

                //// Формирование имени учебного плана
                //int index = fileName.IndexOf(".xml");
                //string nameTitle = fileName.Substring(0, index);
                //newCurriculum.Name = nameTitle;

                //string typePlanFile = newCurriculum.Code;

                //// Формирование учебного года
                //newCurriculum.Year = selectedAcademicYear.Year;

                //var viewModel = container.Resolve<NewCurriculumFromXmlViewModel>();
                //viewModel.CurriculumPlan = newCurriculum;

                //var viewEdit = container.Resolve<NewCurriculumFromXmlView>();
                //viewEdit.DataContext = viewModel;

                //var windowService = container.Resolve<IWindowService>();
                //var result = windowService.ShowDialog("Добавление учебных планов", viewEdit);

                //if (result.HasValue && result.Value == true)
                //{
                //    //Проверка наличия добавляемого в списке планов кафедры
                //    if (!ContainCurriculum(Curriculums, newCurriculum.Name, selectedAcademicYear.Id,
                //                        (x, y, z) => (x.Name).Equals(y) && (x.IdAcademicYear).Equals(z)))
                //    {
                //        var service = container.Resolve<IServiceCurriculum>();

                //        BackgroundWorker worker = new BackgroundWorker();
                //        // Запуск длительной операции добавления данных о новом учебном плане в базу данных
                //        worker.DoWork += (o, ea) =>
                //        {
                //            Curriculum curriculum = SetNewCurriculum(newCurriculum, nameTitle, selectedAcademicYear, service);

                //        // Формирование дисциплин учебного плана и добавление их в базу данных
                //        SetNewDiscipline(curriculum, titleDoc, 1, typePlanFile);

                //        // Формирование практик учебного плана и добавление их в базу данных
                //        SetNewDiscipline(curriculum, titleDoc, 2, typePlanFile);
                //        };
                //        worker.RunWorkerCompleted += (o, ea) =>
                //        {
                //        //операция добавления данных о новом учебном плане в базу данных
                //        // выполнена - осуществляется возврат в основной поток UI
                //        Curriculums.Clear();
                //            Curriculums = service.GetCurriculum(Curriculums, selectedAcademicYear.Id);
                //            IsBusyCurriculum = false;
                //        };
                //        // установка IsBusy перед началом ассинхронного потока визуализации выполнения длительной операции
                //        IsBusyCurriculum = true;
                //        BusyMessage = "Загрузка данных . . .";
                //        worker.RunWorkerAsync();
                //    }

                //    else
                //        MessageBox.Show("Учебный план " + newCurriculum.Name + "\nдля учебного года " + selectedAcademicYear.Year +
                //            "\nИМЕЕТСЯ  в списке планов кафедры!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                //}
            }
        }


        /// <summary>
        /// Признак доступности команды RemoveUniversityExecute - Удаление данных по отдельной дисциплине нагрузки
        /// </summary>
        /// <returns></returns>
        private bool CanAddAllCurriculumExecute()
        {
            return true;
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
                    //if (Disciplines.Count > 0)
                    //{
                    //    foreach (var disc in Disciplines)
                    //        service.RemoveItemDiscipline(disc);
                    //}

                    //service.RemoveItemCurriculum(selectedCurriculum);

                    if (Disciplines.Count > 0)
                    {
                        for (int i = 0; i < Disciplines.Count; i++)
                        {
                            resultDeleteDiscipline = service.RemoveItemDiscipline(Disciplines[i], out messageErrorDelete);
                            if (resultDeleteDiscipline)
                            {
                                MessageBox.Show(messageErrorDelete, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                                break;
                            }
                        }
                    }
                };
                worker.RunWorkerCompleted += (o, ea) =>
                {
                    //операция удаления данных из учебного плана в базе данных
                    // выполнена - осуществляется возврат в основной поток UI
                    //Curriculums.Remove(selectedCurriculum);
                    //Disciplines.Clear();
                    //IsBusyCurriculum = false;
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

        #region CommandRemoveAllCurriculum

        /// <summary>
        /// Удаление данных по учебному плану - поле
        /// </summary>
        private ICommand removeAllCurriculum;

        /// <summary>
        /// Удаление данных по учебному плану - Свойство
        /// </summary>
        public ICommand RemoveAllCurriculum
        {
            get
            {
                return removeAllCurriculum ??
                    (removeAllCurriculum = new RelayCommand(RemoveAllCurriculumExecute, CanRemoveAllCurriculumExecute));
            }
        }

        /// <summary>
        /// Удаление данных по учебному плану - метод
        /// </summary>
        private void RemoveAllCurriculumExecute()
        {
            MessageBoxResult result = MessageBox.Show("Удалить данные по всем Учебным планам\nВы уверены?",
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
                    if (Disciplines.Count > 0)
                    {
                        for (int i = 0; i < Disciplines.Count; i++)
                        {
                            resultDeleteDiscipline = service.RemoveItemDiscipline(Disciplines[i], out messageErrorDelete);
                            if (resultDeleteDiscipline)
                            {
                                MessageBox.Show(messageErrorDelete, "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                                break;
                            }
                        }
                    }
                };
                worker.RunWorkerCompleted += (o, ea) =>
                {
                    //операция удаления данных из учебного плана в базе данных
                    // выполнена - осуществляется возврат в основной поток UI
                    if (!resultDeleteDiscipline)
                    {
                        //service.RemoveItemCurriculum(selectedCurriculum);
                        //Curriculums.Remove(selectedCurriculum);
                        //Disciplines.Clear();
                    }
                    IsBusyCurriculum = false;
                };
                // установка IsBusy перед началом ассинхронного потока визуализации выполнения длительной операции
                IsBusyCurriculum = true;
                BusyMessage = "Удаление данных . . .";
                worker.RunWorkerAsync();
            }
        }

        private bool CanRemoveAllCurriculumExecute()
        {
            return true;
        }

        #endregion CommandRemoveAllCurriculum

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
                //var service = container.Resolve<IServiceCurriculum>();
                //service.RemoveItemDiscipline(selectedDiscipline);

                //Disciplines.Remove(selectedDiscipline);
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
                    (refresh = new RelayCommand(GetTitleFileExecute));
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
                            if (namePlan != "" && selectedEducationForm.FormEducation == "не задано"  && course == "")
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