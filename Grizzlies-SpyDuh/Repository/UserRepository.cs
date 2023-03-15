using Grizzlies_SpyDuh.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace Grizzlies_SpyDuh.Repository
{
    public class UserRepository //: BaseRepository, IUserRepository
    {
        /*-------------------GetBySkill()----------------------*/
        public User GetBySkill(string skillName)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @" SELECT
  [User].Id,
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

                    DbUtils.AddParameter(cmd, "@SkillName", skill);

                    var reader = cmd.ExecuteReader();

                    var users = new List<User>();
                    if (reader.Read())
                    {
                        var user = new User()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),
                            AgencyId = DbUtils.GetInt(reader, "AgencyId"),
                            skillName= new List <Skill>()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name"),
                            }
                        };
                    }

                    reader.Close();

                    return users;
                }
            }
        }

    }
}
