namespace Grizzlies_SpyDuh.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? AgencyId { get; set; }
        public Agency? Agency { get; set; }
        public bool? IsHandler { get; set; }
        public List<Skill>? Skills { get; set; }
        public List<Service>? Services { get; set; }
        public List<User>? SpyEnemy { get; set; }
        public List<User>? SpyTeam { get; set; }
       // public Assignment Assignment { get; set; }

    }

    public class UserBasic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsHandler { get; set; }
    }

    public class UserInfo //concise User properties
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Skill> Skills { get; set; }
    }

    public class SkillCount
    {
        public string SkillName { get; set; }
        public int CountSkill { get; set; }
    }

    public class UserEnemy 
    {
        public string Name { get; set; }
        public string Email { get; set; }

    }
}




