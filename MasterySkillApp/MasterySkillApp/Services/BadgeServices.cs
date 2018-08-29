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
                new BadgeModel {badgeName = "Tecnica", badgeStatus = "26 Tokens", badgeText = "La maestria sobre la herramienta"},
                new BadgeModel {badgeName = "Colaboración", badgeStatus = "15 Tokens", badgeText = "Solo unidos tendremos exito"},
                new BadgeModel {badgeName = "Comunicación", badgeStatus = "18 Tokens", badgeText = "Mi lenguaje es universal"},
                new BadgeModel {badgeName = "Soluciones", badgeStatus = "26 Tokens", badgeText = "Las buenas ideas se explican solas"},
                new BadgeModel {badgeName = "Pasion", badgeStatus = "20 Tokens", badgeText = "Aquella fuerza que me impulsa hacia adelante"}
            };

            return BadgeList ;
        }
    }
}
