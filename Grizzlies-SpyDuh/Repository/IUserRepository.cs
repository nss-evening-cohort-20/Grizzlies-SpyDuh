using Grizzlies_SpyDuh.Models;

namespace Grizzlies_SpyDuh.Repositories;

public interface IUserRepository
{
    List<User> GetBySkill_1(string skillName);
    List<UserInfo> GetBySkill_2(string skillName);
    public List<UserInfo> GetAllUsers();
    User GetByIdWithSkillsAndServices(int id);
    public void Add(User user);
    public List<User> GetNonHandlerByAgencyId(int agencyId);
}