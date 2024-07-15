using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Repositorios.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        public UserController(IUserRepo userRepo) { _userRepo = userRepo; }

        [HttpPost] //criar novo user
        public async Task<ActionResult<User>> Cadastrar([FromBody] User user)
        {
            User userfinal = await _userRepo.Add(user);

            return Ok(userfinal);
        }

        [HttpGet] // listar todos os users
        public async Task<ActionResult<List<User>>> ListarTodosUsuarios()
        {
            List<User> users = await _userRepo.GetUsers();
            return Ok(users);
        }

        [HttpGet("{id}")] // detalhes de um usuário /users/:id
        public async Task<ActionResult<User>> BuscaPorId(int id)
        {
            User user = await _userRepo.GetById(id);
            return Ok(user);
        }
        [HttpPut("atualizar/{id}")] // att de um user /users/:id
        public async Task<ActionResult<User>> Atualizar([FromBody] User user, int id)
        {
            user.Id = id;
                User useraux = await _userRepo.Att(user, id);
            return Ok(useraux);
        }

        [HttpDelete] //exclusão de um user
        public async Task<ActionResult<User>> Apagar(int id)
        {
            bool verifica = await _userRepo.Delete(id);
            return Ok(verifica);
        }
    }
}
