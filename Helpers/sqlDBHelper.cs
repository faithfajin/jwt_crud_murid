using Npgsql;
using System.Data;
using jwt_crud_murid.Models;
using jwt_crud_murid.Models;

namespace jwt_crud_murid.Helpers
{
    public class SqlDBHelper
    {
        private NpgsqlConnection connection;
        private string constr;

        public SqlDBHelper(IConfiguration configuration)
        {
            constr = configuration.GetConnectionString("WebApiDatabase");
            connection = new NpgsqlConnection(constr);
        }

        public SqlDBHelper(string constr)
        {
            this.constr = constr;
            connection = new NpgsqlConnection(constr);
        }

        public NpgsqlCommand GetNpgsqlCommand(string query)
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = query;
            cmd.CommandType = CommandType.Text;
            return cmd;
        }

        public void closeConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public async Task<Admin> GetAdminByUsernameAsync(string username)
        {
            string query = "SELECT * FROM Admin WHERE username = @Username";
            using (NpgsqlCommand cmd = GetNpgsqlCommand(query))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                using (NpgsqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        return new Admin
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Username = reader["username"].ToString(),
                            Password = reader["password"].ToString()
                        };
                    }
                    else
                    {
                        return null;
                    }
                }
            }
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
