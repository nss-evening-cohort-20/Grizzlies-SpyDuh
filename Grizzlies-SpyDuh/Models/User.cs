namespace Grizzlies_SpyDuh.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int AgencyId { get; set; }
        public virtual Agency Agency { get; set; }
    }
}
//we can add user image