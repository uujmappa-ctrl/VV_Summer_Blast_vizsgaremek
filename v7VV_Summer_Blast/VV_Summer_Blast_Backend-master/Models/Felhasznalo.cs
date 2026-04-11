using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VVSummerBlastBackendAPI.Models
{
    [Table("felhasznalok")] // Az adatbázisban kisbetűs táblanevet használunk
    public class Felhasznalo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty; // Sose tárolunk sima szöveges jelszót!

        public string Role { get; set; } = "User"; // Alapértelmezetten mindenki sima felhasználó

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}