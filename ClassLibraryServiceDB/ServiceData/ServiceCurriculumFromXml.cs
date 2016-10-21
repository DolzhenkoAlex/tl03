using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ClassLibraryServiceDB.Model;

namespace ClassLibraryServiceDB.ServiceData
{
    /// <summary>
    /// Сервис формирования данных по учебным планам из XML-файлов
    /// </summary>
    public class ServiceCurriculumFromXml: IServiceCurriculumFromXml
    {
        /// <summary>
        /// Загрузка XML-данных по титулу учебного плана
        /// </summary>
        /// <returns></returns>
        public XDocument GetXmlTitlt(string nameFile)
        {
            XDocument titleDoc = XDocument.Load(nameFile);
            return titleDoc;
        }

        /// <summary>
        /// Код выпускающей кафедры
        /// </summary>
        private string titleCurChair;

        /// <summary>
        /// Формирование данных по титулу учебного плана
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public TitleCurriculum GetTitleCurriculum(XDocument title)
        {
            // Титул учебного плана - временная переменная
            TitleCurriculum titleCur = new TitleCurriculum();

            // Формирование имени учебного плана
            string name = (from t in title.Descendants("Документ")
                             select t.Attribute("PrevName").Value).FirstOrDefault();
            int index = name.IndexOf(".xml");
            titleCur.Name = name.Substring(0, index);

            // Формирование шифра учебного плана (PLM/PLI/PLZ)
            string code = (from t in title.Descendants("План")
                           select t.Attribute("Шифр").Value).FirstOrDefault();
            titleCur.Code = code.Substring(1,3);
            
            // Формироание формы обучения
            titleCur.EducationForm = (from t in title.Descendants("План")
                                      select t.Attribute("ФормаОбучения").Value).FirstOrDefault();
            
            // Формирование выпускающей кафедры
            titleCur.Chair = (from t in title.Descendants("Титул")
                              select t.Attribute("КодКафедры").Value).FirstOrDefault();
            titleCurChair = titleCur.Chair;

            // Формирование факультета
            titleCur.Faculty = (from t in title.Descendants("Титул")
                                select t.Attribute("Факультет").Value).FirstOrDefault();

            // Формирование даты утверждения учебного плана
            try
            {
                titleCur.DataApproval = (from t in title.Descendants("Утверждение")
                                   select t.Attribute("Дата").Value).FirstOrDefault();
            }
            catch (Exception)
            {
                titleCur.DataApproval = "";
            }


            // Формирование номера протокола утверждения учебного плана
            try
            {
                titleCur.Protocol = (from t in title.Descendants("Утверждение")
                               select t.Attribute("НомПротокола").Value).FirstOrDefault();
            }
            catch (Exception)
            {
                titleCur.Protocol = "";
            }
            

            // Формирование квалификации
            titleCur.Qualification = (from t in title.Descendants("Квалификация")
                                      select t.Attribute("Название").Value).FirstOrDefault();

            // Формирование срока обучения
            titleCur.DurationStudy = (from t in title.Descendants("Квалификация")
                                      select t.Attribute("СрокОбучения").Value).FirstOrDefault();


            // Формирование направления подготовки: код/наименование/профиль 
            List<Speciality> listSpec = new List<Speciality>();
            var titleSpec = from t in title.Descendants("Специальность")
                            select new Speciality(
                                (string)t.Attribute("Ном"),
                                (string)t.Attribute("Название"));

            foreach (var item in titleSpec)
                listSpec.Add(item);

            // Проверка наличия строки "ЗАОЧНАЯ ФОРМА ОБУЧЕНИЯ"
            Speciality special = listSpec.Find(x => (x.FullName).Contains("ЗАОЧНАЯ"));
            if (special != null)
                listSpec.Remove(special);

            // Проверка наличия строки "на базе среднего/высшего профессионального образования"
            special = listSpec.Find(x => (x.FullName).Contains("на базе"));
            if (special != null)
                listSpec.Remove(special);

            // Формирование кода, наименование направления и профиля
            foreach (Speciality item in listSpec)
            {
                if (item.FullName.Contains("направление"))
                {
                    string spec = item.FullName;
                    index = spec.IndexOfAny("0123456789".ToCharArray());
                    if (index > 0)
                        spec = spec.Substring(index, spec.Length - index);
                    index = spec.LastIndexOfAny("0123456789".ToCharArray());
                    titleCur.CodeSpeciality = spec.Substring(0, index + 1);
                    titleCur.Speciality = spec.Substring(index + 3, spec.Length - index - 4);
                }
                if (item.FullName.Contains("профиль"))
                {
                    string spec = item.FullName;
                    index = spec.IndexOfAny("0123456789".ToCharArray());
                    if (index > 0)
                        spec = spec.Substring(index, spec.Length - index);
                    index = spec.LastIndexOfAny("0123456789".ToCharArray());
                    titleCur.CodeSpeciality = spec.Substring(0, index + 1);
                    titleCur.Profile = spec.Substring(index + 3, spec.Length - index - 4);
                }
            }
               
            return titleCur;
        }

        /// <summary>
        /// Загрузка данных по дисциплинам учебного плана для дневного обучения
        /// </summary>
        /// <returns></returns>
        public List<Discipline> GetListDiscipline(XDocument title)
        {
            List<Discipline> ListDiscipline = new List<Discipline>();

            var quaeryDiscipline = from disc in title.Descendants("Строка")
                                   from d in disc.Descendants("Сем")
                                   select new Discipline(
                                       (string)disc.Attribute("Дис"), 
                                       (string)disc.Attribute("Кафедра"),
                                       (string)d.Attribute("Ном"),
                                       (string)d.Attribute("Нед"),
                                       (string)d.Attribute("Зач"),
                                       (string)disc.Attribute("НовИдДисциплины"),
                                       (string)d.Attribute("Лек"),
                                       (string)d.Attribute("Лаб"),
                                       (string)d.Attribute("Пр"),
                                       (string)d.Attribute("КР"),
                                       (string)d.Attribute("Экз"),
                                       (string)d.Attribute("КП"),
                                       (string)d.Attribute("КонтрРаб"));

            foreach (Discipline disc in quaeryDiscipline)
            {
                if (disc.Discipl == "Научно-исследовательская работа")
                {
                    disc.CodeChair = titleCurChair;
                    disc.CodeDiscipline = "М3.Б.1";
                    disc.ScientificResearchWork = 1;
                    disc.PracticalExercise = "0";
                }

                ListDiscipline.Add(disc);
            }
            return ListDiscipline;
        }

        /// <summary>
        /// Формирование  данных по дисциплинам учебного плана для заочного обучения
        /// </summary>
        /// <returns></returns>
        public List<Discipline> GetListDisciplineDistanceLearning(XDocument title)
        {
            List<Discipline> ListDiscipline = new List<Discipline>();

            var quaeryDiscipline = from disc in title.Descendants("Строка")
                                   from d in disc.Descendants("Курс")
                                   select new Discipline
                                   {
                                       Discipl = (string)disc.Attribute("Дис"),
                                       CodeDiscipline = (string)disc.Attribute("НовИдДисциплины"),
                                       CodeChair = (string)disc.Attribute("Кафедра"),
                                       Course = (string)d.Attribute("Ном"),
                                       //Pract = (string)d.Attribute("Нед"),
                                       Exam = (string)d.Attribute("Экз"),
                                       SetOff = (string)d.Attribute("Зач"),
                                       Lecture = (string)d.Attribute("Лек"),
                                       Lab = (string)d.Attribute("Лаб"),
                                       PracticalExercise = (string)d.Attribute("Пр"),
                                       CourseWork = (string)d.Attribute("КР"),
                                       CourseProject = (string)d.Attribute("КП"),
                                       ControlWork = (string)d.Attribute("КонтрРаб")};

            foreach (Discipline disc in quaeryDiscipline)
                ListDiscipline.Add(disc);

            return ListDiscipline;
        }


        /// <summary>
        /// Загрузка данных по практикам учебного плана
        /// </summary>
        /// <param name="qual"></param>
        /// <returns></returns>
        public List<Discipline> GetListPractic(XDocument title)
        {
            List<Discipline> ListDiscipline = new List<Discipline>();

            var quaeryDiscipline = from pract in title.Descendants("СпецВидыРаботНов")
                                   from disc in pract.Descendants("ПрочаяПрактика")
                                   from sem in disc.Descendants("Семестр")
                                   from chair in sem.Descendants("Кафедра")
                                   select new Discipline
                                   {
                                       Discipl = (string)disc.Attribute("Наименование"),
                                       CodeChair = (string)chair.Attribute("Код"),
                                       Semester = (string)sem.Attribute("Ном"),
                                       Pract = (string)chair.Attribute("Нед"),
                                       SetOffWithBall = (string)sem.Attribute("ЗачО")
                                   };
                                       
                                       //(
                                       //(string)disc.Attribute("Наименование"),
                                       //
                                       //,
                                       //
                                       //);

            foreach (Discipline disc in quaeryDiscipline)
                ListDiscipline.Add(disc);

            return ListDiscipline;
        }

        /// <summary>
        /// Загрузка данных по учебным практикам заочной формы обучения из учебного плана
        /// </summary>
        /// <param name="qual"></param>
        /// <returns></returns>
        public List<Discipline> GetListPracticTrainingDistanceLearning(XDocument title)
        {
            List<Discipline> ListDiscipline = new List<Discipline>();

            //var quaeryDiscipline = from pract in title.Descendants("СпецВидыРабот")
            //                       from typePractic in pract.Descendants("УчебПрактики")
            //                       from disc in typePractic.Descendants("ПрочаяПрактика")
            //                       select new Discipline {
            //                           Discipl = (string)disc.Attribute("Вид"),
            //                           CodeChair = (string)disc.Attribute("Кафедра"),
            //                           Course = (string)disc.Attribute("Курс"),
            //                           Pract = (string)disc.Attribute("Нед")};

            var quaeryDiscipline = from pract in title.Descendants("СпецВидыРаботНов")
                                   from typePractic in pract.Descendants("УчебПрактики")
                                   from disc in typePractic.Descendants("ПрочаяПрактика")
                                   from sem in disc.Descendants("Семестр")
                                   from chair in sem.Descendants("Кафедра")
                                   select new Discipline
                                   {
                                       Discipl = (string)disc.Attribute("Наименование"),
                                       CodeChair = (string)chair.Attribute("Код"),
                                       Course = (string)sem.Attribute("Ном"),
                                       Pract = (string)chair.Attribute("Нед"),
                                       SetOff = (string)sem.Attribute("Зач"),
                                       SetOffWithBall = (string)sem.Attribute("ЗачО")
                                   };

            if (quaeryDiscipline.Count() > 0)
            {
                foreach (Discipline disc in quaeryDiscipline)
                    ListDiscipline.Add(disc);
            }

            return ListDiscipline;
        }

        /// <summary>
        /// Загрузка данных по производственным практикам заочной формы обучения из учебного плана
        /// </summary>
        /// <param name="qual"></param>
        /// <returns></returns>
        public List<Discipline> GetListPracticProductionDistanceLearning(XDocument title)
        {
            List<Discipline> ListDiscipline = new List<Discipline>();

            //var quaeryDiscipline = from pract in title.Descendants("СпецВидыРабот")
            //                       from typePractic in pract.Descendants("ПрочиеПрактики")
            //                       from disc in typePractic.Descendants("ПрочаяПрактика")
            //                       select new Discipline {
            //                           Discipl = (string)disc.Attribute("Вид"),
            //                           CodeChair = (string)disc.Attribute("Кафедра"),
            //                           Course = (string)disc.Attribute("Курс"),
            //                           Pract = (string)disc.Attribute("Нед")};

            var quaeryDiscipline = from pract in title.Descendants("СпецВидыРаботНов")
                                   from typePractic in pract.Descendants("ПрочиеПрактики")
                                   from disc in typePractic.Descendants("ПрочаяПрактика")
                                   from sem in disc.Descendants("Семестр")
                                   from chair in sem.Descendants("Кафедра")
                                   select new Discipline
                                   {
                                       Discipl = (string)disc.Attribute("Наименование"),
                                       CodeChair = (string)chair.Attribute("Код"),
                                       Course = (string)sem.Attribute("Ном"),
                                       Pract = (string)chair.Attribute("Нед"),
                                       SetOff = (string)sem.Attribute("Зач"),
                                       SetOffWithBall = (string)sem.Attribute("ЗачО")
                                   };

            if (quaeryDiscipline.Count() > 0)
            {
                foreach (Discipline disc in quaeryDiscipline)
                    ListDiscipline.Add(disc);
            }

            return ListDiscipline;
        }


        /// <summary>
        /// Загрузка данных по ГАК учебного плана для бакалавров/магистров
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public List<Discipline> GetListGak(XDocument title)
        {
            List<Discipline> ListDiscipline = new List<Discipline>();

            var quaeryDiscipline = from pract in title.Descendants("СпецВидыРаботНов")
                                   from bkp in pract.Descendants("ВКР")
                                   from gak in bkp.Descendants("ГАК")
                                   from member in gak.Descendants("ЧленГАК")
                                   select new Discipline(
                                       (string)member.Attribute("Каф"),
                                       (string)member.Attribute("Часов"),
                                       (string)gak.Attribute("Час"));

            if (quaeryDiscipline != null)
            {
                int number = 1;
                foreach (Discipline disc in quaeryDiscipline)
                {
                    disc.Discipl = "Член ГАК " + number.ToString();
                    ListDiscipline.Add(disc);
                    number++; 
                }
            }

            return ListDiscipline;
        }

        /// <summary>
        /// Загрузка данных по ГАК учебного плана для специалистов
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public List<Discipline> GetListGakSpecialist(XDocument title)
        {
            List<Discipline> ListDiscipline = new List<Discipline>();

            var quaeryDiscipline = from pract in title.Descendants("СпецВидыРаботНов")
                                   from bkp in pract.Descendants("Диплом")
                                   from gak in bkp.Descendants("ГАК")
                                   from member in gak.Descendants("ЧленГАК")
                                   select new Discipline(
                                       (string)member.Attribute("Каф"),
                                       (string)member.Attribute("Часов"),
                                       (string)gak.Attribute("Час"));

            if (quaeryDiscipline != null)
            {
                int number = 1;
                foreach (Discipline disc in quaeryDiscipline)
                {
                    disc.Discipl = "Член ГАК " + number.ToString();
                    ListDiscipline.Add(disc);
                    number++;
                }
            }

            return ListDiscipline;
        }

        /// <summary>
        /// Загрузки данных по ГЭК учебного плана для бакалавров/магистров
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public List<Discipline> GetListGek(XDocument title)
        {
            List<Discipline> ListDiscipline = new List<Discipline>();

            var quaeryDiscipline = from pract in title.Descendants("СпецВидыРаботНов")
                                   from gek in pract.Descendants("ИтоговыйЭкзамен")
                                   from member in gek.Descendants("ЧленГЭК")
                                   select new Discipline(
                                       (string)member.Attribute("Каф"),
                                       (string)member.Attribute("Часов"),
                                       (string)gek.Attribute("Час"));
            if (quaeryDiscipline != null)
            {
                int number = 1;
                foreach (Discipline disc in quaeryDiscipline)
                {
                    disc.Discipl = "Член ГЭК " + number.ToString(); ;
                    ListDiscipline.Add(disc);
                    number++;
                }
            }

            return ListDiscipline;
        }

        /// <summary>
        /// Загрузки данных по ГЭК учебного плана для для специалистов
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public List<Discipline> GetListGekSpecialist(XDocument title)
        {
            List<Discipline> ListDiscipline = new List<Discipline>();

            var quaeryDiscipline = from pract in title.Descendants("СпецВидыРаботНов")
                                   from gek in pract.Descendants("ИтоговыйЭкзамен")
                                   from member in gek.Descendants("ЧленГЭК")
                                   select new Discipline(
                                       (string)member.Attribute("Каф"),
                                       (string)member.Attribute("Часов"),
                                       (string)gek.Attribute("Час"));
            if (quaeryDiscipline != null)
            {
                int number = 1;
                foreach (Discipline disc in quaeryDiscipline)
                {
                    disc.Discipl = "Член ГЭК " + number.ToString(); ;
                    ListDiscipline.Add(disc);
                    number++;
                }
            }

            return ListDiscipline;
        }

        /// <summary>
        /// Загрузка данных по руководству ВКР для бакалавров/магистров
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public Discipline GetGraduationDesign(XDocument title)
        {
            var discipline = (from pract in title.Descendants("СпецВидыРаботНов")
                              from bkp in pract.Descendants("ВКР")
                                   from gak in bkp.Descendants("Руководство")
                                   from member in gak.Descendants("РуководствоК")
                                   select new Discipline(
                                       (string)member.Attribute("Каф"),
                                       (string)gak.Attribute("Часов"),
                                       (string)member.Attribute("Часов"))).FirstOrDefault();
            if(discipline != null)
            {
                discipline.Discipl = "Руководство ВКР";
            }

            return discipline;
        }

        /// <summary>
        /// Загрузка данных по руководству дипломным проектированием для специалистов
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public Discipline GetGraduationDesignSpecialist(XDocument title)
        {
            var discipline = (from pract in title.Descendants("СпецВидыРаботНов")
                              from bkp in pract.Descendants("Диплом")
                              from gak in bkp.Descendants("Руководство")
                              from member in gak.Descendants("РуководствоК")
                              select new Discipline(
                                  (string)member.Attribute("Каф"),
                                  (string)gak.Attribute("Часов"),
                                  (string)member.Attribute("Часов"))).FirstOrDefault();
            if (discipline != null)
            {
                discipline.Discipl = "Руководство ВКР";
            }

            return discipline;
        }
    }
}
