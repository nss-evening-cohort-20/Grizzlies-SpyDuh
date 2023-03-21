using Grizzlies_SpyDuh.Models;
using Grizzlies_SpyDuh.Repositories;
using Grizzlies_SpyDuh.Utils;
using System.Security.Cryptography;

namespace Grizzlies_SpyDuh.Repository;

public class AssignmentRepository : BaseRepository
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


            var reader = cmd.ExecuteReader();

                var assignments = new List<Assignment>();
                while (reader.Read())
                {

                }
            }
            

    public Assignment GetById(int id)
    {

    }
    public List<Assignment> GetByAgencyId(int agencyId)
    {

    }
    public List<Assignment> GetAllOngoingAssignments()
    {

    }
    public List<Assignment> GetOngoingAssignmentsByAgency(int userId)
    {

    }



    public List<Assignment> GetOngoingAssignmentsByUser(int userId)
    {

    }

    public void Add(Assignment assignment)
    {

    }

    public void Update(int id, Assignment assignment)
    {

    }

    public void Delete(int id)
    {

    }
}
