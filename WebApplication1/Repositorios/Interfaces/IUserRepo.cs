namespace WebApplication1.Repositorios.Interfaces
{
    public interface IUserRepo
    {
        Task<List<User>> GetUsers();
        Task<User> GetById(int id);
        Task<User> Add(User user);
        Task<User> Att(User user, int id);
        Task<bool> Delete(int id);

    }
}
