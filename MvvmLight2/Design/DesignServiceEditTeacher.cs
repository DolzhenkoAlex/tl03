using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    /// <summary>
    /// Класс данныхд по преподавателю, используемых при проектировании приложения
    /// в режиме редактирования данных
    /// </summary>
    public class DesignServiceEditTeacher
    {
        public Teacher Teacher {get; private set;}

        public DesignServiceEditTeacher()
        {
            Teacher = new Teacher()
            {
                Id = 2,
                IdChair = 1,
                LastName = "Долженко",
                FirstName = "Алексей",
                SecondName = "Иванович",
                IdPost = 6,
                IdTypeEmployment = 2,
                Rate = 1,
                DictPost = new DictPost()
                {
                    Post = "Профессор"
                },
                DictTypeEmployment = new DictTypeEmployment()
                {
                    TypeOfEmployment = "основная"
                }
            };
        }

        
    }
}
