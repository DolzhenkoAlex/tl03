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
    class DesignServiceFaculty
    {
        public ObservableCollection<Faculty> Faculties { get; private set; }

        public DesignServiceFaculty()
        {
            Faculties = new ObservableCollection<Faculty>
            {
                new Faculty
                {
                    Id = 2,
                    IdUniversity = 1,
                    Code = 2,
                    Name = "Менеджмента и предпринимательства",
                    ShortName = "МП"
                },
                        
                new Faculty
                {
                    Id = 1,
                    IdUniversity = 1,
                    Code = 3,
                    Name = "Компьютерных технологий и информационной безопасности",
                    ShortName = "КТИБ"
                },

                new Faculty
                {
                    Id = 3,
                    IdUniversity = 1,
                    Code = 4,
                    Name = "Торгового дела",
                    ShortName = "ТД"
                }
            };
        }

    }
}