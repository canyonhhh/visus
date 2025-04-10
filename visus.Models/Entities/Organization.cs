namespace visus.Models.Entities
{
    public class Organization
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;

        // Navigation Property (collection of users)
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}