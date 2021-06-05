using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace BridgeAnalysisWebApplication.Pages
{
    public class CreateMaterialModel : PageModel
    {
        public string message;

        public void OnGet()
        {

        }

        public void OnPost()
        {
            string materialName = Request.Form["materialName"];
            string materialModulusOfRupture = Request.Form["materialModulusOfRupture"];
            string materialShearStrength = Request.Form["materialShearStrength"];
            string materialYoungsModulus = Request.Form["materialYoungsModulus"];

            if (ValidateValues(materialName, materialModulusOfRupture, materialShearStrength, materialYoungsModulus))
            {
                string cs = "Server=localhost;Database=bridgeanalysisdb;Uid=root;Pwd=;";

                using var con = new MySqlConnection(cs);
                con.Open();

                using var cmd = new MySqlCommand();
                cmd.Connection = con;

                cmd.CommandText = $"INSERT INTO materials(Name, ModulusOfRupture, ShearStrength, YoungsModulus) VALUES('{materialName}', {materialModulusOfRupture}, {materialShearStrength}, {materialYoungsModulus})";
                cmd.ExecuteNonQuery();
            }
            else
            {
                message = "Please enter valid values.";
            }
        }

        public bool ValidateValues(string materialName, string materialModulusOfRupture, string materialShearStrength, string materialYoungsModulus)        //Validate values
        {
            bool areValid = true;

            string cs = "Server=localhost;Database=bridgeanalysisdb;Uid=root;Pwd=;";

            using var con = new MySqlConnection(cs);
            con.Open();

            var sqlMats = "SELECT Name FROM materials";

            using var cmdMats = new MySqlCommand(sqlMats, con);

            using MySqlDataReader rdrMats = cmdMats.ExecuteReader();

            while (rdrMats.Read())
            {
                if (materialName == rdrMats.GetString(0))
                {
                    areValid = false;
                }
            }

            rdrMats.Close();

            if(!Double.TryParse(materialModulusOfRupture, out double modulusOfRupture) || modulusOfRupture < 0)
            {
                areValid = false;
            }

            if (!Double.TryParse(materialShearStrength, out double shearStrength) || shearStrength < 0)
            {
                areValid = false;
            }

            if (!Double.TryParse(materialYoungsModulus, out double youngsModulus) || youngsModulus < 0)
            {
                areValid = false;
            }

            return areValid;
        }
    }
}
