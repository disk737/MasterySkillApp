using System;
using System.Collections.Generic;
using System.Text;

namespace MasterySkillApp.Entitys
{
    public class SendUserPassword
    {
        public string userOldPassword { get; set; }
        public string userNewPassword { get; set; }

        public SendUserPassword(string argOldPassword,string argNewPassword)
        {
            this.userOldPassword = argOldPassword;
            this.userNewPassword = argNewPassword;
        }
    }
}
