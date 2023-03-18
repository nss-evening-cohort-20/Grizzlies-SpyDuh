using Grizzlies_SpyDuh.Repositories;
using Grizzlies_SpyDuh.Models;
using Grizzlies_SpyDuh.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

namespace Grizzlies_SpyDuh.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        private User user;

        public UserRepository(IConfiguration configuration) : base(configuration)
        {
        }
        /*-------------------GetBySkill()---1-------------------*/
        public List<User> GetBySkill_1(string SkillName) //used Model User class:User
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @" SELECT
        [User].Id AS UserId,
        [User].Name As UserName, 
        [User].Email, 
        [User].AgencyId, 
        UserSkill.Id, 
        UserSkill.UserId,
        UserSkill.SkillLevel,
        Skill.Id,
        Skill.Name As SkillName
        FROM [User] 
        INNER JOIN UserSkill ON UserSkill.UserId = [User].Id
        INNER JOIN Skill ON Skill.Id = UserSkill.SkillId
        WHERE Skill.Name= @Name";

                    DbUtils.AddParameter(cmd, "@Name", SkillName);

                    var reader = cmd.ExecuteReader();

                    var users = new List<User>();
                    while (reader.Read())
                    {
                        user = new User()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "UserName"),
                            Email = DbUtils.GetString(reader, "Email"),
                            AgencyId = DbUtils.GetInt(reader, "AgencyId"),
                            Skills = new List<Skill>()
                        };

                        if (DbUtils.IsNotDbNull(reader, "UserName"))
                        {
                            var skillNamex = DbUtils.GetString(reader, "SkillName");
                            var existingSkill = user.Skills.Find(s => s.Name == skillNamex);
                            if (existingSkill == null)
                            {

                                user.Skills.Add(new Skill()
                                {
                                    Id = DbUtils.GetInt(reader, "Id"),
                                    Name = skillNamex
                                });

                            }

                        };

                        users.Add(user);
                    }


                    reader.Close();

                    return users;
                }

            }
        }

        /*-------------------GetBySkill()---2-------------------*/
        public List<UserInfo> GetBySkill_2(string SkillName) //used Model User class: UserInfo
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @" SELECT
        [User].Id AS UserId,
        [User].Name As UserName, 
        [User].Email, 
        [User].AgencyId, 
        UserSkill.Id, 
        UserSkill.UserId,
        UserSkill.SkillLevel,
        Skill.Id,
        Skill.Name As SkillName
        FROM [User] 
        INNER JOIN UserSkill ON UserSkill.UserId = [User].Id
        INNER JOIN Skill ON Skill.Id = UserSkill.SkillId
        WHERE Skill.Name= @Name";

                    DbUtils.AddParameter(cmd, "@Name", SkillName);

                    var reader = cmd.ExecuteReader();

                    var users = new List<UserInfo>();
                    while (reader.Read())
                    {
                        var user = new UserInfo()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            Name = DbUtils.GetString(reader, "UserName"),
                            Email = DbUtils.GetString(reader, "Email"),
                            Skills = new List<Skill>()
                        };

                        if (DbUtils.IsNotDbNull(reader, "UserName"))
                        {
                            var skillNamex = DbUtils.GetString(reader, "SkillName");
                            user.Skills.Add(new Skill()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = skillNamex //skillName
                            });
                        };

                        users.Add(user);
                    }
                    reader.Close();

                    return users;
                }

            }
        }

        /*-------------------GetAllUsers()---2-------------------*/
        public List<UserInfo> GetAllUsers() //used Model User class: UserInfo
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @" SELECT
        [User].Id AS UserId,
        [User].Name As UserName, 
        [User].Email, 
        Skill.Name As SkillName
        FROM [User] 
        INNER JOIN UserSkill ON UserSkill.UserId = [User].Id
        INNER JOIN Skill ON Skill.Id = UserSkill.SkillId";

                    var reader = cmd.ExecuteReader();

                    var users = new List<UserInfo>();
                    while (reader.Read())
                    {
                        var user = new UserInfo()
                        {
                            Id = DbUtils.GetInt(reader, "UserId"),
                            Name = DbUtils.GetString(reader, "UserName"),
                            Email = DbUtils.GetString(reader, "Email"),
                            Skills = new List<Skill>()
                        };

                        if (DbUtils.IsNotDbNull(reader, "UserName"))
                        {
                            var skillNamex = DbUtils.GetString(reader, "SkillName");
                            user.Skills.Add(new Skill()
                            {
                                //Id = DbUtils.GetInt(reader, "Id"),
                                Name = skillNamex //skillName
                            });
                        };
                        users.Add(user);
                    }
                reader.Close();

                return users;
                }
            }

        }
    }
}

