using System;
using System.Collections.Generic;
using System.Text;

namespace MasterySkillApp.Entitys
{
    public class SendUserStatus
    {
        public string userStatus { get; set; }

        public SendUserStatus(string argUserStatus)
        {
            this.userStatus = argUserStatus;
        }
    }
}
