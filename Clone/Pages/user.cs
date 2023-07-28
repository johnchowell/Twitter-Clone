using System.Data.SQLite;

namespace Clone.Pages;
public class SiteUser
{
    public int id { get; set; }
    public string name { get; set; }
    public string username { get; set; }
    public string password { get; set; }
    public long creation { get; set; }
    public SiteUser(int id, string name, string username, string password, long creation)
    {
        this.id = id;
        this.name = name;
        this.username = username;
        this.password = password;
        this.creation = creation;
    }
    public static SiteUser? GetUser(string username)
    {
        string query = "SELECT * FROM users WHERE username = @username";
        string conString = "Data Source=database.db; version=3";

        using (SQLiteConnection con = new SQLiteConnection(conString))
        {
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.AddWithValue("@username", username);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            if (rdr.Read())
            {
                
                return new SiteUser(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), (long) Convert.ToDouble(rdr.GetInt64(4)));
            }
            else
            {
                
                return null;
            }
        }
    }

    public static SiteUser? CreateUser(string name, string username, string password)
    {
        string query = "INSERT INTO users (name, username, password) VALUES (@name, @username, @password)";
        string conString = "Data Source=database.db; version=3";

        using (SQLiteConnection con = new SQLiteConnection(conString))
        {
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.ExecuteNonQuery();
            con.Close();
            return GetUser(username);
        }
    }

    public Stack<Post> GetPosts()
    {
        string query = "SELECT * FROM posts WHERE username = @id";
        string conString = "Data Source=database.db; version=3";
        Stack<Post> posts = new Stack<Post>();

        using (SQLiteConnection con = new SQLiteConnection(conString))
        {
            con.Open();
            SQLiteCommand cmd = new SQLiteCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                posts.Push(new Post(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), (long) rdr.GetInt64(4)));
            }
        }

        if (posts.Count > 0)
        {
            return posts;
        }
        else
        {
            return new Stack<Post>();
        }
    }
}