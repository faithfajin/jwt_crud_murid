using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using jwt_crud_murid.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace jwt_crud_murid.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MuridController : Controller
    {
        private string __constr;
        public MuridController(IConfiguration configuration)
        {
            __constr = configuration.GetConnectionString("WebApiDatabase");
        }

        public IActionResult index()
        {
            return View();
        }

        [HttpPost("api/database murid/create data")]
        public IActionResult CreatePerson([FromBody] Murid murid)
        {
            MuridContext context = new MuridContext(this.__constr);
            context.AddDataMurid(murid);
            return Ok("Data murid berhasil di tambahkan");
        }

        [HttpGet("api/database murid/read data")]

        public ActionResult<Murid> ListMurid()
        {
            MuridContext context = new MuridContext(this.__constr);
            List<Murid> ListDataMurid = context.ListDataMurid();
            return Ok(ListDataMurid);
        }

        [HttpPut("api/database murid/update data/{id}")]
        public IActionResult UpdatePerson(int id, [FromBody] Murid murid)
        {
            murid.id_murid = id;
            MuridContext context = new MuridContext(this.__constr);
            context.UpdateDataMurid(murid);
            return Ok("Data murid berhasil di update");
        }

        [HttpDelete("api/database murid/delete data/{id}")]
        public IActionResult DeletePerson(int id)
        {
            MuridContext context = new MuridContext(this.__constr);
            context.DeleteDataMurid(id);
            return Ok("Data murid berhasil di hapus");
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