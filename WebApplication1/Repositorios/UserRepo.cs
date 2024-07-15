using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Repositorios.Interfaces;

namespace WebApplication1.Repositorios
{
  public class UserRepo : IUserRepo
  {
        private readonly SistemadeUsers _dbContext;
    public UserRepo(SistemadeUsers sistemade) 
        { 
            _dbContext = sistemade;
        }


    public async Task<User> Add(User user)
    {
        await _dbContext.Usuarios.AddAsync(user);
        _dbContext.SaveChanges();

            return user;
    }

    public async Task<User> Att(User user, int id)
    {
        User userporid = await GetById(id);
            if (userporid == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            userporid.name = user.name;
            userporid.email = user.email;   
            userporid.password = user.password;
            
            _dbContext.Usuarios.Update(userporid);
            _dbContext.SaveChanges();

            return userporid;
    }

    public async Task<bool> Delete(int id)
    {
            User userporid = await GetById(id);
            if (userporid == null)
            {
                throw new Exception("Usuário não encontrado");
            }

            _dbContext.Usuarios.Remove(userporid);
            _dbContext.SaveChanges();

            return true;
        }

        public Task<User> GetById(int id)
    {
            return _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<User>> GetUsers()
    {
            return _dbContext.Usuarios.ToListAsync();
    }
}
}
