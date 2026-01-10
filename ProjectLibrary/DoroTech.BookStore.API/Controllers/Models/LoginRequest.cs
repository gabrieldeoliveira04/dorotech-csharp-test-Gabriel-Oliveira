using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace DoroTech.BookStore.API.Models
{
    public class LoginRequest
    {
        [Required]
        [SwaggerSchema(Example = "admin")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [SwaggerSchema(Example = "123456")]
        public string Password { get; set; } = string.Empty;
    }
}
