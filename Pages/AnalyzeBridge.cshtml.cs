using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace BridgeAnalysisWebApplication.Pages
{
    public class AnalyzeBridgeModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPost()
        {
            string beamLength = Request.Form["beamLength"];
            string beamWidth = Request.Form["beamWidth"];
            string beamHeight = Request.Form["beamHeight"];
            string beamWeight = Request.Form["beamWeight"];
            string beamMaterial = Request.Form["beamMaterial"];
            string loadType = Request.Form["loadType"];

            string connectionString = "Server=localhost;Database=bridgeanalysisdb;Uid=root;Pwd=;";

            MySqlConnection connection = new MySqlConnection(connectionString);

        }
    }
}
