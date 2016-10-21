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
    /// Класс данных используемых при формировании потоков лекция, используемых при проектировании приложения
    /// </summary>
    public class DesignServiceCreateStream
    {
        public ObservableCollection<TeachingLoad> DisciplineStream { get; private set; }

        public DesignServiceCreateStream ()
        {
            DisciplineStream = new ObservableCollection<TeachingLoad>
            {
                new TeachingLoad {
                Id =1,
                Stream = 2,
                StreamLab = 3,
                StreamPract = 4,
                Discipline ="Информатика",
                NameGroup ="ПЕР-711",
                Course = 1,
                Semester =2,
                Lecture = 36,
                LaboratoryWork = 18,
                PracticalExercises = 36,
                FormEducation = "очная"},
                new TeachingLoad {
                Id =2,
                Stream = 2,
                Discipline ="Информатика",
                NameGroup ="ПЕР-712",
                Course = 1,
                StreamLab = 3,
                StreamPract = 4,
                Semester =2,
                Lecture = 36,
                LaboratoryWork = 18,
                PracticalExercises = 36,
                FormEducation = "очная"},
                new TeachingLoad {
                Id =3,
                Stream = 2,
                Discipline ="Информатика",
                NameGroup ="ПЕР-713",
                 StreamLab = 3,
                StreamPract = 4,
                Course = 1,
                Semester =2,
                Lecture = 36,
                LaboratoryWork = 18,
                PracticalExercises = 36,
                FormEducation = "очная"}
            };
    
    }
   
    
    }
}
