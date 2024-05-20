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
    public class TiposDoctoController : ControllerBase
    {
        private readonly DocTypeService _docsService;
        private readonly ILogger<DocTypeService> _logger;

        public TiposDoctoController(DocTypeService docsService, ILogger<DocTypeService> logger)
        {
            _docsService = docsService;
            _logger = logger;
        }

        //Endpoint para obtener todos los autores:
        [HttpGet]
        public async Task<IActionResult> GetAllDocsType()
        {
            _logger.LogInformation("Controlador - Obteniendo todos los tipos de documento.");
            return Ok(await _docsService.GetAllDocsTypeAsync());
        }
    }
}
