using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectManager.Data
{
    public class ProjectManagerContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ProjectManagerContext() : base("name=ProjectManagerContext")
        {
        }

        public System.Data.Entity.DbSet<ProjectManager.Models.User> User { get; set; }

        public System.Data.Entity.DbSet<ProjectManager.Models.Role> Role { get; set; }
        public System.Data.Entity.DbSet<ProjectManager.Models.Task> Task { get; set; }
        public System.Data.Entity.DbSet<ProjectManager.Models.Project> Project { get; set; }

    }
}
