using Grizzlies_SpyDuh.Models;

namespace Grizzlies_SpyDuh.Repositories;

public interface IUserRepository
{
    List<User> GetBySkill_1(string skillName);
    List<UserInfo> GetBySkill_2(string skillName);
    public List<UserInfo> GetAllUsers();
    (List<UserInfo>, int) GetAllUsersPaginatedWithSkills(int offset, int limit);
    User GetByIdWithSkillsAndServices(int id);
    List<UserEnemy> GetEnemies(string userName);
    public void Add(User user);
    public SkillCount GetSkillCounr(string SkillName);
    public List<User> GetNonHandlerByAgencyId(int agencyId);
    public void UpdateUserService(UserService userService);
    public void DeleteUserService(int id);
    public void UpdateUserSkill(UserSkill userSkill);
    public void DeleteUserSkill(int id);
    public void UpdateUser(UserUD UserUD);
    public void UpdateUser2(UserUpdate UserUpdate);
    public void DeleteUser(int id);
    List<UserBasic> SearchName(string criterion);
    public List<UserFriend> GetUserFriends(string name);
}