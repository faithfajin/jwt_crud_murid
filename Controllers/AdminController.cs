using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using jwt_crud_murid.Models;
using jwt_crud_murid.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace jwt_crud_murid.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly SqlDBHelper _sqlDBHelper;

        public AdminController(IConfiguration config, SqlDBHelper sqlDBHelper)
        {
            _config = config;
            _sqlDBHelper = sqlDBHelper;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Admin login)
        {
            IActionResult response = Unauthorized();

            var admin = await _sqlDBHelper.GetAdminByUsernameAsync(login.Username);

            if (admin != null && admin.Password == login.Password)
            {
                var tokenString = GenerateJSONWebToken(admin);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(Admin admin)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

// Ini buat databasenya Muirdnya kalau mau mencoba //

//CREATE TABLE murid (
//    id_murid SERIAL PRIMARY KEY,
//    nama VARCHAR(100),
//    alamat VARCHAR(255),
//    email VARCHAR(100)
//);

//INSERT INTO murid (nama, alamat, email) VALUES
//('Faith Reyhan', 'Mastrip', 'faithreyhan@gmail.com'),
//('Elian Waluyo', 'Kaliurang', 'elianwaluyo@gmail.com'),
//('Sehat Abadi', 'Sumbersari', 'sehatabadi@gmail.com');

// Ini buat database Adminua kalau mau mencoba //

//CREATE TABLE Admin (
//    id SERIAL PRIMARY KEY,
//    username VARCHAR(50) UNIQUE NOT NULL,
//    password VARCHAR(100) NOT NULL
//);

//INSERT INTO Admin (username, password) 
//VALUES ('faith', 'faith123');
