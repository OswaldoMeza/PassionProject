using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Install  entity framework 6 on Tools > Manage Nuget Packages > Microsoft Entity Framework (ver 6.4)
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.Models
{
    public class Role
    {

        [Key]
        public int RoleId { get; set; }

        public string RoleDesc { get; set; }

        //Representing the "Many" in (One Species to many Pets)
        public ICollection<User> Users { get; set; }
    }
}