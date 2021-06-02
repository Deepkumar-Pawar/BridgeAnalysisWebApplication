using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BridgeAnalysisWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace BridgeAnalysisWebApplication.Pages
{
    public class ViewLoadsModel : PageModel
    {
        public List<Load> loads;
        public List<string> loadColumnNames;

        public void OnGet()
        {
            loads = new List<Load>();
            loadColumnNames = new List<string>();

            string cs = "Server=localhost;Database=bridgeanalysisdb;Uid=root;Pwd=;";

            using var con = new MySqlConnection(cs);
            con.Open();

            var sqlLoads = "SELECT * FROM loads";

            using var cmdLoads = new MySqlCommand(sqlLoads, con);

            using MySqlDataReader rdrLoads = cmdLoads.ExecuteReader();

            loadColumnNames.Add(rdrLoads.GetName(0));
            loadColumnNames.Add(rdrLoads.GetName(1));
            loadColumnNames.Add(rdrLoads.GetName(2));


            while (rdrLoads.Read())
            {
                loads.Add(new Load(rdrLoads.GetString(0), rdrLoads.GetDouble(1), rdrLoads.GetDouble(2)));
            }

            rdrLoads.Close();

        }
    }
}
