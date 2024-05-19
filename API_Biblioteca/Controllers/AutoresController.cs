using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Biblioteca.DAL.Models;
using API_Biblioteca.BLL.Services;

namespace API_Biblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly AuthorService _authorService;
        public AutoresController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        //Endpoint para obtener todos los autores:
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors() => Ok(await _authorService.GetAllAuthorsAsync());

        //Endpoint para obtener un autor por id:
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        //Endpoint para agregar un libro:
        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] Autor author)
        {
            try
            {
                await _authorService.AddAuthorAsync(author);
                await _authorService.CompleteAsync();
                return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //Endpoint para modificar un autor por id:
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] Autor author)
        {
            if (id != author.Id) return BadRequest();

            try
            {
                await _authorService.UpdateAuthor(author);
                await _authorService.CompleteAsync();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null) return NotFound();
            _authorService.DeleteAuthor(author);
            await _authorService.CompleteAsync();
            return NoContent();
        }
    }
}
