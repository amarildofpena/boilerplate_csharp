using Business.Extras;
using Business.Interfaces;
using Business.Model.Users;
using Business.RequestModels.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public ResponseWrapper<IEnumerable<User>> buscarUsuarios(string name, string email)
        {
            try
            {
                var users = _userRepository.ListarUsuarios(name, email);

                if (!users.Any())
                    return new ResponseWrapper<IEnumerable<User>>(404,null,ErrorCodes.GetMessage(ErrorCodes.UserNotFound));

                return new ResponseWrapper<IEnumerable<User>>(200, users);
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<IEnumerable<User>>(500,null,ErrorCodes.GetMessage(ErrorCodes.DatabaseError));
            }
        }
        public ResponseWrapper<CreateUserResponseModel> CreateUser(CreateUserPayloadModel userDto)
        {
            try
            {
                if (_userRepository.ExistsByEmail(userDto.Email))
                    return new ResponseWrapper<CreateUserResponseModel>(400, null, "E-mail já está em uso.");
                
                var user = new User
                {
                    Name = userDto.Name,
                    Email = userDto.Email,
                    Password = HashPassword(userDto.Password),
                    IsGestor = userDto.IsGestor,
                    IdCli = userDto.IdCli,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _userRepository.Add(user);
                _userRepository.SaveChanges();
                var userResponse = new CreateUserResponseModel
                {
                    Name = user.Name,
                    Email = user.Email, 
                };

                return new ResponseWrapper<CreateUserResponseModel>(201, userResponse);
            }
            catch (Exception ex)
            {
                return new ResponseWrapper<CreateUserResponseModel>(500, null, $"Erro ao cadastrar usuário: {ex.Message}");
            }
        }
        public ResponseWrapper<string> Authenticate(string email, string password)
        {
            var user = _userRepository.FindUserByEmail(email);

            if (user == null || !VerifyPassword(password, user.Password))
            {
                return new ResponseWrapper<string>(401, null, "E-mail ou senha inválidos.");
            }

            var token = GenerateJwtToken(user);

            return new ResponseWrapper<string>(200, token);
        }

        #region private zone
        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.Name),
                new Claim("isGestor", user.IsGestor.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:ExpiresInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        #endregion

    }
}
