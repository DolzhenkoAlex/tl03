using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    public class DesignServiseEditUniversity
    {
        public University University { get; private set; }
        
        public DesignServiseEditUniversity()
        {
            University = new University
            {
                Name = "Ростовский государственный экономический университет (РИНХ)",
                ShortName = "РГЭУ (РИНХ)"
            };
        }
    }
}
