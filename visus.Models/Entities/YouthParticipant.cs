using System.ComponentModel.DataAnnotations;

namespace visus.Models.Entities
{
    public class YouthParticipant
    {
        [Key]
        public Guid Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}