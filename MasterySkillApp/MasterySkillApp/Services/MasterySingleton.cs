using MasterySkillApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MasterySkillApp.Services
{
    public class MasterySingleton
    {
        private static MasterySingleton instance = null;

        protected MasterySingleton() { }

        // Aqui pongo los Array que necesito
        public List<BasicAttrModel> _listBasicAttr { get; set; }
        
        public static MasterySingleton Instance
        {
            get
            {
                if (instance == null)
                    instance = new MasterySingleton();

                return instance;

            }
        }
    }
}
