using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight2.Helper
{
    /// <summary>
    /// Трудоемкость учебного плана
    /// </summary>
    public class SummaryOfCurriculumWork
    {

        /// <summary>
        /// Id учебного плана
        /// </summary>
        public int IdCurriculum { get; set; }

        /// <summary>
        /// Имя учебного плана
        /// </summary>
        public string CurriculumName { get; set; }

        /// <summary>
        /// Код направления подготовки 
        /// </summary>
        public string CodeSpeciality { get; set; }

        /// <summary>
        /// Направление подготовки
        /// </summary>
        public string NameSpeciality { get; set; }

        /// <summary>
        /// Профиль
        /// </summary>
        public string Profile { get; set; }

        /// <summary>
        /// Факультет
        /// </summary>
        public string Faculty { get; set; }

        /// <summary>
        /// Форма обучения
        /// </summary>
        public string FormEducation { get; set; }

        /// <summary>
        /// Квалификация
        /// </summary>
        public string NameQualification { get; set; }

        /// <summary>
        /// Курс
        /// </summary>
        public int Course { get; set; }

        /// <summary>
        /// Количество студентов
        /// </summary>
        public int CountStudent { get; set; }

        /// <summary>
        /// Количество экзаменов
        /// </summary>
        public int CountExamination { get; set; }

        /// <summary>
        /// Количество зачетов
        /// </summary>
        public int CountSetOff { get; set; }

        /// <summary>
        /// Количество зачетов с оценкой
        /// </summary>
        public int CountSetOffWithBall { get; set; }

        /// <summary>
        /// Количество курсовых работ
        /// </summary>
        public int CountCourseWork { get; set; }

        /// <summary>
        /// Количество курсовых проектов
        /// </summary>
        public int CountCourseProject { get; set; }

        /// <summary>
        /// Количество контрольных работ
        /// </summary>
        public int CountControlWork { get; set; }

        /// <summary>
        /// Наличие ГАК
        /// </summary>
        public int CountGak { get; set; }

        /// <summary>
        /// Количество недель практики
        /// </summary>
        public int CountPractical { get; set; }

        /// <summary>
        /// Наличие НИР
        /// </summary>
        public int CountScientificResearchWork { get; set; }

        /// <summary>
        /// Общее количество часов лекция
        /// </summary>
        public int TotalNumberOfLectures { get; set; }

        /// <summary>
        /// Общее количество часов лабораторных работ
        /// </summary>
        public int TotalNumberOfLaboratoryWork { get; set; }

        /// <summary>
        /// Общее количество часов практических занятий
        /// </summary>
        public int TotalNumberOfPracticalExercises { get; set; }

        /// <summary>
        /// Создание экземпляра класса Трудоемкость учебного плана
        /// </summary>
        public SummaryOfCurriculumWork() { }

    }
}
