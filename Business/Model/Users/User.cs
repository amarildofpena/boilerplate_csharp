using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Model.Users
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Password { get; set; } = "";
        public string? RememberToken { get; set; }
        public bool IsGestor { get; set; }
        public DateTime? DataUltimoLogin { get; set; }
        public DateTime? DataUltimoLogout { get; set; }
        public DateTime? DataUltimaAtividade { get; set; }
        public int? IdCli { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
