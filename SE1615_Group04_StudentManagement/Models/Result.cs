using System;
using System.Collections.Generic;

#nullable disable

namespace SE1615_Group04_StudentManagement.Models
{
    public partial class Result
    {
        public string Studentid { get; set; }
        public string Name { get; set; }
        public double Marks { get; set; }

        public virtual Student Student { get; set; }
    }
}
