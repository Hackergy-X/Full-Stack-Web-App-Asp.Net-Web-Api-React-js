using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using social_media_ba.Models;

namespace social_media_ba.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ArticleController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("AddArticle")]

        public Response AddArticle(Article article)
        {
            Response response = new Response();

            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SMCon").ToString());
            Dal dal = new Dal();
            response = dal.AddArticle(article, connection);
            return response;
        }


        [HttpGet]
        [Route("ArticleList")]

        public Response ArticleList(Article article)
        {
            Response response = new Response();

            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SMCon").ToString());
            Dal dal = new Dal();
            response = dal.ArticleList(article,connection);
            return response;
        }

        [HttpPost]
        [Route("ArticleApproval")]
        public Response ArticleApproval(Article article)
        {
            Response response = new Response();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SMCon").ToString());
            Dal dal = new Dal();
            response = dal.ArticleApproval(article, connection);
            return response;
        }
    }
}
