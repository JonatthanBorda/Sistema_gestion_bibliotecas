﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Biblioteca.DAL.Models;
using API_Biblioteca.BLL.Services;
using Microsoft.AspNetCore.Authorization;

namespace API_Biblioteca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly AuthorService _authorService;
        private readonly ILogger<AutoresController> _logger;

        public AutoresController(AuthorService authorService, ILogger<AutoresController> logger)
        {
            _authorService = authorService;
            _logger = logger;
        }

        //Endpoint para obtener todos los autores:
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            _logger.LogInformation("Controlador - Obteniendo todos los autores.");
            return Ok(await _authorService.GetAllAuthorsAsync());
        }

        //Endpoint para obtener un autor por id:
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            _logger.LogInformation("Controlador - Obteniendo un autor por Id {AutorId}", id);
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
            {
                _logger.LogWarning("Controlador - El autor con Id {AutorId} no fue encontrado.", id);
                return NotFound();
            }
                
            return Ok(author);
        }

        //Endpoint para agregar un autor:
        [Authorize(Policy = "AdminPolicy")] //Funcionalidad solo para rol Admin
        [HttpPost]
        public async Task<IActionResult> AddAuthor([FromBody] Autor author)
        {
            _logger.LogInformation("Controlador - Agregando un nuevo autor.");
            try
            {
                await _authorService.AddAuthorAsync(author);
                await _authorService.CompleteAsync();
                _logger.LogInformation("Controlador - Autor agregado OK.");
                return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Controlador - Error agregando un autor");
                return BadRequest(new { message = ex.Message });
            }
        }

        //Endpoint para modificar un autor por id:
        [Authorize(Policy = "AdminPolicy")] //Funcionalidad solo para rol Admin
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] Autor author)
        {
            if (id != author.Id)
            {
                _logger.LogWarning("Controlador - El id ingresado no coincide para la actualización del autor con Id {AutorId}", id);
                return BadRequest();
            }
                             
            try
            {
                await _authorService.UpdateAuthorAsync(author);
                await _authorService.CompleteAsync();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Controlador - Error actualizando los datos del autor.");
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Controlador - Error actualizando el autor con Id {AutorId}.", id);
                return StatusCode(500, "Se ha producido un error en el servidor.");
            }
        }

        //Endpoint para eliminar un autor por id:
        [Authorize(Policy = "AdminPolicy")] //Funcionalidad solo para rol Admin
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            _logger.LogInformation("Eliminando autor con Id {AutorId}", id);
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
            {
                _logger.LogWarning("El autor con Id {AutorId} no fue encontrado.", id);
                return NotFound();
            }
                
            await _authorService.DeleteAuthorAsync(id);
            await _authorService.CompleteAsync();
            return NoContent();
        }
    }
}
