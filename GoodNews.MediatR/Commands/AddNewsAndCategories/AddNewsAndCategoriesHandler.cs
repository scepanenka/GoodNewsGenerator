using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoodNews.Data;
using GoodNews.Data.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace GoodNews.MediatR.Commands.AddNewsAndCategories
{
    class AddNewsAndCategoriesHandler : IRequestHandler<AddNewsAndCategories, bool>
    {
        private readonly GoodNewsContext _context;

        public AddNewsAndCategoriesHandler(GoodNewsContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddNewsAndCategories request, CancellationToken cancellationToken)
        {
            try
            {
                var categories = request.News.Select(a => a.Category.Name).Distinct().ToList();
                await AddCategories(categories);
                List<Article> news = new List<Article>();

                foreach (var article in request.News)
                {
                    Category category =
                        await _context.Categories.FirstAsync(c => c.Name.Equals(article.Category.Name),
                            cancellationToken);
                    article.Category = category;
                    news.Add(article);
                }

                await _context.News.AddRangeAsync(news, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                Log.Information($"AddNewsAndCategories -> SUCCSESS. News added.");
                return true;
            }
            catch (Exception e)
            {
                Log.Error($"AddNewsAndCategories -> FAILED adding news: {Environment.NewLine} {e.Message}");
                return false;
            }
        }

        private async Task AddCategories(IEnumerable<string> names)
        {
            var dbCategories = _context.Categories.Select(c => c.Name).ToList();
            var notExistingCategories = names.Except(dbCategories).ToList();
            List<Category> newCategories = new List<Category>();
            foreach (var name in notExistingCategories)
            {
                Category category = new Category
                {
                    Name = name
                };
                newCategories.Add(category);
            }

            try
            {
                await _context.Categories.AddRangeAsync(newCategories);
                await _context.SaveChangesAsync();
                Log.Information($"AddNewsAndCategories -> SUCCSESS. Categories added.");
            }
            catch (Exception e)
            {
                Log.Error($"AddNewsAndCategories -> FAILED adding categories: {Environment.NewLine} {e.Message}");
                throw;
            }
            
        }
    }
}
