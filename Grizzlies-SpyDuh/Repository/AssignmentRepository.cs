using Grizzlies_SpyDuh.Models;
using Grizzlies_SpyDuh.Repositories;
using Grizzlies_SpyDuh.Utils;
using Microsoft.Extensions.Hosting;
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

    public List<Assignment> Search(string criterion, string sortBy, bool sortDescending)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT
	                                asgn.Id,
	                                asgn.Description,
	                                asgn.AgencyId,
	                                asgn.Fatal,
	                                asgn.StartMissionDateTime,
	                                asgn.EndMissionDateTime,
	                                a.Name as AgencyName
                                FROM Assignment asgn
                                LEFT JOIN Agency a
	                                ON asgn.AgencyId = a.Id
                                WHERE asgn.Description LIKE @Criterion";

                if (sortDescending)
                {
                    cmd.CommandText += @" ORDER BY
		                                    (CASE @SortBy WHEN 'Id' THEN [asgn].[Id] END) DESC,
		                                    (CASE @SortBy WHEN 'Description' THEN [Description] END) DESC,
		                                    (CASE @SortBy WHEN 'AgencyId' THEN [AgencyId] END) DESC,
		                                    (CASE @SortBy WHEN 'Fatal' THEN [Fatal] END) DESC,
		                                    (CASE @SortBy WHEN 'StartMissionDateTime' THEN [StartMissionDateTime] END) DESC,
		                                    (CASE @SortBy WHEN 'EndMissionDateTime' THEN [EndMissionDateTime] END) DESC,
		                                    (CASE @SortBy WHEN 'AgencyName' THEN [a].[Name] END) DESC";
                }
                else
                {
                    cmd.CommandText += @" ORDER BY
		                                    (CASE @SortBy WHEN 'Id' THEN [asgn].[Id] END),
		                                    (CASE @SortBy WHEN 'Description' THEN [Description] END),
		                                    (CASE @SortBy WHEN 'AgencyId' THEN [AgencyId] END),
		                                    (CASE @SortBy WHEN 'Fatal' THEN [Fatal] END),
		                                    (CASE @SortBy WHEN 'StartMissionDateTime' THEN [StartMissionDateTime] END),
		                                    (CASE @SortBy WHEN 'EndMissionDateTime' THEN [EndMissionDateTime] END),
		                                    (CASE @SortBy WHEN 'AgencyName' THEN [a].[Name] END)";
                }

                DbUtils.AddParameter(cmd, "@Criterion", $"%{criterion}%");
                DbUtils.AddParameter(cmd, "@SortBy", sortBy);
                var reader = cmd.ExecuteReader();

                var assignments = new List<Assignment>();
                while (reader.Read())
                {
                    assignments.Add(new Assignment()
                    {
                        Id = DbUtils.GetInt(reader, "Id"),
                        Description = DbUtils.GetString(reader, "Description"),
                        Fatal = DbUtils.GetBoolean(reader, "Fatal"),
                        StartMissionDateTime = DbUtils.GetDateTime(reader, "StartMissionDateTime"),
                        EndMissionDateTime = DbUtils.IsNotDbNull(reader, "EndMissionDateTime") ? DbUtils.GetDateTime(reader, "EndMissionDateTime") : null,
                        AgencyId = DbUtils.GetInt(reader, "AgencyId"),
                        Agency = new Agency()
                        {
                            Id = DbUtils.GetInt(reader, "AgencyId"),
                            Name = DbUtils.GetString(reader, "AgencyName")
                        }
                    });
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
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO [dbo].[Assignment]
                                    ([Description]
                                    ,[AgencyId]
                                    ,[Fatal]
                                    ,[StartMissionDateTime]
                                    ,[EndMissionDateTime])
                                    OUTPUT INSERTED.ID
                                VALUES
                                    (@Description, @AgencyId, @Fatal, @StartMissionDateTime, @EndMissionDateTime)";

                DbUtils.AddParameter(cmd, "@Description", assignment.Description);
                DbUtils.AddParameter(cmd, "@AgencyId", assignment.AgencyId);
                DbUtils.AddParameter(cmd, "@Fatal", assignment.Fatal);
                DbUtils.AddParameter(cmd, "@StartMissionDateTime", assignment.StartMissionDateTime);
                DbUtils.AddParameter(cmd, "@EndMissionDateTime", assignment.EndMissionDateTime);

                assignment.Id = (int)cmd.ExecuteScalar();
            }
        }
    }

    public void Update(int id, Assignment assignment)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                        UPDATE Assignment
                           SET [Description] = @Description,
                               [AgencyId] = @AgencyId,
                               [Fatal] = @Fatal,
                               [StartMissionDateTime] = @StartMissionDateTime,
                               [EndMissionDateTime] = @EndMissionDateTime
                         WHERE Id = @Id";
                DbUtils.AddParameter(cmd, "@Id", id);
                DbUtils.AddParameter(cmd, "@Description", assignment.Description);
                DbUtils.AddParameter(cmd, "@AgencyId", assignment.AgencyId);
                DbUtils.AddParameter(cmd, "@Fatal", assignment.Fatal);
                DbUtils.AddParameter(cmd, "@StartMissionDateTime", assignment.StartMissionDateTime);
                DbUtils.AddParameter(cmd, "@EndMissionDateTime", assignment.EndMissionDateTime);

                cmd.ExecuteNonQuery();
            }
        }
    }

    public void Delete(int id)
    {
        using (var conn = Connection)
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Assignment WHERE Id = @Id";
                DbUtils.AddParameter(cmd, "@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
