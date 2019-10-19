﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoodNews.BL.ViewModels
{
    public class EditUserViewModel
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
