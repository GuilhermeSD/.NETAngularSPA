using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace ProjAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController: ControllerBase
    {
        private readonly IRepository _repository;

        public EventoController(IRepository repository)
        {
            _repository = repository;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _repository.GetAllEventosAsync(true));    
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou!");                
            }
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
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                _repository.Add(model);
                
                if(await _repository.SaveChangesAsync()) 
                {
                    return Created($"/api/evento/{model.Id}", model);                        
                }

                return BadRequest();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou!");                
            }
        }    

        [HttpPut("{EventoId}")]
        public async Task<IActionResult> Put(int EventoId, Evento model)
        {
            try
            {
                var e = await _repository.GetEventosAsyncById(EventoId, false);
                if(e==null) return NotFound();

                _repository.Update(model);
                
                if(await _repository.SaveChangesAsync()) 
                {
                    return Created($"/api/evento/{model.Id}", model);                        
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
                if(e==null) return NotFound();
                
                _repository.Delete(e);
                
                if(await _repository.SaveChangesAsync()) 
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