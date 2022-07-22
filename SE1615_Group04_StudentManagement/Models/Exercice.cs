using System;
using System.Collections.Generic;

#nullable disable

namespace SE1615_Group04_StudentManagement.Models
{
    public partial class Exercice
    {
        public string Name { get; set; }
        public string Subjectid { get; set; }
        public string Percentage { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
