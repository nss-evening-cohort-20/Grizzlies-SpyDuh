namespace Grizzlies_SpyDuh.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int AgencyId { get; set; }
        public Agency Agency { get; set; }
        public Skill Skill { get; set; }
        public Service Service { get; set; }
        public List<Skill> Skills { get; set; }
        public virtual List<Service> Services { get; set; }
        public List<SpyEnemy> SpyEnemy { get; set; }
        public List<SpyTeam> SpyTeam { get; set; }
       // public Assignment Assignment { get; set; }

    }

    public class UserBySkill //concise User properties
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Skill> Skills { get; set; }
    }
}
//we can add user image