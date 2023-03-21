namespace Grizzlies_SpyDuh.Models
{
    public class Agency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public User? Handler { get; set; }
        public double? AvgSkillLevel { get; set; }
    }
}
