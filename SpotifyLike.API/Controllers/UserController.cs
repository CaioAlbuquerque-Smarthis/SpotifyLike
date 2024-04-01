using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using SpotifyLike.Application.Conta;
using SpotifyLike.Application.Conta.Dto;
using SpotifyLike.Application.Streaming.Dto;
using SpotifyLike.Domain.Conta.Aggregates;

namespace SpotifyLike.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UsuarioService _usuarioService;

        public UserController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public IActionResult Criar(UsuarioDto dto)
        {
            if (ModelState is { IsValid: false })
                return BadRequest();

            var result = this._usuarioService.Criar(dto);

            return Ok(result);
        }


        [HttpGet("{id}")]
        public IActionResult Obter(Guid id)
        {
            var result = this._usuarioService.Obter(id);

            if (result == null)
                return NotFound();

            return Ok(result);

        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Request.LoginRequest login)
        {
            if (ModelState.IsValid == false)
                return BadRequest();

            var result = this._usuarioService.Autenticar(login.Email, login.Senha);

            if (result == null)
            {
                return BadRequest(new
                {
                    Error = "email ou senha inválidos"
                });
            }

            return Ok(result);
        }

        [HttpPost("favoritar/{idUsuario}")]
        public IActionResult Favoritar([FromBody] MusicaDto musicaDto, Guid idUsuario) 
        {
            if (ModelState.IsValid == false)
                return BadRequest();
            var result = this._usuarioService.Favoritar(musicaDto, idUsuario);
            return Ok(result);
        }

    }
}
