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
    /// Класс данных по кафедрам, используемых при проектировании приложения
   /// </summary>
 
   public class DesignServiceChair
    {
       public ObservableCollection<Chair> Chairs { get; private set; }

       public DesignServiceChair()
       {
           Chairs = new ObservableCollection<Chair>
           {
               new Chair
                        {
                            CodeChair = 22,
                            NameChair = "Информационных систем и прикладной информатики",
                            ShortNameChair = "ИСПИ"
                        },
                        
                        new Chair
                        {
                            CodeChair = 24,
                            NameChair = "Фундаментальная и прикладная математика",
                            ShortNameChair = "ФПМ"
                        },
                        
                        new Chair
                        {
                            CodeChair = 35,
                            NameChair = "Информационные технологии и защита информации",
                            ShortNameChair = "ИТиЗИ",
                        }
                    };
           }
       }
}

