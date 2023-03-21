using Grizzlies_SpyDuh.Models;
using Grizzlies_SpyDuh.Repositories;
using Grizzlies_SpyDuh.Utils;
using System.Security.Cryptography;

namespace Grizzlies_SpyDuh.Repository;

public class AssignmentRepository : BaseRepository, IAssignmentRepository
{
    public AssignmentRepository(IConfiguration configuration) : base(configuration) { }

    /*----------------------GetAssignments------*/

    public List<Assignment> GetAll()
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @" SELECT  [Assignment].[Id] AS AssignmentId
                                    ,[Description]
                                    ,[Fatal]
                                    ,[StartMissionDateTime]
                                    ,[EndMissionDateTime]
	                                ,[AgencyId]
	                                ,[Agency].Name AS AgencyName 
                                  FROM [SpyDuh].[dbo].[Assignment]
                                  LEFT JOIN Agency
                                  ON Agency.Id = Assignment.AgencyId";


                var reader = cmd.ExecuteReader();

                var assignments = new List<Assignment>();
                while (reader.Read())
                {
                    var assignment = new Assignment()
                    {
                        Id = DbUtils.GetInt(reader, "AssignmentId"),
                        Description = DbUtils.GetString(reader, "Description"),
                        Fatal = DbUtils.GetBoolean(reader, "Fatal"),
                        StartMissionDateTime = DbUtils.GetDateTime(reader, "StartMissionDateTime"),
                        EndMissionDateTime = DbUtils.IsNotDbNull(reader, "EndMissionDateTime") ? DbUtils.GetDateTime(reader, "EndMissionDateTime") : null,
                        AgencyId = DbUtils.GetInt(reader, "AgencyId"),
                        Agency = new Agency()
                        {
                            Id = DbUtils.GetInt(reader, "AgencyId"),
                            Name = DbUtils.GetString(reader, "AgencyName"),
                        }
                    };
                    assignments.Add(assignment);
                }
                reader.Close();
                return assignments;
            }
        }
    }

    public Assignment GetById(int id)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @" SELECT  [Assignment].[Id] AS AssignmentId
                                    ,[Description]
                                    ,[Fatal]
                                    ,[StartMissionDateTime]
                                    ,[EndMissionDateTime]
	                                ,[AgencyId]
	                                ,[Agency].Name AS AgencyName 
                                  FROM [SpyDuh].[dbo].[Assignment]
                                  LEFT JOIN Agency
                                  ON Agency.Id = Assignment.AgencyId
                                  WHERE Assignment.Id = @Id";
                DbUtils.AddParameter(cmd, "@Id", id);


                var reader = cmd.ExecuteReader();

                Assignment assignment = null;
                if (reader.Read())
                {
                    assignment = new Assignment()
                    {
                        Id = DbUtils.GetInt(reader, "AssignmentId"),
                        Description = DbUtils.GetString(reader, "Description"),
                        Fatal = DbUtils.GetBoolean(reader, "Fatal"),
                        StartMissionDateTime = DbUtils.GetDateTime(reader, "StartMissionDateTime"),
                        EndMissionDateTime = DbUtils.IsNotDbNull(reader, "EndMissionDateTime") ? DbUtils.GetDateTime(reader, "EndMissionDateTime") : null,
                        AgencyId = DbUtils.GetInt(reader, "AgencyId"),
                        Agency = new Agency()
                        {
                            Id = DbUtils.GetInt(reader, "AgencyId"),
                            Name = DbUtils.GetString(reader, "AgencyName"),
                        }
                    };
                    
                }
                reader.Close();
                return assignment;
            }
        }
    }
    public List<Assignment> GetByAgencyId(int agencyId)
    {

        //var idList = agencyIds.Split(",");
        //List<int> listOfInts = new List<int>();
        //foreach (var id in idList)
        //{
        //    var parsedId = int.Parse(id);
        //    listOfInts.Add(parsedId);
        //}
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"   SELECT  [Assignment].[Id] AS AssignmentId
                                    ,[Description]
                                    ,[Fatal]
                                    ,[StartMissionDateTime]
                                    ,[EndMissionDateTime]
	                                ,[AgencyId]
	                                ,[Agency].Name AS AgencyName 
                                  FROM [SpyDuh].[dbo].[Assignment]
                                  LEFT JOIN Agency
                                  ON Agency.Id = Assignment.AgencyId
                                  WHERE Agency.Id IN (@AgencyIds)";

                DbUtils.AddParameter(cmd, "@AgencyIds", agencyId);
                var reader = cmd.ExecuteReader();

                var assignments = new List<Assignment>();
                while (reader.Read())
                {
                    var assignment = new Assignment()
                    {
                        Id = DbUtils.GetInt(reader, "AssignmentId"),
                        Description = DbUtils.GetString(reader, "Description"),
                        Fatal = DbUtils.GetBoolean(reader, "Fatal"),
                        StartMissionDateTime = DbUtils.GetDateTime(reader, "StartMissionDateTime"),
                        EndMissionDateTime = DbUtils.IsNotDbNull(reader, "EndMissionDateTime") ? DbUtils.GetDateTime(reader, "EndMissionDateTime") : null,
                        AgencyId = DbUtils.GetInt(reader, "AgencyId"),
                        Agency = new Agency()
                        {
                            Id = DbUtils.GetInt(reader, "AgencyId"),
                            Name = DbUtils.GetString(reader, "AgencyName"),
                        }
                    };
                    assignments.Add(assignment);
                }
                reader.Close();
                return assignments;
            }
        }
    }
    public List<Assignment> GetAllOngoingAssignments()
    {

        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"  SELECT  [Assignment].[Id] AS AssignmentId
                                    ,[Description]
                                    ,[Fatal]
                                    ,[StartMissionDateTime]
                                    ,[EndMissionDateTime]
	                                ,[AgencyId]
	                                ,[Agency].Name AS AgencyName 
                                  FROM [SpyDuh].[dbo].[Assignment]
                                  LEFT JOIN Agency
                                  ON Agency.Id = Assignment.AgencyId
                                  WHERE EndMissionDateTime > GetDate() OR EndMissionDateTime IS NULL";


                var reader = cmd.ExecuteReader();

                var assignments = new List<Assignment>();
                while (reader.Read())
                {
                    var assignment = new Assignment()
                    {
                        Id = DbUtils.GetInt(reader, "AssignmentId"),
                        Description = DbUtils.GetString(reader, "Description"),
                        Fatal = DbUtils.GetBoolean(reader, "Fatal"),
                        StartMissionDateTime = DbUtils.GetDateTime(reader, "StartMissionDateTime"),
                        EndMissionDateTime = DbUtils.IsNotDbNull(reader, "EndMissionDateTime") ? DbUtils.GetDateTime(reader, "EndMissionDateTime") : null,
                        AgencyId = DbUtils.GetInt(reader, "AgencyId"),
                        Agency = new Agency()
                        {
                            Id = DbUtils.GetInt(reader, "AgencyId"),
                            Name = DbUtils.GetString(reader, "AgencyName"),
                        }
                    };
                    assignments.Add(assignment);
                }
                reader.Close();
                return assignments;
            }
        }
    }
    public List<Assignment> GetOngoingAssignmentsByAgency(int agencyId)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @" SELECT  [Assignment].[Id] AS AssignmentId
                                    ,[Description]
                                    ,[Fatal]
                                    ,[StartMissionDateTime]
                                    ,[EndMissionDateTime]
	                                ,[AgencyId]
	                                ,[Agency].Name AS AgencyName 
                                  FROM [SpyDuh].[dbo].[Assignment]
                                  LEFT JOIN Agency
                                  ON Agency.Id = Assignment.AgencyId
                                  WHERE (EndMissionDateTime > GetDate() OR EndMissionDateTime IS NULL) AND Agency.Id = @Id";

                DbUtils.AddParameter(cmd, "@Id", agencyId);
                var reader = cmd.ExecuteReader();

                var assignments = new List<Assignment>();
                while (reader.Read())
                {
                    var assignment = new Assignment()
                    {
                        Id = DbUtils.GetInt(reader, "AssignmentId"),
                        Description = DbUtils.GetString(reader, "Description"),
                        Fatal = DbUtils.GetBoolean(reader, "Fatal"),
                        StartMissionDateTime = DbUtils.GetDateTime(reader, "StartMissionDateTime"),
                        EndMissionDateTime = DbUtils.IsNotDbNull(reader, "EndMissionDateTime") ? DbUtils.GetDateTime(reader, "EndMissionDateTime") : null,
                        AgencyId = DbUtils.GetInt(reader, "AgencyId"),
                        Agency = new Agency()
                        {
                            Id = DbUtils.GetInt(reader, "AgencyId"),
                            Name = DbUtils.GetString(reader, "AgencyName"),
                        }
                    };
                    assignments.Add(assignment);
                }
                reader.Close();
                return assignments;
            }
        }
    }



    public List<Assignment> GetOngoingAssignmentsByUser(int userId)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"   SELECT  [Assignment].[Id] AS AssignmentId
                                    ,[Description]
                                    ,[Fatal]
                                    ,[StartMissionDateTime]
                                    ,[EndMissionDateTime]
	                                ,[User].[AgencyId]
	                                ,[Agency].Name AS AgencyName 
									
                                  FROM [SpyDuh].[dbo].[Assignment]
                                  LEFT JOIN Agency
                                  ON Agency.Id = Assignment.AgencyId
								  LEFT JOIN [UserAssignment]
								  ON UserAssignment.AssignmentId = Assignment.Id
								  LEFT JOIN [User]
								  ON [User].Id = UserAssignment.UserId
                                  WHERE (EndMissionDateTime > GetDate() OR EndMissionDateTime IS NULL) AND [User].Id = @UserId";

                DbUtils.AddParameter(cmd, "@UserId", userId);
                var reader = cmd.ExecuteReader();

                var assignments = new List<Assignment>();
                while (reader.Read())
                {
                    var assignment = new Assignment()
                    {
                        Id = DbUtils.GetInt(reader, "AssignmentId"),
                        Description = DbUtils.GetString(reader, "Description"),
                        Fatal = DbUtils.GetBoolean(reader, "Fatal"),
                        StartMissionDateTime = DbUtils.GetDateTime(reader, "StartMissionDateTime"),
                        EndMissionDateTime = DbUtils.IsNotDbNull(reader, "EndMissionDateTime") ? DbUtils.GetDateTime(reader, "EndMissionDateTime") : null,
                        AgencyId = DbUtils.GetInt(reader, "AgencyId"),
                        Agency = new Agency()
                        {
                            Id = DbUtils.GetInt(reader, "AgencyId"),
                            Name = DbUtils.GetString(reader, "AgencyName"),
                        }
                    };
                    assignments.Add(assignment);
                }
                reader.Close();
                return assignments;
            }
        }
    }

    public void Add(Assignment assignment)
    {
        throw new NotImplementedException();
    }

    public void Update(int id, Assignment assignment)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}
