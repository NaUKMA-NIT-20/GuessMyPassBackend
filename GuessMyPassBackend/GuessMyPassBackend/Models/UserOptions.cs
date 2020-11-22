using System;

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
