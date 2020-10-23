using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessMyPassBackend.Models
{
    public class UserOptions
    {
        public string Password { get; set; } = String.Empty;
        public string NewPassword { get; set; } = String.Empty;

        public string Username { get; set; } = String.Empty;
        public string NewUsername { get; set; } = String.Empty;
    }
}
