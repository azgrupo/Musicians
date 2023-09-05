using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicians.Application.DTO
{
    public class UserRegistrationDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserTokenDto
    {
        public string Email { get; set; }
        public string? JwtToken { get; set; }
        public int? Id { get; set; }
    }
}
