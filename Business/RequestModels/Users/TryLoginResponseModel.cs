using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.RequestModels.Users
{
    public class TryLoginResponseModel
    {
        public string Token { get; set; }
        public DateTime ValidadeToken { get; set; }
    }
}
