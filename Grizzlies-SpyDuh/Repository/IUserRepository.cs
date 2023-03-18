using Grizzlies_SpyDuh.Models;

namespace Grizzlies_SpyDuh.Repositories;

public interface IUserRepository
{
    User GetBySkill(string skillName);
    User GetByIdWithSkillsAndServices(int id);
}