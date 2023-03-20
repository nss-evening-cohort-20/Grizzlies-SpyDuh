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

        public UserRepository(IConfiguration configuration) : base(configuration) { }


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

        /*-------------------GetAllUsers()----------------------*/
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
        /*-------------------GetByIdWithSkillsAndServices()----------------------*/
        public User GetByIdWithSkillsAndServices(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT 
                                        u.Id AS UserId, 
                                        u.Name AS UserName, 
                                        Email, 
                                        u.AgencyId, 
                                        ag.Name AS AgencyName, 
                                        s.Id AS SkillId, 
                                        s.Name AS SkillName, 
                                        svc.Id AS ServiceId, 
                                        svc.Name AS ServiceName, 
                                        usvc.ServicePrice 
                                        FROM [User] u
                                        LEFT JOIN UserSkill us
                                        ON u.Id = us.UserId
                                        LEFT JOIN Skill s
                                        ON us.SkillId = s.Id
                                        LEFT JOIN UserService usvc
                                        ON u.Id = usvc.UserId
                                        LEFT JOIN [Service] svc
                                        ON svc.Id = usvc.ServiceId
                                        LEFT JOIN Agency ag
                                        ON ag.Id = u.AgencyId
                                        WHERE u.Id = @Id;";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    var reader = cmd.ExecuteReader();
                    User user = null;
                    while (reader.Read())
                    {

                        if (user == null)
                        {
                            user = new User()
                            {
                                Id = DbUtils.GetInt(reader, "UserId"),
                                Name = DbUtils.GetString(reader, "UserName"),
                                Email = DbUtils.GetString(reader, "Email"),
                                AgencyId = DbUtils.GetInt(reader, "AgencyId"),
                                Agency = new Agency()
                                {
                                    Id = DbUtils.GetInt(reader, "AgencyId"),
                                    Name = DbUtils.GetString(reader, "AgencyName")
                                },
                                Skills = new List<Skill>(),
                                Services = new List<Service>()
                            };
                        }

                        if (DbUtils.IsNotDbNull(reader, "SkillId"))
                        {
                            var skillId = DbUtils.GetInt(reader, "SkillId");
                            var existingSkill = user.Skills.FirstOrDefault(s => s.Id == skillId);
                            if (existingSkill == null)
                            {

                                user.Skills.Add(new Skill()
                                {
                                    Id = skillId,
                                    Name = DbUtils.GetString(reader, "SkillName")
                                });
                            }

                        }

                        if (DbUtils.IsNotDbNull(reader, "ServiceId"))
                        {
                            var serviceId = DbUtils.GetInt(reader, "ServiceId");
                            var existingService = user.Services.FirstOrDefault(s => s.Id == serviceId);
                            if (existingService == null)
                            {
                                user.Services.Add(new Service()
                                {
                                    Id = serviceId,
                                    Name = DbUtils.GetString(reader, "ServiceName")
                                });
                            }
                        }
                    }
                    reader.Close();
                    return user;
                }
            }
        }

        // /*-------------------GetEnemies()----------------------*/
        public List<UserEnemy> GetEnemies(string userName) //Tom Bishop
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                    SELECT Name, Email
                                    FROM [User] 
                                    WHERE [User].Id IN (
                                            SELECT DISTINCT[SpyEnemy].UserId2
                                            FROM [User]   
                                            INNER JOIN [SpyEnemy] 
                                            ON  [User].Id = [SpyEnemy].UserId1
                                            WHERE [User].Name= @Name )";

                    DbUtils.AddParameter(cmd, "Name", userName);

                    var reader = cmd.ExecuteReader();

                    var users = new List<UserEnemy>();
                    while (reader.Read())
                    {
                        var user = new UserEnemy()
                        {
                            Name = DbUtils.GetString(reader, "Name"),
                            Email = DbUtils.GetString(reader, "Email"),

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
