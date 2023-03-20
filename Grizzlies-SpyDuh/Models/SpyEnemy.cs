namespace Grizzlies_SpyDuh.Models;


public class SpyEnemy
{
    public int Id { get; set; }
    public int UserId1 { get; set; }
    public int UserId2 { get; set; }
    public UserEnemy UserEnemy { get; set; }
}


