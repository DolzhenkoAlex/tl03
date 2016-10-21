using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight2.Helper
{
    /// <summary>
    /// Параметры нагрузки по дисциплинам
    /// </summary>
    public class LoadDiscipline
    {
        public decimal Lecture {get;set;}
        public decimal PracticalExercises { get; set; }
        public decimal LaboratoryWork { get; set; }
        public decimal Examination { get; set; }
        public decimal SetOff { get; set; }
        public decimal SetOffWithBall { get; set; }
        public decimal Consultation { get; set; }
        public decimal CourseProject { get; set; }
        public decimal CourseWorkt { get; set; }
        public decimal Practical { get; set; }
        public decimal GraduationDesign { get; set; }
        public decimal ControlWork { get; set; }
        public decimal Gac { get; set; }
        public decimal Dot { get; set; }
        public decimal ScientificResearchWork { get; set; }
        public decimal Others { get; set; }
        public decimal SumLoad { get; set; }
    }
}
