using System;
using System.Collections.Generic;

#nullable disable

namespace SE1615_Group04_StudentManagement.Models
{
    public partial class Role
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
        }

        public int Rid { get; set; }
        public string Rname { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}
