using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.Models.ViewModels
{
    public class ShowTasks
    {

        //one individual Task
        public virtual Task Task {get;set;}
        //a list for every user assigned to them
        public List<User> User { get; set; }

        //a SEPARATE list for representing the ADD of an User to a Task,

        public List<User> All_User { get; set; }

    }
}