using System;
using System.Collections.Generic;
using System.Text;

namespace MasterySkillApp.Models
{
    public class ListUserModel
    {
        public List<UserModel> UserModels { get; set; }
    }

    public class UserModel
    {
        public string userEmail { get; set; }
        public string userPassword { get; set; }
        public string userUUID { get; set; }
        public string userName { get; set; }
        public string userStatus { get; set; }
    }
}
