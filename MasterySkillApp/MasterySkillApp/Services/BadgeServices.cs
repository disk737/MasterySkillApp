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
                new BadgeModel {BadgeName = "Tecnica", BadgeStatus = "26 Tokens", BadgeText = "La maestria sobre la herramienta"},
                new BadgeModel {BadgeName = "Colaboración", BadgeStatus = "15 Tokens", BadgeText = "Solo unidos tendremos exito"},
                new BadgeModel {BadgeName = "Comunicación", BadgeStatus = "18 Tokens", BadgeText = "Mi lenguaje es universal"},
                new BadgeModel {BadgeName = "Soluciones", BadgeStatus = "26 Tokens", BadgeText = "Las buenas ideas se explican solas"},
                new BadgeModel {BadgeName = "Pasion", BadgeStatus = "20 Tokens", BadgeText = "Aquella fuerza que me impulsa hacia adelante"}
            };

            return BadgeList ;
        }
    }
}
