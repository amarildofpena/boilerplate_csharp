using Business.Model.Users;
using Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Adapters
{
    public static class UserAdapter
    {
        public static User? ToUser(this UserDTO user)
        {
            if (user == null)
                return null;
            return new User
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                IdCli = user.IdCli,
                IsGestor = user.IsGestor,
                RememberToken = user.RememberToken,
                DataUltimoLogin = user.DataUltimoLogin,
                DataUltimaAtividade = user.DataUltimaAtividade,
                DataUltimoLogout = user.DataUltimoLogout,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Password = user.Password
            };

        }

        public static IEnumerable<User> ToListOfUser(this IEnumerable<UserDTO> users)
        {
            foreach (var user in users)
                yield return user.ToUser();
        }

        

        public static UserDTO ToUserDto(this User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                IdCli = user.IdCli,
                IsGestor = user.IsGestor,
                RememberToken = user.RememberToken,
                DataUltimoLogin = user.DataUltimoLogin,
                DataUltimaAtividade = user.DataUltimaAtividade,
                DataUltimoLogout = user.DataUltimoLogout,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Password = user.Password
            };
        }
    }
}
