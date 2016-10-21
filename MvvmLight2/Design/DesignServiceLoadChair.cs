using System.Collections.ObjectModel;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    /// <summary>
    /// Класс данных по Нагрузке кафедры кафедрам, используемых при проектировании приложения
    /// </summary>
    public class DesignServiceLoadChair
    {
        public ObservableCollection<TeachingLoad> LoadChairTeaching { get; private set; }

        public TeachingLoadChair LoadChair { get; private set; }

        public DesignServiceLoadChair()
        {
            LoadChair = new TeachingLoadChair
            {
                SumLoadChair = 824.8m
            };
            
            LoadChairTeaching = new ObservableCollection<TeachingLoad>
            {
                new TeachingLoad
                {
                    Speciality = "230700",
                    Profile = "Информационный менеджмент",
                    Qualification  = "бакалавр",
                    FormEducation = "очная",
                    Stream =1,
                    Discipline = "Информационные ресурсы и системы",
                    Code = "Б3.Б.2",
                    NameGroup = "331-ПИ",
                    Student = 19,
                    ForeignStudent = 0,
                    Subgroup = 2,
                    ShortNameFaculty = "КТиИБ",
                    Semester = 5,
                    Course = 3,
                    Lecture = 36,
                    LaboratoryWork = 18,
                    PracticalExercises = 18,
                    CourseProject = 45,
                    Examination = 7.5M,
                    SumLoad = 124.5M,
                    Consultation = 1.5M
                },

                new TeachingLoad
                {
                    Speciality = "230700",
                    Profile = "Информационный менеджмент",
                    Qualification  = "бакалавр",
                    FormEducation = "очная",
                    Stream =1,
                    Discipline = "Программная инженерия",
                    Code = "Б3.Б.1.3",
                    NameGroup = "331-ПИ",
                    Student = 19,
                    ForeignStudent = 0,
                    Subgroup = 2,
                    ShortNameFaculty = "КТиИБ",
                    Semester = 5,
                    Course = 3,
                    Lecture = 36,
                    LaboratoryWork = 18,
                    PracticalExercises = 18,
                    Examination = 4.5M,
                    SetOff = 2.7M,
                    SumLoad = 76.5M,
                    Consultation = 1.1M
                },

                new TeachingLoad
                {
                    Speciality = "230700",
                    Profile = "Информационный менеджмент",
                    Qualification  = "бакалавр",
                    FormEducation = "заочная",
                    Stream =1,
                    Discipline = "Программная инженерия",
                    Code = "Б3.Б.1.3",
                    NameGroup = "331-ПИ",
                    Student = 19,
                    ForeignStudent = 0,
                    Subgroup = 2,
                    ShortNameFaculty = "КТиИБ",
                    Semester = 5,
                    Course = 3,
                    Lecture = 4,
                    LaboratoryWork = 8,
                    PracticalExercises = 2,
                    Examination = 6.5M,
                    SumLoad = 34.1M,
                    Consultation = 1.6M,
                    ControlWork = 12
                },

                new TeachingLoad
                {
                    Speciality = "230700",
                    Profile = "Информационный менеджмент",
                    Qualification  = "бакалавр",
                    FormEducation = "очная",
                    Stream =1,
                    Discipline = "Дипломное проектирование",
                    ShortNameFaculty = "КТиИБ",
                    Semester = 8,
                    Course = 4,
                    GraduationDesign = 182.8M,
                    SumLoad = 182.8M,
                       
                },

                new TeachingLoad
                {
                    Discipline = "Член диссертационного совета",
                    Others = 100,
                    SumLoad = 100,
                       
                }
            };
        }
    }
}
