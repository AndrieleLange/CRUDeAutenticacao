using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Data;
using WebApplication1.Repositorios;
using WebApplication1.Repositorios.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        public AutenticacaoController(IUserRepo userRepo) { _userRepo = userRepo; }

        private async Task<User?> Get(string email, string password)
        {
            var users = await _userRepo.GetUsers();
            return users.Where(x => x.email == email && x.password == password).FirstOrDefault();
        }


        [HttpPost("{email}/{senha}")]
        public async Task<ActionResult<dynamic>> Authenticate(string email, string senha)
        {
            // Recupera o usuário=> 
            var user = await Get(email, senha);

            // Verifica se o usuário existe
            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            // Gera o Token
            var token = TokenService.GenerateToken(user);

            // Oculta a senha
            user.password = "";

            // Retorna os dados
            return new
            {
                user = user,
                token = token
            };
        }

    }
}
