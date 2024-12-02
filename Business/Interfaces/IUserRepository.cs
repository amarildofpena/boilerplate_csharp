using Business.Model.Users;

namespace Business.Interfaces
{
    public interface IUserRepository
    {
        List<User> ListarUsuarios(string nome, string email);
        User? FindUserByEmail(string email);
        void Add(User user);
        void SaveChanges();
        bool ExistsByEmail(string email);


    }
}
