using Business.Model.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rest.RequestModels.Users
{
    public class SearchUserResponseModel
    {
        public List<User> Users = new List<User>();
    }
}
