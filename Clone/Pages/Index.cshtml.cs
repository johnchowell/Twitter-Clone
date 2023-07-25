using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Data;

namespace Clone.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        InitiateDatabase();
    }

    public void InitiateDatabase()
    {
        string conString = "Data Source=database.db; version=3";
        using (SQLiteConnection con = new SQLiteConnection(conString))
        {
            con.Open();
            string sql = "CREATE TABLE IF NOT EXISTS users (id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(20), email VARCHAR(20), password VARCHAR(20))";
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            cmd.ExecuteNonQuery();

            sql = "CREATE TABLE IF NOT EXISTS posts (id INTEGER PRIMARY KEY AUTOINCREMENT, title VARCHAR(20), content VARCHAR(500), user_id varchar(20), date DATETIME)";
            cmd = new SQLiteCommand(sql, con);
            cmd.ExecuteNonQuery();

            con.Close();
        }
    }

    public string GetUser()
    {
        
        if (Request.Cookies.TryGetValue("user", out string user))
        {
            return user;
        }
        else
        {
            return "Not Signed In User";
        }
    }
    public Stack<Post> GetPosts()
    {
        string conString = "Data Source=database.db; version=3";
        using (SQLiteConnection con = new SQLiteConnection(conString))
        {
            con.Open();
            string sql = "SELECT * FROM posts";
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            SQLiteDataReader rdr = cmd.ExecuteReader();
            Stack<Post> posts = new Stack<Post>();
            while (rdr.Read())
            {
                posts.Push(new Post(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetDateTime(4)));
            }
            con.Close();
            
            if (posts.Count == 0)
            {
                posts.Push(new Post(0, "No Posts", "No Posts", "No Posts", DateTime.Now));
            }
            return posts;
        }
    }
}
