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
    public class User
    {

        [Key]
        public int UserId { get; set; }
        public string UserFname { get; set; }
        public string UserLname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        //public string Test { get; set; }

        //////////potential user picture added///////
        //public int HasPic { get; set; }
        //public string PicExtension { get; set; }


        //Representing the Many in (One User to Many Roles)        
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        //Representing the "Many" in (Many User to many Tasks)
        public ICollection<Task> Tasks { get; set; }
        
    }
}