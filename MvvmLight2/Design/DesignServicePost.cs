using MvvmLight2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight2.Design
{
    public class DesignServicePost
    {
        public ObservableCollection<DictPost> Posts { get; private set; }

        public DesignServicePost()
        {
            Posts = new ObservableCollection<DictPost>
           {
               new DictPost
                        {
                            Post = "заведующий кафедрой",
                        },
                        
                        new DictPost
                        {
                            Post = "старший преподаватель",
                        },
                        
                        new DictPost
                        {
                            Post = "профессор",
                        }
                    };
        }
    }
}