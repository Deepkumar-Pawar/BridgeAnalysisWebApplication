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
    public class ViewMaterialsModel : PageModel
    {
        public List<Material> materials;
        public List<string> materialColumnNames;

        public void OnGet()
        {

            materials = new List<Material>();
            materialColumnNames = new List<string>();

            string cs = "Server=localhost;Database=bridgeanalysisdb;Uid=root;Pwd=;";

            using var con = new MySqlConnection(cs);
            con.Open();

            var sqlMats = "SELECT * FROM materials";

            using var cmdMats = new MySqlCommand(sqlMats, con);

            using MySqlDataReader rdrMats = cmdMats.ExecuteReader();

            materialColumnNames.Add(rdrMats.GetName(0));
            materialColumnNames.Add(rdrMats.GetName(1));
            materialColumnNames.Add(rdrMats.GetName(2));
            materialColumnNames.Add(rdrMats.GetName(3));

            while (rdrMats.Read())
            {
                materials.Add(new Material(rdrMats.GetString(0), rdrMats.GetDouble(1), rdrMats.GetDouble(2), rdrMats.GetDouble(3)));
            }

            rdrMats.Close();

        }
    }
}
