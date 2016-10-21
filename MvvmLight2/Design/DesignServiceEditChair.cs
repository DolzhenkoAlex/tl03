using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    public class DesignServiceEditChair
    {
        public Chair Chair { get; private set; }

        public DesignServiceEditChair()
        {
            Chair = new Chair
            {
                CodeChair = 22,
                NameChair = "Компьютерных технологий и прикладной информатики",
                ShortNameChair = "КТПИ"
            };
 
        }
    }
}
