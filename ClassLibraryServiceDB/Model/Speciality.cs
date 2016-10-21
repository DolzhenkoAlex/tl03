using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryServiceDB.Model
{
    public class Speciality
    {
        string numberString;
        string fullName;

        public string NumberString
        {
            get { return numberString; }
            set { numberString = value; }
        }
        
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }

        public Speciality(string numberString, string fullName)
        {
            this.numberString = numberString;
            this.fullName = fullName;
        }
    }
}
