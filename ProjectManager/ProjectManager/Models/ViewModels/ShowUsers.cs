using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.Models.ViewModels
{
    public class ShowUsers
    {

        //one individual owner
        public virtual User User {get;set;}
        //Here will be a list of every task they are assigned
        public List<Task> Task { get; set; }

        //a SEPARATE list for representing the ADD of an user to a task,
        //i.e. show a dropdownlist of all tasks, with assign task button.
        public List<Task> all_tasks { get; set; }

    }
}