using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    public class DesignServiceCurriculum
    {
        public ObservableCollection<Curriculum> Curriculums { get; private set; }

        public ObservableCollection<DisciplinePlan> Disciplines { get; private set; }

        public DesignServiceCurriculum()
        {
            Curriculums = new ObservableCollection<Curriculum>
            {
            new Curriculum
                {
                    Name = "230400_2012",
                    Protocol = "123",
                    DurationStudy = "4г",
                    Course1 = true,
                    Course2 = true,
                    DataApproval = new DateTime(2011, 08, 01)
                },
            new Curriculum
                {
                    Name = "230400_2014",
                    Protocol = "124",
                    DurationStudy = "4г",
                    Course3 = true,
                    Course4 = true,
                    DataApproval = new DateTime(2012, 09, 02)
                },
                new Curriculum
                {
                    Name = "230400_2015",
                    Protocol = "125",
                    DurationStudy = "4г",
                    Course2 = true,
                    Course3 = true,
                    DataApproval = new DateTime(2013, 10, 03)
                }
            };

            Disciplines = new ObservableCollection<DisciplinePlan>
            {
                new DisciplinePlan
                       {
                           CodePlan = "Б1.Б.2",
                           Discipline = "История",
                           LecturePlan = 18,
                           LaboratoryWorkPlan = 0,
                           PracticalExercisesPlan = 36,
                           Course = 1,
                           ExaminationPlan = 1,
                           SetOffPlan = 0,
                           SetOffWithBallPlan = 1,
                           Semester = 1,
                           CourseProjectPlan = 0,
                           CourseWorktPlan = 0,
                           ControlWorkPlan = 0
                       },

                       new DisciplinePlan
                       {
                           CodePlan = "Б1.Б.3",
                           Discipline = "Иностранный язык",
                           LecturePlan = 0,
                           LaboratoryWorkPlan = 0,
                           PracticalExercisesPlan = 72,
                           ExaminationPlan = 0,
                           SetOffPlan = 1,
                           SetOffWithBallPlan = 0,
                           Course = 1,
                           Semester = 1
                       },
                       new DisciplinePlan
                       {
                           CodePlan = "Б1.Б.3",
                           Discipline = "Иностранный язык",
                           LecturePlan = 0,
                           LaboratoryWorkPlan = 0,
                           PracticalExercisesPlan = 72,
                           ExaminationPlan = 0,
                           SetOffPlan = 1,
                           SetOffWithBallPlan = 0,
                           Course = 1,
                           Semester = 2
                       },
                       new DisciplinePlan
                       {
                           CodePlan = "Б1.Б.4",
                           Discipline = "Экономическая теория",
                           LecturePlan = 18,
                           LaboratoryWorkPlan = 0,
                           PracticalExercisesPlan = 18,
                           ExaminationPlan = 0,
                           SetOffPlan = 1,
                           SetOffWithBallPlan = 0,
                           Course = 1,
                           Semester = 1
                       },
                       new DisciplinePlan
                       {
                           CodePlan = "Б1.Б.4",
                           Discipline = "Экономическая теория",
                           LecturePlan = 18,
                           LaboratoryWorkPlan = 0,
                           PracticalExercisesPlan = 36,
                           ExaminationPlan = 2,
                           SetOffPlan = 0,
                           SetOffWithBallPlan = 0,
                           Course = 1,
                           Semester = 2
                       },
                       new DisciplinePlan
                       {
                           CodePlan = "Б1.В.1",
                           Discipline = "Правовые основы прикладной информатики",
                           LecturePlan = 18,
                           LaboratoryWorkPlan = 0,
                           PracticalExercisesPlan = 36,
                           ExaminationPlan = 0,
                           SetOffPlan = 1,
                           SetOffWithBallPlan = 0,
                           Course = 1,
                           Semester = 1
                       },
                       new DisciplinePlan
                       {
                           CodePlan = "Б2.Б.1",
                           Discipline = "Математика",
                           LecturePlan = 36,
                           LaboratoryWorkPlan = 0,
                           PracticalExercisesPlan = 36,
                           ExaminationPlan = 0,
                           SetOffPlan = 1,
                           SetOffWithBallPlan = 0,
                           Course = 1,
                           Semester = 1
                       },
                        new DisciplinePlan
                       {
                           CodePlan = "Б2.Б.1",
                           Discipline = "Математика",
                           LecturePlan = 36,
                           LaboratoryWorkPlan = 0,
                           PracticalExercisesPlan = 36,
                           ExaminationPlan = 2,
                           SetOffPlan = 0,
                           SetOffWithBallPlan = 0,
                           Course = 1,
                           Semester = 2
                       }
            };
        }

    }
}
