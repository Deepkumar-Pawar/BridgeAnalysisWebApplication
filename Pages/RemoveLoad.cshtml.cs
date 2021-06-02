using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace BridgeAnalysisWebApplication.Pages
{
    public class RemoveLoadModel : PageModel
    {
        public string message;

        public void OnGet()
        {
        }

        public void OnPost()
        {
            string loadName = Request.Form["loadName"];

            string cs = "Server=localhost;Database=bridgeanalysisdb;Uid=root;Pwd=;";

            using var con = new MySqlConnection(cs);
            con.Open();

            using var cmd = new MySqlCommand();
            cmd.Connection = con;

            cmd.CommandText = $"DELETE FROM loads WHERE Name = '{loadName}'";
            cmd.ExecuteNonQuery();

            message = "Removed specified load.";
        }
    }
}
