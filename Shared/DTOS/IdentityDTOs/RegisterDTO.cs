﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOS.IdentityDTOs
{
    public class RegisterDTO
    {
        [EmailAddress]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        [Phone]
        public string PhoneNumber { get; set; } = null!;
    }
}
