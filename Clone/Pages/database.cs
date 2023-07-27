using System.Data.SQLite;

namespace Clone.Pages;

public class database
{
    private static string userElements = "id INTEGER PRIMARY KEY AUTOINCREMENT, name VARCHAR(50), username varchar(50), password varchar(50), creation INTEGER DEFAULT (UNIXEPOCH())";
    private static string postElements = "id INTEGER PRIMARY KEY AUTOINCREMENT, title TEXT, content TEXT, user_id varchar(50), timestamp varchar(8) DEFAULT (UNIXEPOCH())";
    private static string conString = "Data Source=database.db; version=3";

    public static void InitiateDatabase()
    {
        using (SQLiteConnection con = new SQLiteConnection(conString))
        {
            con.Open();
            string sql = "CREATE TABLE IF NOT EXISTS users (" + userElements + ")";
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            cmd.ExecuteNonQuery();

            sql = "CREATE TABLE IF NOT EXISTS posts (" + postElements + ")";
            cmd = new SQLiteCommand(sql, con);
            cmd.ExecuteNonQuery();

            con.Close();
        }
    }

    
}