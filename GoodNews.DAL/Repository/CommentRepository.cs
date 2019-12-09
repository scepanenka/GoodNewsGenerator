using System;
using System.Linq;
using GoodNews.Data;
using GoodNews.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.DAL.Repository
{
    public class CommentRepository : EntityFrameworkRepository<Comment>
    {
        public CommentRepository(GoodNewsContext context) : base(context)
        {
         
        }

    }
}
