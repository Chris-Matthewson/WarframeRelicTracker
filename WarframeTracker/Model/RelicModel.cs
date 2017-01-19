using System.Collections.Generic;
using System.Linq;
using WarframeTracker.Enums;

namespace WarframeTracker.Model
{
    public class RelicModel
    {
        public RelicType RelicType { get; set; }
        public RelicSuffix RelicSuffix { get; set; }
        public List<ItemComponent> Components { get; set; } = new List<ItemComponent>();

        private bool _isNeeded = true;
        public bool IsNeeded {
            get
            {
                CheckIfRelicIsNeeded();
                return _isNeeded;
            }
        }

        private void CheckIfRelicIsNeeded()
        {
            if (Components.Any(itemComponent => !itemComponent.Owned))
            {
                _isNeeded = true;
                return;
            }

            _isNeeded = false;
            return;
        }

        public override string ToString()
        {
            var returnString = RelicType + " " + RelicSuffix + " ";
            foreach (var itemComponent in Components)
            {
                returnString += "(" + itemComponent.Item + " " + itemComponent.Name + " " + (itemComponent.Owned ? "owned" : "not owned") + ")";
            }
            return returnString;
        }
    }
}
