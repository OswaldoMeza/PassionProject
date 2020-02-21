using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.Models.ViewModels
{
    public class ShowProjects
    {

        //one individual Project
        public virtual Project Project {get;set;}
        //a list for every Task assigned to them
        public List<Task> Task { get; set; }

        public List<Task> All_Task { get; set; }

    }
}