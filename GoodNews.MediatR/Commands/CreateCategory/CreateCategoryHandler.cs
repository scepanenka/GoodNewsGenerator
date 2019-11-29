using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using GoodNews.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GoodNews.MediatR.Commands.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategory, Category>
    {
        private readonly GoodNewsContext _context;

        public CreateCategoryHandler(GoodNewsContext context)
        {
            _context = context;
        }
        public async Task<Category> Handle(CreateCategory request, CancellationToken cancellationToken)
        {
            
            
            Category category = await _context.Categories.FirstOrDefaultAsync(n => n.Name.Equals(request.Name),
                cancellationToken: cancellationToken);

            if (category == null)
            {
                category = new Category
                {
                    Name = request.Name
                };
                await _context.Categories.AddAsync(category, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return category;
        }
    }
}
