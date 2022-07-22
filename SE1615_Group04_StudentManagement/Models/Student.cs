using System;
using System.Collections.Generic;

#nullable disable

namespace SE1615_Group04_StudentManagement.Models
{
    public partial class Student
    {
        public Student()
        {
            RegisterSubjects = new HashSet<RegisterSubject>();
            Results = new HashSet<Result>();
        }

        public string Username { get; set; }
        public string Studentid { get; set; }
        public string Classid { get; set; }
        public string Name { get; set; }
        public string Gmail { get; set; }

        public virtual Class Class { get; set; }
        public virtual Account UsernameNavigation { get; set; }
        public virtual ICollection<RegisterSubject> RegisterSubjects { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}
