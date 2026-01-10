using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace DoroTech.BookStore.API.Models
{
    public class LoginRequest
    {
        [Required]
        [SwaggerSchema( "admin")] //valor incluido no schema do swagger
        public string Username { get; set; } = string.Empty;

        [Required]
        [SwaggerSchema("123456")]//valor incluido no schema do swagger
        public string Password { get; set; } = string.Empty;
    }
}

