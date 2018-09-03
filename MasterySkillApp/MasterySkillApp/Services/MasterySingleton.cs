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

        // Aqui pongo los Array que necesito que se mantengan
        public List<BasicAttrModel> _listBasicAttr { get; set; }
        public List<UserModel> _listUserModel { get; set; }

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
