using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.Models.ViewModels
{
    public class UpdateTasks
    {
        //when we need to update a Task
        //we need the Task info as well as a list of Projects

        public Task Task { get; set; }
        public List<Project> Project { get; set; }
    }
}