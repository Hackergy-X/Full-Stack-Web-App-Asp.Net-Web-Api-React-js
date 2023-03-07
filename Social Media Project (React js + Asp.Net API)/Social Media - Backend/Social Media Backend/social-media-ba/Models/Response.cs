using System.ComponentModel.DataAnnotations.Schema;

namespace social_media_ba.Models
{
    public class Response
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public List<Registration> ListRegistration { get; set; }
        public Registration Registration { get; set; }
        public List<Article> ListArticle { get; set; }
        public List<News> ListNews { get; set; }
        public List<Events> ListEvents { get; set; }

    }
}
