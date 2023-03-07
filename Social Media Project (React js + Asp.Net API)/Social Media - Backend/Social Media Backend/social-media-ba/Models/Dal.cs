using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;
namespace social_media_ba.Models
{
    public class Dal
    {
        public Response Registration(Registration registration, SqlConnection connection)
        {
            Response response = new Response();

            SqlCommand cmd = new SqlCommand("INSERT INTO Registration(Name,Email,Password,PhoneNo,IsActive,IsApproved) VALUES('" + registration.Name + "','" + registration.Email + "','" + registration.Password + "','" + registration.PhoneNo + "',1,0)", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if(i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Registration successful";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Registration failed";
            }

            return response;
        }
        
        public Response Login(Registration registration, SqlConnection connection)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Registration WHERE Email= '" + registration.Email + "' AND Password= '" + registration.Password + "' ", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            Response response = new Response();
            if(dt.Rows.Count > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Login successful";
                Registration reg = new Registration();
                reg.Id = Convert.ToInt32(dt.Rows[0]["Id"]);
                reg.Name = Convert.ToString(dt.Rows[0]["Name"]);
                reg.Email = Convert.ToString(dt.Rows[0]["Email"]);
                response.Registration = reg;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Login failed";
                response.Registration = null;

            }

            return response;
        }

        public Response UserApproval(Registration registration, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("UPDATE Registration SET IsApproval = 1 WHERE Id= '" +registration.Id+ "' AND IsActive= 1 ", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if(i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "User approved";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "User approval failed";
            }


            return response;
        }
        public Response AddNews(News news, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("INSERT INTO News(Title,Content,Email,IsActive,CreatedOn) VALUES('"+news.Title+"','" + news.Content + "','" +news.Email+ "',1,GETDATE())", connection);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "News created";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "News creation failed";
            }

            return response;
        }

        public Response NewsList(SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM News WHERE IsActive = 1", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<News> lstNews = new List<News>();
            if(dt.Rows.Count > 0)
            {
                for(int i=0; i < dt.Rows.Count; i++)
                {
                    News news = new News();
                    news.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    news.Title = Convert.ToString(dt.Rows[i]["Title"]);
                    news.Content = Convert.ToString(dt.Rows[i]["Content"]);
                    news.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    news.IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]);
                    news.CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]);
                    lstNews.Add(news);
                }
               if(lstNews.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "News data found";
                    response.ListNews = lstNews;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "News data not found";
                    response.ListNews = null;
                }

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "News data not found";
                response.ListNews = null;
            }

            return response;
        }
        public Response AddArticle(Article article, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("INSERT INTO Article(Title,Content,Email,Image,IsActive,ISApproved) VALUES('" + article.Title+"','" + article.Content + "','" + article.Email+ "','"+ article.Image+ "',1,0)", connection);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Article created";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Article creation failed";
            }

            return response;
        }
        public Response ArticleList(Article article,SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter da = null;
            if (article.type == "User")
            {
                new SqlDataAdapter("SELECT * FROM Article WHERE Email= '" +article.Email+ "' AND IsActive = 1", connection);

            }
            if (article.type == "Page")
            {
                new SqlDataAdapter("SELECT * FROM Article WHERE IsActive = 1", connection);

            }
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Article> lstArticle = new List<Article>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Article art = new Article();
                    art.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    art.Title = Convert.ToString(dt.Rows[i]["Title"]);
                    art.Content = Convert.ToString(dt.Rows[i]["Content"]);
                    art.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    art.Image = Convert.ToString(dt.Rows[i]["Email"]);
                    art.IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]);
                    lstArticle.Add(art);
                }
                if (lstArticle.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Article data found";
                    response.ListArticle = lstArticle;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Article data not found";
                    response.ListArticle = null;
                }

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Article data not found";
                response.ListArticle = null;
            }

            return response;
        }

        public Response ArticleApproval(Article article, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("UPDATE Article SET IsApproval = 1 WHERE Id= '" + article.Id + "' AND IsActive= 1 ", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Article approved";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Article approval failed";
            }


            return response;
        }

        public Response StaffRegistration(Staff staff, SqlConnection connection)
        {
            Response response = new Response();

            SqlCommand cmd = new SqlCommand("INSERT INTO Staff(Name,Email,Password,IsActive) VALUES('" + staff.Name + "','" + staff.Email + "','" + staff.Password + "',1)", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Staff registration successful";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Staff registration failed";
            }

            return response;
        }
        public Response DeleteStaff(Staff staff, SqlConnection connection)
        {
            Response response = new Response();

            SqlCommand cmd = new SqlCommand("DELETE FROM Staff WHERE Id= '" + staff.Id + "' AND IsActive = 1", connection);
            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Staff deleted successful";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Staff deletion failed";
            }

            return response;
        }

        public Response AddEvent(Events events, SqlConnection connection)
        {
            Response response = new Response();
            SqlCommand cmd = new SqlCommand("INSERT INTO Events(Title,Content,Email,IsActive,CreatedOn) VALUES('" + events.Title + "','" + events.Content + "','" + events.Email + "',1,GETDATE())", connection);

            connection.Open();
            int i = cmd.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Events created";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Events creation failed";
            }

            return response;
        }
        public Response EventList(SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Events WHERE IsActive = 1", connection);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Events> lstEvents = new List<Events>();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Events evt = new Events();
                    evt.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                    evt.Title = Convert.ToString(dt.Rows[i]["Title"]);
                    evt.Content = Convert.ToString(dt.Rows[i]["Content"]);
                    evt.Email = Convert.ToString(dt.Rows[i]["Email"]);
                    evt.IsActive = Convert.ToInt32(dt.Rows[i]["IsActive"]);
                    evt.CreatedOn = Convert.ToString(dt.Rows[i]["CreatedOn"]);
                    lstEvents.Add(evt);
                }
                if (lstEvents.Count > 0)
                {
                    response.StatusCode = 200;
                    response.StatusMessage = "Events data found";
                    response.ListEvents = lstEvents;
                }
                else
                {
                    response.StatusCode = 100;
                    response.StatusMessage = "Events data not found";
                    response.ListEvents = null;
                }

            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "Events data not found";
                response.ListEvents = null;
            }

            return response;
        }
    }
}
