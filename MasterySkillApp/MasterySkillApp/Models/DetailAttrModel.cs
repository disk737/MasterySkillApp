using System;
using System.Collections.Generic;
using System.Text;

namespace MasterySkillApp.Models
{

    public class ListDetailAttr
    {
        public List<DetailAttrModel> AttrDetail { get; set; }
    }

    public class DetailAttrModel : BasicAttrModel
    {
        public string userName { get; set; }
        public string attrDetailDate { get; set; }
    }
}
