using Book_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookAPI.Interfaces;
using Microsoft.AspNetCore.Http;
using Book_API.Utils;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Book_API.Filters.Auth;
using Book_API.Middleware;

namespace Book_API.Controllers
{
    [Route("api/books")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class BookController : Controller
    {
        private IBooksService booksService;
        public BookController(IBooksService booksService)
        {
            this.booksService = booksService;
            BookModel bm1 = new BookModel
            {
                Id = Guid.NewGuid(),
                Name = "test",
                Author = "test",
                Year = 2015
            };
            BookModel bm2 = new BookModel
            {
                Id = Guid.NewGuid(),
                Name = "test 2",
                Author = "test 2",
                Year = 2017
            };
            BookModel bm3 = new BookModel
            {
                Id = Guid.NewGuid(),
                Name = "test 2",
                Author = "test 2",
                Year = -300
            };
            BookModel bmUpdate = new BookModel
            {
                Id = Guid.NewGuid(),
                Name = "test u",
                Author = "test u",
                Year = 2017
            };
            this.Add(bm1);
            this.Add(bm2);
            this.Add(bm3);
            

        }

        [HttpGet]
        [Route("{id}")]
        [Produces("application/json")]
        [Authorize(Policy = Policies.All)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // also StatusCode = 404 ?
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BookModel), StatusCodes.Status200OK)]
        public IActionResult Get([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id cannot be empty");
            var result = booksService.GetById(id);
            if (result == null)
                return NotFound();
            return Ok(result.ToModel());
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Policy = Policies.All)]


        public IActionResult Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var results = booksService.Get(pageNumber, pageSize);
            if (!results.Any())
                return NoContent();

            return Ok(results.Select(x => x.ToModel()));
        }

        [HttpPost]
        [Produces("application/json")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BookModel), StatusCodes.Status200OK)]
        public IActionResult Add([FromBody] BookModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = booksService.Add(model.ToDTO(null));
            return Ok(result.ToModel());
        }

        [HttpPut]
        [Route("{id}")]
        [Produces("application/json")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BookModel), StatusCodes.Status200OK)]
        public IActionResult Update([FromRoute] Guid id, [FromBody] BookModel updatedModel)
        {
            if (id == Guid.Empty)
                return BadRequest("Id cannot be empty");
            if (id != updatedModel.Id)
                return BadRequest("Id's don't match");

            var result = booksService.Update(updatedModel.ToDTO(id));
            if (result)
                return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        [HttpDelete]
        [Route("{id}")]
        [Produces("application/json")]
        [Authorize(Policy = Policies.Admin)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Remove([FromRoute] Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id cannot be empty");
            var result = booksService.Remove(id);
            if (result)
                return Ok();
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
