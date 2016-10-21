using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryServiceDB.Model;
using Excel = Microsoft.Office.Interop.Excel;

namespace ClassLibraryServiceDB.ServiceData
{
    public class ServiceCurriculumFromExlOldPlan
    {
        //Excel.Application excelApp;
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
        /// Титул учебного плана
        /// </summary>
        TitleCurriculum titleCur;
        /// <summary>
        /// Колекция дисциплин
        /// </summary>
        List<Discipline> Disciplines;
        /// <summary>
        /// Новая импортируемая дисциплина
        /// </summary>
        Discipline DisciplinesImport;
        /// <summary>
        /// Код кафедры
        /// </summary>
        string CodeChair;
        /// <summary>
        /// Курс дисциплины
        /// </summary>
        string Course;
        /// <summary>
        /// Обозначение дисциплины
        /// </summary>
        string CodeDiscipline;
        /// <summary>
        /// Квалификация
        /// </summary>
        string Qualific;
        /// <summary>
        /// Форма обучения
        /// </summary>
        string EducationForm;
        /// <summary>
        /// Наименование специальности
        /// </summary>
        string NameOfSpeciality;
        /// <summary>
        /// Наименование факультета
        /// </summary>
        string NameFaculty;
        /// <summary>
        /// Профиль дисциплины
        /// </summary>
        string Profile;
        /// <summary>
        /// Срок обучения
        /// </summary>
        string DurationOfStudy;
        /// <summary>
        /// № протокола
        /// </summary>
        string Protocol;
        /// <summary>
        /// Дата утверждения
        /// </summary>
        string DateOfApproval;


        public TitleCurriculum GetCurriculum(Excel.Workbook workbookOut)
        {
            titleCur = new TitleCurriculum();
            this.workbook = workbookOut;
            string name = workbook.Name;
            sheet = workbook.Worksheets[1];

            // Определение очного / заочного учебного плана
            EducationForm = Convert.ToString(sheet.Range["AM11"].Value);
            if (EducationForm == "очная")
                FullTimeCurriculum();
            else
                ExternalCurriculum();

            if (Qualific == "специалистов" || Qualific == "специалиста")
            {
                Qualific = "Специалист";
                NameOfSpeciality = Convert.ToString(sheet.Range["A13"].Value);
                Profile = Convert.ToString(sheet.Range["A14"].Value);
            }
            else
            {
                if (Qualific == "бакалавров" || Qualific == "бакалавра")
                    Qualific = "Бакалавр";
                else if (Qualific == "магистра" || Qualific == "магистров")
                    Qualific = "Магистр";
                NameOfSpeciality = Convert.ToString(sheet.Range["A14"].Value);
                Profile = Convert.ToString(sheet.Range["A15"].Value);
            }

            // Убираем лишнее слово из названия специальности
            int ixSeparator = NameOfSpeciality.IndexOf(" ") + 1;
            NameOfSpeciality = NameOfSpeciality.Substring(ixSeparator);
            // Вытаскиваем код и наименование 
            int ixSeparator1 = NameOfSpeciality.IndexOf(" ") + 1;
            string CodeOfSpeciality = NameOfSpeciality.Substring(0, ixSeparator1 - 1);
            NameOfSpeciality = NameOfSpeciality.Substring(ixSeparator1);
            // Убираем кавычки из названия
            ixSeparator1 = NameOfSpeciality.Length;
            NameOfSpeciality = NameOfSpeciality.Substring(1, ixSeparator1 - 2);
            
            ixSeparator = name.IndexOf(".xl");
            titleCur.Name = name.Substring(0, ixSeparator) + "_ex";

            Protocol = Convert.ToString(sheet.Range["N9"].Value);
            DurationOfStudy = Convert.ToString(sheet.Range["K3"].Value);
            
            // Вычисляем профиль специальности
            ixSeparator = -1;
            ixSeparator = Profile.IndexOfAny("0123456789".ToCharArray());

            if (ixSeparator >= 0)
            {
                titleCur.CodeSpeciality = Profile.Substring(ixSeparator, 8);
                titleCur.Profile = Profile.Substring(ixSeparator + 9);
            }
            else
                titleCur.CodeSpeciality = CodeOfSpeciality;
        
            titleCur.EducationForm = EducationForm;
            titleCur.Qualification = Qualific;
            titleCur.Chair = CodeChair;        
            titleCur.DataApproval = DateOfApproval;
            titleCur.DurationStudy = DurationOfStudy;
            titleCur.Faculty = NameFaculty;
            titleCur.Protocol = Protocol;
            titleCur.Speciality = NameOfSpeciality;

            // Выгрузка данных по дисциплинам

            sheet = workbook.Worksheets[2];
            sheet_dop = workbook.Worksheets[3];

            //Excel.Workbook WB; //в эту книгу будем копировать
            //Excel.Worksheet WS;// в этот лист
            //excelApp.SheetsInNewWorkbook = 5;
            //WB = excelApp.Workbooks.Add(Type.Missing);//создаем новую книгу

            //WS = WB.Worksheets[1];
            //sheet.get_Range("A1:BP1283").Copy(); // копи
            //WS.get_Range("A1:BP1283").PasteSpecial(); // паст
            //sheet = WS;
            //WB.Close(Type.Missing, Type.Missing, Type.Missing);

            #region Выгрузка дисциплин
            
            Disciplines = new List<Discipline>();

            // блок дисциплин Б1.Б
            for (int i = 6; i < 65; i++)
            {
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        FirstDisciplinesFullTime(i);
                    else
                        FirstDisciplinesExternal(i);
                }
                else
                    break;
            }

            // блок дисциплин Б1.В
            for (int i = 66; i < 100; i++)
            {
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        FirstDisciplinesFullTime(i);
                    else
                        FirstDisciplinesExternal(i);
                }
                else
                    break;
            }

            // блок дисциплин Б1.ДВ1
            for (int i = 103; i < 110; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 102;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б1.ДВ2
            for (int i = 113; i < 120; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 112;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б1.ДВ3
            for (int i = 123; i < 130; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 122;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б1.ДВ4
            for (int i = 133; i < 140; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 132;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б1.ДВ5
            for (int i = 143; i < 150; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 142;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б2 ч.1
            for (int i = 234; i < 293; i++)
            {
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        FirstDisciplinesFullTime(i);
                    else
                        FirstDisciplinesExternal(i);
                }
                else
                    break;
            }

            // блок дисциплин Б2 ч.2
            for (int i = 294; i < 328; i++)
            {
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        FirstDisciplinesFullTime(i);
                    else
                        FirstDisciplinesExternal(i);
                }
                else
                    break;
            }

            // блок дисциплин Б2.ДВ1
            for (int i = 331; i < 338; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 330;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б2.ДВ2
            for (int i = 341; i < 348; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 340;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б2.ДВ3
            for (int i = 351; i < 358; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 350;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б2.ДВ4
            for (int i = 361; i < 368; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 360;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б2.ДВ5
            for (int i = 371; i < 378; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 370;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б3 ч.1
            for (int i = 462; i < 521; i++)
            {
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        FirstDisciplinesFullTime(i);
                    else
                        FirstDisciplinesExternal(i);
                }
                else
                    break;
            }

            // блок дисциплин Б3 ч.2
            for (int i = 522; i < 556; i++)
            {
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        FirstDisciplinesFullTime(i);
                    else
                        FirstDisciplinesExternal(i);
                }
                else
                    break;
            }

            // блок дисциплин Б3.ДВ1
            for (int i = 559; i < 566; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 558;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б3.ДВ2
            for (int i = 569; i < 576; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 568;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б3.ДВ3
            for (int i = 579; i < 586; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 578;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б3.ДВ4
            for (int i = 589; i < 596; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 588;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б3.ДВ5
            for (int i = 599; i < 606; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 598;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б3.ДВ6
            for (int i = 609; i < 616; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 608;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б3.ДВ7
            for (int i = 619; i < 626; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 618;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б4 ч.1
            for (int i = 690; i < 749; i++)
            {
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        FirstDisciplinesFullTime(i);
                    else
                        FirstDisciplinesExternal(i);
                }
                else
                    break;
            }

            // блок дисциплин Б4 ч.2
            for (int i = 750; i < 784; i++)
            {
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        FirstDisciplinesFullTime(i);
                    else
                        FirstDisciplinesExternal(i);
                }
                else
                    break;
            }

            // блок дисциплин Б4.ДВ1
            for (int i = 787; i < 794; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 786;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б4.ДВ2
            for (int i = 797; i < 804; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 796;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б4.ДВ3
            for (int i = 807; i < 814; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 806;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б4.ДВ4
            for (int i = 817; i < 824; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 816;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б5 ч.1
            for (int i = 918; i < 977; i++)
            {
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        FirstDisciplinesFullTime(i);
                    else
                        FirstDisciplinesExternal(i);
                }
                else
                    break;
            }

            // блок дисциплин Б5 ч.2
            for (int i = 978; i < 1012; i++)
            {
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        FirstDisciplinesFullTime(i);
                    else
                        FirstDisciplinesExternal(i);
                }
                else
                    break;
            }

            // блок дисциплин Б5.ДВ1
            for (int i = 1015; i < 1022; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 1014;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б5.ДВ2
            for (int i = 1025; i < 1032; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 1024;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин Б5.ДВ3
            for (int i = 1035; i < 1042; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 1034;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }


            // блок дисциплин ФТД
            for (int i = 1146; i < 1166; i++)
            {
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        FirstDisciplinesFullTime(i);
                    else
                        FirstDisciplinesExternal(i);
                }
                else
                    break;
            }

            // блок дисциплин РЕЗ ч.1
            for (int i = 1169; i < 1228; i++)
            {
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        FirstDisciplinesFullTime(i);
                    else
                        FirstDisciplinesExternal(i);
                }
                else
                    break;
            }

            // блок дисциплин РЕЗ ч.2
            for (int i = 1229; i < 1263; i++)
            {
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        FirstDisciplinesFullTime(i);
                    else
                        FirstDisciplinesExternal(i);
                }
                else
                    break;
            }

            // блок дисциплин РЕЗ.ДВ1
            for (int i = 1266; i < 1273; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 1265;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // блок дисциплин РЕЗ.ДВ2
            for (int i = 1276; i < 1283; i++)
            {
                // Переменная для формирования кода дисциплины
                int Number = 1275;
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                {
                    if (EducationForm == "очная")
                        SecondDisciplinesFullTime(i, Number);
                    else
                        SecondDisciplinesExternal(i, Number);
                }
                else
                    break;
            }

            // Загрузка практик
            if (EducationForm == "очная")
                sheet = workbook.Worksheets[4];
            else
                sheet = workbook.Worksheets[5];

            for (int i = 3; i < 67; i++)
            {
                // Переменная для контроля значения ячейки Код дисциплины
                string control = Convert.ToString(sheet.Range["A" + i].Value);

                if (control != "" && control != null)
                    GetPractic(i);
                else if (i == 32)
                    i += 5;

            }

            titleCur.Disciplines = Disciplines;

            #endregion

            return titleCur;
        }

        private void FullTimeCurriculum()
        {
            Qualific = Convert.ToString(sheet.Range["Z11"].Value);

            // Выгрузка данных с листа "Нормы"
            sheet = workbook.Worksheets[5];
            CodeChair = Convert.ToString(sheet.Range["F45"].Value);
            NameFaculty = Convert.ToString(sheet.Range["F46"].Value);
            DateOfApproval = Convert.ToString(sheet.Range["F49"].Value);
            sheet = workbook.Worksheets[1];
        }

        private void ExternalCurriculum()
        {
            Qualific = Convert.ToString(sheet.Range["X11"].Value);
            EducationForm = "заочная";

            // Выгрузка данных с листа "Нормы"
            sheet = workbook.Worksheets[6];
            CodeChair = Convert.ToString(sheet.Range["J47"].Value);
            NameFaculty = Convert.ToString(sheet.Range["J48"].Value);
            DateOfApproval = Convert.ToString(sheet.Range["H51"].Value);
            sheet = workbook.Worksheets[1];
        }

        /// <summary>
        /// Метод получения основных дисциплин очного учебного плана
        /// </summary>
        private void FirstDisciplinesFullTime(int i)
        {
            CodeChair = Convert.ToString(sheet.Range["BP" + i].Value);
            // семестр № 1
            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            string controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["M" + i].Value);
            string controlPracticalExercisesPlan = Convert.ToString(sheet.Range["N" + i].Value);

            if ((controlLaboratoryWorkPlan != null || controlPracticalExercisesPlan != null) && CodeChair != null && CodeChair != "0")
            {
                DisciplinesImport = new Discipline();

                DisciplinesImport.CodeDiscipline = Convert.ToString(sheet.Range["A" + i].Value);
                Course = Convert.ToString(sheet.Range["L2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["L" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["M" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["N" + i].Value);
                DisciplinesImport.Semester = Convert.ToString(sheet.Range["L3"].Value);
                DisciplinesImport.SemesterYear = "осень";

                GetGeneralPropertiesOfDisciplineFullTime(i);

                Disciplines.Add(DisciplinesImport);

            }

            // семестр № 2

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["Q" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["R" + i].Value);

            if ((controlLaboratoryWorkPlan != null || controlPracticalExercisesPlan != null) && CodeChair != null && CodeChair != "0")
            {
                DisciplinesImport = new Discipline();

                DisciplinesImport.CodeDiscipline = Convert.ToString(sheet.Range["A" + i].Value);
                Course = Convert.ToString(sheet.Range["L2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["P" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["Q" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["R" + i].Value);
                DisciplinesImport.Semester = Convert.ToString(sheet.Range["P3"].Value);
                DisciplinesImport.SemesterYear = "весна";

                GetGeneralPropertiesOfDisciplineFullTime(i);

                Disciplines.Add(DisciplinesImport);

            }

            // семестр № 3

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["U" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["V" + i].Value);

            if ((controlLaboratoryWorkPlan != null || controlPracticalExercisesPlan != null) && CodeChair != null && CodeChair != "0")
            {
                DisciplinesImport = new Discipline();

                DisciplinesImport.CodeDiscipline = Convert.ToString(sheet.Range["A" + i].Value);
                Course = Convert.ToString(sheet.Range["T2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["T" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["U" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["V" + i].Value);
                DisciplinesImport.Semester = Convert.ToString(sheet.Range["T3"].Value);
                DisciplinesImport.SemesterYear = "осень";

                GetGeneralPropertiesOfDisciplineFullTime(i);

                Disciplines.Add(DisciplinesImport);

            }

            // семестр № 4

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["Y" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["Z" + i].Value);

            if ((controlLaboratoryWorkPlan != null || controlPracticalExercisesPlan != null) && CodeChair != null && CodeChair != "0")
            {
                DisciplinesImport = new Discipline();

                DisciplinesImport.CodeDiscipline = Convert.ToString(sheet.Range["A" + i].Value);
                Course = Convert.ToString(sheet.Range["T2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["X" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["Y" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["Z" + i].Value);
                DisciplinesImport.Semester = Convert.ToString(sheet.Range["X3"].Value);
                DisciplinesImport.SemesterYear = "весна";

                GetGeneralPropertiesOfDisciplineFullTime(i);

                Disciplines.Add(DisciplinesImport);

            }

            // семестр № 5

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["AC" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["AD" + i].Value);

            if ((controlLaboratoryWorkPlan != null || controlPracticalExercisesPlan != null) && CodeChair != null && CodeChair != "0")
            {
                DisciplinesImport = new Discipline();

                DisciplinesImport.CodeDiscipline = Convert.ToString(sheet.Range["A" + i].Value);
                Course = Convert.ToString(sheet.Range["AB2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AB" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["AC" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AD" + i].Value);
                DisciplinesImport.Semester = Convert.ToString(sheet.Range["AB3"].Value);
                DisciplinesImport.SemesterYear = "осень";

                GetGeneralPropertiesOfDisciplineFullTime(i);

                Disciplines.Add(DisciplinesImport);

            }

            // семестр № 6

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["AG" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["AH" + i].Value);

            if ((controlLaboratoryWorkPlan != null || controlPracticalExercisesPlan != null) && CodeChair != null && CodeChair != "0")
            {
                DisciplinesImport = new Discipline();

                DisciplinesImport.CodeDiscipline = Convert.ToString(sheet.Range["A" + i].Value);
                Course = Convert.ToString(sheet.Range["AB2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AF" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["AG" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AH" + i].Value);
                DisciplinesImport.Semester = Convert.ToString(sheet.Range["AF3"].Value);
                DisciplinesImport.SemesterYear = "весна";

                GetGeneralPropertiesOfDisciplineFullTime(i);

                Disciplines.Add(DisciplinesImport);

            }

            // семестр № 7

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["AK" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["AL" + i].Value);

            if ((controlLaboratoryWorkPlan != null || controlPracticalExercisesPlan != null) && CodeChair != null && CodeChair != "0")
            {
                DisciplinesImport = new Discipline();

                DisciplinesImport.CodeDiscipline = Convert.ToString(sheet.Range["A" + i].Value);
                Course = Convert.ToString(sheet.Range["AJ2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AJ" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["AK" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AL" + i].Value);
                DisciplinesImport.Semester = Convert.ToString(sheet.Range["AJ3"].Value);
                DisciplinesImport.SemesterYear = "осень";

                GetGeneralPropertiesOfDisciplineFullTime(i);

                Disciplines.Add(DisciplinesImport);

            }

            // семестр № 8

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["AO" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["AP" + i].Value);

            if ((controlLaboratoryWorkPlan != null || controlPracticalExercisesPlan != null) && CodeChair != null && CodeChair != "0")
            {
                DisciplinesImport = new Discipline();

                DisciplinesImport.CodeDiscipline = Convert.ToString(sheet.Range["A" + i].Value);
                Course = Convert.ToString(sheet.Range["AJ2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AN" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["AO" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AP" + i].Value);
                DisciplinesImport.Semester = Convert.ToString(sheet.Range["AN3"].Value);
                DisciplinesImport.SemesterYear = "весна";

                GetGeneralPropertiesOfDisciplineFullTime(i);

                Disciplines.Add(DisciplinesImport);

            }

            // семестр № 9

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["AS" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["AT" + i].Value);

            if ((controlLaboratoryWorkPlan != null || controlPracticalExercisesPlan != null) && CodeChair != null && CodeChair != "0")
            {
                DisciplinesImport = new Discipline();

                DisciplinesImport.CodeDiscipline = Convert.ToString(sheet.Range["A" + i].Value);
                Course = Convert.ToString(sheet.Range["AR2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AR" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["AS" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AT" + i].Value);
                DisciplinesImport.Semester = Convert.ToString(sheet.Range["AR3"].Value);
                DisciplinesImport.SemesterYear = "осень";

                GetGeneralPropertiesOfDisciplineFullTime(i);

                Disciplines.Add(DisciplinesImport);

            }

            // семестр № 10

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["AW" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["AX" + i].Value);

            if ((controlLaboratoryWorkPlan != null || controlPracticalExercisesPlan != null) && CodeChair != null && CodeChair != "0")
            {
                DisciplinesImport = new Discipline();

                DisciplinesImport.CodeDiscipline = Convert.ToString(sheet.Range["A" + i].Value);
                Course = Convert.ToString(sheet.Range["AR2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AV" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["AW" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AX" + i].Value);
                DisciplinesImport.Semester = Convert.ToString(sheet.Range["AV3"].Value);
                DisciplinesImport.SemesterYear = "весна";

                GetGeneralPropertiesOfDisciplineFullTime(i);

                Disciplines.Add(DisciplinesImport);

            }

        }

        /// <summary>
        /// Метод получения базовых свойств дисциплины очного учебного плана
        /// </summary>
        private void GetGeneralPropertiesOfDisciplineFullTime(int i)
        {
            DisciplinesImport.Discipl = Convert.ToString(sheet.Range["B" + i].Value);
            DisciplinesImport.CodeChair = Convert.ToString(sheet.Range["BP" + i].Value);

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

            string CourseProject = Convert.ToString(sheet.Range["E" + i].Value);
            if (CourseProject != null)
                DisciplinesImport.CourseProject = GetCourseProject(DisciplinesImport.Discipl, DisciplinesImport.Semester);
            DisciplinesImport.ControlWork = "0";

            string CourseWork = Convert.ToString(sheet.Range["F" + i].Value);
            if (CourseWork == DisciplinesImport.Semester)
                DisciplinesImport.CourseWork = "1";
            else
                DisciplinesImport.CourseWork = "0";
        }

        /// <summary>
        /// Метод получения доплнительных дисциплин очного учебного плана
        /// </summary>
        private void SecondDisciplinesFullTime(int i, int Number)
        {
            CodeChair = Convert.ToString(sheet.Range["BP" + i].Value);

            // семестр № 2

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            string controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["Q" + i].Value);
            string controlPracticalExercisesPlan = Convert.ToString(sheet.Range["R" + i].Value);

            if ((controlLaboratoryWorkPlan != null && controlLaboratoryWorkPlan != "0") || 
                (controlPracticalExercisesPlan != null && controlPracticalExercisesPlan != "0") && 
                (CodeChair != null && CodeChair != "0"))
            {
                DisciplinesImport = new Discipline();
                             
                CodeDiscipline = Convert.ToString(sheet.Range["B" + Number].Value);
                int index = CodeDiscipline.IndexOf(" ");
                CodeDiscipline = CodeDiscipline.Substring(0, index);
                CodeDiscipline = CodeDiscipline + "." + Convert.ToString(sheet.Range["A" + i].Value);
                DisciplinesImport.CodeDiscipline = CodeDiscipline;
                Course = Convert.ToString(sheet.Range["L2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["P" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["Q" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["R" + i].Value);
                DisciplinesImport.Semester = Convert.ToString(sheet.Range["P3"].Value);
                DisciplinesImport.SemesterYear = "весна";

                GetGeneralPropertiesOfDisciplineFullTime(i);

                DisciplinesImport.CourseProject = "0";
                DisciplinesImport.CourseWork = "0";
                DisciplinesImport.ControlWork = "0";

                Disciplines.Add(DisciplinesImport);

            }

            // семестр № 3

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["U" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["V" + i].Value);

            if ((controlLaboratoryWorkPlan != null && controlLaboratoryWorkPlan != "0") ||
                (controlPracticalExercisesPlan != null && controlPracticalExercisesPlan != "0") &&
                (CodeChair != null && CodeChair != "0"))
            {
                DisciplinesImport = new Discipline();
                                  
                CodeDiscipline = Convert.ToString(sheet.Range["B" + Number].Value);
                int index = CodeDiscipline.IndexOf(" ");
                CodeDiscipline = CodeDiscipline.Substring(0, index);
                CodeDiscipline = CodeDiscipline + "." + Convert.ToString(sheet.Range["A" + i].Value);
                DisciplinesImport.CodeDiscipline = CodeDiscipline;
                Course = Convert.ToString(sheet.Range["T2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["T" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["U" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["V" + i].Value);
                DisciplinesImport.Semester = Convert.ToString(sheet.Range["T3"].Value);
                DisciplinesImport.SemesterYear = "осень";

                GetGeneralPropertiesOfDisciplineFullTime(i);

                DisciplinesImport.CourseProject = "0";
                DisciplinesImport.CourseWork = "0";
                DisciplinesImport.ControlWork = "0";

                Disciplines.Add(DisciplinesImport);

            }

            // семестр № 4

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["Y" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["Z" + i].Value);

            if ((controlLaboratoryWorkPlan != null && controlLaboratoryWorkPlan != "0") ||
                (controlPracticalExercisesPlan != null && controlPracticalExercisesPlan != "0") &&
                (CodeChair != null && CodeChair != "0"))
            {
                DisciplinesImport = new Discipline();
                 
                CodeDiscipline = Convert.ToString(sheet.Range["B" + Number].Value);
                int index = CodeDiscipline.IndexOf(" ");
                CodeDiscipline = CodeDiscipline.Substring(0, index);
                CodeDiscipline = CodeDiscipline + "." + Convert.ToString(sheet.Range["A" + i].Value);
                DisciplinesImport.CodeDiscipline = CodeDiscipline;
                Course = Convert.ToString(sheet.Range["T2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["X" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["Y" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["Z" + i].Value);
                DisciplinesImport.Semester = Convert.ToString(sheet.Range["X3"].Value);
                DisciplinesImport.SemesterYear = "весна";

                GetGeneralPropertiesOfDisciplineFullTime(i);

                DisciplinesImport.CourseProject = "0";
                DisciplinesImport.CourseWork = "0";
                DisciplinesImport.ControlWork = "0";

                Disciplines.Add(DisciplinesImport);

            }

            // семестр № 5

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["AC" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["AD" + i].Value);

            if ((controlLaboratoryWorkPlan != null && controlLaboratoryWorkPlan != "0") ||
                (controlPracticalExercisesPlan != null && controlPracticalExercisesPlan != "0") &&
                (CodeChair != null && CodeChair != "0"))
            {
                DisciplinesImport = new Discipline();
                 
                CodeDiscipline = Convert.ToString(sheet.Range["B" + Number].Value);
                int index = CodeDiscipline.IndexOf(" ");
                CodeDiscipline = CodeDiscipline.Substring(0, index);
                CodeDiscipline = CodeDiscipline + "." + Convert.ToString(sheet.Range["A" + i].Value);
                DisciplinesImport.CodeDiscipline = CodeDiscipline;
                Course = Convert.ToString(sheet.Range["AB2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AB" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["AC" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AD" + i].Value);
                DisciplinesImport.Semester = Convert.ToString(sheet.Range["AB3"].Value);
                DisciplinesImport.SemesterYear = "осень";

                GetGeneralPropertiesOfDisciplineFullTime(i);

                DisciplinesImport.CourseProject = "0";
                DisciplinesImport.CourseWork = "0";
                DisciplinesImport.ControlWork = "0";

                Disciplines.Add(DisciplinesImport);

            }

            // семестр № 6

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["AG" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["AH" + i].Value);

            if ((controlLaboratoryWorkPlan != null && controlLaboratoryWorkPlan != "0") ||
                (controlPracticalExercisesPlan != null && controlPracticalExercisesPlan != "0") &&
                (CodeChair != null && CodeChair != "0"))
            {
                DisciplinesImport = new Discipline();
                 
                CodeChair = Convert.ToString(sheet.Range["BP" + i].Value);
                 
                CodeDiscipline = Convert.ToString(sheet.Range["B" + Number].Value);
                int index = CodeDiscipline.IndexOf(" ");
                CodeDiscipline = CodeDiscipline.Substring(0, index);
                CodeDiscipline = CodeDiscipline + "." + Convert.ToString(sheet.Range["A" + i].Value);
                DisciplinesImport.CodeDiscipline = CodeDiscipline;
                Course = Convert.ToString(sheet.Range["AB2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AF" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["AG" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AH" + i].Value);
                DisciplinesImport.Semester = Convert.ToString(sheet.Range["AF3"].Value);
                DisciplinesImport.SemesterYear = "весна";

                GetGeneralPropertiesOfDisciplineFullTime(i);

                DisciplinesImport.CourseProject = "0";
                DisciplinesImport.CourseWork = "0";
                DisciplinesImport.ControlWork = "0";

                Disciplines.Add(DisciplinesImport);

            }

            // семестр № 7

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["AK" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["AL" + i].Value);

            if ((controlLaboratoryWorkPlan != null && controlLaboratoryWorkPlan != "0") ||
                (controlPracticalExercisesPlan != null && controlPracticalExercisesPlan != "0") &&
                (CodeChair != null && CodeChair != "0"))
            {
                DisciplinesImport = new Discipline();
                 
                CodeDiscipline = Convert.ToString(sheet.Range["B" + Number].Value);
                int index = CodeDiscipline.IndexOf(" ");
                CodeDiscipline = CodeDiscipline.Substring(0, index);
                CodeDiscipline = CodeDiscipline + "." + Convert.ToString(sheet.Range["A" + i].Value);
                DisciplinesImport.CodeDiscipline = CodeDiscipline;
                Course = Convert.ToString(sheet.Range["AJ2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AJ" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["AK" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AL" + i].Value);
                DisciplinesImport.Semester = Convert.ToString(sheet.Range["AJ3"].Value);
                DisciplinesImport.SemesterYear = "осень";

                GetGeneralPropertiesOfDisciplineFullTime(i);

                DisciplinesImport.CourseProject = "0";
                DisciplinesImport.CourseWork = "0";
                DisciplinesImport.ControlWork = "0";

                Disciplines.Add(DisciplinesImport);

            }

            // семестр № 8

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["AO" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["AP" + i].Value);

            if ((controlLaboratoryWorkPlan != null && controlLaboratoryWorkPlan != "0") ||
                (controlPracticalExercisesPlan != null && controlPracticalExercisesPlan != "0") &&
                (CodeChair != null && CodeChair != "0"))
            {
                DisciplinesImport = new Discipline();
                 
                CodeDiscipline = Convert.ToString(sheet.Range["B" + Number].Value);
                int index = CodeDiscipline.IndexOf(" ");
                CodeDiscipline = CodeDiscipline.Substring(0, index);
                CodeDiscipline = CodeDiscipline + "." + Convert.ToString(sheet.Range["A" + i].Value);
                DisciplinesImport.CodeDiscipline = CodeDiscipline;
                Course = Convert.ToString(sheet.Range["AJ2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AN" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["AO" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AP" + i].Value);
                DisciplinesImport.Semester = Convert.ToString(sheet.Range["AN3"].Value);
                DisciplinesImport.SemesterYear = "весна";

                GetGeneralPropertiesOfDisciplineFullTime(i);

                DisciplinesImport.CourseProject = "0";
                DisciplinesImport.CourseWork = "0";
                DisciplinesImport.ControlWork = "0";

                Disciplines.Add(DisciplinesImport);

            }

            // семестр № 9

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["AS" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["AT" + i].Value);

            if ((controlLaboratoryWorkPlan != null && controlLaboratoryWorkPlan != "0") ||
                (controlPracticalExercisesPlan != null && controlPracticalExercisesPlan != "0") &&
                (CodeChair != null && CodeChair != "0"))
            {
                DisciplinesImport = new Discipline();

                CodeDiscipline = Convert.ToString(sheet.Range["B" + Number].Value);
                int index = CodeDiscipline.IndexOf(" ");
                CodeDiscipline = CodeDiscipline.Substring(0, index);
                CodeDiscipline = CodeDiscipline + "." + Convert.ToString(sheet.Range["A" + i].Value);
                DisciplinesImport.CodeDiscipline = CodeDiscipline;
                Course = Convert.ToString(sheet.Range["AR2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AR" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["AS" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AT" + i].Value);
                DisciplinesImport.Semester = Convert.ToString(sheet.Range["AR3"].Value);
                DisciplinesImport.SemesterYear = "осень";

                GetGeneralPropertiesOfDisciplineFullTime(i);

                DisciplinesImport.CourseProject = "0";
                DisciplinesImport.CourseWork = "0";
                DisciplinesImport.ControlWork = "0";

                Disciplines.Add(DisciplinesImport);

            }

        }
        
        /// <summary>
        /// Метод получения основных дисциплин заочного учебного плана
        /// </summary>
        private void FirstDisciplinesExternal(int i)
        {
            CodeChair = Convert.ToString(sheet.Range["CD" + i].Value);

            // курс № 1

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            string controlLecturePlan = Convert.ToString(sheet.Range["S" + i].Value);
            string controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["T" + i].Value);
            string controlPracticalExercisesPlan = Convert.ToString(sheet.Range["U" + i].Value);

            if ((controlLecturePlan != null && controlLecturePlan != "0") ||
                (controlLaboratoryWorkPlan != null && controlLaboratoryWorkPlan != "0") ||
                (controlPracticalExercisesPlan != null && controlPracticalExercisesPlan != "0") &&
                (CodeChair != null && CodeChair != "0"))
            {
                DisciplinesImport = new Discipline();

                DisciplinesImport.CodeDiscipline = Convert.ToString(sheet.Range["A" + i].Value);
                Course = Convert.ToString(sheet.Range["S2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["S" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["T" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["U" + i].Value);
                DisciplinesImport.ControlWork = Convert.ToString(sheet.Range["W" + i].Value);

                GetGeneralPropertiesOfDisciplineExternal(i);

                Disciplines.Add(DisciplinesImport);
            }

            // курс № 2

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLecturePlan = Convert.ToString(sheet.Range["AB" + i].Value);
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["AC" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["AD" + i].Value);

            if ((controlLecturePlan != null && controlLecturePlan != "0") ||
                (controlLaboratoryWorkPlan != null && controlLaboratoryWorkPlan != "0") ||
                (controlPracticalExercisesPlan != null && controlPracticalExercisesPlan != "0") &&
                (CodeChair != null && CodeChair != "0"))
            {
                DisciplinesImport = new Discipline();

                DisciplinesImport.CodeDiscipline = Convert.ToString(sheet.Range["A" + i].Value);
                Course = Convert.ToString(sheet.Range["AB2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AB" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["AC" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AD" + i].Value);
                DisciplinesImport.ControlWork = Convert.ToString(sheet.Range["AF" + i].Value);

                GetGeneralPropertiesOfDisciplineExternal(i);

                Disciplines.Add(DisciplinesImport);
            }

            // курс № 3

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLecturePlan = Convert.ToString(sheet.Range["AK" + i].Value);
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["AL" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["AM" + i].Value);

            if ((controlLecturePlan != null && controlLecturePlan != "0") ||
                (controlLaboratoryWorkPlan != null && controlLaboratoryWorkPlan != "0") ||
                (controlPracticalExercisesPlan != null && controlPracticalExercisesPlan != "0") &&
                (CodeChair != null && CodeChair != "0"))
            {
                DisciplinesImport = new Discipline();

                DisciplinesImport.CodeDiscipline = Convert.ToString(sheet.Range["A" + i].Value);
                Course = Convert.ToString(sheet.Range["AK2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AK" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["AL" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AM" + i].Value);
                DisciplinesImport.ControlWork = Convert.ToString(sheet.Range["AO" + i].Value);

                GetGeneralPropertiesOfDisciplineExternal(i);

                Disciplines.Add(DisciplinesImport);
            }

            // курс № 4

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLecturePlan = Convert.ToString(sheet.Range["AT" + i].Value);
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["AU" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["AV" + i].Value);

            if ((controlLecturePlan != null && controlLecturePlan != "0") ||
                (controlLaboratoryWorkPlan != null && controlLaboratoryWorkPlan != "0") ||
                (controlPracticalExercisesPlan != null && controlPracticalExercisesPlan != "0") &&
                (CodeChair != null && CodeChair != "0"))
            {
                DisciplinesImport = new Discipline();

                DisciplinesImport.CodeDiscipline = Convert.ToString(sheet.Range["A" + i].Value);
                Course = Convert.ToString(sheet.Range["AT2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AT" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["AU" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AV" + i].Value);
                DisciplinesImport.ControlWork = Convert.ToString(sheet.Range["AX" + i].Value);

                GetGeneralPropertiesOfDisciplineExternal(i);

                Disciplines.Add(DisciplinesImport);
            }

            // курс № 5

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLecturePlan = Convert.ToString(sheet.Range["BC" + i].Value);
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["BD" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["BE" + i].Value);

            if ((controlLecturePlan != null && controlLecturePlan != "0") ||
                (controlLaboratoryWorkPlan != null && controlLaboratoryWorkPlan != "0") ||
                (controlPracticalExercisesPlan != null && controlPracticalExercisesPlan != "0") &&
                (CodeChair != null && CodeChair != "0"))
            {
                DisciplinesImport = new Discipline();

                DisciplinesImport.CodeDiscipline = Convert.ToString(sheet.Range["A" + i].Value);
                Course = Convert.ToString(sheet.Range["BC2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["BC" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["BD" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["BE" + i].Value);
                DisciplinesImport.ControlWork = Convert.ToString(sheet.Range["BG" + i].Value);

                GetGeneralPropertiesOfDisciplineExternal(i);

                Disciplines.Add(DisciplinesImport);
            }

            // курс № 6

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLecturePlan = Convert.ToString(sheet.Range["BL" + i].Value);
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["BM" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["BN" + i].Value);

            if ((controlLecturePlan != null && controlLecturePlan != "0") ||
                (controlLaboratoryWorkPlan != null && controlLaboratoryWorkPlan != "0") ||
                (controlPracticalExercisesPlan != null && controlPracticalExercisesPlan != "0") &&
                (CodeChair != null && CodeChair != "0"))
            {
                DisciplinesImport = new Discipline();

                DisciplinesImport.CodeDiscipline = Convert.ToString(sheet.Range["A" + i].Value);
                Course = Convert.ToString(sheet.Range["BL2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["BL" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["BM" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["BN" + i].Value);
                DisciplinesImport.ControlWork = Convert.ToString(sheet.Range["BP" + i].Value);

                GetGeneralPropertiesOfDisciplineExternal(i);

                Disciplines.Add(DisciplinesImport);
            }
        }

        /// <summary>
        /// Метод получения доплнительных дисциплин заочного учебного плана
        /// </summary>
        private void SecondDisciplinesExternal(int i, int Number)
        {
            CodeChair = Convert.ToString(sheet.Range["CD" + i].Value);

            // курс № 1

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            string controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["T" + i].Value);
            string controlPracticalExercisesPlan = Convert.ToString(sheet.Range["U" + i].Value);

            if ((controlLaboratoryWorkPlan != null && controlLaboratoryWorkPlan != "0") ||
                (controlPracticalExercisesPlan != null && controlPracticalExercisesPlan != "0") &&
                (CodeChair != null && CodeChair != "0"))
            {
                DisciplinesImport = new Discipline();

                CodeDiscipline = Convert.ToString(sheet.Range["B" + Number].Value);
                int index = CodeDiscipline.IndexOf(" ");
                CodeDiscipline = CodeDiscipline.Substring(0, index);
                CodeDiscipline = CodeDiscipline + "." + Convert.ToString(sheet.Range["A" + i].Value);
                DisciplinesImport.CodeDiscipline = CodeDiscipline;
                Course = Convert.ToString(sheet.Range["S2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["S" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["T" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["U" + i].Value);
                DisciplinesImport.ControlWork = Convert.ToString(sheet.Range["W" + i].Value);

                GetGeneralPropertiesOfDisciplineExternal(i);

                Disciplines.Add(DisciplinesImport);
            }

            // курс № 2

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["AC" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["AD" + i].Value);

            if ((controlLaboratoryWorkPlan != null && controlLaboratoryWorkPlan != "0") ||
                (controlPracticalExercisesPlan != null && controlPracticalExercisesPlan != "0") &&
                (CodeChair != null && CodeChair != "0"))
            {
                DisciplinesImport = new Discipline();

                CodeDiscipline = Convert.ToString(sheet.Range["B" + Number].Value);
                int index = CodeDiscipline.IndexOf(" ");
                CodeDiscipline = CodeDiscipline.Substring(0, index);
                CodeDiscipline = CodeDiscipline + "." + Convert.ToString(sheet.Range["A" + i].Value);
                DisciplinesImport.CodeDiscipline = CodeDiscipline;
                Course = Convert.ToString(sheet.Range["AB2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AB" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["AC" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AD" + i].Value);
                DisciplinesImport.ControlWork = Convert.ToString(sheet.Range["AF" + i].Value);

                GetGeneralPropertiesOfDisciplineExternal(i);

                Disciplines.Add(DisciplinesImport);
            }

            // курс № 3

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["AL" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["AM" + i].Value);

            if ((controlLaboratoryWorkPlan != null && controlLaboratoryWorkPlan != "0") ||
                (controlPracticalExercisesPlan != null && controlPracticalExercisesPlan != "0") &&
                (CodeChair != null && CodeChair != "0"))
            {
                DisciplinesImport = new Discipline();

                CodeDiscipline = Convert.ToString(sheet.Range["B" + Number].Value);
                int index = CodeDiscipline.IndexOf(" ");
                CodeDiscipline = CodeDiscipline.Substring(0, index);
                CodeDiscipline = CodeDiscipline + "." + Convert.ToString(sheet.Range["A" + i].Value);
                DisciplinesImport.CodeDiscipline = CodeDiscipline;
                Course = Convert.ToString(sheet.Range["AK2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AK" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["AL" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AM" + i].Value);
                DisciplinesImport.ControlWork = Convert.ToString(sheet.Range["AO" + i].Value);

                GetGeneralPropertiesOfDisciplineExternal(i);

                Disciplines.Add(DisciplinesImport);
            }

            // курс № 4

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["AU" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["AV" + i].Value);

            if ((controlLaboratoryWorkPlan != null && controlLaboratoryWorkPlan != "0") ||
                (controlPracticalExercisesPlan != null && controlPracticalExercisesPlan != "0") &&
                (CodeChair != null && CodeChair != "0"))
            {
                DisciplinesImport = new Discipline();

                CodeDiscipline = Convert.ToString(sheet.Range["B" + Number].Value);
                int index = CodeDiscipline.IndexOf(" ");
                CodeDiscipline = CodeDiscipline.Substring(0, index);
                CodeDiscipline = CodeDiscipline + "." + Convert.ToString(sheet.Range["A" + i].Value);
                DisciplinesImport.CodeDiscipline = CodeDiscipline;
                Course = Convert.ToString(sheet.Range["AT2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["AT" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["AU" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["AV" + i].Value);
                DisciplinesImport.ControlWork = Convert.ToString(sheet.Range["AX" + i].Value);

                GetGeneralPropertiesOfDisciplineExternal(i);

                Disciplines.Add(DisciplinesImport);
            }

            // курс № 5

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["BD" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["BE" + i].Value);

            if ((controlLaboratoryWorkPlan != null && controlLaboratoryWorkPlan != "0") ||
                (controlPracticalExercisesPlan != null && controlPracticalExercisesPlan != "0") &&
                (CodeChair != null && CodeChair != "0"))
            {
                DisciplinesImport = new Discipline();

                CodeDiscipline = Convert.ToString(sheet.Range["B" + Number].Value);
                int index = CodeDiscipline.IndexOf(" ");
                CodeDiscipline = CodeDiscipline.Substring(0, index);
                CodeDiscipline = CodeDiscipline + "." + Convert.ToString(sheet.Range["A" + i].Value);
                DisciplinesImport.CodeDiscipline = CodeDiscipline;
                Course = Convert.ToString(sheet.Range["BC2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["BC" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["BD" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["BE" + i].Value);
                DisciplinesImport.ControlWork = Convert.ToString(sheet.Range["BG" + i].Value);

                GetGeneralPropertiesOfDisciplineExternal(i);

                Disciplines.Add(DisciplinesImport);
            }

            // курс № 6

            // Переменные для контроля значений ячеек лаб. раборы и практич. занятий дисциплины
            controlLaboratoryWorkPlan = Convert.ToString(sheet.Range["BM" + i].Value);
            controlPracticalExercisesPlan = Convert.ToString(sheet.Range["BN" + i].Value);

            if ((controlLaboratoryWorkPlan != null && controlLaboratoryWorkPlan != "0") ||
                (controlPracticalExercisesPlan != null && controlPracticalExercisesPlan != "0") &&
                (CodeChair != null && CodeChair != "0"))
            {
                DisciplinesImport = new Discipline();

                CodeDiscipline = Convert.ToString(sheet.Range["B" + Number].Value);
                int index = CodeDiscipline.IndexOf(" ");
                CodeDiscipline = CodeDiscipline.Substring(0, index);
                CodeDiscipline = CodeDiscipline + "." + Convert.ToString(sheet.Range["A" + i].Value);
                DisciplinesImport.CodeDiscipline = CodeDiscipline;
                Course = Convert.ToString(sheet.Range["BL2"].Value);
                DisciplinesImport.Course = Course.Substring(0, 1);
                DisciplinesImport.Lecture = Convert.ToString(sheet.Range["BL" + i].Value);
                DisciplinesImport.Lab = Convert.ToString(sheet.Range["BM" + i].Value);
                DisciplinesImport.PracticalExercise = Convert.ToString(sheet.Range["BN" + i].Value);
                DisciplinesImport.ControlWork = Convert.ToString(sheet.Range["BP" + i].Value);

                GetGeneralPropertiesOfDisciplineExternal(i);

                Disciplines.Add(DisciplinesImport);
            }
        }

        /// <summary>
        /// Метод получения базовых свойств дисциплины заочного учебного плана
        /// </summary>
        private void GetGeneralPropertiesOfDisciplineExternal(int i)
        {
            DisciplinesImport.Discipl = Convert.ToString(sheet.Range["B" + i].Value);
            DisciplinesImport.CodeChair = Convert.ToString(sheet.Range["CD" + i].Value);

            // Переменная для контроля значений ячеек по экзаменам и зачетам
            string controlExamAndSetOff = Convert.ToString(sheet.Range["E" + i].Value);
            if (controlExamAndSetOff != null && controlExamAndSetOff.Length > 1)
                DisciplinesImport.Exam = GetSemesterNumber(controlExamAndSetOff, DisciplinesImport.Course);
            else
            {
                if (controlExamAndSetOff == DisciplinesImport.Course)
                    DisciplinesImport.Exam = "1";
                else
                    DisciplinesImport.Exam = "0";
            }

            controlExamAndSetOff = Convert.ToString(sheet.Range["F" + i].Value);
            if (controlExamAndSetOff != null && controlExamAndSetOff.Length > 1)
                DisciplinesImport.SetOff = GetSemesterNumber(controlExamAndSetOff, DisciplinesImport.Course);
            else
            {
                if (controlExamAndSetOff == DisciplinesImport.Course)
                    DisciplinesImport.SetOff = "1";
                else
                    DisciplinesImport.SetOff = "0";
            }

            string CourseProject = Convert.ToString(sheet.Range["G" + i].Value);
            if (CourseProject == DisciplinesImport.Course)
                DisciplinesImport.CourseWork = "1";
            else
                DisciplinesImport.CourseProject = "0";

            string CourseWork = Convert.ToString(sheet.Range["H" + i].Value);
            if (CourseWork == DisciplinesImport.Course)
                DisciplinesImport.CourseWork = "1";
            else
                DisciplinesImport.CourseWork = "0";
        }

        public void GetPractic(int i)
        {
            DisciplinesImport = new Discipline();

            if (EducationForm == "очная")
            {
                DisciplinesImport.Semester = Convert.ToString(sheet.Range["B" + i].Value);
                int semester = Convert.ToInt32(DisciplinesImport.Semester);
                if (semester % 2 == 0)
                    DisciplinesImport.Course = Convert.ToString(semester / 2);
                else
                    DisciplinesImport.Course = Convert.ToString((semester / 2) + 0.5);

                DisciplinesImport.SemesterYear = "весна";
                DisciplinesImport.SetOff = "1";
            }
            else
            {
                DisciplinesImport.Course = Convert.ToString(sheet.Range["B" + i].Value);
                DisciplinesImport.SetOff = "1";
            }
            DisciplinesImport.Pract = Convert.ToString(sheet.Range["E" + i].Value);
            DisciplinesImport.CodeDiscipline = "ПР";
            DisciplinesImport.Discipl = Convert.ToString(sheet.Range["A" + i].Value) + " практика";
            DisciplinesImport.Lecture = "0";
            DisciplinesImport.Lab = "0";
            DisciplinesImport.PracticalExercise = "0";
            DisciplinesImport.ControlWork = "0";
            DisciplinesImport.CourseProject = "0";
            DisciplinesImport.CourseWork = "0";
            DisciplinesImport.Exam = "0";
            DisciplinesImport.CodeChair = Convert.ToString(sheet.Range["F" + i].Value);

            Disciplines.Add(DisciplinesImport);
        }

        /// <summary>
        /// Метод синтаксического разбора
        /// </summary>
        private string GetSemesterNumber(string semesterExamAndSetOff, string semester)
        {
            string[] n;
            n = new string[semesterExamAndSetOff.Length];
            for (int i = 0; i < semesterExamAndSetOff.Length; i++)
            {
                n[i] = semesterExamAndSetOff.Substring(i, 1);
                if (n[i] == semester)
                {
                    semesterExamAndSetOff = "1";
                    break;
                }
            }
            if (semesterExamAndSetOff.Length > 1)
                semesterExamAndSetOff = "0";
            return semesterExamAndSetOff;
        }

        /// <summary>
        /// Метод получения номера семестра курсового проекта
        /// для дисциплины учебного плана
        /// </summary>
        private string GetCourseProject(string Discipline, string semester)
        {
            string CourseProject = null;
            string ImportDiscipline;
            for (int i = 5; i < 65; i++)
            {
                ImportDiscipline = Convert.ToString(sheet_dop.Range["N" + i].Value);
                if (Discipline == ImportDiscipline)
                {
                    CourseProject = "1";
                    break;
                }
                else
                    CourseProject = "0";
                i++;
            }

            return CourseProject;
        }
    }
}
