﻿namespace Grizzlies_SpyDuh.Models
{
    public class UserSkill
    {
        public int Id { get; set; } 

        public int UserId { get; set; }

        public int SkillId { get; set; }

        public int SkillLevel { get; set;}
    }
}
