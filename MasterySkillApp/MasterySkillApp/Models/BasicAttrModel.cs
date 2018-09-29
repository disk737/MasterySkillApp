using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace MasterySkillApp.Models
{
    public class ListBasicAttrModel : BaseResponse
    {
        public List<BasicAttrModel> BasicAttrs { get; set; }
    }

    public class ListAttrPoints : BaseResponse
    {

        [JsonProperty("AttrPoints")]
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
