using Grizzlies_SpyDuh.Models;
using Grizzlies_SpyDuh.Repositories;
using Grizzlies_SpyDuh.Utils;

namespace Grizzlies_SpyDuh.Repository;

public class AgencyRepository : BaseRepository, IAgencyRepository
{
    public AgencyRepository(IConfiguration configuration) : base(configuration) { }

    public List<Agency> GetAll()
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT
	                                    Agency.Id,
	                                    Agency.Name,
	                                    u.Id as UserId,
	                                    u.Name as UserName,
	                                    u.Email as UserEmail,
	                                    u.IsHandler,
	                                    UserSkill.SkillLevel
                                    FROM Agency
                                    JOIN [User] u
	                                    ON Agency.Id = u.AgencyId
                                    JOIN UserSkill
	                                    ON u.Id = UserSkill.UserId
                                    JOIN Skill
	                                    ON UserSkill.SkillId = Skill.Id
                                    ORDER BY Id";

                var reader = cmd.ExecuteReader();
                var agencies = new List<Agency>();

                while (reader.Read())
                {
                    var agencyId = DbUtils.GetInt(reader, "Id");
                    var existingAgency = agencies.FirstOrDefault(a => a.Id == agencyId);

                    if (existingAgency == null)
                    {
                        existingAgency = new Agency()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Members = new List<UserBasic>(),
                            SkillLevels = new List<int>()
                        };

                        agencies.Add(existingAgency);
                    }

                    var userId = DbUtils.GetInt(reader, "UserId");
                    var existingUser = existingAgency.Members.FirstOrDefault(u => u.Id == userId);

                    if (userId != null && existingUser == null)
                    {
                        existingAgency.Members.Add(new UserBasic()
                        {
                            Id = userId,
                            Name = DbUtils.GetString(reader, "UserName"),
                            Email = DbUtils.GetString(reader, "UserEmail"),
                            IsHandler = DbUtils.GetNullableBoolean(reader, "IsHandler")
                        });
                    }

                    if (DbUtils.IsNotDbNull(reader, "SkillLevel"))
                    {
                        existingAgency.SkillLevels.Add(DbUtils.GetInt(reader, "SkillLevel"));
                    }
                }

                reader.Close();
                return agencies;
            }
        }
    }
}