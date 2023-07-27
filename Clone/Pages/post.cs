using System.Data.SQLite;

namespace Clone.Pages;

public class Post
{
    public int id { get; set; }
    public string title { get; set; }
    public string content { get; set; }
    public string username { get; set; }
    public long time { get; set; }

    public Post(int id, string title, string content, string username, long time)
    {
        this.id = id;
        this.title = title;
        this.content = content;
        this.username = username;
        this.time = time;
    }
    public void MakePost()
    {

        string query = "INSERT INTO posts (title, content, username) VALUES (@title, @content, @user)";
        string conString = "Data Source=database.db; version=3";

        using (SQLiteConnection con = new SQLiteConnection(conString))
        {
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@content", content);
            cmd.Parameters.AddWithValue("@user", username);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    public Post? GetPostFromId(int id)
    {
        string conString = "Data Source=database.db; version=3";
        using (SQLiteConnection con = new SQLiteConnection(conString))
        {
            con.Open();
            string sql = "SELECT * FROM posts WHERE id = @id";
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", id);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                con.Close();
                return new Post(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), int.Parse(rdr.GetString(4)));
            }
            else
            {
                con.Close();
                return null;
            }
        }
    }


    public static string GetTimeRelative(long time)
    {
        switch (DateTime.Now.Subtract(new DateTime(time)).Days)
        {
            case 0:
                return "Today";
            case 1:
                return "Yesterday";
            default:
                return DateTime.Now.Subtract(new DateTime(time)).Days + " days ago";
        }
    }
}