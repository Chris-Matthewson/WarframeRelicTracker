using System;
using System.Collections.Generic;
using System.Linq;
using WarframeTracker.Enums;

namespace WarframeTracker.Model
{
    public class RelicModel
    {
        public string Name
        {
            get { return RelicType + " " + RelicSuffix; }
            set { throw new NotImplementedException(); }
        }

        public RelicType RelicType { get; set; } = RelicType.Lith;
        public RelicSuffix RelicSuffix { get; set; } = RelicSuffix.A1;
        public List<ComponentModel> Components { get; set; } = new List<ComponentModel>();
        public bool IsNeeded => Components.All(x => x.Owned);
        

        

        public override string ToString()
        {
            return Name + (IsNeeded ? "Is Needed" : "Not Needed") + 
                "(" + string.Join(Environment.NewLine, Components.Select(x => x.ItemName + " " + x.ComponentName)) + ")";
        }
    }
}
