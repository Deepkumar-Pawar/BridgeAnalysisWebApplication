using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BridgeAnalysisWebApplication.Classes;
using BridgeAnalysisWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using MySql.Data.MySqlClient;

namespace BridgeAnalysisWebApplication.Pages
{
    public class AnalyzeBridgeModel : PageModel
    {
        public List<Material> materials;
        public List<Load> loads;
        public int numOfPillars;

        public double defaultFactorOfSafety = 1.5;

        public string message;

        private IMemoryCache _cache;

        public void OnGet()
        {
            GetMaterialsAndLoads();
            _cache.Set<int>("NumOfPillars", 0);
        }

        public void OnPost()
        {
            GetMaterialsAndLoads();

            numOfPillars = Int32.Parse(_cache.Get("NumOfPillars").ToString());

            if (numOfPillars != 0 && (string.IsNullOrEmpty(Request.Form["numOfPillars"]) || Int32.Parse(Request.Form["numOfPillars"]) == numOfPillars))
            {
                string beamLength = Request.Form["beamLength"];
                string beamWidth = Request.Form["beamWidth"];
                string beamHeight = Request.Form["beamHeight"];
                string beamWeight = Request.Form["beamWeight"];
                string beamMaterialName = Request.Form["beamMaterialName"];
                string loadTypeName = Request.Form["loadTypeName"];

                if (ValidateValues(beamLength, beamWidth, beamHeight, beamWeight))
                {
                    Material beamMaterial = new Material();
                    Load beamLoad = new Load();

                    foreach (Material material in materials)
                    {
                        if (material.Name == beamMaterialName)
                        {
                            beamMaterial = material;
                            break;
                        }
                    }

                    foreach (Load load in loads)
                    {
                        if (load.Name == loadTypeName)
                        {
                            beamLoad = load;
                            break;
                        }
                    }

                    bool pillarsAreValid = (numOfPillars >= 2);
                    Pillar[] pillars = new Pillar[pillarsAreValid?numOfPillars:0];
                    string pillarDistance = "";


                    for (int i = 0; i < numOfPillars; i++)
                    {
                        pillarDistance = Request.Form["pillarDistance" + i.ToString()];

                        if (!ValidatePillarDistanceValue(pillarDistance, beamLength))
                        {
                            pillarsAreValid = false;
                            break;
                        }

                        pillars[i] = new Pillar(Double.Parse(pillarDistance), beamMaterial);
                    }

                    if (pillarsAreValid)
                    {
                        Beam bridgeBeam = new Beam(Double.Parse(beamLength), Double.Parse(beamHeight), Double.Parse(beamWidth), beamMaterial);

                        double bridgeObjectLoadPerLength = beamLoad.Magnitude / beamLoad.Length;
                        double bridgeTotalLoadPerLength = bridgeObjectLoadPerLength + (Double.Parse(beamWeight) / Double.Parse(beamLength));

                        BeamBridge beamBridge = new BeamBridge(bridgeBeam, pillars, bridgeObjectLoadPerLength, bridgeTotalLoadPerLength);

                        beamBridge.Pillars = beamBridge.ArrangePillarsInOrder();

                        double factorOfSafety = defaultFactorOfSafety;

                        if (Request.Form["factorOfSafety"] != "")
                        {
                            factorOfSafety = Double.Parse(Request.Form["factorOfSafety"]);
                        }

                        BridgeAnalyzer analyzer = new BridgeAnalyzer();

                        if (analyzer.AnalyzeBridge(beamBridge, factorOfSafety))
                        {
                            message = "The Bridge has passed the necessary tests.";
                        }
                        else
                        {
                            message = "The Bridge has failed the necessary tests.";
                        }
                    }
                    else
                    {
                        message = "Please ensure that the pillar distance values are non negative and that there are 2 or more pillars.";
                    }
                }
                else
                {
                    message = "Please enter valid values.";
                }
            }
            else
            {
                if ((Request.Form["numOfPillars"] != "")) { numOfPillars = Int32.Parse(Request.Form["numOfPillars"]); }
            }

            _cache.Set<int>("NumOfPillars", numOfPillars);
        }

        public bool ValidatePillarDistanceValue(string pillarDistance, string beamLength)
        {
            return (Double.TryParse(pillarDistance, out double distance) && distance >= 0 && distance <= Double.Parse(beamLength));
        }

        public bool ValidateValues(string beamLength, string beamWidth, string beamHeight, string beamWeight)
        {
            bool areValid = true;

            if (!Double.TryParse(beamLength, out double length) || length < 0)
            {
                areValid = false;
            }

            if (!Double.TryParse(beamWidth, out double width) || width < 0)
            {
                areValid = false;
            }

            if (!Double.TryParse(beamHeight, out double height) || height < 0)
            {
                areValid = false;
            }

            if (!Double.TryParse(beamWeight, out double weight) || weight < 0)
            {
                areValid = false;
            }

            return areValid;
        }

        public AnalyzeBridgeModel(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        public void GetMaterialsAndLoads()
        {
            materials = new List<Material>();
            loads = new List<Load>();

            string cs = "Server=localhost;Database=bridgeanalysisdb;Uid=root;Pwd=;";

            using var con = new MySqlConnection(cs);
            con.Open();

            var sqlMats = "SELECT * FROM materials";

            using var cmdMats = new MySqlCommand(sqlMats, con);

            using MySqlDataReader rdrMats = cmdMats.ExecuteReader();

            while (rdrMats.Read())
            {
                materials.Add(new Material(rdrMats.GetString(0), rdrMats.GetDouble(1), rdrMats.GetDouble(2), rdrMats.GetDouble(3)));
            }

            rdrMats.Close();

            var sqlLoads = "SELECT * FROM loads";

            using var cmdLoads = new MySqlCommand(sqlLoads, con);

            using MySqlDataReader rdrLoads = cmdLoads.ExecuteReader();

            while (rdrLoads.Read())
            {
                loads.Add(new Load(rdrLoads.GetString(0), rdrLoads.GetDouble(1), rdrLoads.GetDouble(2)));
            }

            rdrLoads.Close();
        }
    }
}
