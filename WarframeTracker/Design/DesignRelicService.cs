using System.Collections.Generic;
using System.Collections.ObjectModel;
using WarframeTracker.DataService;
using WarframeTracker.Enums;
using WarframeTracker.Model;

namespace WarframeTracker.Design
{
    public class DesignRelicService : IRelicService
    {
        public List<RelicModel> GetRelics(RelicType type, string profileName)
        {
            return new List<RelicModel>
            {
                new RelicModel
                {
                    RelicType = RelicType.Lith,
                    RelicSuffix = RelicSuffix.A1,
                    Components = new ObservableCollection<ComponentModel>
                    {
                        new ComponentModel { ItemName = "Paris Prime", ComponentName = "String", Owned = false},
                        new ComponentModel { ItemName = "Forma", ComponentName = "Blueprint", Owned = true},
                    }
                },
                new RelicModel
                {
                    RelicType = RelicType.Lith,
                    RelicSuffix = RelicSuffix.A1,
                    Components = new ObservableCollection<ComponentModel>
                    {
                        new ComponentModel { ItemName = "Paris Prime", ComponentName = "String", Owned = false},
                        new ComponentModel { ItemName = "Forma", ComponentName = "Blueprint", Owned = true},
                    }
                },
                new RelicModel
                {
                    RelicType = RelicType.Lith,
                    RelicSuffix = RelicSuffix.A1,
                    Components = new ObservableCollection<ComponentModel>
                    {
                        new ComponentModel { ItemName = "Paris Prime", ComponentName = "String", Owned = false},
                        new ComponentModel { ItemName = "Forma", ComponentName = "Blueprint", Owned = true},
                    }
                }
            };
        }

        public List<SellItemModel> GetSellItems(string profileName)
        {
            return new List<SellItemModel>
            {
                new SellItemModel
                {
                    Components = new ObservableCollection<SellComponentModel>
                    {
                        new SellComponentModel
                        {
                            IsOwned = true,
                            ItemCount = "99",
                            ItemPart = "String",
                            SellPrice = "999"
                        }
                    },
                    ItemName = "Paris Prime"
                }
            };
        }

        public void Save(List<RelicModel> lith, List<RelicModel> meso, List<RelicModel> neo, List<RelicModel> axi, List<SellItemModel> sellItems, string profileName)
        {
        }
    }
}
