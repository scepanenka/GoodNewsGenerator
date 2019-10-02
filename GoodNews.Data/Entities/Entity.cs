using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GoodNews.DAL.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        
    }
}
