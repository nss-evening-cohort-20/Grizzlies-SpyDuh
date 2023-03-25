using System.Text.Json.Serialization;

namespace Grizzlies_SpyDuh.Models
{
    public class Agency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public List<int>? SkillLevels { get; set; }
        public double? AvgSkillLevel => SkillLevels != null ? double.Parse(((double)SkillLevels?.Sum() / (double)SkillLevels.Count()).ToString("0.00")) : null;
        public List<UserBasic>? Members { get; set; }
    }
}