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
    public class TaskController : Controller
    {
        private ProjectManagerContext db = new ProjectManagerContext();

        public ActionResult List()
        {
            List<Task> task = db.Task.SqlQuery("Select * from Tasks").ToList();
            return View(task);
        }


        public ActionResult New()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Add(string TaskDesc, string DateCreated, string DateFinished, string Status, int ProjectId)
        {
            string query = "insert into Tasks (TaskDesc, DateCreated, DateFinished, Status, ProjectId) values (@TaskDesc, @DateCreated, @DateFinished, @Status, @ProjectId)";

            SqlParameter[] sqlparams = new SqlParameter[5];
            sqlparams[0] = new SqlParameter("@TaskDesc", TaskDesc);
            sqlparams[1] = new SqlParameter("@DateCreated", DateCreated);
            sqlparams[2] = new SqlParameter("@DateFinished", DateFinished);
            sqlparams[3] = new SqlParameter("@Status", Status);
            sqlparams[4] = new SqlParameter("@ProjectId", ProjectId);


            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }



        public ActionResult Show(int id)
        {
            //find data about the task
            string main_query = "select * from Tasks where TaskId = @id";
            SqlParameter pk_parameter = new SqlParameter("@id", id);
            Task Task = db.Task.SqlQuery(main_query, pk_parameter).FirstOrDefault();

            //find data about all users that task has assigned to it(through id)
            //remember to check the generated column names! (SQL Server Object Explorer)
            string aside_query = "select * from Users inner join TaskUsers on User.UserId = TaskUsers.User_UserId where TaskUsers.Task_TaskId=@id";
            SqlParameter fk_parameter = new SqlParameter("@id",id);
            List<User> TaskUsers = db.User.SqlQuery(aside_query, fk_parameter).ToList();

            string all_user_query = "select * from Users";
            List<User> AllUsers = db.User.SqlQuery(all_user_query).ToList();

            ShowTasks viewmodel = new ShowTasks();
            viewmodel.Task = Task;
            viewmodel.User = TaskUsers;
            viewmodel.All_User = AllUsers;

            return View(viewmodel);
        }


        // this method inserts into the bridging table
        // just need the User Id
        [HttpPost]
        public ActionResult AttachUser(int id, int UserId)
        {
            //Debug.WriteLine("task id is"+id+" and user id is "+UserId);

            //first, check if that pet is already owned by that owner
            string check_query = "select * from Users inner join TaskUsers on TaskUsers.User_UserId = User.UserId where UserId_UserId=@UserId and Task_TaskId=@id";
            SqlParameter[] check_params = new SqlParameter[2];
            check_params[0] = new SqlParameter("@id", id);
            check_params[1] = new SqlParameter("@UserId", UserId);
            List<User> User = db.User.SqlQuery(check_query, check_params).ToList();
            //only execute add if the pet isn't already owned by that owner!
            if (User.Count <= 0) { 


                //first id above is the ownerid, then the petid
                string query = "insert into TaskUsers (User_UserId, Task_TaskId) values (@UserId, @id)";
                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@id", id);
                sqlparams[1] = new SqlParameter("@UserId",UserId);


                db.Database.ExecuteSqlCommand(query, sqlparams);
            }

            return RedirectToAction("Show/"+id);

        }


        //URL: /Task/DetachUser/id?UserId=pid
        [HttpGet]
        public ActionResult DetachUser(int id, int UserId)
        {
            //This method is a more rare instance where two items are passed through a GET URL
            //Debug.WriteLine("task id is" + id + " and user is " + UserId);

            string query = "delete from TaskUsers where User_UserId=@UserId and Task_TaskId=@id";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@UserId",UserId);
            sqlparams[1] = new SqlParameter("@id",id);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("Show/"+id);
        }

        //
        public ActionResult Update(int id)
        {
            string query = "select * from Tasks where TaskId = @id";
            SqlParameter parameter = new SqlParameter("@id", id);
            Task Task = db.Task.SqlQuery(query, parameter).FirstOrDefault();

            return View(Task);
        }


        [HttpPost]
        public ActionResult Update(int id, string TaskDesc, string DateCreated, string DateFinished, string Status, int ProjectId)
        {
            string query = "update Tasks set TaskDesc=@TaskDesc, DateCreated=@DateCreated, DateFinished=@DateFinished, Status=@Status, ProjectId=@ProjectId where TaskId = @id";

            SqlParameter[] sqlparams = new SqlParameter[6];
            
            sqlparams[0] = new SqlParameter("@TaskDesc", TaskDesc);
            sqlparams[1] = new SqlParameter("@DateCreated", DateCreated);
            sqlparams[2] = new SqlParameter("@DateFinished", DateFinished);
            sqlparams[3] = new SqlParameter("@Status", Status);
            sqlparams[4] = new SqlParameter("@ProjectId", ProjectId);
            sqlparams[5] = new SqlParameter("@id", id);


            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }

        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from Tasks where TaskId=@id";
            SqlParameter parameter = new SqlParameter("@id", id);
            Task Task = db.Task.SqlQuery(query, parameter).FirstOrDefault();
            return View(Task);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from Tasks where TaskId=@id";
            SqlParameter parameter = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, parameter);



            return RedirectToAction("List");
        }

    }
}