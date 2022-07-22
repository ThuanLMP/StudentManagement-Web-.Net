using System;
using System.Collections.Generic;

#nullable disable

namespace SE1615_Group04_StudentManagement.Models
{
    public partial class Account
    {
        public Account()
        {
            Students = new HashSet<Student>();
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public int? Rid { get; set; }

        public virtual Role RidNavigation { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
