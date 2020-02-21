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
    public class RoleController : Controller
    {
        private ProjectManagerContext db = new ProjectManagerContext();
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }

        //TODO: Each line should be a separate method in this class
        // List
        public ActionResult List(string rolesearchkey)
        {
            //Debug.WriteLine("The parameter is "+rolesearchkey);

            string query = "Select * from Roles";
            if (rolesearchkey!="")
            {
                query = query + " where RoleDesc like '%"+rolesearchkey+"%'";
            }

            List<Role> myrole = db.Role.SqlQuery(query).ToList();

            return View(myrole);
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(string RoleDesc)
        {
            string query = "insert into Roles (RoleDesc) values (@RoleDesc)";
            SqlParameter parameter = new SqlParameter("@RoleDesc", RoleDesc);

            db.Database.ExecuteSqlCommand(query, parameter);
            return RedirectToAction("List");
        }

        public ActionResult Show(int id)
        {
            string query = "select * from Roles where RoleId = @id";
            SqlParameter parameter = new SqlParameter("@id", id);
            Role selectedrole = db.Role.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedrole);
        }

        public ActionResult Update(int id)
        {
            string query = "select * from Roles where RoleId = @id";
            SqlParameter parameter = new SqlParameter("@id", id);
            Role selectedrole = db.Role.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedrole);
        }
        [HttpPost]
        public ActionResult Update(int id, string RoleDesc)
        {
            string query = "update Roles set RoleDesc = @RoleDesc where RoleId = @id";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@RoleDesc", RoleDesc);
            sqlparams[1] = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }

        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from Roles where RoleID=@id";
            SqlParameter parameter = new SqlParameter("@id",id);
            Role selectedrole = db.Role.SqlQuery(query, parameter).FirstOrDefault();
            return View(selectedrole);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from Roles where RoleId=@id";
            SqlParameter parameter = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, parameter);


            //for the sake of referential integrity, unset the species for all users
            string refquery = "update Users set RoleId = '' where UserId=@id";
            db.Database.ExecuteSqlCommand(refquery, parameter); //same param as before

            return RedirectToAction("List");
        }

    }
}