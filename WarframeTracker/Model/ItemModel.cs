using System;
using System.Collections.Generic;
using System.Linq;

namespace WarframeTracker.Model
{
    public class ItemModel
    {
        public string Name { get; set; }
        public List<ComponentModel> Components { get; set; } = new List<ComponentModel>();
        public bool Owned => Components.All(component => component.Owned);
        
        public override string ToString()
        {
            return Name + (Owned ? "Owned" : "Not Owned") +
                   "(" + string.Join(Environment.NewLine, Components.Select(x => x.ComponentName)) + ")";
        }
    }
}
