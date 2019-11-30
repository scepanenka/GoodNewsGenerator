using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using GoodNews.Data.Entities;
using MediatR;

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
            
            
            Category category = _context.Categories.FirstOrDefault(n => n.Name.Equals(request.Name));

            if (category == null)
            {
                category = new Category
                {
                    Name = request.Name
                };
                _context.Categories.Add(category);
                _context.SaveChanges();
            }
            return await Task.FromResult(category);
        }
    }
}
