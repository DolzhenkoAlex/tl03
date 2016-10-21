using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    
    /// <summary>
    /// Класс данных по факультетам, используемых при проектировании приложения
    /// </summary>
    public class DesignServiceTeachers
    {

        public ObservableCollection<Teacher> Teachers { get; private set; }
        //public ObservableCollection<Chair> Chairs { get; private set; }

       public DesignServiceTeachers()
       {

           //Chairs = new ObservableCollection<Chair>
           //{
           //    new Chair
           //    {
           //         CodeChair = 22,
           //         NameChair = "Информационных систем и прикладной информатики",
           //         ShortNameChair = "ИСПИ"
           //     }
           //};
           
           Teachers = new ObservableCollection<Teacher>
           {
               new Teacher
               {
                    FirstName = "Ирина",
                    LastName = "Шполянская",
                    SecondName = "Юрьевна",
                    Rate = 1,
                    DictPost = new DictPost {Post = "Заведующий кафедрой"},
                    DictTypeEmployment = new DictTypeEmployment {TypeOfEmployment = "основная"}
                },
                new Teacher 
                {
                    LastName = "Долженко",
                    FirstName = "Алексей",
                    SecondName = "Иванович",
                    Rate = 1,
                    DictPost = new DictPost {Post = "Профессор"},
                    DictTypeEmployment = new DictTypeEmployment {TypeOfEmployment = "основная"}
                },
                new Teacher 
                {
                    LastName = "Орлова",
                    FirstName = "Надежда",
                    SecondName = "Владимировна",
                    Rate = 1,
                    DictPost = new DictPost {Post = "Доцент"},
                    DictTypeEmployment = new DictTypeEmployment {TypeOfEmployment = "основная"}
                },

                new  Teacher 
                {
                    LastName = "Глушенко",
                    FirstName = "Сергей",
                    SecondName = "Андреевич",
                    Rate = 1,
                    DictPost = new DictPost {Post = "Ассистент"},
                    DictTypeEmployment = new DictTypeEmployment {TypeOfEmployment = "совместитель"}
                },

                new Teacher 
                {
                    LastName = "Воробьева",
                    FirstName = "Анна",
                    SecondName = "Михайловна",
                    Rate = 1,
                        DictPost = new DictPost {Post = "Ассистент"},
                    DictTypeEmployment = new DictTypeEmployment {TypeOfEmployment = "основная"}
                }
           };
       }
    }
}