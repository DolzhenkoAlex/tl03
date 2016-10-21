using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight2.Helper
{
    /// <summary>
    /// Дисциплины учебного плана, закрепленные за кафедрой 
    /// </summary>
    public class DataDisciplineChair: INotifyPropertyChanged
    {
         #region Property

        /// <summary>
        /// Id дисциплины
        /// </summary>
        int id;

        /// <summary>
        /// Id дисциплины
        /// </summary>
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        /// <summary>
        /// Id учебного года
        /// </summary>
        int idAcademicYear;

        /// <summary>
        /// Id учебного года
        /// </summary>
        public int IdAcademicYear
        {
          get { return idAcademicYear; }
          set 
          { 
              idAcademicYear = value; 
              OnPropertyChanged("IdAcademicYear");
          }
        }

        /// <summary>
        /// Id кафедры
        /// </summary>
        int idChair;

        /// <summary>
        /// Id кафедры
        /// </summary>
        public int IdChair
        {
          get { return idChair; }
          set 
          { 
              idChair = value;
              OnPropertyChanged("IdChair");
          }
        }

        /// <summary>
        /// Шифр дисциплины
        /// </summary>
        string codePlan;

        /// <summary>
        /// Шифр дисциплины
        /// </summary>
        public string CodePlan
        {
            get { return codePlan; }
            set
            {
                codePlan = value;
                OnPropertyChanged("CodePlan");
            }
        }

        /// <summary>
        /// Код кафедры
        /// </summary>
        int codeChair;

        /// <summary>
        /// Код кафедры
        /// </summary>
        public int CodeChair
        {
            get { return codeChair; }
            set
            {
                codeChair = value;
                OnPropertyChanged("CodeChair");
            }
        }

        /// <summary>
        /// Наименование дисциплины, учебного плана
        /// </summary>
        string discipline;

        /// <summary>
        /// Наименование дисциплины, учебного плана
        /// </summary>
        public string Discipline
        {
            get { return discipline; }
            set
            {
                discipline = value;
                OnPropertyChanged("Discipline");
            }
        }

        /// <summary>
        /// Семестр
        /// </summary>
        int semester;

        /// <summary>
        /// Семестр
        /// </summary>
        public int Semester
        {
            get { return semester; }
            set
            {
                semester = value;
                OnPropertyChanged("Semester");
            }
        }

        /// <summary>
        /// Осенний/весенний семестр
        /// </summary>
        string semesterYear;

        /// <summary>
        /// Осенний/весенний семестр
        /// </summary>
        public string SemesterYear
        {
            get { return semesterYear; }
            set 
            { 
                semesterYear = value;
                OnPropertyChanged("SemesterYear");
            }
        }


        /// <summary>
        /// Курс обучения
        /// </summary>
        int course;

        /// <summary>
        /// Курс обучения
        /// </summary>
        public int Course
        {
            get { return course; }
            set
            {
                course = value;
                OnPropertyChanged("Course");
            }
        }

        /// Количество часов лекций
        double lecturePlan;

        /// <summary>
        /// Количество часов лекций
        /// </summary>
        public double LecturePlan
        {
            get { return lecturePlan; }
            set
            {
                lecturePlan = value;
                OnPropertyChanged("LecturePlan");
            }
        }

        /// Количество часов практических занятий
        double practicalExercisesPlan;

        /// <summary>
        /// Количество часов практических занятий
        /// </summary>
        public double PracticalExercisesPlan
        {
            get { return practicalExercisesPlan; }
            set
            {
                practicalExercisesPlan = value;
                OnPropertyChanged("PracticalExercisesPlan");
            }
        }

        /// Количество часов лабораторных занятий
        double laboratoryWorkPlan;

        /// <summary>
        /// Количество часов лабораторных занятий
        /// </summary>
        public double LaboratoryWorkPlan
        {
            get { return laboratoryWorkPlan; }
            set
            {
                laboratoryWorkPlan = value;
                OnPropertyChanged("LaboratoryWorkPlan");
            }
        }

        /// Номер семестра экзамена
        int examinationPlan;

        /// <summary>
        /// Номер семестра экзамена
        /// </summary>
        public int ExaminationPlan
        {
            get { return examinationPlan; }
            set
            {
                examinationPlan = value;
                OnPropertyChanged("ExaminationPlan");
            }
        }

        /// Номер семестра зачета
        int setOffPlan;

        /// <summary>
        /// Номер семестра зачета
        /// </summary>
        public int SetOffPlan
        {
            get { return setOffPlan; }
            set
            {
                setOffPlan = value;
                OnPropertyChanged("SetOffPlan");
            }
        }

        /// Номер семестра курсового проекта
        int courseProjectPlan;

        /// <summary>
        /// Номер семестра курсового проекта
        /// </summary>
        public int CourseProjectPlan
        {
            get { return courseProjectPlan; }
            set
            {
                courseProjectPlan = value;
                OnPropertyChanged("CourseProjectPlan");
            }
        }

        /// Номер семестра курсовой работы
        int courseWorkPlan;

        /// <summary>
        /// Номер семестра курсовой работы
        /// </summary>
        public int CourseWorktPlan
        {
            get { return courseWorkPlan; }
            set
            {
                courseWorkPlan = value;
                OnPropertyChanged("CourseWorkPlan");
            }
        }

        /// Количество часов производственной практики
        int practicalPlan;

        /// <summary>
        /// Количество часов производственной практики
        /// </summary>
        public int PracticalPlan
        {
            get { return practicalPlan; }
            set
            {
                practicalPlan = value;
                OnPropertyChanged("PracticalPlan");
            }
        }

        /// Количество контрольных работ
        int controlWorkPlan;

        /// <summary>
        /// Количество контрольных работ
        /// </summary>
        public int ControlWorkPlan
        {
            get { return controlWorkPlan; }
            set
            {
                controlWorkPlan = value;
                OnPropertyChanged("ControlWorkPlan");
            }
        }

        /// <summary>
        /// Учебный год
        /// </summary>
        string year;

        /// <summary>
        /// Учебный год
        /// </summary>
        public string Year
        {
            get { return year; }
            set
            {
                year = value;
                OnPropertyChanged("AcademicYear");
            }
        }

        /// <summary>
        /// Форма обучения
        /// </summary>
        string formEducation;

        /// <summary>
        /// Форма обучения
        /// </summary>
        public string FormEducation
        {
            get { return formEducation; }
            set
            {
                formEducation = value;
                OnPropertyChanged("FormEducation");
            }
        }

        /// <summary>
        /// Квалификация
        /// </summary>
        string nameQualification;

        /// <summary>
        /// Квалификация
        /// </summary>
        public string NameQualification
        {
            get { return nameQualification; }
            set
            {
                nameQualification = value;
                OnPropertyChanged("NameQualification");
            }
        }

        /// <summary>
        /// Профиль подготовки
        /// </summary>
        string profile;

        /// <summary>
        /// Профиль подготовки
        /// </summary>
        public string Profile
        {
            get { return profile; }
            set
            {
                profile = value;
                OnPropertyChanged("Profile");
            }
        }


        /// <summary>
        /// Наименование факультета
        /// </summary>
        string shortName;

        /// <summary>
        /// Наименование факультета
        /// </summary>
        public string ShortName
        {
            get { return shortName; }
            set
            {
                shortName = value;
                OnPropertyChanged("ShortNameFaculty");
            }
        }


        

        

        /// <summary>
        /// Шифр направления
        /// </summary>
        string codeSpeciality;

        /// <summary>
        /// Шифр направления
        /// </summary>
        public string CodeSpeciality
        {
            get { return codeSpeciality; }
            set
            {
                codeSpeciality = value;
                OnPropertyChanged("CodeSpeciality");
            }
        }

        /// <summary>
        /// Наименование направления подготовки
        /// </summary>
        string nameSpeciality;

        /// <summary>
        /// Наименование направления подготовки
        /// </summary>
        public string NameSpeciality
        {
            get { return nameSpeciality; }
            set
            {
                nameSpeciality = value;
                OnPropertyChanged("NameSpeciality");
            }
        }

        


        

        #endregion Property
        
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Constractor

        public DataDisciplineChair() 
        { } 
       

        public DataDisciplineChair(int id,
            string codePlan,
            int codeChair,
            string codeSpeciality,
            string nameSpeciality,
            string profile,
            string chair,
            string faculty,
            string academicYear,
            string qualific,
            string formEducation,
            int course,
            int semester,
            string discipline,
            double lecturePlan,
            double practicalExercisesPlan,
            double laboratoryWorkPlan,
            int setOffPlan,
            int courseProjectPlan,
            int courseWorkPlan,
            double practicalPlan,
            int controlWorkPlan)
        {
            this.id = id;
            this.codePlan = codePlan;
            this.codeSpeciality = codeSpeciality;
            this.nameSpeciality = nameSpeciality;
            this.profile = profile;
            this.nameQualification = qualific;
            this.codeChair = codeChair;
            //this.ch = ch;
            this.shortName = faculty;
            this.year = academicYear;
            this.formEducation = formEducation;
            this.course = course;
            this.semester = semester;
            this.discipline = discipline;
            this.lecturePlan = lecturePlan;
            this.practicalExercisesPlan = practicalExercisesPlan;
            this.laboratoryWorkPlan = laboratoryWorkPlan;
            this.setOffPlan = setOffPlan;
            this.courseProjectPlan = courseProjectPlan;
            this.courseWorkPlan = courseWorkPlan;
            this.controlWorkPlan = controlWorkPlan;
        }

        public DataDisciplineChair(DataDisciplineChair source)
        {
            this.id = source.id;
            this.codePlan = source.codePlan;
            this.codeSpeciality = source.codeSpeciality;
            this.nameSpeciality = source.nameSpeciality;
            this.profile = source.profile;
            this.nameQualification= source.nameQualification;
            this.codeChair = source.codeChair;
            //this.ch = source.ch;
            this.shortName = source.shortName;
            this.year = source.year;
            this.formEducation = source.formEducation;
            this.course = source.course;
            this.semester = source.semester;
            this.discipline = source.discipline;
            this.lecturePlan = source.lecturePlan;
            this.practicalExercisesPlan = source.practicalExercisesPlan;
            this.laboratoryWorkPlan = source.laboratoryWorkPlan;
            this.setOffPlan = source.setOffPlan;
            this.courseProjectPlan = source.courseProjectPlan;
            this.courseWorkPlan = source.courseWorkPlan;
            this.controlWorkPlan = source.controlWorkPlan;
        }

        #endregion Constractor
    }
}
