using System.ComponentModel.DataAnnotations;

namespace visus.Models.Entities
{
    public class YouthParticipant
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.Now;
    }
}