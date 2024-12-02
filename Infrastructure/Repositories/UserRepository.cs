using Business.Model.Users;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Adapters;
using Business.Interfaces;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Infrastructure.DTO;

namespace Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase,IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly CentralDbContext _context;
        public UserRepository(IServiceProvider serviceProvider, ILogger<UserRepository> logger, CentralDbContext context) : base(serviceProvider)
        {
            _logger = logger;
            _context = context;
        }
        public void Add(User user)
        {
            _context.Usuarios.Add(user.ToUserDto());
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool ExistsByEmail(string email)
        {
            return _context.Usuarios.Any(u => u.Email == email);
        }
        public User? FindUserByEmail( string email)
        {
            try
            {
                var query = _context.Usuarios.AsQueryable();

                if (!string.IsNullOrEmpty(email))
                {
                    query = query.Where(u => u.Email.Contains(email));
                }

                return query.FirstOrDefault().ToUser();
            }
            catch (SqlException sqlEx)
            {
                // Logando o erro no banco de dados
                _logger.LogError(sqlEx, "Erro ao consultar o banco de dados: {Message}", sqlEx.Message);
                throw new Exception("Erro ao acessar o banco de dados. Consulte o log para mais detalhes.");
            }
            catch (Exception ex)
            {
                // Logando erros gerais
                _logger.LogError(ex, "Erro desconhecido: {Message}", ex.Message);
                throw new Exception("Ocorreu um erro ao processar a consulta. Por favor, tente novamente.");
            }
        }

        public List<User> ListarUsuarios(string nome, string email)
        {
            try
            {
                return ExecuteInScope((CentralDbContext dbContext) =>
                {
                    var query = dbContext.Usuarios.AsQueryable();

                    // Filtros opcionais
                    if (!string.IsNullOrEmpty(nome))
                    {
                        query = query.Where(u => u.Name.Contains(nome));
                    }

                    if (!string.IsNullOrEmpty(email))
                    {
                        query = query.Where(u => u.Email.Contains(email));
                    }

                    // Projeção para o DTO
                    return query.Select(u => new UserDTO
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Email = u.Email,
                        Password = u.Password,
                        RememberToken = u.RememberToken,
                        IsGestor = u.IsGestor,
                        DataUltimoLogin = u.DataUltimoLogin,
                        DataUltimoLogout = u.DataUltimoLogout,
                        DataUltimaAtividade = u.DataUltimaAtividade,
                        IdCli = u.IdCli,
                        CreatedAt = u.CreatedAt,
                        UpdatedAt = u.UpdatedAt
                    }).ToListOfUser().ToList();
                });
            }
            catch (SqlException sqlEx)
            {
                // Logando o erro no banco de dados
                _logger.LogError(sqlEx, "Erro ao consultar o banco de dados: {Message}", sqlEx.Message);
                throw new Exception("Erro ao acessar o banco de dados. Consulte o log para mais detalhes.");
            }
            catch (Exception ex)
            {
                // Logando erros gerais
                _logger.LogError(ex, "Erro desconhecido: {Message}", ex.Message);
                throw new Exception("Ocorreu um erro ao processar a consulta. Por favor, tente novamente.");
            }
        }
    }
}
