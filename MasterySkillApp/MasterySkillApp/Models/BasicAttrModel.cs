using System;
using System.Collections.Generic;
using System.Text;

namespace MasterySkillApp.Models
{
    public class ListBasicAttrModel
    {
        public List<BasicAttrModel> BasicAttrs { get; set; }
    }

    public class ListAttrPoints
    {
        public List<BasicAttrModel> AttrPoints { get; set; }
    }

    public class BasicAttrModel
    {
        public string basicAttrID { get; set; }
        public string attrName { get; set; }
        public string attrDescrp { get; set; }
        public string attrImg { get; set; }
        public string attrTotalPoints { get; set; }
    }

    
}
