using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Biblioteca.DAL.Models;
using API_Biblioteca.BLL.Services;
using Sistema_Biblioteca_Shared;

namespace API_Biblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly ILogger<AutoresController> _logger;
        public LibrosController(BookService bookService, ILogger<AutoresController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        //Endpoint para obtener todos los libros:
        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            _logger.LogInformation("Controlador - Obteniendo todos los libros.");
            return Ok(await _bookService.GetAllBooksAsync());
        }
            

        //Endpoint para obtener un libro por id:
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            _logger.LogInformation("Controlador - Obteniendo un libro por Id {LibroId}", id);
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                _logger.LogWarning("Controlador - El libro con Id {LibroId} no fue encontrado.", id);
                return NotFound();
            }
                
            return Ok(book);
        }

        //Endpoint para agregar un libro:
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] LibroDTO book)
        {
            _logger.LogInformation("Controlador - Agregando un nuevo libro.");
            try
            {
                await _bookService.AddBookAsync(book);
                await _bookService.CompleteAsync();
                return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Controlador - Error agregando un libro.");
                return BadRequest(new { message = ex.Message });
            }
        }

        //Endpoint para modificar un libro por id:
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] LibroDTO book)
        {
            if (id != book.Id)
            {
                _logger.LogWarning("Controlador - El id ingresado no coincide para la actualización del libro con Id {LibroId}", id);
                return BadRequest();
            }               

            try
            {
                await _bookService.UpdateBookAsync(book);
                await _bookService.CompleteAsync();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Controlador - Error actualizando los datos del libro.");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Controlador - Error actualizando el libro con Id {LibroId}.", id);
                return StatusCode(500, "Se ha producido un error en el servidor.");
            }
        }

        //Endpoint para eliminar un libro por id:
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            _logger.LogInformation("Controlador - Eliminando libro con Id {LibroId}", id);
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                _logger.LogWarning("Controlador - El libro con Id {LibroId} no fue encontrado.", id);
                return NotFound();
            }

            await _bookService.DeleteBookAsync(id);
            await _bookService.CompleteAsync();
            return NoContent();
        }
    }
}
