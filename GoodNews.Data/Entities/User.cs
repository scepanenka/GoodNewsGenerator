using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace GoodNews.Data.Entities
{
    public class User : IdentityUser
    {
        public DateTime BirthDate { get; set; }
    }
}
