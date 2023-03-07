using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using social_media_ba.Models;

namespace social_media_ba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public NewsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("AddNews")]

        public Response AddNews(News news)
        {
            Response response = new Response();

            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SMCon").ToString());
            Dal dal = new Dal();
            response = dal.AddNews(news, connection);
            return response;
        }


        [HttpGet]
        [Route("NewsList")]

        public Response NewsList(News news)
        {
            Response response = new Response();

            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SMCon").ToString());
            Dal dal = new Dal();
            response = dal.NewsList(connection);
            return response;
        }
    }
}
