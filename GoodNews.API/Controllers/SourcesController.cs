using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodNews.Data.Entities;
using GoodNews.MediatR.Commands.AddSource;
using GoodNews.MediatR.Commands.DeleteSource;
using GoodNews.MediatR.Commands.UpdateSource;
using GoodNews.MediatR.Queries.GetSourceById;
using GoodNews.MediatR.Queries.GetSources;
using GoodNews.MediatR.Queries.SourceExists;
using MediatR;
using Serilog;

namespace GoodNews.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SourcesController : ControllerBase
    {
        private readonly IMediator _mediator;
        // private readonly GoodNewsContext _context;


        public SourcesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get all sources
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Source>> GetSources()
        {
            try
            {
                return await _mediator.Send(new GetSources());
                Log.Information("Sources loaded");
            }
            catch (Exception e)
            {
                Log.Error($"Error loading sources {e.Message}");
                throw;
            }

        }

        // GET: api/Sources/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Source>> GetSource(Guid id)
        {
            var source = await _mediator.Send(new GetSourceById(id));

            if (source == null)
            {
                return NotFound();
            }

            return source;
        }

        // PUT: api/Sources/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSource(Guid id)
        {
            var source = await _mediator.Send(new GetSourceById(id));

            if (id != source.Id)
            {
                Log.Error("Bad request");
                return BadRequest();
            }
            try
            {
                bool result = await _mediator.Send(new UpdateSource(source));
                if (result)
                {
                    Log.Information($"Source '{source.Name}' updated");
                    return Ok();
                }

            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!await SourceExists(id))
                {
                    Log.Error($"Source not founded: {e.Message}");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Sources
        /// <summary>
        /// Add source
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="url"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PostSource([FromBody] string name, string description, string url, string selector)
        {
            var source = new Source()
            {
                Name = name,
                Description = description,
                Url = url,
                QuerySelector = selector
            };

            try
            {

                await _mediator.Send(new AddSource(source));

                Log.Information($"Source was added successfully");

                return StatusCode(201, source);
            }
            catch (Exception e)
            {
                Log.Error($"Error adding source:{Environment.NewLine}{e.Message}");
                return BadRequest();
            }

        }

        // DELETE: api/Sources/5
        /// <summary>
        /// Delete source
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Source>> DeleteSource(Guid id)
        {
            var source = await _mediator.Send(new GetSourceById(id));
            if (source == null)
            {
                return NotFound();
            }

            bool result = await _mediator.Send(new DeleteSource(source));

            return result ? Ok() : StatusCode(500);
        }

        private async Task<bool> SourceExists(Guid id)
        {
            return await _mediator.Send(new SourceExists(id));
        }
    }
}
