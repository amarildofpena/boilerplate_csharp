using Business.Extras;
using Business.Model.Users;
using Business.RequestModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUserService
    {
        ResponseWrapper<IEnumerable<User>> buscarUsuarios(string nome, string email);
        ResponseWrapper<CreateUserResponseModel> CreateUser(CreateUserPayloadModel userDto);
        ResponseWrapper<string> Authenticate(string email, string password);
    }
}
