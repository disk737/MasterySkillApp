using MasterySkillApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterySkillApp.Services
{
    public class UserServices
    {

        public List<UserModel> GetUserModels()
        {
            List<UserModel> UserList = new List<UserModel>
            {
                new UserModel{UserName = "Angelica Avila", UserStatus = "I bring the help"},
                new UserModel{UserName = "Ivan Hidalgo", UserStatus = "I am your backup!!"},
                new UserModel{UserName = "Mariana Alzate", UserStatus = "Automation  master"},
                new UserModel{UserName = "Jaime Trujillo", UserStatus = "Architect OS"},
                new UserModel{UserName = "Giovanny Cifuentes", UserStatus = "Hippies will inherit the earth"},
                new UserModel{UserName = "Arturo Suarez", UserStatus = "Golden Newbie"},
                new UserModel{UserName = "Juan Camilo Moreno", UserStatus = "Code Master"},
            };

            return UserList;
        }
    }
}
