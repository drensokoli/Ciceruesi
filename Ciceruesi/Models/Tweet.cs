namespace Ciceruesi.Models
{
    public class Tweet
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }

        public Tweet()
        {
            this.Created = DateTime.Now;
        }
    }
}
