using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    public class DesignServiceNewLoadChair
    {
        public TeachingLoad NewLoadChair { get; private set; }

        public DesignServiceNewLoadChair()
        {
            NewLoadChair = new TeachingLoad
            {
                Discipline = "Руководство практикой",
                Student = 25,
                ForeignStudent = 2,
                Speciality = "Прикладная информатика",
                Profile = "ИТ-менеджер",
                Qualification = "бакалавр",
                Course = 3,
                Semester = 6,
                Gac = 10,
                Practical = 16,
                GraduationDesign = 45,
                Others = 10
            };
        }
    }
}
