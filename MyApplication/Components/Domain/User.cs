using System.ComponentModel.DataAnnotations;

namespace MyApplication.Components.Domain
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; } 

    }
}
