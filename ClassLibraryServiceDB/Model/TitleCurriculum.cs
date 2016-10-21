using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryServiceDB.Model
{
    // Класс данных титула учебного плана
    public class TitleCurriculum
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Year { get; set; }
        public string Chair { get; set; }
        public string ChairName { get; set; }
        public string CodeSpeciality { get; set; }
        public string Speciality { get; set; }
        public string Profile { get; set; }
        public string Qualification { get; set; }
        public string EducationForm { get; set; }
        public bool Course1 { get; set; }
        public bool Course2 { get; set; }
        public bool Course3 { get; set; }
        public bool Course4 { get; set; }
        public bool Course5 { get; set; }
        public bool Course6 { get; set; }
        public string DataApproval { get; set; }
        public string DurationStudy { get; set; }
        public string Protocol { get; set; }
        public string Faculty { get; set; }

        public List<Discipline> Disciplines;

        public TitleCurriculum() 
        {
            Course1 = false;
            Course2 = false;
            Course3 = false;
            Course4 = false;
            Course5 = false;
            Course6 = false;
        }

        public TitleCurriculum(string name, string year, string chair, string codeSpeciality,
            string speciality, string profile, string qualification, string dataApproval,
            string durationStudy, string protocol, string faculty)
        {
            this.Name = name;
            this.Year = year;
            this.Chair = chair;
            this.CodeSpeciality = codeSpeciality;
            this.Speciality = speciality;
            this.Profile = profile;
            this.Qualification = qualification;
            this.DataApproval = dataApproval;
            this.DurationStudy = durationStudy;
            this.Protocol = protocol;
            this.Faculty = faculty;
        }

    }
}
