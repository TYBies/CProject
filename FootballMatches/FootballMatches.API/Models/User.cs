using System.ComponentModel.DataAnnotations;

namespace FootballMatches.API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public string? Salt { get; set; }      
        public DateTime CreatedAt { get; set; }   
    }
}
