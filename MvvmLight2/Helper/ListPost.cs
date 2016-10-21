using System.Collections.ObjectModel;
using System.Linq;
using MvvmLight2.Model;

namespace MvvmLight2.Helper
{
    /// <summary>
    /// Справочник должностей
    /// </summary>
    public class ListPost : ObservableCollection<DictPost>
    {
        public ListPost()
        {
            using (var context = new DBTeachingLoadEntities())
            {
                var QueryPost = from post in context.DictPosts
                                where post.StatusDel == true
                                orderby post.Post
                                select post;
                foreach (DictPost p in QueryPost)
                    this.Add(p);
            }
        }
    }
}
