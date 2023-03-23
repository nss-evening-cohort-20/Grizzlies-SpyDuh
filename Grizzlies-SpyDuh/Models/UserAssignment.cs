namespace Grizzlies_SpyDuh.Models;

public class UserAssignment
{
    public int Id { get; set; }
    public string Role { get; set; }
    public int AssignmentId { get; set; }
    public int ServiceId { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
