using Grizzlies_SpyDuh.Models;
using Grizzlies_SpyDuh.Repositories;
using Grizzlies_SpyDuh.Utils;

namespace Grizzlies_SpyDuh.Repository;

public class AgencyRepository : BaseRepository, IAgencyRepository
{
    public AgencyRepository(IConfiguration configuration) : base(configuration) { }

    public List<Agency> GetAllAvgSkill()
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT
	                                    Agency.Id as Id,
	                                    Agency.Name as Agency,
	                                    AVG(CAST(SkillLevel AS float)) as AvgSkillLevel
                                    FROM Agency
                                    JOIN [User] u
	                                    ON Agency.Id = u.Id
                                    JOIN UserSkill
	                                    ON u.Id = UserSkill.UserId
                                    JOIN Skill
	                                    ON UserSkill.SkillId = Skill.Id
                                    GROUP BY Agency.Id, Agency.Name";

                var reader = cmd.ExecuteReader();

                var agencies = new List<Agency>();

                while (reader.Read())
                {
                    var agency = new Agency()
                    {
                        Id = DbUtils.GetInt(reader, "Id"),
                        Name = DbUtils.GetString(reader, "Agency"),
                        AvgSkillLevel = DbUtils.GetDouble(reader, "AvgSkillLevel")
                    };

                    agencies.Add(agency);
                }

                reader.Close();
                return agencies;
            }
        }
    }
}