using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SuperNumberProject.Components;
using SuperNumberProject.Models;
using SuperNumberProject.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace SuperNumberProject.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperNumberApiController : ControllerBase
    {
        private readonly SuperNumberServices _superNumberServices;

        public SuperNumberApiController(SuperNumberServices superNumberServices)
        {
            _superNumberServices = superNumberServices;
        }

        [HttpPost("create")]
        public IActionResult CreateSuperNumber([FromBody] CreateSuperNumberRequest request)
        {
            var result = _superNumberServices.CreateSuperNumber(request.Number, request.IdUser);
            return Ok(result.Data);
        }

        [HttpDelete("delete/{iduser}")]
        public IActionResult DeleteHistoricalSuperNumbers(Guid iduser)
        {
            var result = _superNumberServices.DeleteHistoricalSuperNumbers(iduser);
            return Ok(result);
        }

        [HttpGet("history/{idUser}")]
        public IActionResult GetHistorical(string idUser)
        {
            try
            {
                Guid id= Guid.Parse(idUser.ToString());
                var historicalData = _superNumberServices.GetHistoricalData(id);
                if (historicalData == null || !historicalData.Any())
                {
                    return NotFound("No se encontró historial para el usuario.");
                }

                return Ok(historicalData);
            }
            catch (Exception ex)
            {
                // Registro de la excepción, si es necesario
                return StatusCode(500, "Ocurrió un error al obtener el historial.");
            }
        }
    }

    public class CreateSuperNumberRequest
    {
        public int Number { get; set; }
        public Guid IdUser { get; set; }
    }
    
}

