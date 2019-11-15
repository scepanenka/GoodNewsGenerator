﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoodNews.Data.Entities;

namespace GoodNews.MVC.ViewModels
{
    public class CommentViewModel
    {
        public string Author { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

    }
}
