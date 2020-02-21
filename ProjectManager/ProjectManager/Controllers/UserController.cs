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
    public class UserController : Controller
    {

        private ProjectManagerContext db = new ProjectManagerContext();

        // GET: User
        public ActionResult List(string usersearchkey)
        {

            string query = "Select * from Users";

            if (usersearchkey != "")
            {
                query = query + " where UserFname like '%" + usersearchkey + "%'"/* or UserLname like '%" + usersearchkey + "%'"*/;
                //Debug.WriteLine("The query is" + query);
            }
            List<User> User = db.User.SqlQuery(query).ToList();
            return View(User);

        }

        // GET: User/Details
        public ActionResult Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.User.Find(id); //EF 6 technique
            User User = db.User.SqlQuery("select * from Users where UserId=@UserId", new SqlParameter("@UserId", id)).FirstOrDefault();
            if (User == null)
            {
                return HttpNotFound();
            }

            //Use this query to show tasks on the User show page
            string query = "select * from Tasks inner join TaskUsers on Tasks.TaskId = TaskUsers.Task_TaskId where User_UserId = @id";
            SqlParameter parameter = new SqlParameter("@id", id);
            List<Task> TaskUsers = db.Task.SqlQuery(query, parameter).ToList();


            ShowUsers viewmodel = new ShowUsers();
            viewmodel.User = User;
            viewmodel.Task = TaskUsers;


            return View(viewmodel);
        }

        //THE [HttpPost] Means that this method will only be activated on a POST form submit to the following URL
        //URL: /User/Add
        [HttpPost]
        public ActionResult Add(string UserFname, string UserLname, string Email, string Phone, int RoleId)
        {

            string query = "insert into Users (UserFname, UserLname, Email, Phone, RoleId) values (@UserFname, @UserLname, @Email, @Phone, @RoleId)";
            SqlParameter[] sqlparams = new SqlParameter[5]; //0,1,2,3,4 pieces of information to add
            //each piece of information is a key and value pair
            sqlparams[0] = new SqlParameter("@UserFname", UserFname);
            sqlparams[1] = new SqlParameter("@UserLname", UserLname);
            sqlparams[2] = new SqlParameter("@Email", Email);
            sqlparams[3] = new SqlParameter("@Phone", Phone);
            sqlparams[4] = new SqlParameter("@RoleId", RoleId);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }


        public ActionResult New()
        {

            List<Role> role = db.Role.SqlQuery("select * from Roles").ToList();

            return View(role);
        }

        public ActionResult Update(int id)
        {
            //need information about a particular pet
            User selecteduser = db.User.SqlQuery("select * from Users where Userid = @id", new SqlParameter("@id", id)).FirstOrDefault();
            List<Role> roles= db.Role.SqlQuery("select * from Roles").ToList();

            UpdateUsers UpdateUsersViewModel = new UpdateUsers();
            UpdateUsersViewModel.User = selecteduser;
            UpdateUsersViewModel.Role = roles;

            return View(UpdateUsersViewModel);
        }

        [HttpPost]
        public ActionResult Update(int id, string UserFname, string UserLname, string Email, string Phone, int RoleId/*, HttpPostedFileBase UserPic*/)
        {
            //start off with assuming there is no picture

            //int haspic = 0;
            //string petpicextension = "";
            ////checking to see if some information is there
            //if (PetPic != null)
            //{
            //    Debug.WriteLine("Something identified...");
            //    //checking to see if the file size is greater than 0 (bytes)
            //    if (PetPic.ContentLength > 0)
            //    {
            //        Debug.WriteLine("Successfully Identified Image");
            //        //file extensioncheck taken from https://www.c-sharpcorner.com/article/file-upload-extension-validation-in-asp-net-mvc-and-javascript/
            //        var valtypes = new[] { "jpeg", "jpg", "png", "gif" };
            //        var extension = Path.GetExtension(PetPic.FileName).Substring(1);

            //        if (valtypes.Contains(extension))
            //        {
            //            try
            //            {
            //                //file name is the id of the image
            //                string fn = id + "." + extension;

            //                //get a direct file path to ~/Content/Pets/{id}.{extension}
            //                string path = Path.Combine(Server.MapPath("~/Content/Pets/"), fn);

            //                //save the file
            //                PetPic.SaveAs(path);
            //                //if these are all successful then we can set these fields
            //                haspic = 1;
            //                petpicextension = extension;

            //            }
            //            catch (Exception ex)
            //            {
            //                Debug.WriteLine("Pet Image was not saved successfully.");
            //                Debug.WriteLine("Exception:" + ex);
            //            }



            //        }
            //    }
            //}

            //Add this to the Query once Picture implementation is completed
            //, HasPic=@haspic, PicExtension=@petpicextension
            string query = "update Users set UserFname=@UserFname, UserLname=@UserLname, Email=@Email, Phone=@Phone, RoleId=@RoleId where UserId=@UserId";
            SqlParameter[] sqlparams = new SqlParameter[6];
            sqlparams[0] = new SqlParameter("@UserFname", UserFname);
            sqlparams[1] = new SqlParameter("@UserLname", UserLname);
            sqlparams[2] = new SqlParameter("@Email", Email);
            sqlparams[3] = new SqlParameter("@Phone", Phone);
            sqlparams[4] = new SqlParameter("@RoleId", RoleId);
            sqlparams[5] = new SqlParameter("@UserId", id);
            //sqlparams[6] = new SqlParameter("@HasPic", haspic);
            //sqlparams[7] = new SqlParameter("@petpicextension", petpicextension);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            //logic for updating the User in the database goes here
            return RedirectToAction("List");
        }

        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from Users where UserId = @id";
            SqlParameter param = new SqlParameter("@id", id);
            User selecteduser = db.User.SqlQuery(query, param).FirstOrDefault();

            return View(selecteduser);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from Users where UserId = @id";
            SqlParameter param = new SqlParameter("@UserId", id);
            db.Database.ExecuteSqlCommand(query, param);

            return RedirectToAction("List");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

