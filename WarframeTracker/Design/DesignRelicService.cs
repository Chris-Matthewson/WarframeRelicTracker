using System.Collections.Generic;
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
                    Components = new List<ComponentModel>
                    {
                        new ComponentModel { Item = "Paris Prime", Name = "String", Owned = false},
                        new ComponentModel { Item = "Forma", Name = "Blueprint", Owned = true},
                    }
                },
                new RelicModel
                {
                    RelicType = RelicType.Lith,
                    RelicSuffix = RelicSuffix.A1,
                    Components = new List<ComponentModel>
                    {
                        new ComponentModel { Item = "Paris Prime", Name = "String", Owned = false},
                        new ComponentModel { Item = "Forma", Name = "Blueprint", Owned = true},
                    }
                },
                new RelicModel
                {
                    RelicType = RelicType.Lith,
                    RelicSuffix = RelicSuffix.A1,
                    Components = new List<ComponentModel>
                    {
                        new ComponentModel { Item = "Paris Prime", Name = "String", Owned = false},
                        new ComponentModel { Item = "Forma", Name = "Blueprint", Owned = true},
                    }
                }
            };
        }

        public void Save(List<RelicModel> lith, List<RelicModel> meso, List<RelicModel> neo, List<RelicModel> axi, string profileName)
        {
            
        }
    }
}
