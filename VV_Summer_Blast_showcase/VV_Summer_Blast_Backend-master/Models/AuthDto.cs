using System.ComponentModel.DataAnnotations;

namespace VVSummerBlastBackendAPI.Models
{
    // Adatátviteli objektumok (DTO) a hitelesítéshez, hogy ne a teljes felhasználói modellt mozgassuk
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [EmailAddress] // Beépített ellenõrzés a formátumra
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "A jelszónak legalább 6 karakternek kell lennie!")]
        public string Password { get; set; } = string.Empty;
    }

    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}