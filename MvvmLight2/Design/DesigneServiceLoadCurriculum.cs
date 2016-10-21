using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    public class DesigneServiceLoadCurriculum
    {
        public ObservableCollection<Curriculum> Curriculums { get; private set; }

        public DesigneServiceLoadCurriculum()
        {
            Curriculums = new ObservableCollection<Curriculum>()
            {
                new Curriculum()
                {
                    DataApproval = new DateTime(2011, 02, 15),
                    Protocol = "123",
                    DurationStudy = "4г 5м",
                    Course1 = true,
                    Course2 = true
                }
            };
        }
    }
}
