using System;
using System.Collections.Generic;
using System.Text;

namespace MasterySkillApp.Services
{
    public class MasterySingleton
    {
        private static MasterySingleton instance = null;

        protected MasterySingleton() { }

        // Aqui pongo los Array que necesito
        //public ObservableCollection<Challenge> _obsListChallenge { get; set; }

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
