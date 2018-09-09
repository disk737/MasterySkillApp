using System;
using System.Collections.Generic;
using System.Text;

namespace MasterySkillApp.Models
{
    public class SendAttrModel
    {
        public string receiverUUID { get; set; }
        public string basicAttrID { get; set; }

        // Creo el constructor
        public SendAttrModel(string argReceiverUUID, string argBasicAttrID)
        {
            this.receiverUUID = argReceiverUUID;
            this.basicAttrID = argBasicAttrID;
        }
    }
}
