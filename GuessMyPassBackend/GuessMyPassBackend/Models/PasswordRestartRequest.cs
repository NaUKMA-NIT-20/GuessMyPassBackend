using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessMyPassBackend.Models
{
    public class PasswordRestartRequest
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
    }
}
