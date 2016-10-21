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
    /// Интерфейс получения данных из XML-файла
    /// </summary>
    public interface IServiceCurriculumFromXml
    {
        /// <summary>
        /// Итнерфейс загрузки XML-документа по титулу учебного плана
        /// </summary>
        /// <returns></returns>
        XDocument GetXmlTitlt(string nameFile);

        /// <summary>
        /// Интерфейс формирования данных по титулу учебного плана
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        TitleCurriculum GetTitleCurriculum(XDocument title);

        /// <summary>
        /// Интерфейс формирования  данных по дисциплинам учебного плана для очного обучения
        /// </summary>
        /// <returns></returns>
        List<Discipline> GetListDiscipline(XDocument title);

        /// <summary>
        /// Интерфейс формирования  данных по дисциплинам учебного плана для заочного обучения
        /// </summary>
        /// <returns></returns>
        List<Discipline> GetListDisciplineDistanceLearning(XDocument title);

        /// <summary>
        /// Интерфейс формирования данных по практикам учебного плана
        /// </summary>
        /// <param name="qual"></param>
        /// <returns></returns>
        List<Discipline> GetListPractic(XDocument title);

        /// <summary>
        /// Интерфейс загрузки данных по учебным практикам заочной формы обучения из учебного плана
        /// </summary>
        /// <param name="qual"></param>
        /// <returns></returns>
        List<Discipline> GetListPracticTrainingDistanceLearning(XDocument title);

        /// <summary>
        /// Интерфейс загрузки данных по производственным практикам заочной формы обучения из учебного плана
        /// </summary>
        /// <param name="qual"></param>
        /// <returns></returns>
        List<Discipline> GetListPracticProductionDistanceLearning(XDocument title);

        /// <summary>
        /// Интерфейв загрузки данных по ГАК учебного плана
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        List<Discipline> GetListGak(XDocument title);

        /// <summary>
        /// Интерфейс загрузки данных по ГАК учебного плана для специалистов
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        List<Discipline> GetListGakSpecialist(XDocument title);

        /// <summary>
        /// Интерфейс загрузки данных по ГЭК учебного плана
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        List<Discipline> GetListGek(XDocument title);

        /// <summary>
        /// Интерфейс загрузки данных по ГЭК учебного плана для для специалистов
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        List<Discipline> GetListGekSpecialist(XDocument title);

        /// <summary>
        /// Интерфейс загрузки данных по руководству ВКР учебного плана
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        Discipline GetGraduationDesign(XDocument title);

        /// <summary>
        /// Интерфейс загрузки данных по руководству дипломным проектированиет
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        Discipline GetGraduationDesignSpecialist(XDocument title);
    }
}
