﻿namespace Grizzlies_SpyDuh.Models
{
    public class UserSkill
    {
        public int Id { get; set; } 

        public int UserId { get; set; }
        public User? User { get; set; }

        public int SkillId { get; set; }
        public Skill? Skill { get; set; }    

        public int SkillLevel { get; set;}
    }
}
