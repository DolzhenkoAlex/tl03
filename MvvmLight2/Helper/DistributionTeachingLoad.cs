using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using MvvmLight2.Model;
using MvvmLight2.ViewModel;

namespace MvvmLight2.Helper
{
    /// <summary>
    /// Распределяемая нагрузка кафедры по преподавателям
    /// </summary>
    public class DistributionTeachingLoad : INotifyPropertyChanged, IDataErrorInfo
    {
         #region Property


        #region Признак распределения нагрузки кафедры
        /// <summary>
        /// Признак распределения нагрузки кафедры 
        /// </summary>
        int idLoad;

        /// <summary>
        /// Признак распределения нагрузки кафедры 
        /// </summary>
        public int IdLoad
        {
            get { return idLoad; }
            set
            {
                if (value == idLoad)
                    return;
                idLoad = value;
                OnPropertyChanged("IdLoad");
            }
        }

        #endregion Признак распределения нагрузки кафедры

        #region Наименование группы обучающихся

        /// Наименование группы обучающихся
        string nameGroup;

        /// <summary>
        /// Наименование группы
        /// </summary>
        public string NameGroup
        {
            get { return nameGroup; }
            set 
            {
                if (value == nameGroup)
                    return;
                nameGroup = value;
                OnPropertyChanged("NameGroup");
            }
        }

        #endregion Наименование группы обучающихся

        #region Количество подгрупп

        /// <summary>
        /// Количество подгрупп 
        /// </summary>
        int? subgroup = 1;

        /// <summary>
        /// Количество подгрупп
        /// </summary>
        public int? Subgroup
        {
            get { return subgroup; }
            set
            {
                if (value == subgroup)
                    return;
                subgroup = value;
                OnPropertyChanged("Subgroup");
            }
        }

        #endregion Количество подгрупп

        #region Количество обучающихся в группе

        /// Количество обучающихся в группе
        int? student;

        /// <summary>
        /// Количество обучающихся в группе
        /// </summary>
        public int? Student
        {
            get { return student; }
            set 
            {
                if (value == student)
                    return;
                student = value;
                OnPropertyChanged("Student");
            }
        }

        #endregion Количество обучающихся в группе

        #region Количество иностранных студентов

        /// <summary>
        /// Количество иностранных студентов
        /// </summary>
        int? foreignStudent;

        /// <summary>
        /// Количество иностранных студентов
        /// </summary>
        public int? ForeignStudent 
        {
            get { return foreignStudent; }
            set
            {
                if (value == foreignStudent)
                    return;
                foreignStudent = value;
                OnPropertyChanged("ForeignStudent ");
            }
        }

        #endregion Количество иностранных студентов

        #region Шифр направления

        /// <summary>
        /// Шифр направления
        /// </summary>
        string speciality;

        /// <summary>
        /// Шифр направления
        /// </summary>
        public string Speciality
        {
            get { return speciality; }
            set
            {
                if (value == speciality)
                    return;
                speciality = value;
                OnPropertyChanged("Speciality");
            }
        }

        #endregion Шифр направления

        #region Профиль подготовки

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
                if (value == profile)
                    return;
                profile = value;
                OnPropertyChanged("Profile");
            }
        }

        #endregion Профиль подготовки

        #region Квалификация

        /// <summary>
        /// Квалификация
        /// </summary>
        string qualification;

        /// <summary>
        /// Квалификация
        /// </summary>
        public string Qualification
        {
            get { return qualification; }
            set 
            {
                if (value == qualification)
                    return;
                qualification = value;
                OnPropertyChanged("Qualification");
            }
        }

        #endregion Квалификация

        #region Дисциплина

        /// Дисциплина
        string discipline;

        /// <summary>
        /// Дисциплина
        /// </summary>
        public string Discipline
        {
            get { return discipline; }
            set 
            {
                if (value == discipline)
                    return;
                discipline = value;
                OnPropertyChanged("Discipline");
            }
        }

        #endregion Дисциплина

        #region Форма обучения

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
                if (value == formEducation)
                    return;
                formEducation = value;
                OnPropertyChanged("FormEducation");
            }
        }

        #endregion Форма обучения

        #region Краткое наименование факультета

        /// Краткое наименование факультета
        string shortNameFaculty;

        /// <summary>
        /// Краткое наименование факультета
        /// </summary>
        public string ShortNameFaculty
        {
            get { return shortNameFaculty; }
            set 
            {
                if (value == shortNameFaculty)
                    return;
                shortNameFaculty = value;
                OnPropertyChanged("ShortNameFaculty");
            }
        }

        #endregion Краткое наименование факультета

        #region Тип нагрузки: звонковая, незвонковая
        /// <summary>
        /// Тип нагрузки: звонковая, незвонковая
        /// </summary>
        bool? typeDiscipline;

        /// <summary>
        /// Тип нагрузки: звонковая, незвонковая
        /// </summary>
        public bool? TypeDiscipline
        {
            get { return typeDiscipline; }
            set 
            {
                if (value == typeDiscipline)
                    return;
                typeDiscipline = value; 
                OnPropertyChanged("TypeDiscipline");
            }
        }

        #endregion Тип нагрузки: звонковая, незвонковая

        #region Шифр дисциплины по плану

        /// Шифр дисциплины по плану
        string code;

        /// <summary>
        /// Шифр дисциплины по плану
        /// </summary>
        public string Code
        {
            get { return code; }
            set
            {
                if (value == code)
                    return;
                code = value;
                OnPropertyChanged("Code");
            }
        }

        #endregion Шифр дисциплины по плану

        #region Семестр

        /// Семестр
        int? semester;

        /// <summary>
        /// Семестр
        /// </summary>
        public int? Semester
        {
            get { return semester; }
            set 
            {
                if (value == semester)
                    return;
                semester = value;
                OnPropertyChanged("Semester");
            }
        }

        #endregion Семестр

        #region Осенний/весенний семестр

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
                if (value == semesterYear)
                    return;
                semesterYear = value;
                OnPropertyChanged("SemesterYear");
            }
        }

        #endregion Осенний/весенний семестр

        #region Курс - год обучения

        /// Курс - год обучения
        int? course;

        /// <summary>
        /// Курс - год обучения
        /// </summary>
        public int? Course
        {
            get { return course; }
            set 
            {
                if (value == course)
                    return;
                course = value;
                OnPropertyChanged("Course");
            }
        }

        #endregion Курс - год обучения

        /// Количество часов лекций
         decimal lecture;

        /// <summary>
        /// Количество часов лекций
        /// </summary>
        public decimal Lecture
        {
            get { return lecture; }
            set 
            {
                if (value == lecture)
                    return;
                lecture = value;
                SumLoad = SetSumLoad();
                OnPropertyChanged("Lecture");
            }
        }

        /// Количество часов практических занятий
        decimal practicalExercises;

        /// <summary>
        /// Количество часов практических занятий
        /// </summary>
        public decimal PracticalExercises
        {
            get { return practicalExercises; }
            set 
            {
                if (value == practicalExercises)
                    return;
                practicalExercises = value;
                SumLoad = SetSumLoad();
                OnPropertyChanged("PracticalExercises");
            }
        }

         /// Количество часов лабораторных занятий
        decimal laboratoryWork;

        /// <summary>
        /// Количество часов практических занятий
        /// </summary>
        public decimal LaboratoryWork
        {
            get { return laboratoryWork; }
            set 
            {
                if (value == laboratoryWork)
                    return;
                laboratoryWork = value;
                SumLoad = SetSumLoad();
                OnPropertyChanged("LaboratoryWork");
            }
        }

        /// Количество часов на экзамен
        decimal examination;

        /// <summary>
        /// Количество часов на экзамен
        /// </summary>
        public decimal Examination
        {
            get { return examination; }
            set 
            {
                if (value == examination)
                    return;
                examination = value;
                SumLoad = SetSumLoad();
                OnPropertyChanged("Examination");
            }
        }

        /// Количество часов на  консультацию
        decimal consultation;

        /// <summary>
        /// Количество часов на  консультацию
        /// </summary>
        public decimal Consultation
        {
            get { return consultation; }
            set 
            {
                if (value == consultation)
                    return;
                consultation = value;
                SumLoad = SetSumLoad();
                OnPropertyChanged("Consultation");
            }
        }

         /// Количество часов на зачет
         decimal setOff;

        /// <summary>
        /// Количество часов на зачет
        /// </summary>
        public decimal SetOff
        {
            get { return setOff; }
            set 
            {
                if (value == setOff)
                    return;
                setOff = value;
                SumLoad = SetSumLoad();
                OnPropertyChanged("SetOff");
            }
        }

        /// Количество часов на зачет с оценкой
        decimal setOffWithBall;

        /// <summary>
        /// Количество часов на зачет
        /// </summary>
        public decimal SetOffWithBall
        {
            get { return setOffWithBall; }
            set
            {
                if (value == setOffWithBall)
                    return;
                setOffWithBall = value;
                SumLoad = SetSumLoad();
                OnPropertyChanged("SetOff");
            }
        }

         /// Количество часов на курсовой проект
         decimal courseProject;

        /// <summary>
        /// Количество часов на курсовой проект
        /// </summary>
        public decimal CourseProject
        {
            get { return courseProject; }
            set 
            {
                if (value == courseProject)
                    return;
                courseProject = value;
                SumLoad = SetSumLoad();
                OnPropertyChanged("CourseProject");
            }
        }

        /// Количество часов на курсовую работу
         decimal courseWorkt;

        /// <summary>
        /// Количество часов на курсовую работу
        /// </summary>
        public decimal CourseWorkt
        {
            get { return courseWorkt; }
            set 
            {
                if (value == courseWorkt)
                    return;
                courseWorkt = value;
                SumLoad = SetSumLoad();
                OnPropertyChanged("CourseWorkt");
            }
        }

        /// Количество часов производственной практики
        decimal practical;

        /// <summary>
        /// Количество часов производственной практики
        /// </summary>
        public decimal Practical
        {
            get { return practical; }
            set
            {
                if (value == practical)
                    return;
                practical = value;
                SumLoad = SetSumLoad();
                OnPropertyChanged("Practical");
            }
        }

        /// Количество часов на дипломное проектирование
        decimal graduationDesign;

        /// <summary>
        /// Количество часов на дипломное проектирование
        /// </summary>
        public decimal GraduationDesign
        {
            get { return graduationDesign; }
            set
            {
                if (value == graduationDesign)
                    return;
                graduationDesign = value;
                SumLoad = SetSumLoad();
                OnPropertyChanged("GraduationDesign");
            }
        }

        /// Количество часов на контрольные работы
        decimal controlWork;

        /// <summary>
        /// Количество часов на контрольные работы
        /// </summary>
        public decimal ControlWork
        {
            get { return controlWork; }
            set
            {
                if (value == controlWork)
                    return;
                controlWork = value;
                SumLoad = SetSumLoad();
                OnPropertyChanged("ControlWork");
            }
        }

        ///  Количество часов на государственную аттестационную комиссию
        decimal gac;

        /// <summary>
        ///  Количество часов на государственную аттестационную комиссию
        /// </summary>
        public decimal Gac
        {
            get { return gac; }
            set 
            {
                if (value == gac)
                    return;
                gac = value;
                SumLoad = SetSumLoad();
                OnPropertyChanged("Gac");
            }
        }

         /// Количество часов с применением ДОТ
         decimal dot;

        /// <summary>
        /// Количество часов с применением ДОТ
        /// </summary>
        public decimal Dot
        {
            get { return dot; }
            set
            {
                if (value == dot)
                    return;
                dot = value;
                SumLoad = SetSumLoad();
                OnPropertyChanged("Dot");
            }
        }

        /// <summary>
        /// Количество часов на научно-исследовательскую работу
        /// </summary>
        private decimal scientificResearchWork;

        /// <summary>
        /// Количество часов на научно-исследовательскую работу
        /// </summary>
        public decimal ScientificResearchWork
        {
            get { return scientificResearchWork; }
            set 
            {
                if (value == scientificResearchWork)
                    return;
                scientificResearchWork = value;
                SumLoad = SetSumLoad();
                OnPropertyChanged("ScientificResearchWork");
            }
        } 

         /// Количество часов на прочую нагрузку
        decimal others;

        /// <summary>
        /// Количество часов с на прочую нагрузку
        /// </summary>
        public decimal Others
        {
            get { return others; }
            set
            {
                if (value == others)
                    return;
                others = value;
                SumLoad = SetSumLoad();
                OnPropertyChanged("Others");
            }
        }

        /// Суммарное количество часов по дисциплине
        decimal sumLoad;

        /// <summary>
        /// Суммарное количество часов по дисциплине
        /// </summary>
        public decimal SumLoad
        {
            get { return sumLoad; }
            set
            {
                if (value == sumLoad)
                    return;
                sumLoad = value;
                OnPropertyChanged("SumLoad");
            }
        }

        // Количество нераспределенных часов по дисциплине
        decimal sumUnload;

        /// <summary>
        /// Количество нераспределенных часов по дисциплине
        /// </summary>
        public decimal SumUnload
        {
            get { return sumUnload; }
            set 
            {
                if (value == sumUnload)
                    return;
                sumUnload = value;
                OnPropertyChanged("SumUnload");
            }
        }

        /// <summary>
        /// Флаг изменения нагрузки
        /// </summary>
        string flagChange;

        /// <summary>
        /// Флаг изменения нагрузки
        /// </summary>
        public string FlagChange
        {
          get { return flagChange; }
          set 
          {
              if (value == flagChange)
                  return;
              flagChange = value;
              OnPropertyChanged("FlagChange");
          }
        }

        /// Признак распределена(true)/нераспределена(false) дисциплпина
        bool isLoad;

        /// <summary>
        /// Признак распределена(true)/нераспределена(false) дисциплпина
        /// </summary>
        public bool IsLoad
        {
            get { return isLoad; }
            set 
            {
                if (value == isLoad)
                    return;
                isLoad = value;
                OnPropertyChanged("IsLoad");
            }
        }

                /// <summary>
        /// Признак ошибки распределения дисциплины - 
        /// превышение плана
        /// </summary>
        bool isFaultLoad = true;

        /// <summary>
        /// Признак ошибки распределения дисциплины - 
        /// превышение плана
        /// </summary>
        public bool IsFaultLoad
        {
            get { return isFaultLoad; }
            set
            {
                if (value == isFaultLoad)
                    return;
                isFaultLoad = value;
                OnPropertyChanged("IsFaultLoad");
            }
        }


        /// Статус нагрузки: план, закрепленный преподаватель
        string status;

        /// <summary>
        /// Статус нагрузки: план, закрепленный преподаватель
        /// </summary>
        public string Status
        {
            get { return status; }
            set
            {
                if (value == status)
                    return;
                status = value;
                OnPropertyChanged("Status");
            }
        }

        #endregion Property

        #region Реализация интерфейса IDataErrorInfo

        /// <summary>
        /// Свойство Error интерфейса IDataErrorInfo указывает ошибку для объекта в целом
        /// </summary>
        public string Error
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        ///  Индексатор интерфейса IDataErrorInfo указывает ошибки на уровне его индивидуальных свойств
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string this[string name]
        {
            get
            {
                string result = null;

                if (name == "Lecture"
                    || name == "PracticalExercises"
                    || name == "LaboratoryWork"
                    || name == "Examination"
                    || name == "SetOff"
                    || name == "Consultation"
                    || name == "CourseProject"
                    || name == "CourseWorkt"
                    || name == "Practical"
                    || name == "GraduationDesign"
                    || name == "ControlWork"
                    || name == "Gac"
                    || name == "Dot"
                    || name == "Others")
                {
                    if (!(bool)this.IsFaultLoad)
                        result = "Превышение плана недопустимо";

                    if(lecture < 0
                        || PracticalExercises < 0
                        || LaboratoryWork < 0
                        || Examination < 0
                        || SetOff < 0
                        || Consultation < 0
                        || CourseProject < 0
                        || CourseWorkt < 0
                        || GraduationDesign < 0
                        || Gac < 0
                        || Practical < 0
                        || ControlWork < 0
                        || Dot < 0
                        || Others < 0)
                        result = "Отрицательное значение недопустимо";
                }
                return result;
            }
        }

        #endregion Реализация интерфейса IDataErrorInfo

        #region Method

        /// <summary>
        /// Вычисление суммарной нагрузки по дисциплине
        /// </summary>
        /// <returns></returns>
        private decimal SetSumLoad()
        {
            decimal sum = 0;

                sum += lecture;
                sum += practicalExercises;
                sum += laboratoryWork;
                sum += examination;
                sum += setOff;
                sum += setOffWithBall;
                sum += consultation;
                sum += courseProject;
                sum += courseWorkt;
                sum += graduationDesign;
                sum += practical;
                sum += controlWork;
                sum += gac;
                sum += dot;
                sum += scientificResearchWork;
                sum += others;

            return sum;
        }

        #endregion Method

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
                switch (propertyName)
                {
                    case "SumLoad": // Изменилась сумма часов для дисциплины по преподавателю
                        // Сообщение об изменении в нагрузке преподавателя по дисциплине
                        Messenger.Default.Send<DistributionTeachingLoad, LoadTeacherViewModel>(this);

                        //Messenger.Default.Send<DataTeachingLoad, DistributionTeachingLoadViewModel>(this);
                        //Messenger.Default.Send<DataTeachingLoad, ChairLoadViewModel>(this);
                        break;

                    case "IsLoad": // Изменился признак распределения/нераспределения дисциплины
                        // Сообщение об изменении признака распределения/нераспределения дисциплины
                        //Messenger.Default.Send<TeachingLoad, TeachingLoadChair>(this);
                        break;

                    default:
                        break;
                }
            }
        }

        #endregion

        #region Constractor

        public DistributionTeachingLoad() { }

        //public DistributionTeachingLoad(
        //    string nameGroup,
        //    int student,
        //    int subgroup,
        //    int foreignStudent,
        //    string speciality,
        //    string profile,
        //    string qualification,
        //    string discipline,
        //    string formEducation,
        //    string shortNameFaculty,
        //    string code,
        //    int? semester,
        //    int? course,
        //    decimal? lecture,
        //    decimal? practicalExercises,
        //    decimal? laboratoryWork,
        //    decimal? examination,
        //    decimal? setOff,
        //    decimal? consultation,
        //    decimal? courseProject,
        //    decimal? courseWorkt,
        //    decimal? practical,
        //    decimal? graduationDesign,
        //    decimal? controlWork,
        //    decimal? gac,
        //    decimal? dot,
        //    decimal? others,
        //    decimal? sumLoad,
        //    decimal? sumUnload,
        //    string flagChange,
        //    bool? isLoad,
        //    bool? isFaultLoad,
        //    string status) 
        //{
        //    this.nameGroup = nameGroup;
        //    this.student = student;
        //    this.subgroup = subgroup;
        //    this.foreignStudent = foreignStudent;
        //    this.speciality = speciality;
        //    this.profile = profile;
        //    this.qualification = qualification;
        //    this.discipline = discipline;
        //    this.formEducation = formEducation;
        //    this.shortNameFaculty = shortNameFaculty;
        //    this.code = code;
        //    this.semester = semester;
        //    this.course = course;
        //    this.lecture = lecture;
        //    this.practicalExercises = practicalExercises;
        //    this.laboratoryWork = laboratoryWork;
        //    this.examination = examination;
        //    this.setOff = setOff;
        //    this.consultation = consultation;
        //    this.courseProject = courseProject;
        //    this.courseWorkt = courseWorkt;
        //    this.practical = practical;
        //    this.graduationDesign = graduationDesign;
        //    this.controlWork = controlWork;
        //    this.gac = gac;
        //    this.dot = dot;
        //    this.others = others;
        //    this.sumLoad = sumLoad;
        //    this.sumUnload = sumUnload;
        //    this.flagChange = flagChange;
        //    this.isLoad = isLoad;
        //    this.IsFaultLoad = IsFaultLoad;
        //    this.status = status;


        //    //Messenger.Default.Register<DistributionTeachingLoadViewModel>(this,
        //    //    x => IsFaultLoad = x.IsFaultLoad);
        //}

        //public DistributionTeachingLoad(DistributionTeachingLoad source)
        //{
        //    this.nameGroup = source.nameGroup;
        //    this.student = source.student;
        //    this.subgroup = source.subgroup;
        //    this.foreignStudent = source.foreignStudent;
        //    this.speciality = source.speciality;
        //    this.profile = source.profile;
        //    this.qualification = source.qualification;
        //    this.discipline = source.discipline;
        //    this.formEducation = source.formEducation;
        //    this.shortNameFaculty = source.shortNameFaculty;
        //    this.code = source.code;
        //    this.semester = source.semester;
        //    this.course = source.course;
        //    this.lecture = source.lecture;
        //    this.practicalExercises = source.practicalExercises;
        //    this.laboratoryWork = source.laboratoryWork;
        //    this.examination = source.examination;
        //    this.setOff = source.setOff;
        //    this.consultation = source.consultation;
        //    this.courseProject = source.courseProject;
        //    this.courseWorkt = source.courseWorkt;
        //    this.practical = source.practical;
        //    this.graduationDesign = source.graduationDesign;
        //    this.controlWork = source.controlWork;
        //    this.gac = source.gac;
        //    this.dot = source.dot;
        //    this.others = source.others;
        //    this.sumLoad = source.sumLoad;
        //    this.sumUnload = source.sumUnload;
        //    this.flagChange = source.flagChange;
        //    this.isLoad = source.isLoad;
        //    this.IsFaultLoad = source.IsFaultLoad;
        //    this.status = source.status;

        //    //Messenger.Default.Register<DistributionTeachingLoadViewModel>(this,
        //    //    x => IsFaultLoad = x.IsFaultLoad);
        //}

        #endregion Constractor
    }
}
