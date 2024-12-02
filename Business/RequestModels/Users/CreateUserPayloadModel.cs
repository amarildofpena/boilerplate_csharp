using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.RequestModels.Users
{
    public class CreateUserPayloadModel
    {
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public bool IsGestor { get; set; }
        public int? IdCli { get; set; }
        
    }
}
