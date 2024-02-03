using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotifyLike.Domain.Streaming.Aggregates;
using SpotifyLike.Repository;

namespace SpotifyLike.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BandaController : ControllerBase
    {

        private SpotifyLikeContext Context{ get; set; }

        public BandaController(SpotifyLikeContext context)
        {
            Context = context;
        }

        [HttpGet]

        public IActionResult GetBandas() 
        {
            var result = this.Context.Bandas.ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]

        public IActionResult GetBandas(Guid id) 
        {

            var result = this.Context.Bandas.Where(x => x.Id == id).FirstOrDefault();

            if (result == null) 
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult SaveBanda([FromBody] Banda banda)
        {
            this.Context.Bandas.Add(banda);
            this.Context.SaveChanges();

            return Created($"/banda/{banda.Id}", banda);
        }
    }
}
