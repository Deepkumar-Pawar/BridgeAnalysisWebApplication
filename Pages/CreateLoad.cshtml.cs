using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace BridgeAnalysisWebApplication.Pages
{
    public class CreateLoadModel : PageModel
    {
        public string message;

        public void OnGet()
        {

        }

        public void OnPost()
        {
            string loadName = Request.Form["loadName"];
            string loadMagnitude = Request.Form["loadMagnitude"];
            string loadLength = Request.Form["loadLength"];

            if (ValidateValues(loadName, loadMagnitude, loadLength))
            {
                string cs = "Server=localhost;Database=bridgeanalysisdb;Uid=root;Pwd=;";

                using var con = new MySqlConnection(cs);
                con.Open();

                using var cmd = new MySqlCommand();
                cmd.Connection = con;

                cmd.CommandText = $"INSERT INTO loads(Name, Magnitude, Length) VALUES('{loadName}', {loadMagnitude}, {loadLength})";
                cmd.ExecuteNonQuery();
            }
            else
            {
                message = "Please enter valid values.";
            }
        }

        public bool ValidateValues(string loadName, string loadMagnitude, string loadLength)
        {
            bool areValid = true;

            string cs = "Server=localhost;Database=bridgeanalysisdb;Uid=root;Pwd=;";

            using var con = new MySqlConnection(cs);
            con.Open();

            var sqlLoads = "SELECT Name FROM loads";

            using var cmdLoads = new MySqlCommand(sqlLoads, con);

            using MySqlDataReader rdrLoads = cmdLoads.ExecuteReader();

            while (rdrLoads.Read())
            {
                if (loadName == rdrLoads.GetString(0))
                {
                    areValid = false;
                }
            }

            rdrLoads.Close();

            if (!Double.TryParse(loadMagnitude, out double magnitude) || magnitude < 0)
            {
                areValid = false;
            }

            if (!Double.TryParse(loadLength, out double length) || length < 0)
            {
                areValid = false;
            }

            return areValid;
        }
    }
}
