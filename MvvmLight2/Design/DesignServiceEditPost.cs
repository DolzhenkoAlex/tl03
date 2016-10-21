using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Design
{
    public class DesignServiceEditPost
    {
        public DictPost DictPost { get; private set; }

        public DesignServiceEditPost()
        {
            DictPost = new DictPost
            {
                Post = "ассистент",

            };
        }
    }
}
