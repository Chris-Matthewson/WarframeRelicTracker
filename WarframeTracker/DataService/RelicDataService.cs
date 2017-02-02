using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using WarframeTracker.Enums;
using WarframeTracker.Model;

namespace WarframeTracker.DataService
{
    public class RelicDataService : IRelicService
    {
        private readonly string _relicDirectory = @"C:\ProgramData\WarframeRelicTracker\";

        public List<RelicModel> GetRelics(RelicType type, string profileName)
        {
            var filePath = _relicDirectory + profileName + @"\" + type + ".json";
            Debug.WriteLine("Checking for file: " + filePath);

            var relics = File.Exists(filePath) ? 
                         JsonConvert.DeserializeObject<List<RelicModel>>(File.ReadAllText(filePath)) : 
                         new List<RelicModel>();

            return relics;
        }

        public List<SellItemModel> GetSellItems(string profileName)
        {
            //File.WriteAllText(@"C:\ProgramData\WarframeRelicTracker\default\sellItems.json", JsonConvert.SerializeObject(sellItems, Formatting.Indented));
            var filePath = _relicDirectory + profileName + @"\sellItems.json";
            Debug.WriteLine("Checking for file: " + filePath);

            var sellItems = File.Exists(filePath) ?
                            JsonConvert.DeserializeObject<List<SellItemModel>>(File.ReadAllText(filePath)) :
                            new List<SellItemModel>();

            return sellItems;
        }

        public void Save(List<RelicModel> lith, List<RelicModel> meso, List<RelicModel> neo, List<RelicModel> axi, List<SellItemModel> sellItems, string profileName)
        {
            string filePath;

            filePath = _relicDirectory + profileName + @"\Lith.json";
            Debug.WriteLine("Saving: " + filePath);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(lith, Formatting.Indented, new JsonSerializerSettings {PreserveReferencesHandling = PreserveReferencesHandling.All}));

            filePath = _relicDirectory + profileName + @"\Meso.json";
            Debug.WriteLine("Saving: " + filePath);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(meso, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.All }));

            filePath = _relicDirectory + profileName + @"\Neo.json";
            Debug.WriteLine("Saving: " + filePath);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(neo, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.All }));

            filePath = _relicDirectory + profileName + @"\Axi.json";
            Debug.WriteLine("Saving: " + filePath);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(axi, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.All }));

            filePath = _relicDirectory + profileName + @"\sellItems.json";
            Debug.WriteLine("Saving: " + filePath);
            File.WriteAllText(filePath, JsonConvert.SerializeObject(sellItems, Formatting.Indented, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.All }));
        }
    }
}
