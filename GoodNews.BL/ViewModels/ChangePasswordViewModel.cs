﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodNews.BL.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }

        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
