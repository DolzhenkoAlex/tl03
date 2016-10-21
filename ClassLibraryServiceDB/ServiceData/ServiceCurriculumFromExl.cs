using ClassLibraryServiceDB.Model;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ClassLibraryServiceDB.ServiceData
{
    public class ServiceCurriculumFromExl: IServiceCurriculumFromExl
    {
        #region Properties
        /// <summary>
        /// Титул учебного плана
        /// </summary>
        TitleCurriculum titleCur;
        /// <summary>
        /// Колекция дисциплин
        /// </summary>
        List<Discipline> Disciplines;
        /// <summary>
        /// Импортируемая дисциплина
        /// </summary>
        Discipline DisciplinesImport;
        /// <summary>
        /// Импортируемый учебный план старого образца
        /// </summary>
        ServiceCurriculumFromExlOldPlan ImportExlOldPlan;
        /// <summary>
        /// Поля для работы с Excel
        /// </summary>
        Excel.Workbook workbook = null;
        /// <summary>
        /// Поля для работы с Excel
        /// </summary>
        Excel.Worksheet sheet = null;
        /// <summary>
        /// Поля для работы с Excel
        /// </summary>
        Excel.Worksheet sheet_dop = null;
        /// <summary>
        /// Форма обучения
        /// </summary>
        string EducationForm;
        /// <summary>
        /// Квалификация
        /// </summary>
        string Qualification;
        /// <summary>
        /// Обозначение дисциплины
        /// </summary>
        string CodeDiscipline;
        /// <summary>
        /// Обозначение кафедры
        /// </summary>
        string CodeChair;
        /// <summary>
        /// Массив пропуска ячеек для Б
        /// </summary>
        string[] PassValueB = { null,
                                   "Б1", 
                                   "Б1.Б", 
                                   "Б1.В", 
                                   "Б1.В.ОД", 
                                   "*",
                                   "Б2",
                                   "Б2.Б",
                                   "Б2.В",
                                   "Б2.В.ОД",
                                   "Б3",
                                   "Б3.Б",
                                   "Б3.В",
                                   "Б3.В.ОД",
                                   "Индекс",
                                   "Б5",
                                   "Б5.У",
                                   "Б5.Н",
                                   "Б5.П",
                                   "Б6",
                                   "ФТД" };
        /// <summary>
        /// Массив пропуска ячеек для С
        /// </summary>
        string[] PassValueS = { null,
                                   "С1", 
                                   "С1.Б", 
                                   "С1.В", 
                                   "С1.В.ОД", 
                                   "*",
                                   "С2",
                                   "С2.Б",
                                   "С2.В",
                                   "С2.В.ОД",
                                   "С3",
                                   "С3.Б",
                                   "С3.В",
                                   "С3.В.ОД",
                                   "Индекс",
                                   "С5",
                                   "С5.У",
                                   "С5.Н",
                                   "С5.П",
                                   "С6",
                                   "ФТД" };
        /// <summary>
        /// Массив пропуска ячеек для М
        /// </summary>
        string[] PassValueM = { null,
                                   "М1", 
                                   "М1.Б", 
                                   "М1.В", 
                                   "М1.В.ОД", 
                                   "*",
                                   "М2",
                                   "М2.Б",
                                   "М2.В",
                                   "М2.В.ОД",
                                   "Индекс",
                                   "М3",
                                   "М3.У",
                                   "М3.Н",
                                   "М3.П",
                                   "М3.Д",
                                   "М4",
                                   "ФТД" };



        
        #endregion

        /// <summary>
        /// Импорт учебного плана из Excel - метод
        /// </summary>
        public TitleCurriculum GetCurriculum(Excel.Workbook workbook)
        {
            titleCur = new TitleCurriculum();
            ImportExlOldPlan = new ServiceCurriculumFromExlOldPlan();
            this.workbook = workbook;
            // Выгрузка данных с титульного листа
            sheet = workbook.Worksheets[1];
            string type = Convert.ToString(sheet.Range["H5"].Value);
            if (type == "УЧЕБНЫЙ ПЛАН")
            {
                // Определение вида учебного плана
                EducationForm = Convert.ToString(sheet.Range["P9"].Value);
                if (EducationForm == null)
                    EducationForm = Convert.ToString(sheet.Range["Q9"].Value);
                int ixSeparator = EducationForm.IndexOf(":") + 2;
                EducationForm = EducationForm.Substring(ixSeparator);
                Qualification = Convert.ToString(sheet.Range["B21"].Value);
            
                // Выгрузка дисциплин
            
                string KindOfCurriculum = Qualification + "-" + EducationForm.Substring(0, 5);
                switch (KindOfCurriculum)
                {
                    case "Бакалавр-очная":
                        titleCur.Disciplines = GetListDisciplineFullTimeBachelor(titleCur);
                        break;
                    case "Бакалавр-заочн":
                        titleCur.Disciplines = GetListDisciplineExternalBachelor(titleCur);
                        break;
                    case "Магистр-очная":
                        titleCur.Disciplines = GetListDisciplineFullTimeMaster(titleCur);
                        break;
                    case "Магистр-заочн":
                        break;
                    default:
                        break;
                }

                GetGeneralPropertiesOfCurriculum();
            
            }
            else
                titleCur = ImportExlOldPlan.GetCurriculum(workbook);

            return titleCur;
        }

        /// <summary>
        /// Метод добавления дисциплин Бакалавра-очника
        /// </summary>
        public List<Discipline> GetListDisciplineFullTimeBachelor(TitleCurriculum titleCur)
        {
            titleCur.ChairName = Convert.ToString(sheet.Range["C17"].Value);
            sheet = workbook.Worksheets[3];
            titleCur.Chair = GetCodeChairFullTimeBachelor(titleCur.ChairName);
            Disciplines = new List<Discipline>();

            for (int i = 16; i < 300; i++)
            {
                CodeDiscipline = Convert.ToString(sheet.Range["A" + i].Value);

                // Загрузка практики
                if (CodeDiscipline == "Б5")
                {
                    GetPracticFullTimeBachelor(i, titleCur);
                }

                // Проверка пропускаемой строки
                for (int j = 0; j < PassValueB.Length; j++)
                {
                    if (CodeDiscipline == PassValueB[j])
                        goto m;
                }
                if (CodeDiscipline == "Б1.В.ДВ" || CodeDiscipline == "Б2.В.ДВ" || CodeDiscipline == "Б3.В.ДВ")
                {
                    i = GetSecondDisciplinesFullTime(i + 3);
                    goto m;
                }

                // семестр № 1
                // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                int controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["AA" + i].Value);
                int controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["AB" + i].Value);
                CodeChair = Convert.ToString(sheet.Range["EU" + i].Value);

                if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                {
                    DisciplinesImport = new Discipline();
                    DisciplinesImport.Course = "1";
                    DisciplinesImport.Lecture = Convert.ToString(sheet.Range["Z" + i].Value);
                    DisciplinesImport.Lab = Convert.ToString(sheet.Range["AA" + i].Value);
                    DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AB" + i].Value);
                    DisciplinesImport.Semester = "1";
                    DisciplinesImport.SemesterYear = "осень";

                    GetGeneralPropertiesOfDisciplineFullTimeBachelor(i);

                    Disciplines.Add(DisciplinesImport);
                }

                // семестр № 2
                // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["AH" + i].Value);
                controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["AI" + i].Value);

                if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                {
                    DisciplinesImport = new Discipline();

                    DisciplinesImport.Course = "1";
                    DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AG" + i].Value);
                    DisciplinesImport.Lab = Convert.ToString(sheet.Range["AH" + i].Value);
                    DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AI" + i].Value);
                    DisciplinesImport.Semester = "2";
                    DisciplinesImport.SemesterYear = "весна";

                    GetGeneralPropertiesOfDisciplineFullTimeBachelor(i);

                    Disciplines.Add(DisciplinesImport);

                }

                // семестр № 3
                // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["AQ" + i].Value);
                controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["AR" + i].Value);

                if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                {
                    DisciplinesImport = new Discipline();

                    DisciplinesImport.Course = "2";
                    DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AP" + i].Value);
                    DisciplinesImport.Lab = Convert.ToString(sheet.Range["AQ" + i].Value);
                    DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AR" + i].Value);
                    DisciplinesImport.Semester = "3";
                    DisciplinesImport.SemesterYear = "осень";

                    GetGeneralPropertiesOfDisciplineFullTimeBachelor(i);

                    Disciplines.Add(DisciplinesImport);

                }

                // семестр № 4
                // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["AX" + i].Value);
                controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["AY" + i].Value);

                if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                {
                    DisciplinesImport = new Discipline();

                    DisciplinesImport.Course = "2";
                    DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AW" + i].Value);
                    DisciplinesImport.Lab = Convert.ToString(sheet.Range["AX" + i].Value);
                    DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AY" + i].Value);
                    DisciplinesImport.Semester = "4";
                    DisciplinesImport.SemesterYear = "весна";

                    GetGeneralPropertiesOfDisciplineFullTimeBachelor(i);

                    Disciplines.Add(DisciplinesImport);

                }

                // семестр № 5
                // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["BG" + i].Value);
                controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["BH" + i].Value);

                if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                {
                    DisciplinesImport = new Discipline();

                    DisciplinesImport.Course = "3";
                    DisciplinesImport.Lecture = Convert.ToString(sheet.Range["BF" + i].Value);
                    DisciplinesImport.Lab = Convert.ToString(sheet.Range["BG" + i].Value);
                    DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["BH" + i].Value);
                    DisciplinesImport.Semester = "5";
                    DisciplinesImport.SemesterYear = "осень";

                    GetGeneralPropertiesOfDisciplineFullTimeBachelor(i);

                    Disciplines.Add(DisciplinesImport);

                }

                // семестр № 6
                // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["BN" + i].Value);
                controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["BO" + i].Value);

                if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                {
                    DisciplinesImport = new Discipline();

                    DisciplinesImport.Course = "3";
                    DisciplinesImport.Lecture = Convert.ToString(sheet.Range["BM" + i].Value);
                    DisciplinesImport.Lab = Convert.ToString(sheet.Range["BN" + i].Value);
                    DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["BO" + i].Value);
                    DisciplinesImport.Semester = "6";
                    DisciplinesImport.SemesterYear = "весна";

                    GetGeneralPropertiesOfDisciplineFullTimeBachelor(i);

                    Disciplines.Add(DisciplinesImport);

                }

                // семестр № 7
                // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["BW" + i].Value);
                controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["BX" + i].Value);

                if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                {
                    DisciplinesImport = new Discipline();

                    DisciplinesImport.Course = "4";
                    DisciplinesImport.Lecture = Convert.ToString(sheet.Range["BV" + i].Value);
                    DisciplinesImport.Lab = Convert.ToString(sheet.Range["BW" + i].Value);
                    DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["BX" + i].Value);
                    DisciplinesImport.Semester = "7";
                    DisciplinesImport.SemesterYear = "осень";

                    GetGeneralPropertiesOfDisciplineFullTimeBachelor(i);

                    Disciplines.Add(DisciplinesImport);
                }

                // семестр № 8
                // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["CD" + i].Value);
                controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["CE" + i].Value);

                if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                {
                    DisciplinesImport = new Discipline();

                    DisciplinesImport.Course = "4";
                    DisciplinesImport.Lecture = Convert.ToString(sheet.Range["CC" + i].Value);
                    DisciplinesImport.Lab = Convert.ToString(sheet.Range["CD" + i].Value);
                    DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["CE" + i].Value);
                    DisciplinesImport.Semester = "8";
                    DisciplinesImport.SemesterYear = "весна";

                    GetGeneralPropertiesOfDisciplineFullTimeBachelor(i);

                    Disciplines.Add(DisciplinesImport);

                }

            m: continue;
            }

            return Disciplines;
        }

        /// <summary>
        /// Метод добавления дисциплин Бакалавра-заочника
        /// </summary>
        public List<Discipline> GetListDisciplineExternalBachelor(TitleCurriculum titleCur)
        {
            titleCur.ChairName = Convert.ToString(sheet.Range["C17"].Value);
            sheet = workbook.Worksheets[4];
            titleCur.Chair = GetCodeChairExternalBachelor(titleCur.ChairName);
            Disciplines = new List<Discipline>();
            
            for (int i = 16; i < 300; i++)
            {
                CodeDiscipline = Convert.ToString(sheet.Range["A" + i].Value);
                string CodeDiscipline1 = Convert.ToString(sheet.Range["A" + 16].Value);
                // Загрузка практики
                if ((CodeDiscipline == "Б5") || (CodeDiscipline == "С5"))
                {
                    GetPracticExternalBachelor(i, titleCur);
                }

                // Проверка пропускаемой строки
                if (CodeDiscipline1 == "Б1")
                {
                    for (int j = 0; j < PassValueB.Length; j++)
                    {
                        if (CodeDiscipline == PassValueB[j])
                            goto m;
                    }
                    if (CodeDiscipline1 == "Б1.В.ДВ" || CodeDiscipline == "Б2.В.ДВ" || CodeDiscipline == "Б3.В.ДВ")
                    {
                        i = GetSecondDisciplinesExternal(i + 3);
                        goto m;
                    }
                }
                else
                {
                    for (int j = 0; j < PassValueS.Length; j++)
                    {
                        if (CodeDiscipline == PassValueS[j])
                            goto m;
                    }
                    if (CodeDiscipline == "С1.В.ДВ" || CodeDiscipline == "С2.В.ДВ" || CodeDiscipline == "С3.В.ДВ")
                    {
                        i = GetSecondDisciplinesExternal(i + 3);
                        goto m;
                    }
                }

                // курс № 1
                // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                int controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["AM" + i].Value);
                int controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["AN" + i].Value);
                CodeChair = Convert.ToString(sheet.Range["DD" + i].Value);

                if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                {
                    DisciplinesImport = new Discipline();

                    DisciplinesImport.Course = "1";
                    DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AL" + i].Value);
                    DisciplinesImport.Lab = Convert.ToString(sheet.Range["AM" + i].Value);
                    DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AN" + i].Value);

                    GetGeneralPropertiesOfDisciplineExternalBachelor(i);

                    Disciplines.Add(DisciplinesImport);
                }

                // курс № 2
                // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["AW" + i].Value);
                controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["AX" + i].Value);

                if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                {
                    DisciplinesImport = new Discipline();

                    DisciplinesImport.Course = "2";
                    DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AV" + i].Value);
                    DisciplinesImport.Lab = Convert.ToString(sheet.Range["AW" + i].Value);
                    DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AX" + i].Value);

                    GetGeneralPropertiesOfDisciplineExternalBachelor(i);

                    Disciplines.Add(DisciplinesImport);

                }

                // курс № 3
                // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["BG" + i].Value);
                controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["BH" + i].Value);

                if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                {
                    DisciplinesImport = new Discipline();

                    DisciplinesImport.Course = "3";
                    DisciplinesImport.Lecture = Convert.ToString(sheet.Range["BF" + i].Value);
                    DisciplinesImport.Lab = Convert.ToString(sheet.Range["BG" + i].Value);
                    DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["BH" + i].Value);

                    GetGeneralPropertiesOfDisciplineExternalBachelor(i);

                    Disciplines.Add(DisciplinesImport);

                }

                // курс № 4
                // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["BQ" + i].Value);
                controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["BR" + i].Value);

                if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                {
                    DisciplinesImport = new Discipline();

                    DisciplinesImport.Course = "4";
                    DisciplinesImport.Lecture = Convert.ToString(sheet.Range["BP" + i].Value);
                    DisciplinesImport.Lab = Convert.ToString(sheet.Range["BQ" + i].Value);
                    DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["BR" + i].Value);

                    GetGeneralPropertiesOfDisciplineExternalBachelor(i);

                    Disciplines.Add(DisciplinesImport);

                }

                // курс № 5
                // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["CA" + i].Value);
                controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["CB" + i].Value);

                if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                {
                    DisciplinesImport = new Discipline();

                    DisciplinesImport.Course = "5";
                    DisciplinesImport.Lecture = Convert.ToString(sheet.Range["BZ" + i].Value);
                    DisciplinesImport.Lab = Convert.ToString(sheet.Range["CA" + i].Value);
                    DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["CB" + i].Value);

                    GetGeneralPropertiesOfDisciplineExternalBachelor(i);

                    Disciplines.Add(DisciplinesImport);

                }

            m: continue;
            }

            return Disciplines;
        }

        /// <summary>
        /// Метод добавления дисциплин Магистранта-очника"
        /// </summary>
        public List<Discipline> GetListDisciplineFullTimeMaster(TitleCurriculum titleCur)
        {
            return Disciplines;
        }

        /// <summary>
        /// Метод добавления дисциплин Бакалавра-заочника
        /// </summary>
        public List<Discipline> GetListDisciplineExternalMaster()
        {
            return Disciplines;
        }

        /// <summary>
        /// Метод получения базовых свойств дисциплин очного учебного плана
        /// </summary>
        private void GetGeneralPropertiesOfDisciplineFullTimeBachelor(int i)
        {
            DisciplinesImport.CodeDiscipline = Convert.ToString(sheet.Range["A" + i].Value);
            DisciplinesImport.Discipl = Convert.ToString(sheet.Range["B" + i].Value);
            DisciplinesImport.CodeChair = Convert.ToString(sheet.Range["EU" + i].Value);

            // Переменная для контроля значений ячеек по экзаменам и зачетам
            string controlExamAndSetOff = Convert.ToString(sheet.Range["C" + i].Value);
            if (controlExamAndSetOff != null && controlExamAndSetOff.Length > 1)
                DisciplinesImport.Exam = GetSemesterNumber(controlExamAndSetOff, DisciplinesImport.Semester);
            else
            {
                if (controlExamAndSetOff == DisciplinesImport.Semester)
                    DisciplinesImport.Exam = "1";
                else
                    DisciplinesImport.Exam = "0";
            }

            controlExamAndSetOff = Convert.ToString(sheet.Range["D" + i].Value);
            if (controlExamAndSetOff != null && controlExamAndSetOff.Length > 1)
                DisciplinesImport.SetOff = GetSemesterNumber(controlExamAndSetOff, DisciplinesImport.Semester);
            else
            {
                if (controlExamAndSetOff == DisciplinesImport.Semester)
                    DisciplinesImport.SetOff = "1";
                else
                    DisciplinesImport.SetOff = "0";
            }

            // Переменная для контроля значений ячеек по курсовым проектам и курсовым работам
            string controlCourseProjectAndWork = Convert.ToString(sheet.Range["F" + i].Value);
            if (controlCourseProjectAndWork == DisciplinesImport.Semester)
                DisciplinesImport.CourseProject = "1";
            else
                DisciplinesImport.CourseProject = "0";
            
            controlCourseProjectAndWork = Convert.ToString(sheet.Range["G" + i].Value);
            if (controlCourseProjectAndWork == DisciplinesImport.Semester)
                DisciplinesImport.CourseWork = "1";
            else
                DisciplinesImport.CourseWork = "0";
            DisciplinesImport.ControlWork = "0";
        }

        /// <summary>
        /// Метод получения базовых свойств дисциплин заочного учебного плана
        /// </summary>
        private void GetGeneralPropertiesOfDisciplineExternalBachelor(int i)
        {
            DisciplinesImport.CodeDiscipline = Convert.ToString(sheet.Range["A" + i].Value);
            DisciplinesImport.Discipl = Convert.ToString(sheet.Range["B" + i].Value);
            DisciplinesImport.CodeChair = Convert.ToString(sheet.Range["DD" + i].Value);

            // Переменная для контроля значений ячеек по экзаменам и зачетам
            string controlExamAndSetOff = Convert.ToString(sheet.Range["C" + i].Value);
            if (controlExamAndSetOff != null && controlExamAndSetOff.Length > 1)
                DisciplinesImport.Exam = GetSemesterNumber(controlExamAndSetOff, DisciplinesImport.Course);
            else
            {
                if (controlExamAndSetOff == DisciplinesImport.Course)
                    DisciplinesImport.Exam = "1";
                else
                    DisciplinesImport.Exam = "0";
            }

            controlExamAndSetOff = Convert.ToString(sheet.Range["D" + i].Value);
            if (controlExamAndSetOff != null && controlExamAndSetOff.Length > 1)
                DisciplinesImport.SetOff = GetSemesterNumber(controlExamAndSetOff, DisciplinesImport.Course);
            else
            {
                if (controlExamAndSetOff == DisciplinesImport.Course)
                    DisciplinesImport.SetOff = "1";
                else
                    DisciplinesImport.SetOff = "0";
            }

            // Переменная для контроля значений ячеек по курсовым проектам и курсовым работам
            string controlCourseProjectAndWork = Convert.ToString(sheet.Range["F" + i].Value);
            if (controlCourseProjectAndWork == DisciplinesImport.Course)
                DisciplinesImport.CourseProject = "1";
            else
                DisciplinesImport.CourseProject = "0";

            controlCourseProjectAndWork = Convert.ToString(sheet.Range["G" + i].Value);
            if (controlCourseProjectAndWork == DisciplinesImport.Course)
                DisciplinesImport.CourseWork = "1";
            else
                DisciplinesImport.CourseWork = "0";
            DisciplinesImport.ControlWork = Convert.ToString(sheet.Range["H" + i].Value);
        }

        private int GetSecondDisciplinesFullTime(int i)
        {
            CodeDiscipline = Convert.ToString(sheet.Range["A" + (i - 1)].Value);
            string CodePlan = null;
            while (CodePlan != "ДВ*")
            {
                if (CodePlan == "*")
                {
                    CodeDiscipline = Convert.ToString(sheet.Range["A" + (i + 1)].Value);
                    i = i + 2;
                    CodePlan = Convert.ToString(sheet.Range["A" + i].Value);
                }
                else
                {
                    // семестр № 1
                    // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                    int controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["AA" + i].Value);
                    int controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["AB" + i].Value);
                    CodeChair = Convert.ToString(sheet.Range["EU" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "1";
                        DisciplinesImport.Lecture = Convert.ToString(sheet.Range["Z" + i].Value);
                        DisciplinesImport.Lab = Convert.ToString(sheet.Range["AA" + i].Value);
                        DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AB" + i].Value);
                        DisciplinesImport.Semester = "1";
                        DisciplinesImport.SemesterYear = "осень";

                        GetGeneralPropertiesOfDisciplineFullTimeBachelor(i);
                        DisciplinesImport.CodeDiscipline = CodeDiscipline + "." + DisciplinesImport.CodeDiscipline;

                        Disciplines.Add(DisciplinesImport);

                    }

                    // семестр № 2
                    // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["AH" + i].Value);
                    controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["AI" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "1";
                        DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AG" + i].Value);
                        DisciplinesImport.Lab = Convert.ToString(sheet.Range["AH" + i].Value);
                        DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AI" + i].Value);
                        DisciplinesImport.Semester = "2";
                        DisciplinesImport.SemesterYear = "весна";

                        GetGeneralPropertiesOfDisciplineFullTimeBachelor(i);
                        DisciplinesImport.CodeDiscipline = CodeDiscipline + "." + DisciplinesImport.CodeDiscipline;

                        Disciplines.Add(DisciplinesImport);

                    }

                    // семестр № 3
                    // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["AQ" + i].Value);
                    controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["AR" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "2";
                        DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AG" + i].Value);
                        DisciplinesImport.Lab = Convert.ToString(sheet.Range["AQ" + i].Value);
                        DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AR" + i].Value);
                        DisciplinesImport.Semester = "3";
                        DisciplinesImport.SemesterYear = "осень";

                        GetGeneralPropertiesOfDisciplineFullTimeBachelor(i);
                        DisciplinesImport.CodeDiscipline = CodeDiscipline + "." + DisciplinesImport.CodeDiscipline;

                        Disciplines.Add(DisciplinesImport);

                    }

                    // семестр № 4
                    // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["AX" + i].Value);
                    controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["AY" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "2";
                        DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AW" + i].Value);
                        DisciplinesImport.Lab = Convert.ToString(sheet.Range["AX" + i].Value);
                        DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AY" + i].Value);
                        DisciplinesImport.Semester = "4";
                        DisciplinesImport.SemesterYear = "весна";

                        GetGeneralPropertiesOfDisciplineFullTimeBachelor(i);
                        DisciplinesImport.CodeDiscipline = CodeDiscipline + "." + DisciplinesImport.CodeDiscipline;

                        Disciplines.Add(DisciplinesImport);

                    }

                    // семестр № 5
                    // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["BG" + i].Value);
                    controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["BH" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "3";
                        DisciplinesImport.Lecture = Convert.ToString(sheet.Range["BF" + i].Value);
                        DisciplinesImport.Lab = Convert.ToString(sheet.Range["BG" + i].Value);
                        DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["BH" + i].Value);
                        DisciplinesImport.Semester = "5";
                        DisciplinesImport.SemesterYear = "осень";

                        GetGeneralPropertiesOfDisciplineFullTimeBachelor(i);
                        DisciplinesImport.CodeDiscipline = CodeDiscipline + "." + DisciplinesImport.CodeDiscipline;

                        Disciplines.Add(DisciplinesImport);

                    }

                    // семестр № 6
                    // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["BN" + i].Value);
                    controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["BO" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "3";
                        DisciplinesImport.Lecture = Convert.ToString(sheet.Range["BM" + i].Value);
                        DisciplinesImport.Lab = Convert.ToString(sheet.Range["BN" + i].Value);
                        DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["BO" + i].Value);
                        DisciplinesImport.Semester = "6";
                        DisciplinesImport.SemesterYear = "весна";

                        GetGeneralPropertiesOfDisciplineFullTimeBachelor(i);
                        DisciplinesImport.CodeDiscipline = CodeDiscipline + "." + DisciplinesImport.CodeDiscipline;

                        Disciplines.Add(DisciplinesImport);

                    }

                    // семестр № 7
                    // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["BW" + i].Value);
                    controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["BX" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "4";
                        DisciplinesImport.Lecture = Convert.ToString(sheet.Range["BV" + i].Value);
                        DisciplinesImport.Lab = Convert.ToString(sheet.Range["BW" + i].Value);
                        DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["BX" + i].Value);
                        DisciplinesImport.Semester = "7";
                        DisciplinesImport.SemesterYear = "осень";

                        GetGeneralPropertiesOfDisciplineFullTimeBachelor(i);
                        DisciplinesImport.CodeDiscipline = CodeDiscipline + "." + DisciplinesImport.CodeDiscipline;

                        Disciplines.Add(DisciplinesImport);

                    }

                    // семестр № 8
                    // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["CD" + i].Value);
                    controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["CE" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "4";
                        DisciplinesImport.Lecture = Convert.ToString(sheet.Range["CC" + i].Value);
                        DisciplinesImport.Lab = Convert.ToString(sheet.Range["CD" + i].Value);
                        DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["CE" + i].Value);
                        DisciplinesImport.Semester = "8";
                        DisciplinesImport.SemesterYear = "весна";

                        GetGeneralPropertiesOfDisciplineFullTimeBachelor(i);
                        DisciplinesImport.CodeDiscipline = CodeDiscipline + "." + DisciplinesImport.CodeDiscipline;

                        Disciplines.Add(DisciplinesImport);

                    }

                    i++;
                    CodePlan = Convert.ToString(sheet.Range["A" + i].Value);
                    i++;
                }
            }

            return i;
        }

        private int GetSecondDisciplinesExternal(int i)
        {
            CodeDiscipline = Convert.ToString(sheet.Range["A" + (i - 1)].Value);
            string CodePlan = null;
            while (CodePlan != "ДВ*")
            {
                if (CodePlan == "*")
                {
                    CodeDiscipline = Convert.ToString(sheet.Range["A" + (i + 1)].Value);
                    i = i + 2;
                    CodePlan = Convert.ToString(sheet.Range["A" + i].Value);
                }
                else
                {
                    // курс № 1
                    // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                    int controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["AM" + i].Value);
                    int controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["AN" + i].Value);
                    CodeChair = Convert.ToString(sheet.Range["DD" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "1";
                        DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AL" + i].Value);
                        DisciplinesImport.Lab = Convert.ToString(sheet.Range["AM" + i].Value);
                        DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AN" + i].Value);

                        GetGeneralPropertiesOfDisciplineExternalBachelor(i);
                        DisciplinesImport.CodeDiscipline = CodeDiscipline + "." + DisciplinesImport.CodeDiscipline;

                        Disciplines.Add(DisciplinesImport);

                    }

                    // курс № 2
                    // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["AW" + i].Value);
                    controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["AX" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "2";
                        DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AV" + i].Value);
                        DisciplinesImport.Lab = Convert.ToString(sheet.Range["AW" + i].Value);
                        DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AX" + i].Value);

                        GetGeneralPropertiesOfDisciplineExternalBachelor(i);
                        DisciplinesImport.CodeDiscipline = CodeDiscipline + "." + DisciplinesImport.CodeDiscipline;

                        Disciplines.Add(DisciplinesImport);

                    }

                    // курс № 3
                    // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["BG" + i].Value);
                    controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["BH" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "3";
                        DisciplinesImport.Lecture = Convert.ToString(sheet.Range["BF" + i].Value);
                        DisciplinesImport.Lab = Convert.ToString(sheet.Range["BG" + i].Value);
                        DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["BH" + i].Value);

                        GetGeneralPropertiesOfDisciplineExternalBachelor(i);
                        DisciplinesImport.CodeDiscipline = CodeDiscipline + "." + DisciplinesImport.CodeDiscipline;

                        Disciplines.Add(DisciplinesImport);

                    }

                    // курс № 4
                    // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["BQ" + i].Value);
                    controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["BR" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "4";
                        DisciplinesImport.Lecture = Convert.ToString(sheet.Range["BP" + i].Value);
                        DisciplinesImport.Lab = Convert.ToString(sheet.Range["BQ" + i].Value);
                        DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["BR" + i].Value);

                        GetGeneralPropertiesOfDisciplineExternalBachelor(i);
                        DisciplinesImport.CodeDiscipline = CodeDiscipline + "." + DisciplinesImport.CodeDiscipline;

                        Disciplines.Add(DisciplinesImport);

                    }

                    // курс № 5
                    // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["CA" + i].Value);
                    controlPracticalExercisesPlan = Convert.ToInt32(sheet.Range["CB" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlPracticalExercisesPlan != 0) && CodeChair != null)
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "5";
                        DisciplinesImport.Lecture = Convert.ToString(sheet.Range["BZ" + i].Value);
                        DisciplinesImport.Lab = Convert.ToString(sheet.Range["CA" + i].Value);
                        DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["CB" + i].Value);

                        GetGeneralPropertiesOfDisciplineExternalBachelor(i);
                        DisciplinesImport.CodeDiscipline = CodeDiscipline + "." + DisciplinesImport.CodeDiscipline;

                        Disciplines.Add(DisciplinesImport);
                    }

                    i++;
                    CodePlan = Convert.ToString(sheet.Range["A" + i].Value);
                    i++;
                }
            }

            return i;
        }

        /// <summary>
        /// Метод синтаксического разбора
        /// </summary>
        private string GetSemesterNumber(string semesterExamAndSetOff, string semester)
        {
            int[] n;
            string sem = semesterExamAndSetOff.Substring(1, 1);
            if (sem=="-")
            {            
                int m = Convert.ToInt32(semesterExamAndSetOff.Substring(2, 1));
                for(int i=1; i<=m; i++)
                {
                    if (semester == i.ToString())
                    {
                        semesterExamAndSetOff = "1";
                        break;
                    }
                }
            }
            else
            {
                n = new int[semesterExamAndSetOff.Length];
                for (int i = 0; i < semesterExamAndSetOff.Length; i++)
                {
                    n[i] = Convert.ToInt32(semesterExamAndSetOff.Substring(i, 1));
                    if (n[i]==Convert.ToInt32(semester))
                    {
                        semesterExamAndSetOff = "1";
                        break;
                    }
                }
            }
            if (semesterExamAndSetOff.Length > 1)
                semesterExamAndSetOff = "0";

            return semesterExamAndSetOff;
        }

        /// <summary>
        /// Метод получения Кода кафедры очного учебного плана
        /// </summary>
        private string GetCodeChairFullTimeBachelor(string NameChair)
        {
            for (int i = 110; i < 170; i++)
            {
                string NameChair_1 = Convert.ToString(sheet.Range["EV" + i].Value);
                if (NameChair == NameChair_1)
                {
                    NameChair = Convert.ToString(sheet.Range["EU" + i].Value);
                    break;
                }
            }
            return NameChair;
        }

        /// <summary>
        /// Метод получения Кода кафедры заочного учебного плана
        /// </summary>
        private string GetCodeChairExternalBachelor(string NameChair)
        {
            for (int i = 110; i < 170; i++)
            {
                string NameChair_1 = Convert.ToString(sheet.Range["DE" + i].Value);
                if (NameChair == NameChair_1)
                {
                    NameChair = Convert.ToString(sheet.Range["DD" + i].Value);
                    break;
                }
            }
            return NameChair;
        }

        public void GetPracticFullTimeBachelor(int x, TitleCurriculum titleCur)
        {
            for (int i = x; i < x + 15; i++)
            {
                string practicName = Convert.ToString(sheet.Range["B" + i].Value);
                i++;
                string practicName1 = Convert.ToString(sheet.Range["B" + i].Value);

                if ((practicName != null) && (practicName1 != null))
                {
                    
                    // семестр № 1
                    // Переменные для контроля значений ячеек лекций и лаб. работ практики 
                    int controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["AA" + i].Value);
                    int controlLecturePlan = Convert.ToInt32(sheet.Range["Z" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlLecturePlan != 0))
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "1";
                        DisciplinesImport.Pract = Convert.ToString(sheet.Range["Z" + i].Value);
                        DisciplinesImport.Semester = "1";
                        DisciplinesImport.SemesterYear = "осень";
                        GetPracticProperties(x, practicName1, titleCur);
                        Disciplines.Add(DisciplinesImport);
                    }

                    // семестр № 2
                    // Переменные для контроля значений ячеек лекций лаб. работ практики 
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["AH" + i].Value);
                    controlLecturePlan = Convert.ToInt32(sheet.Range["AG" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlLecturePlan != 0))
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "1";
                        DisciplinesImport.Pract = Convert.ToString(sheet.Range["AG" + i].Value);
                        DisciplinesImport.Semester = "2";
                        DisciplinesImport.SemesterYear = "весна";
                        GetPracticProperties(x, practicName1, titleCur);
                        Disciplines.Add(DisciplinesImport);

                    }

                    // семестр № 3
                    // Переменные для контроля значений ячеек лекций лаб. работ практики 
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["AQ" + i].Value);
                    controlLecturePlan = Convert.ToInt32(sheet.Range["AP" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlLecturePlan != 0))
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "2";
                        DisciplinesImport.Pract = Convert.ToString(sheet.Range["AP" + i].Value);
                        DisciplinesImport.Semester = "3";
                        DisciplinesImport.SemesterYear = "осень";
                        GetPracticProperties(x, practicName1, titleCur);
                        Disciplines.Add(DisciplinesImport);

                    }

                    // семестр № 4
                    // Переменные для контроля значений ячеек лекций лаб. работ практики 
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["AX" + i].Value);
                    controlLecturePlan = Convert.ToInt32(sheet.Range["AW" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlLecturePlan != 0))
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "2";
                        DisciplinesImport.Pract = Convert.ToString(sheet.Range["AW" + i].Value);
                        DisciplinesImport.Semester = "4";
                        DisciplinesImport.SemesterYear = "весна";
                        GetPracticProperties(x, practicName1, titleCur);
                        Disciplines.Add(DisciplinesImport);

                    }

                    // семестр № 5
                    // Переменные для контроля значений ячеек лекций лаб. работ практики 
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["BG" + i].Value);
                    controlLecturePlan = Convert.ToInt32(sheet.Range["BF" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlLecturePlan != 0))
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "3";
                        DisciplinesImport.Pract = Convert.ToString(sheet.Range["BF" + i].Value);
                        DisciplinesImport.Semester = "5";
                        DisciplinesImport.SemesterYear = "осень";
                        GetPracticProperties(x, practicName1, titleCur);
                        Disciplines.Add(DisciplinesImport);

                    }

                    // семестр № 6
                    // Переменные для контроля значений ячеек лекций лаб. работ практики 
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["BN" + i].Value);
                    controlLecturePlan = Convert.ToInt32(sheet.Range["BM" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlLecturePlan != 0))
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "3";
                        DisciplinesImport.Pract = Convert.ToString(sheet.Range["BM" + i].Value);
                        DisciplinesImport.Semester = "6";
                        DisciplinesImport.SemesterYear = "весна";
                        GetPracticProperties(x, practicName1, titleCur);
                        Disciplines.Add(DisciplinesImport);

                    }

                    // семестр № 7
                    // Переменные для контроля значений ячеек лекций лаб. работ практики 
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["BW" + i].Value);
                    controlLecturePlan = Convert.ToInt32(sheet.Range["BV" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlLecturePlan != 0))
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "4";
                        DisciplinesImport.Pract = Convert.ToString(sheet.Range["BV" + i].Value);
                        DisciplinesImport.Semester = "7";
                        DisciplinesImport.SemesterYear = "осень";
                        GetPracticProperties(x, practicName1, titleCur);
                        Disciplines.Add(DisciplinesImport);
                    }

                    // семестр № 8
                    // Переменные для контроля значений ячеек лекций лаб. работ практики 
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["CD" + i].Value);
                    controlLecturePlan = Convert.ToInt32(sheet.Range["CC" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlLecturePlan != 0))
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "4";
                        DisciplinesImport.Pract = Convert.ToString(sheet.Range["CC" + i].Value);
                        DisciplinesImport.Semester = "8";
                        DisciplinesImport.SemesterYear = "весна";
                        GetPracticProperties(x, practicName1, titleCur);
                        Disciplines.Add(DisciplinesImport);

                    }
                }
                i--;
            }
        }

        private void GetPracticProperties(int x, string practicName1, TitleCurriculum titleCur)
        {
            DisciplinesImport.CodeDiscipline = Convert.ToString(sheet.Range["A" + x].Value);
            DisciplinesImport.Discipl = practicName1 + " практика";
            DisciplinesImport.Lecture = "0";
            DisciplinesImport.Lab = "0";
            DisciplinesImport.PracticalExercise = "0";
            DisciplinesImport.ControlWork = "0";
            DisciplinesImport.CourseProject = "0";
            DisciplinesImport.CourseWork = "0";
            DisciplinesImport.SetOff = "1";
            DisciplinesImport.Exam = "0";
            DisciplinesImport.CodeChair = titleCur.Chair;
        }

        public void GetPracticExternalBachelor(int x, TitleCurriculum titleCur)
        {
            for (int i = x; i < x + 15; i++)
            {
                string practicName = Convert.ToString(sheet.Range["B" + i].Value);
                i++;               
                string practicName1 = Convert.ToString(sheet.Range["B" + i].Value);

                if ((practicName != null) && (practicName1 != null))
                {
                    // курс № 1
                    // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                    int controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["AM" + i].Value);
                    int controlLecturePlan = Convert.ToInt32(sheet.Range["AL" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlLecturePlan != 0))
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "1";
                        DisciplinesImport.Pract = Convert.ToString(sheet.Range["AL" + i].Value);
                        GetPracticProperties(x, practicName1, titleCur);

                        Disciplines.Add(DisciplinesImport);
                    }

                    // курс № 2
                    // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["AW" + i].Value);
                    controlLecturePlan = Convert.ToInt32(sheet.Range["AV" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlLecturePlan != 0))
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "2";
                        DisciplinesImport.Pract = Convert.ToString(sheet.Range["AV" + i].Value);
                        GetPracticProperties(x, practicName1, titleCur);

                        Disciplines.Add(DisciplinesImport);

                    }

                    // курс № 3
                    // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["BG" + i].Value);
                    controlLecturePlan = Convert.ToInt32(sheet.Range["BF" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlLecturePlan != 0))
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "3";
                        DisciplinesImport.Pract = Convert.ToString(sheet.Range["BF" + i].Value);
                        GetPracticProperties(x, practicName1, titleCur);

                        Disciplines.Add(DisciplinesImport);

                    }

                    // курс № 4
                    // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["BQ" + i].Value);
                    controlLecturePlan = Convert.ToInt32(sheet.Range["BP" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlLecturePlan != 0))
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "4";
                        DisciplinesImport.Pract = Convert.ToString(sheet.Range["BP" + i].Value);
                        GetPracticProperties(x, practicName1, titleCur);

                        Disciplines.Add(DisciplinesImport);

                    }

                    // курс № 5
                    // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
                    controlLaboratoryWorkPlan = Convert.ToInt32(sheet.Range["CA" + i].Value);
                    controlLecturePlan = Convert.ToInt32(sheet.Range["BZ" + i].Value);

                    if ((controlLaboratoryWorkPlan != 0 || controlLecturePlan != 0))
                    {
                        DisciplinesImport = new Discipline();

                        DisciplinesImport.Course = "5";
                        DisciplinesImport.Pract = Convert.ToString(sheet.Range["BZ" + i].Value);
                        GetPracticProperties(x, practicName1, titleCur);

                        Disciplines.Add(DisciplinesImport);

                    }
                }
                i--;
            }
        }


        /// <summary>
        /// Метод получения базовых свойств учебного плана
        /// </summary>
        private void GetGeneralPropertiesOfCurriculum()
        {
            sheet = workbook.Worksheets[1];
            string NameOfSpeciality = Convert.ToString(sheet.Range["B12"].Value);
            // Убираем лишнее слово из названия специальности
            int ixSeparator = NameOfSpeciality.IndexOf(" ") + 1;
            NameOfSpeciality = NameOfSpeciality.Substring(ixSeparator);
            // Вытаскиваем код и наименование 
            int ixSeparator1 = NameOfSpeciality.IndexOf(" ") + 1;
            string CodeOfSpeciality = NameOfSpeciality.Substring(0, ixSeparator1 - 1);
            NameOfSpeciality = NameOfSpeciality.Substring(ixSeparator1);
            // Убираем кавычки из названия
            ixSeparator1 = NameOfSpeciality.Length;
            titleCur.Speciality = NameOfSpeciality.Substring(1, ixSeparator1 - 2);

            string Profile = Convert.ToString(sheet.Range["B13"].Value);
            // Вычисляем профиль специальности
                ixSeparator = -1;
            if (Profile != null)
                ixSeparator = Profile.IndexOfAny("0123456789".ToCharArray());

            if (ixSeparator >= 0)
            {
                titleCur.CodeSpeciality = Profile.Substring(ixSeparator, 8);
                titleCur.Profile = Profile.Substring(ixSeparator + 9);
            }
            else
                titleCur.CodeSpeciality = CodeOfSpeciality;

            titleCur.EducationForm = EducationForm;
            titleCur.Qualification = Qualification;
            titleCur.DurationStudy = Convert.ToString(sheet.Range["I21"].Value);
            titleCur.Faculty = Convert.ToString(sheet.Range["C18"].Value);
            titleCur.DataApproval = Convert.ToString(sheet.Range["A7"].Value);
            titleCur.Protocol = Convert.ToString(sheet.Range["C6"].Value);

        }

        /// <summary>
        /// Метод получения id учебного плана
        /// </summary>
        //private int GetCurriculumId(string NameOfCurriculum)
        //{
        //    int CurriculumId = 1;
        //    LCurriculum = new ListCurriculum();
        //    foreach (var c in LCurriculum)
        //    {
        //        if (NameOfCurriculum == c.Name)
        //        {
        //            CurriculumId = c.Id;
        //            break;
        //        }
        //    }
        //    return CurriculumId;
        //}

        /// <summary>
        /// Метод получения id специальности
        /// </summary>
        //private int GetSpecialityId(string CodeOfSpeciality)
        //{
        //    int SpecialityId = 1;
        //    Specialities = new ListSpeciality();
        //    foreach (var c in Specialities)
        //    {
        //        if (CodeOfSpeciality == c.CodeSpeciality)
        //        {
        //            SpecialityId = c.Id;
        //            break;
        //        }
        //    }
        //    return SpecialityId;
        //}

        /// <summary>
        /// Метод получения кода квалификации
        /// </summary>
        //private int GetQualificationId(string Qualific)
        //{
        //    int QualificationId = 1;
        //    Qualifications = new ListQualification();
        //    foreach (var c in Qualifications)
        //    {
        //        if (Qualific == c.NameQualification)
        //        {
        //            QualificationId = c.Id;
        //            break;
        //        }
        //    }
        //    return QualificationId;
        //}

        /// <summary>
        /// Метод получения кода формы обучения
        /// </summary>
        //private int GetEducationFormId(string EducationForm)
        //{
        //    int EducationFormId = 1;
        //    EducationForms = new ListEducationForm();
        //    foreach (var c in EducationForms)
        //    {
        //        if (EducationForm == c.FormEducation)
        //        {
        //            EducationFormId = c.Id;
        //            break;
        //        }
        //    }
        //    return EducationFormId;
        //}

        /// <summary>
        /// Метод получения кода кафедры
        /// </summary>
        //private int GetChairId(int CodeChair)
        //{
        //    int ChairId = 1;
        //    Chairs = new ListChair();
        //    foreach (var c in Chairs)
        //    {
        //        if (CodeChair == c.CodeChair)
        //        {
        //            ChairId = c.Id;
        //            break;
        //        }
        //    }
        //    return ChairId;
        //}




        /// <summary>
        /// Метод получения номера семестра курсового проекта
        /// для дисциплины учебного плана
        /// </summary>
        private int GetCourseProject(string Discipline)
        {
            int CourseProject = 0;
            string ImportDiscipline;
            for (int i = 5; i < 65; i++)
            {
                ImportDiscipline = Convert.ToString(sheet_dop.Range["N" + i].Value);
                if (Discipline == ImportDiscipline)
                {
                    CourseProject = (int)Convert.ToUInt32(sheet_dop.Range["P" + i].Value);
                    break;
                }
                i++;
            }
            return CourseProject;
        }


    }
}
