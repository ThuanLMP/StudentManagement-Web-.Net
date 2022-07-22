using System;
using System.Collections.Generic;

#nullable disable

namespace SE1615_Group04_StudentManagement.Models
{
    public partial class Class
    {
        public Class()
        {
            CourseClasses = new HashSet<CourseClass>();
            Students = new HashSet<Student>();
        }

        public string Classid { get; set; }
        public string Name { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime Enddate { get; set; }

        public virtual ICollection<CourseClass> CourseClasses { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
