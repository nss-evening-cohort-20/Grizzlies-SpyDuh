using Grizzlies_SpyDuh.Models;

namespace Grizzlies_SpyDuh.Repositories;

public interface IUserRepository
{
    List<User> GetBySkill_1(string skillName);
    List<UserBySkill> GetBySkill_2(string skillName);
}