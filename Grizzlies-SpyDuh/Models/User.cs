namespace Grizzlies_SpyDuh.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int AgencyId { get; set; }
        public Agency Agency { get; set; }
        public List<Skill>Skill { get; set; }
        public List<Service>Service { get; set; }

        //public List<SpyEnemy> SpyEnemy { get; set; }
        //public List<SpyTeam>SpyTeam { get; set;}
        //public Assignment Assignment { get; set;}

    }
}
//we can add user image