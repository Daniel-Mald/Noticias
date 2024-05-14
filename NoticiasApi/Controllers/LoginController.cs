using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoticiasApi.Helpers;
using NoticiasApi.Models.DTOs;
using NoticiasApi.Models.Entities;
using NoticiasApi.Repositories;
using System.Security.Claims;

namespace NoticiasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IRepository<Usuarios> _repos;
        private readonly JwtHelper _jwtHelper;
        public LoginController(IRepository<Usuarios> repos, JwtHelper _helper)
        {
            _repos = repos;
            _jwtHelper = _helper;
        }


        [HttpPost]
        public IActionResult Authenticate(LoginDTO dto)
        {
            var user = _repos.GetAll().FirstOrDefault(x => x.NombreUsuario == dto.Usuario && x.Contraseña == dto.Contraseña);

            if(user == null)
            {
                return Unauthorized();
            }
            var token  = _jwtHelper.GetToken(user.Nombre,user.EsAdmin == true?"Admin":"Periodista",
                new List<Claim> { new Claim("Id", user.Id.ToString()) });


            return Ok(token);
        }
    }
}
