namespace Grizzlies_SpyDuh.Models
{
    public class Agency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int>? SkillLevels { get; set; }
        public double? AvgSkillLevel => (double)SkillLevels?.Sum() / (double)SkillLevels.Count();
        public List<UserBasic> Members { get; set; }
    }
}