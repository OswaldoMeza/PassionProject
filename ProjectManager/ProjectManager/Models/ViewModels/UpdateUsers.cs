using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectManager.Models.ViewModels
{
    public class UpdateUsers
    {
        //when we need to update a user
        //we need the User info as well as a list of Roles

        public User User { get; set; }
        public List<Role> Role { get; set; }
    }
}