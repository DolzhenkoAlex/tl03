using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryServiceDB.Model
{
    /// <summary>
    /// Дисциплина учебного плана
    /// </summary>
    public class Discipline
    {
        string discipline;

        /// <summary>
        /// Наименование дисциплины
        /// </summary>
        public string Discipl
        {
            get { return discipline; }
            set { discipline = value; }
        }

        string codeDiscipline;

        /// <summary>
        /// Код дисциплины
        /// </summary>
        public string CodeDiscipline
        {
            get { return codeDiscipline; }
            set { codeDiscipline = value; }
        }

        string course;

        /// <summary>
        /// Курс
        /// </summary>
        public string Course
        {
            get { return course; }
            set { course = value; }
        }

        string semester;

        /// <summary>
        /// Семестр
        /// </summary>
        public string Semester
        {
            get { return semester; }
            set { semester = value; }
        }

        string semesterYear;

        /// <summary>
        /// Семестр-осень/весна
        /// </summary>
        public string SemesterYear
        {
            get { return semesterYear; }
            set { semesterYear = value; }
        }

        string lecture;

        /// <summary>
        /// Лекции
        /// </summary>
        public string Lecture
        {
            get { return lecture; }
            set { lecture = value; }
        }

        string lab;

        /// <summary>
        /// Лабораторные работы
        /// </summary>
        public string Lab
        {
            get { return lab; }
            set { lab = value; }
        }

        string pract;

        /// <summary>
        /// Учебная/производственная практика
        /// </summary>
        public string Pract
        {
            get { return pract; }
            set { pract = value; }
        }

        string practicalExercise;

        /// <summary>
        /// Практические занятия
        /// </summary>
        public string PracticalExercise
        {
            get { return practicalExercise; }
            set { practicalExercise = value; }
        }

        string codeChair;

        /// <summary>
        /// Код кафедры
        /// </summary>
        public string CodeChair
        {
            get { return codeChair; }
            set { codeChair = value; }
        }

        string exam;

        /// <summary>
        /// Экзамен
        /// </summary>
        public string Exam
        {
            get { return exam; }
            set { exam = value; }
        }

        string setOff;

        /// <summary>
        /// Зачет
        /// </summary>
        public string SetOff
        {
            get { return setOff; }
            set { setOff = value; }
        }

        string setOffWithBall;

        /// <summary>
        /// Зачет с оценкой
        /// </summary>
        public string SetOffWithBall
        {
            get { return setOffWithBall; }
            set { setOffWithBall = value; }
        }

        string courseProject;

        /// <summary>
        /// Курсовой проект
        /// </summary>
        public string CourseProject
        {
            get { return courseProject; }
            set { courseProject = value; }
        }

        string courseWork;

        /// <summary>
        /// Курсовая работа
        /// </summary>
        public string CourseWork
        {
            get { return courseWork; }
            set { courseWork = value; }
        }

        string controlWork;

        /// <summary>
        /// Контрольная работа
        /// </summary>
        public string ControlWork
        {
            get { return controlWork; }
            set { controlWork = value; }
        }

        private string gak;

        /// <summary>
        /// ГАК/ГЭК
        /// </summary>
        public string Gak
        {
            get { return gak; }
            set { gak = value; }
        }

        private string graduationDesign;

        /// <summary>
        /// Дипломное проектирование
        /// </summary>
        public string GraduationDesign
        {
            get { return graduationDesign; }
            set { graduationDesign = value; }
        }

        private decimal scientificResearchWork = 0;

        /// <summary>
        /// Научно-исследовательская работа
        /// </summary>
        public decimal ScientificResearchWork
        {
            get { return scientificResearchWork; }
            set { scientificResearchWork = value; }
        } 


        

        public Discipline()
        { }

        public Discipline(string discipline, string codeChair,  string semester,  string pract, string setOff)
        {
            this.discipline = discipline;
            this.codeChair = codeChair;
            //this.course = course;
            this.semester = semester;
            this.pract = pract;
            this.setOff = setOff;
        }

        public Discipline(string discipline, string codeChair, string semester,string pract, string setOff, 
            string codeDiscipline,  
            string lecture, 
            string lab,  
            string practicalExercise, 
            string courseWork,
            string exam,  
            string courseProject,
            string controlWork) 
            :this(discipline, codeChair, semester, pract, setOff)
        {
            this.codeDiscipline = codeDiscipline;
            this.lecture = lecture;
            this.lab = lab;
            this.exam = exam;
            this.courseProject = courseProject;
            this.courseWork = courseWork;
            this.practicalExercise = practicalExercise;
            this.controlWork = controlWork;
        }

        public Discipline(string codeChair, string gak, string graduationDesign)
        {
            this.codeChair = codeChair;
            this.gak = gak;
            this.graduationDesign = graduationDesign;
        }
    }
}
