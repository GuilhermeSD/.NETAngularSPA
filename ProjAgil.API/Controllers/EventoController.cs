using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjAgil.API.Dtos;
using Repository;

namespace ProjAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;
        public EventoController(IRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _repository.GetAllEventosAsync(true);
                var result = _mapper.Map<IEnumerable<EventoDto>>(eventos);
                return Ok(result);
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou!");
            }
        }

        [HttpPost("upload")]
        public IActionResult Upload()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folder = Path.Combine("Resourcers", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folder);

                if(file.Length > 0) 
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, fileName);

                    using(var stream = new FileStream(fullPath, FileMode.Create)) 
                    {
                        file.CopyTo(stream);
                    }
                    return Ok();
                }                
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou!");
            }

            return BadRequest();
        }

        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)
        {
            try
            {
                return Ok(await _repository.GetEventosAsyncById(EventoId, true));
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou!");
            }
        }

        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                return Ok(await _repository.GetAllEventosAsyncByTema(tema, true));
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou!");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);
                _repository.Add(evento);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/evento/{evento.Id}", _mapper.Map<EventoDto>(evento));
                }

                return BadRequest();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou!");
            }
        }

        [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int EventoId, EventoDto model)
        {
            try
            {
                var e = await _repository.GetEventosAsyncById(EventoId, false);
                if (e == null) return NotFound();

                _mapper.Map(model, e);
                _repository.Update(e);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"/api/evento/{e.Id}", _mapper.Map<EventoDto>(e));
                }

                return BadRequest();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou!");
            }
        }

        [HttpDelete("{EventoId}")]
        public async Task<IActionResult> Delete(int EventoId)
        {
            try
            {
                var e = await _repository.GetEventosAsyncById(EventoId, false);
                if (e == null) return NotFound();

                _repository.Delete(e);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }

                return BadRequest();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou!");
            }
        }
    }
}