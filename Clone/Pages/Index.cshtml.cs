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
            con.Close();
        }
    }
}
