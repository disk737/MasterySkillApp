using System;
using System.Collections.Generic;
using System.Text;

namespace MasterySkillApp.Models
{
    public class ListUserModel : BaseResponse
    {
        public List<UserModel> UserModels { get; set; }
    }

    public class UserModel : BaseResponse
    {
        public string userEmail { get; set; }
        public string userPassword { get; set; }
        public string userUUID { get; set; }
        public string userName { get; set; }
        public string userLastName { get; set; }
        public string userStatus { get; set; }
        public string userDevice { get; set; }
        public string userGroup { get; set; }
        public string userTitle { get; set; }
        public string userCodeGroup { get; set; }
        public string userFullName => string.Format("{0} {1}", userName,userLastName);
        public string userFullDetail => string.IsNullOrEmpty(userTitle) ? userStatus : string.Format("{0} \"{1}\"", userStatus, userTitle); // Explicacion: Logica ? Verdadero : Falso

    }


}
