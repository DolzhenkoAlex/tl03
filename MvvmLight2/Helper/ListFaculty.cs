using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLight2.Model;

namespace MvvmLight2.Helper
{
    public class ListFaculty: ObservableCollection<Faculty>
    {
        public ListFaculty()
        {
            //AppSettingsReader ar = new AppSettingsReader();
            //int idUniver = (int)ar.GetValue("IdUniversity", typeof(int));

            int idUniver = 1;

            using (var context = new DBTeachingLoadEntities())
            {
                var QueryFaculty = from fac in context.Faculties
                                   where (fac.IdUniversity == idUniver) && (fac.StatusDel == true)
                                   orderby fac.Code
                                   select fac;
                foreach (var f in QueryFaculty)
                    this.Add(f);
            }
        }
    }
}

