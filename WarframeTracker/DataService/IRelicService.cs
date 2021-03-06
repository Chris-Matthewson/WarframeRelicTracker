﻿using System.Collections.Generic;
using WarframeTracker.Enums;
using WarframeTracker.Model;

namespace WarframeTracker.DataService
{
    public interface IRelicService
    {
        List<RelicModel> GetRelics(RelicType type, string profileName);
        List<SellItemModel> GetSellItems(string profileName);

        void Save(List<RelicModel> lith, List<RelicModel> meso, 
                   List<RelicModel> neo, List<RelicModel> axi,
                   List<SellItemModel> sellItems,
                   string profileName);
    }
}
