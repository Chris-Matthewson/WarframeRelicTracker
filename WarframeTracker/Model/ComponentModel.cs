using System.Collections.Generic;
using System.Linq;

namespace WarframeTracker.Model
{
    public class ComponentModel
    {
        public string ItemName { get; set; } = "Paris Prime";
        public string ComponentName { get; set; } = "String";
        public List<RelicModel> Relics { get; set; } = new List<RelicModel> {new RelicModel()};
        public bool Owned { get; set; } = false;

        public override string ToString()
        {
            return ItemName + " " + ComponentName + 
                   "(" + string.Join(", ", Relics.Select(x => x.Name).ToList()) + ")" +
                   (Owned ? "Owned" : "NotOwned)");
        }
    }
}
