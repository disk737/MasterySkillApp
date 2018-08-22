using MasterySkillApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MasterySkillApp.Services
{
    public class BadgeServices
    {
        public List<BadgeModel> GetBadgeModels()
        {
            List<BadgeModel> BadgeList = new List<BadgeModel>
            {
                new BadgeModel {BadgeName = "Tecnica", BadgeStatus = "26 Pts"},
                new BadgeModel {BadgeName = "Colaboración", BadgeStatus = "15 Pts"},
                new BadgeModel {BadgeName = "Comunicación", BadgeStatus = "18 Pts"},
                new BadgeModel {BadgeName = "Soluciones", BadgeStatus = "26 Pts"},
            };

            return BadgeList ;
        }
    }
}
