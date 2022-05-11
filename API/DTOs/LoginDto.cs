using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class LoginDto
    {
        public string Username { get; set; }= string.Empty;//Will never be null. Validation occurs on client side
        public string  Password { get; set; }= string.Empty;
    }
}