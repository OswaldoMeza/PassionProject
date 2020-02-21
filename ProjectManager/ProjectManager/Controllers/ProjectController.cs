using System;
using System.Collections.Generic;
using System.Data;
//required for SqlParameter class
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using System.IO;
using ProjectManager.Data;
using ProjectManager.Models;
using ProjectManager.Models.ViewModels;

namespace ProjectManager.Controllers
{
    public class ProjectController : Controller
    {
        private ProjectManagerContext db = new ProjectManagerContext();

        public ActionResult List()
        {
            List<Project> project = db.Project.SqlQuery("Select * from Projects").ToList();
            return View(project);
        }


        public ActionResult New()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Add(string ProjectDesc, string DateCreated, string DateFinished, string Status)
        {
            string query = "insert into Projects (ProjectDesc, DateCreated, DateFinished, Status) values (@ProjectDesc, @DateCreated, @DateFinished, @Status)";

            SqlParameter[] sqlparams = new SqlParameter[4];
            sqlparams[0] = new SqlParameter("@ProjectDesc", ProjectDesc);
            sqlparams[1] = new SqlParameter("@DateCreated", DateCreated);
            sqlparams[2] = new SqlParameter("@DateFinished", DateFinished);
            sqlparams[3] = new SqlParameter("@Status", Status);


            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }



        public ActionResult Show(int id)
        {
            //find data about the Project
            string main_query = "select * from Projects where ProjectId = @id";
            SqlParameter pk_parameter = new SqlParameter("@id", id);
            Project Project = db.Project.SqlQuery(main_query, pk_parameter).FirstOrDefault();

            //find data about all tasks that Projects has assigned to it(through id)

            string aside_query = "select * from Tasks inner join Projects on Task.ProjectId = Project.ProjectId where Task.TaskId=@id";
            SqlParameter fk_parameter = new SqlParameter("@id",id);
            List<Task> task = db.Task.SqlQuery(aside_query, fk_parameter).ToList();

            string all_task_query = "select * from Tasks";
            List<Task> AllTask = db.Task.SqlQuery(all_task_query).ToList();

            ShowProjects viewmodel = new ShowProjects();
            viewmodel.Project = Project;
            viewmodel.Task = task;
            viewmodel.All_Task = AllTask;

            return View(viewmodel);
        }

        [HttpPost]
        public ActionResult Update(int id, string ProjectDesc, string DateCreated, string DateFinished, string Status)
        {
            string query = "update Projects set ProjectDesc=@ProjectDesc, DateCreated=@DateCreated, DateFinished=@DateFinished, Status=@Status where TaskId = @id";

            SqlParameter[] sqlparams = new SqlParameter[5];
            
            sqlparams[0] = new SqlParameter("@ProjectDesc", ProjectDesc);
            sqlparams[1] = new SqlParameter("@DateCreated", DateCreated);
            sqlparams[2] = new SqlParameter("@DateFinished", DateFinished);
            sqlparams[3] = new SqlParameter("@Status", Status);
            sqlparams[4] = new SqlParameter("@id", id);


            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }

        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from Projects where ProjectId=@id";
            SqlParameter parameter = new SqlParameter("@id", id);
            Project Project = db.Project.SqlQuery(query, parameter).FirstOrDefault();
            return View(Project);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from Projects where ProjectId=@id";
            SqlParameter parameter = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, parameter);



            return RedirectToAction("List");
        }

    }
}