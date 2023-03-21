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

        /*-------------------GetEnemies()----------------------*/
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
        /*-------------------Add()----------------------*/
        public void Add(User user)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO [User]
                                            (Name,
                                            Email,
                                            AgencyId)
                                        OUTPUT INSERTED.ID
                                        VALUES (@Name, @Email, @AgencyId)";

                    DbUtils.AddParameter(cmd, "@Name", user.Name);
                    DbUtils.AddParameter(cmd, "@Email", user.Email);
                    DbUtils.AddParameter(cmd, "@AgencyId", user.AgencyId);
                    user.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public List<User> GetNonHandlerByAgencyId(int agencyId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT
	                                        u.Id,
	                                        u.Name,
	                                        Email,
	                                        Agency.Id as AgencyId,
	                                        Agency.Name as Agency,
	                                        IsHandler,
                                            Skill.Id as SkillId,
	                                        Skill.Name as SkillName,
	                                        SkillLevel,
                                            Service.Id as ServiceId,
	                                        Service.Name as ServiceName,
	                                        ServicePrice
                                        FROM [User] u
                                        JOIN Agency
	                                        ON u.AgencyId = Agency.Id
                                        LEFT JOIN UserSkill
	                                        ON u.Id = UserSkill.UserId
                                        LEFT JOIN Skill
	                                        ON UserSkill.SkillId = Skill.Id
                                        LEFT JOIN UserService
	                                        ON u.Id = UserService.UserId
                                        LEFT JOIN [Service]
	                                        ON UserService.ServiceId = Service.Id
                                        WHERE AgencyId = @AgencyId AND IsHandler = 0";

                    DbUtils.AddParameter(cmd, "@AgencyId", agencyId);

                    var reader = cmd.ExecuteReader();

                    var users = new List<User>();

                    while (reader.Read())
                    {
                        var userId = DbUtils.GetInt(reader, "Id");
                        var existingUser = users.FirstOrDefault(u => u.Id == userId);
                        if (existingUser == null)
                        {
                            existingUser = new User()
                            {
                                Id = userId,
                                Name = DbUtils.GetString(reader, "Name"),
                                Email = DbUtils.GetString(reader, "Email"),
                                AgencyId = agencyId,
                                Agency = new Agency()
                                {
                                    Id = agencyId,
                                    Name = DbUtils.GetString(reader, "Agency")
                                },
                                IsHandler = DbUtils.GetBoolean(reader, "IsHandler"),
                                Skills = new List<Skill>(),
                                Services = new List<Service>()
                            };

                            users.Add(existingUser);
                        }

                        var skillId = DbUtils.GetInt(reader, "SkillId");
                        var existingSkill = existingUser.Skills.FirstOrDefault(s => s.Id == skillId);
                        if (existingSkill == null)
                        {
                            existingUser.Skills.Add(new Skill()
                            {
                                Id = skillId,
                                Name = DbUtils.GetString(reader, "SkillName"),
                                Level = DbUtils.GetInt(reader, "SkillLevel"),
                            });
                        }

                        var serviceId = DbUtils.GetInt(reader, "ServiceId");
                        var existingService = existingUser.Services.FirstOrDefault(s => s.Id == serviceId);
                        if (existingService == null)
                        {
                            existingUser.Services.Add(new Service()
                            {
                                Id = serviceId,
                                Name = DbUtils.GetString(reader, "ServiceName"),
                                Price = DbUtils.GetDouble(reader, "ServicePrice"),
                            });
                        }
                    }

                    reader.Close();
                    return users;
                }
            }
        }

        public void UpdateUserService(UserService userService)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE UserService
	                                        SET ServiceId = @ServiceId,
		                                        UserId = @UserId,
		                                        ServicePrice = @ServicePrice
                                        WHERE Id = @Id;";

                    DbUtils.AddParameter(cmd, "@ServiceId", userService.ServiceId);
                    DbUtils.AddParameter(cmd, "@UserId", userService.UserId);
                    DbUtils.AddParameter(cmd, "@ServicePrice", userService.ServicePrice);
                    DbUtils.AddParameter(cmd, "@Id", userService.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUserService(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM UserService WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateUserSkill(UserSkill userSkill)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE UserSkill
	                                        SET SkillId = @SkillId,
		                                        UserId = @UserId,
		                                        SkillLevel = @SkillLevel
                                        WHERE Id = @Id;";

                    DbUtils.AddParameter(cmd, "@SkillId", userSkill.SkillId);
                    DbUtils.AddParameter(cmd, "@UserId", userSkill.UserId);
                    DbUtils.AddParameter(cmd, "@SkillLevel", userSkill.SkillLevel);
                    DbUtils.AddParameter(cmd, "@Id", userSkill.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUserSkill(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM UserSkill WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /*-------------------------------UpdateUser()--------------------------------------*/
        public void UpdateUser(UserEnemy UserEnemy)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    UPDATE User 
                    SET Name = @Name, 
                        Email = @Email 
                    WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Id", UserEnemy.Id);
                    DbUtils.AddParameter(cmd, "@Name", UserEnemy.Name);
                    DbUtils.AddParameter(cmd, "@Email", UserEnemy.Email);

                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
