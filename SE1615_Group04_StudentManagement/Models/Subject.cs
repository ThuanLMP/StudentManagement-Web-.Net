using System;
using System.Collections.Generic;

#nullable disable

namespace SE1615_Group04_StudentManagement.Models
{
    public partial class Subject
    {
        public Subject()
        {
            CourseClasses = new HashSet<CourseClass>();
            Exercices = new HashSet<Exercice>();
            RegisterSubjects = new HashSet<RegisterSubject>();
        }

        public string Subjectid { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CourseClass> CourseClasses { get; set; }
        public virtual ICollection<Exercice> Exercices { get; set; }
        public virtual ICollection<RegisterSubject> RegisterSubjects { get; set; }
    }
}
