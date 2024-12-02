using Business.Model.Users;
using Rest.RequestModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Adapters
{
    public static class UserAdapter
    {
        public static SearchUserResponseModel ToListUserResponseModel(this IEnumerable<User> users)
        {
            return new SearchUserResponseModel
            {
                Users = users.ToList()
            };
        }

    }
}
