using System.Collections.Generic;
using System.Threading.Tasks;
using GoodNews.Core;
using Microsoft.AspNetCore.Mvc;

namespace GoodNews.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly INewsService _newsService;

        public ValuesController(IParser parser, INewsService newsService)
        {
            _newsService = newsService;
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            await _newsService.Run();
            return new string[] { "value1", "value2" };
        }
    }
}
