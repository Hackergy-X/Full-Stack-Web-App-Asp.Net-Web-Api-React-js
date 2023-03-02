using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
            if(i > 0)
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
            Registration reverseUser = new Registration();
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Registration WHERE Email= '"+registration.Email+"' AND Password= '"+registration.Password+"' AND IsActive=1 ", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            reverseUser.Equals(dt);
            if(dt.Rows.Count > 0)
            {
                return Ok("Login Successfully");
            }
            else
            {
                return BadRequest("User Not Found");
            }
        }
    }
}
