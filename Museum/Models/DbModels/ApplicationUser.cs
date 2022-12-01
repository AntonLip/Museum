using Microsoft.AspNetCore.Identity;
using System;

namespace Museum.Models.DbModels
{
    public class ApplicationUser : IdentityUser
    {  
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}