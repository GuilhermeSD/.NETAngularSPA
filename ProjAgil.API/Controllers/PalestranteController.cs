using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace ProjAgil.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PalestranteController: ControllerBase
    {
        private readonly IRepository _repository;

        public PalestranteController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{PalestranteId}")]
        public async Task<IActionResult> Get(int PalestranteId)
        {
            try
            {
                return Ok(await _repository.GetPalestrantesAsyncById(PalestranteId, true));    
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou!");                
            }
        }
                // GET api/values
        [HttpGet("getByName/{Nome}")]
        public async Task<IActionResult> Get(string nome)
        {
            try
            {
                return Ok(await _repository.GetAllPalestrantesByNameAsync(nome, true));    
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou!");                
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Palestrante model)
        {
            try
            {
                _repository.Add(model);
                if((await _repository.SaveChangesAsync())) 
                {
                    Created($"api/Palestrante/{model.Id}", model);
                }

                return BadRequest();    
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou!");                
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(int PalestranteId, Palestrante model)
        {
            try
            {
                var p = _repository.GetPalestrantesAsyncById(PalestranteId, false);
                if(p==null)return NotFound();

                _repository.Update(model);
                if((await _repository.SaveChangesAsync())) 
                {
                    Created($"api/Palestrante/{model.Id}", model);
                }

                return BadRequest();    
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Banco de dados Falhou!");                
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int PalestranteId)
        {
            try
            {
                var p = _repository.GetPalestrantesAsyncById(PalestranteId, false);
                if(p==null)return NotFound();

                _repository.Delete(p);
                if((await _repository.SaveChangesAsync())) 
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