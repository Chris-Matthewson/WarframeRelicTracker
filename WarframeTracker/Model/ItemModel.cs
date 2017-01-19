using System.Collections.Generic;
using System.Linq;

namespace WarframeTracker.Model
{
    public class ItemModel
    {
        public List<ItemComponent> Components { get; set; }

        private bool _owned;

        public bool Owned
        {
            get
            {
                CheckIfOwned();
                return _owned;
            }
        }

        private void CheckIfOwned()
        {
            if (Components.Any(component => !component.Owned))
            {
                _owned = false;
                return;
            }

            _owned = true;
        }
    }
}
