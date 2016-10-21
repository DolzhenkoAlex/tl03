using System.Collections.ObjectModel;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    /// <summary>
    /// Класс данных по студенческим группам, используемых при проектировании приложения
    /// </summary>
    public class DesignServiceGroup
    {
        public ObservableCollection<StudentGroup> Groups { get; private set; }

        public DesignServiceGroup()
        {
            Groups = new ObservableCollection<StudentGroup>
            {
                new StudentGroup
                {
                    IdFaculty = 1,
                    NameGroup = "311-БИН",
                    IdAcademicYear = 2,
                    IdFormEducation = 2,
                    IdQualification = 2,
                    IdSpeciality = 4,
                    CountComStudent = 3,
                    CountForeignStudent = 0,
                    CountStudent = 15,
                    CountSubgroup = 2,
                    Course = 1
                },
                new StudentGroup
                {
                    IdFaculty = 1,
                    NameGroup = "311-ПИ",
                    IdAcademicYear = 2,
                    IdFormEducation = 2,
                    IdQualification = 2,
                    IdSpeciality = 1,
                    CountComStudent = 2,
                    CountForeignStudent = 1,
                    CountStudent = 10,
                    CountSubgroup = 1,
                    Course = 1
                },
                new StudentGroup
                {
                    IdFaculty = 1,
                    NameGroup = "311-ИСТ",
                    IdAcademicYear = 2,
                    IdFormEducation = 2,
                    IdQualification = 2,
                    IdSpeciality = 2,
                    CountComStudent = 4,
                    CountForeignStudent = 1,
                    CountStudent = 12,
                    CountSubgroup = 1,
                    Course = 1
                }
            };
            
        }
    }
}
