using GoodNews.Data.Entities;
using MediatR;

namespace GoodNews.MediatR.Commands.CreateCategory
{
    public class CreateCategory : IRequest<Category>
    {
        public string Name { get; }
        public CreateCategory(string name)
        {
            Name = name;
        }
    }
}
