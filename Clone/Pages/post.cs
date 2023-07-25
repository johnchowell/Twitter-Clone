namespace Clone.Pages;

public class Post
{
    public int id { get; set; }
    public string title { get; set; }
    public string content { get; set; }
    public string user_id { get; set; }
    public DateTime date { get; set; }

    public Post(int id, string title, string content, string user_id, DateTime date)
    {
        this.id = id;
        this.title = title;
        this.content = content;
        this.user_id = user_id;
        this.date = date;
    }
}