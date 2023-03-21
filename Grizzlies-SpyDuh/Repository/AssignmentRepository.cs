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
        throw new NotImplementedException();

        //In controller, get list of query string paremeters, and split into array.
        //pass array of Ids here.
        //with a WHERE IN clause, nothing will break if a bad ID is sent.
        //use agency list in sql parameter list along with In keyword
        //return list of results like normal
    }
    public List<Assignment> GetAllOngoingAssignments()
    {
        throw new NotImplementedException();
    }
    public List<Assignment> GetOngoingAssignmentsByAgency(int userId)
    {
        throw new NotImplementedException();
    }



    public List<Assignment> GetOngoingAssignmentsByUser(int userId)
    {
        throw new NotImplementedException();
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
