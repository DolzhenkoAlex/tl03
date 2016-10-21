using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    public class DesignerServiceEditDiscilpine
    {
        public DisciplinePlan Discipline { get; private set; }

        public DesignerServiceEditDiscilpine()
        {
            Discipline = new DisciplinePlan()
            {
                CodePlan = "Б2.Б.5",
                Discipline = "Информатика и программирование",
                LecturePlan = 36,
                LaboratoryWorkPlan = 36,
                PracticalExercisesPlan = 18,
                CourseProjectPlan = 1,
                CourseWorktPlan =1,
                ControlWorkPlan = 2,
                ExaminationPlan = 0,
                PracticalPlan = 2,
                SetOffPlan = 1,
                SetOffWithBallPlan = 0,
                GraduationDesignPlan = 0,
                GakPlan = 0,
                ScientificResearchWorkPlan = 0,
                Course = 1,
                Semester = 1
            };
        }
    }
}
