using System;
using System.Collections.Generic;
using System.Text;

namespace MasterySkillApp.Models
{

    public class ListDetailAttr : BaseResponse
    {
        public List<DetailAttrModel> AttrDetail { get; set; }
    }

    public class DetailAttrModel : BasicAttrModel
    {
        public string userName { get; set; }
        public string userLastName { get; set; }
        public string attrDetailDate { get; set; }
        public string attrDetailMessage { get; set; }
        public string userFullName => string.Format("{0} {1}", userName, userLastName);
    }
}
