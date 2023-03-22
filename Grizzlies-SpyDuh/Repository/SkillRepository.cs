using Grizzlies_SpyDuh.Models;
using Grizzlies_SpyDuh.Repositories;
using Grizzlies_SpyDuh.Utils;

namespace Grizzlies_SpyDuh.Repository
{
    public class SkillRepository : BaseRepository, ISkillRepository
    {
        public SkillRepository(IConfiguration configuration) : base(configuration)
        { }

        public List<Skill> GetAll()

        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [Id]
                                        ,[Name]
                                       FROM [SpyDuh].[dbo].[Skill]";

                    var reader = cmd.ExecuteReader();

                    var skills = new List<Skill>();

                    while (reader.Read())
                    {
                        skills.Add(new Skill()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name")

                        });
                    }

                    reader.Close();
                    return skills;

                }
            }

        }

    }
}
