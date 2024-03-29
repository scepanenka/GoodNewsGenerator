﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoodNews.API.Models
{
    public class CommentPostModel
    {

        [Required]
        public Guid ArticleId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
