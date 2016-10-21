using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    /// <summary>
    /// 
    /// </summary>
    public class DesigneServiceEditCurriculum
    {
        public Curriculum Curriculum { get; private set; }

        public DesigneServiceEditCurriculum()
        {

            Curriculum = new Curriculum()
            {
                DataApproval = new DateTime(2011, 02, 15),
                Protocol = "123",
                DurationStudy = "4г",
                Course1 = true,
                Course2 = true
            };
        }
    }
}
