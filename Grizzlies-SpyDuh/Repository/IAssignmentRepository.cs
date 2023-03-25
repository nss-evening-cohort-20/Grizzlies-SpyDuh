using Grizzlies_SpyDuh.Models;

namespace Grizzlies_SpyDuh.Repository
{
    public interface IAssignmentRepository
    {
        void Add(Assignment assignment);
        void Delete(int id);
        List<Assignment> GetAll();
        List<Assignment> GetAllOngoingAssignments();
        List<Assignment> GetByAgencyId(int agencyId);
        Assignment GetById(int id);
        List<Assignment> GetOngoingAssignmentsByAgency(int userId);
        List<Assignment> GetOngoingAssignmentsByUser(int userId);
        List<Assignment> Search(string criterion, string sortBy, bool sortDescending);
        void Update(int id, Assignment assignment);
    }
}