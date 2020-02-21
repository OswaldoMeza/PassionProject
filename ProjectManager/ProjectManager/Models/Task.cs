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
    public class Task
    {

        [Key]
        public int TaskId { get; set; }

        public string TaskDesc { get; set; }
        public string DateCreated { get; set; }
        public string DateFinished { get; set; }
        public string Status { get; set; }

        //Representing the "One" in (One Project to many Tasks)
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project{ get; set; }


        //Representing the "Many" in (Many Tasks to many Users)
        public ICollection<User> Users { get; set; }
    }
}