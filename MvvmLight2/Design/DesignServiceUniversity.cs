using System;
using System.Collections.ObjectModel;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    /// <summary>
    /// Класс данныхд по университетам/филиалам, используемых при проектировании приложения
    /// </summary>
    public class DesignServiceUniversity
    {
        public ObservableCollection<University> Universities { get; private set; }

        public DesignServiceUniversity()
        {
            this.GetData(
                (universities, error) =>
                {
                    if (error != null)
                    {
                        // Report error here
                        return;
                    }

                    Universities = universities;
                });
        }


        public void GetData(Action<ObservableCollection<University>, Exception> callback)
        {
            ObservableCollection<University> universities = new ObservableCollection<University>()
            {
                new University
                {
                    Id = 1,
                    Name = "Ростовский государственный экономический университет (РИНХ)",
                    ShortName = "РГЭУ (РИНХ)",
                    //Faculties = new System.Data.Objects.DataClasses.EntityCollection<Faculty>()
                    //{
                    //    new Faculty
                    //    {
                    //        Id = 2,
                    //        IdUniversity = 1,
                    //        Code = 2,
                    //        Name = "Менеджмента и предпринимательства",
                    //        ShortName = "МП"
                    //    },
                        
                    //    new Faculty
                    //    {
                    //        Id = 1,
                    //        IdUniversity = 1,
                    //        Code = 3,
                    //        Name = "Компьютерных технологий и информационной безопасности",
                    //        ShortName = "КТИБ"
                    //    },

                    //    new Faculty
                    //    {
                    //        Id = 3,
                    //        IdUniversity = 1,
                    //        Code = 2,
                    //        Name = "Торгового дела",
                    //        ShortName = "ТД"
                    //    }
                    //}
                },
                
                
                new University
                {
                    Id = 3,
                    Name = "Гуковский институт экономики и права - филиал «РГЭУ (РИНХ)»",
                    ShortName = "КФ РГЭУ",
                    //Faculties = new System.Data.Objects.DataClasses.EntityCollection<Faculty>()
                    //{
                    //    new Faculty
                    //    {
                    //        Code = 28,
                    //        Name = "Общенаучный",
                    //        ShortName = "ОН"
                    //    },
                        
                    //    new Faculty
                    //    {
                    //        Code = 30,
                    //        Name = "Экономический",
                    //        ShortName = "Эк"
                    //    },

                    //    new Faculty
                    //    {
                    //        Code = 32,
                    //        Name = "Финансовый",
                    //        ShortName = "ФФ",
                    //    }
                    //}
                },
                
                new University
                {
                    Id = 2,
                    Name = "Азовский филиал РГЭУ",
                    ShortName = "АФ РГЭУ",
                    //Faculties = new System.Data.Objects.DataClasses.EntityCollection<Faculty>()
                    //{
                    //    new Faculty
                    //    {
                    //        Code = 28,
                    //        Name = "Общественных наук",
                    //        ShortName = "ОН"
                    //    },
                        
                    //    new Faculty
                    //    {
                    //        Code = 30,
                    //        Name = "Информатизации",
                    //        ShortName = "ИН"
                    //    },

                    //    new Faculty
                    //    {
                    //        Code = 32,
                    //        Name = "Финансово-экономический",
                    //        ShortName = "ФЭ",
                    //    }
                    //}
                }
            };
            callback(universities, null);
        }
    }
}
