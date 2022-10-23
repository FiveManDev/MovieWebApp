using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models.DTO
{
    public class ChangeFirstLastNameDTO
    {

        [Required]
        public Guid userID { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 3)]
        public string LastName { get; set; }

    }
}
