namespace Grizzlies_SpyDuh.Models;

public class Assignment
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int AgencyId { get; set; }
    public Agency Agency { get; set; }
    public bool Fatal { get; set; }
    public DateTime StartMissionDateTime { get; set; }
    public DateTime EndMissionDateTime { get; set;}

    public List<UserAssignment> UserAssignments { get; set; }

    public int DaysRemainingInAssignment => (EndMissionDateTime - DateTime.Today).Days;
}
