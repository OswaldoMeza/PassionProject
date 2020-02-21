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
    public class Project
    {

        [Key]
        public int ProjectId { get; set; }
        public string ProjectDesc { get; set; }
        public string DateCreated { get; set; }
        public string DateFinished { get; set; }
        public string Status { get; set; }

        //Haven't decided if it's necessary to have a connection between Users and projects
        //public ICollection<User> Users { get; set; }
    }
}