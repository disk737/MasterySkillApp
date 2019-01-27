using MasterySkillApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterySkillApp.Entitys
{
    public class GroupModel
    {
        public string groupsID { get; set; }
        public string groupName { get; set; }
        
    }

    public class ListGroupsModel : BaseResponse
    {
        public List<GroupModel> GroupModel { get; set; }
    }
}
