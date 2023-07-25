using System.Data.SQLite;

namespace Clone.Pages;

public class Post
{
    public int id { get; set; }
    public string title { get; set; }
    public string content { get; set; }
    public string user_id { get; set; }
    public DateOnly date { get; set; }
    public TimeOnly time { get; set; }

    public Post(int id, string title, string content, string user_id, TimeOnly time, DateOnly date)
    {
        this.id = id;
        this.title = title;
        this.content = content;
        this.user_id = user_id;
        this.date = date;
        this.time = time;

        string query = "INSERT INTO posts (title, content, user_id) VALUES (@title, @content, @user)";
        string conString = "Data Source=database.db; version=3";

        using (SQLiteConnection con = new SQLiteConnection(conString))
        {
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@content", content);
            cmd.Parameters.AddWithValue("@user", user_id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

    public Post GetPostFromId(int id)
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
                return new Post(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), TimeOnly.Parse(rdr.GetString(4)), DateOnly.Parse(rdr.GetString(5)));
            }
            else
            {
                return null;
            }
        }
    }
}