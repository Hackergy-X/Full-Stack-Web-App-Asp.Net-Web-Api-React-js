using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;
using System.Data;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IConfiguration _configuration;


        public RegistrationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("registration")]
        public async Task<ActionResult<string>> registration(Registration registration)
        {
            Registration reverseUser = new Registration();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection").ToString());
            SqlCommand cmd = new SqlCommand("INSERT INTO Registration(UserName,Password,Email,IsActive) VALUES('" + registration.UserName + "','" + registration.Password + "','" + registration.Email + "','1')", con);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i > 0)
            {
                return Ok("Data Inserted");
            }
            else
            {
                return BadRequest("Error");
            }


            return "Null";
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> login(Registration registration)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            var query = "SELECT * FROM Registration WHERE Email = '" + registration.Email + "' AND Password = '" + registration.Password + "'";

            using (var connection = new SqlConnection(connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    var reader = await command.ExecuteReaderAsync();
                    var info = new List<RegistrationDto>();

                    while (reader.Read())
                    {
                        info.Add(new RegistrationDto
                        {
                            Id = (int)reader["Id"],
                            UserName = reader["UserName"].ToString(),
                            Password = reader["Password"].ToString(),
                            Email = reader["Email"].ToString(),
                            IsActive = (int)reader["IsActive"]
                        });
                    }

                    connection.Close();

                    if (info.Count == 0)
                    {
                        return BadRequest("Login Failed");
                    }
                    else
                    {
                        return Ok(info);
                    }
                    
                }
                //SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection").ToString());
                //SqlCommand cmd = new SqlCommand("SELECT * FROM Registration WHERE Email = '" + registration.Email + "' AND Password = '" + registration.Password + "'", con);
                //con.Open();
                //SqlDataReader dr = cmd.ExecuteReader();
                //if (dr.Read())
                //{
                //    // return the executeReader
                //    return Ok(dr.Read());
                //}
                //else
                //{
                //    return BadRequest("Login Failed");
                //}
                return BadRequest("Login Failed");
            }
        }
    }
}
