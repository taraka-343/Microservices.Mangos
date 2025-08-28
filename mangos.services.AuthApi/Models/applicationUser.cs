﻿using Microsoft.AspNetCore.Identity;

namespace mangos.services.AuthApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string name { get; set; }
    }
}
