using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using social_media_ba.Models;

namespace social_media_ba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EventsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("AddEvent")]

        public Response AddEvent(Events events)
        {
            Response response = new Response();

            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SMCon").ToString());
            Dal dal = new Dal();
            response = dal.AddEvent(events, connection);
            return response;
        }


        [HttpGet]
        [Route("EventList")]

        public Response EventList()
        {
            Response response = new Response();

            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SMCon").ToString());
            Dal dal = new Dal();
            response = dal.EventList(connection);
            return response;
        }
    }
}
