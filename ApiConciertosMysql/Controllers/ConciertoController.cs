using ApiConciertosMysql.Models;
using ApiConciertosMysql.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiConciertosMysql.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConciertoController : ControllerBase
    {
        private RepositoryConcierto repo;

        public ConciertoController(RepositoryConcierto repo)
        {
            this.repo = repo;
        }

        [HttpGet]

        public async Task<ActionResult<List<CategoriaEvento>>> GetCategorias()
        {
            return await this.repo.GetCategoriasEventosAsync();
        }


        [HttpGet]

        public async Task<ActionResult<List<Eventos>>> GetEventos()
        {
            return await this.repo.GetEventosAsync();
        }

        [HttpGet("{idcategoria}")]
       
        public async Task<ActionResult<List<Eventos>>> GetEventosPorCategoria(int idcategoria)
        {
            return await this.repo.GetEventosPorCategoriaAsync(idcategoria);
        }


        [HttpPost]
        
        public async Task<ActionResult> InsertarEvento(Eventos eventos)
        {
            await this.repo.InsertarEventoAsync(eventos.Nombre, eventos.Artista, eventos.IdCategoria, eventos.Imagen);
            return Ok();
        }
    }
}
