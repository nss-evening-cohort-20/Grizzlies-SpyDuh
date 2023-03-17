using Grizzlies_SpyDuh.Repositories;
using Grizzlies_SpyDuh.Models;
using Grizzlies_SpyDuh.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace Grizzlies_SpyDuh.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private User user;

        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }
        /*-------------------GetBySkill()----------------------*/
        public User GetUserBySkill(string skillName)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @" SELECT
        [User].Id AS UserId,
        [User].Name, 
        [User].Email, 
        [User].AgencyId, 
        UserSkill.Id, 
        UserSkill.UserId,
        UserSkill.SkillLevel,
        Skill.Id,
        Skill.Name
        FROM [User] 
        INNER JOIN UserSkill ON UserSkill.UserId = [User].Id
        INNER JOIN Skill ON Skill.Id = UserSkill.SkillId
        WHERE Skill.Name= @skillName";

                    DbUtils.AddParameter(cmd, "@Name", skillName);

                    var reader = cmd.ExecuteReader();

                    User users = null;
                    while (reader.Read())
                    {
                        user = new User()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            AgencyId = DbUtils.GetInt(reader, "AgencyId"),
                            Skill = new Skill()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                            }
                        };

                    }

                    reader.Close();

                    return user;
                }
            }
        }

    }
}
