using MvvmLight2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight2.Design
{
    /// <summary>
    /// Класс данных по закрепления дисциплин, используемых при проектировании приложения
    /// </summary>
    class DesignServiceFixedDiscipline
    {
        public ObservableCollection<tlsp_getFixedDisciplines_Result> FixedDisciplines { get; private set; }

        public DesignServiceFixedDiscipline()
        {
            FixedDisciplines = new ObservableCollection<tlsp_getFixedDisciplines_Result>
            {
                new tlsp_getFixedDisciplines_Result
                {
                    Name= "230400_3.pml",
                    CodePlan= "Б3.В.ДВ.3.1",
                    Discipline ="Web-технологии",
                    CodeSpeciality = "230400",
                    NameSpeciality= "ИСиТ",
                    NameQualification = "бакалавр",
                    FormEducation = "очная",
                    ShortName = "ФКТиИБ",
                    Course = 3,
                    Semester = 5,
                    LecturePlan = 18,
                    LaboratoryWorkPlan = 36,
                    PracticalExercisesPlan = 0,
                    CourseProjectPlan =0,
                    CourseWorktPlan = 0,
                    ControlWorkPlan = 0,
                    ExaminationPlan = 3,
                    SetOffPlan = 0,
                    ScientificResearchWorkPlan = 0,
                    PracticalPlan = 0,
                    CodeChair = 22,
                    GakPlan = 0,
                    GraduationDesignPlan = 0
                },

                new tlsp_getFixedDisciplines_Result
                {
                    Name= "230400_1.pml",
                    CodePlan= "Б3.В.ОД.9",
                    Discipline ="Базы данных",
                    CodeSpeciality = "23040001",
                    NameSpeciality= "ИСиТ",
                    Profile = "ИС в бизнесе",
                    NameQualification = "бакалавр",
                    FormEducation = "очная",
                    ShortName = "ФКТиИБ",
                    Course = 1,
                    Semester = 2,
                    LecturePlan = 36,
                    LaboratoryWorkPlan = 36,
                    PracticalExercisesPlan = 0,
                    CourseProjectPlan =0,
                    CourseWorktPlan = 0,
                    ControlWorkPlan = 0,
                    ExaminationPlan = 2,
                    SetOffPlan = 0,
                    ScientificResearchWorkPlan = 0,
                    PracticalPlan = 0,
                    CodeChair = 22,
                    GakPlan = 0,
                    GraduationDesignPlan = 0
                }
            };
        }
    }
}
