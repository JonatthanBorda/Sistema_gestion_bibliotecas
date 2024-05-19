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
    public class LibrosController : ControllerBase
    {
        private readonly BookService _bookService;
        public LibrosController(BookService bookService)
        {
            _bookService = bookService;
        }

        //Endpoint para obtener todos los libros:
        [HttpGet]
        public async Task<IActionResult> GetAllBooks() => Ok(await _bookService.GetAllBooksAsync());

        //Endpoint para obtener un libro por id:
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        //Endpoint para agregar un libro:
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] Libro book)
        {
            try
            {
                await _bookService.AddBookAsync(book);
                await _bookService.CompleteAsync();
                return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //Endpoint para modificar un libro por id:
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Libro book)
        {
            if (id != book.Id) return BadRequest();

            try
            {
                _bookService.UpdateBook(book);
                await _bookService.CompleteAsync();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        //Endpoint para eliminar un libro por id:
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null) return NotFound();
            _bookService.DeleteBook(book);
            await _bookService.CompleteAsync();
            return NoContent();
        }
    }
}
