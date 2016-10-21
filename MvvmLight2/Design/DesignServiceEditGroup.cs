using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    public class DesignServiceEditGroup
    {
        public StudentGroup Group { get; private set; }

        public DesignServiceEditGroup()
        {
            Group = new StudentGroup
            {
                NameGroup= "112-МЕН",
                CountStudent = 25,
                CountSubgroup = 2,
                Course = 1,
                CountComStudent = 1,
                CountForeignStudent = 2,
            };
        }
    }
}
