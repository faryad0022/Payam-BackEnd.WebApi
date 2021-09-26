using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Core.DTOs.Account
{
    public class SetNewPasswordDTO
    {
        public string ActiveCode { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
    }
}
