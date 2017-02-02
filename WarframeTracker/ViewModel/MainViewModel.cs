using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using WarframeTracker.DataService;
using WarframeTracker.Dialogs;
using WarframeTracker.Enums;
using WarframeTracker.Model;

namespace WarframeTracker.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly List<ItemModel> _items;
        private readonly IRelicService _relicDataService;
        private readonly List<RelicModel> _relics;
        private readonly List<SellItemModel> _sellItems = new List<SellItemModel>();
        private RelayCommand _addRelicCommand;
        private bool _axiFilter = true;

        private bool _isNeededFilter;

        private ICollectionView _itemsCollectionView;
        private bool _lithFilter = true;
        private bool _mesoFilter = true;
        private bool _neoFilter = true;
        private ICollectionView _relicCollectionView;

        private bool _rotationFilter;
        private string _searchString = "";

        private ICollectionView _sellItemsCollectionView;

        public MainViewModel(IRelicService relicDataService)
        {
            PropertyChanged += OnPropertyChanged;

            _relicDataService = relicDataService;

            _relics = _relicDataService.GetRelics(RelicType.Lith, "default")
                .Concat(_relicDataService.GetRelics(RelicType.Meso, "default"))
                .Concat(_relicDataService.GetRelics(RelicType.Neo, "default"))
                .Concat(_relicDataService.GetRelics(RelicType.Axi, "default")).ToList();
            _items = ExtrapolateItems();

            _sellItems = _relicDataService.GetSellItems("default");

            _relicCollectionView = CollectionViewSource.GetDefaultView(_relics);
            _relicCollectionView.Filter = RelicFilter;

            _itemsCollectionView = CollectionViewSource.GetDefaultView(_items);
            _itemsCollectionView.Filter = ItemsFilter;

            _sellItemsCollectionView = CollectionViewSource.GetDefaultView(_sellItems);
            _sellItemsCollectionView.Filter = SellItemsFilter;
        }

        public ICollectionView ItemsCollectionView
        {
            get { return _itemsCollectionView; }
            set
            {
                if (Equals(_itemsCollectionView, value))
                    return;
                _itemsCollectionView = value;
                RaisePropertyChanged();
            }
        }

        public string SearchString
        {
            get { return _searchString; }
            set
            {
                if (_searchString.Equals(value))
                    return;
                _searchString = value;
                RaisePropertyChanged();
            }
        }

        public bool LithFilter
        {
            get { return _lithFilter; }
            set
            {
                if (_lithFilter.Equals(value))
                    return;
                _lithFilter = value;
                RaisePropertyChanged();
            }
        }

        public bool MesoFilter
        {
            get { return _mesoFilter; }
            set
            {
                if (_mesoFilter.Equals(value))
                    return;
                _mesoFilter = value;
                RaisePropertyChanged();
            }
        }

        public bool NeoFilter
        {
            get { return _neoFilter; }
            set
            {
                if (_neoFilter.Equals(value))
                    return;
                _neoFilter = value;
                RaisePropertyChanged();
            }
        }

        public bool AxiFilter
        {
            get { return _axiFilter; }
            set
            {
                if (_axiFilter.Equals(value))
                    return;
                _axiFilter = value;
                RaisePropertyChanged();
            }
        }

        public bool IsNeededFilter
        {
            get { return _isNeededFilter; }
            set
            {
                if (Equals(_isNeededFilter, value))
                    return;
                _isNeededFilter = value;
                RaisePropertyChanged();
            }
        }

        public ICollectionView SellItemsCollectionView
        {
            get { return _sellItemsCollectionView; }
            set
            {
                if (Equals(_sellItemsCollectionView, value))
                    return;
                _sellItemsCollectionView = value;
                RaisePropertyChanged();
            }
        }

        public ICollectionView RelicCollectionView
        {
            get { return _relicCollectionView; }
            set
            {
                if (Equals(_relicCollectionView, value))
                    return;
                _relicCollectionView = value;
                RaisePropertyChanged();
            }
        }


        public ICommand AddRelicCommand => _addRelicCommand ?? (_addRelicCommand = new RelayCommand(AddRelic));

        public bool RotationFilter
        {
            get { return _rotationFilter; }
            set
            {
                if (Equals(_rotationFilter, value))
                    return;
                _rotationFilter = value;
                RaisePropertyChanged();
            }
        }


        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if ((propertyChangedEventArgs.PropertyName == null) ||
                (propertyChangedEventArgs.PropertyName == "LithFilter") ||
                (propertyChangedEventArgs.PropertyName == "MesoFilter") ||
                (propertyChangedEventArgs.PropertyName == "NeoFilter") ||
                (propertyChangedEventArgs.PropertyName == "AxiFilter") ||
                (propertyChangedEventArgs.PropertyName == "IsNeededFilter") ||
                (propertyChangedEventArgs.PropertyName == "RotationFilter"))
                _relicCollectionView.Refresh();

            if ((propertyChangedEventArgs.PropertyName == null) ||
                (propertyChangedEventArgs.PropertyName == "SearchString"))
            {
                _itemsCollectionView.Refresh();
                _relicCollectionView.Refresh();
                _sellItemsCollectionView.Refresh();
            }
        }

        private bool ItemsFilter(object o)
        {
            var item = o as ItemModel;

            if (item == null)
                return false;

            return item.Search(SearchString);
        }

        private bool SellItemsFilter(object o)
        {
            var item = o as SellItemModel;

            if (item == null)
                return false;

            return item.Search(SearchString);
        }

        protected void AddRelic()
        {
            var relicDialog = new AddRelicDialog();

            if (relicDialog.ShowDialog() != true)
                return;

            _relics.Add(relicDialog.Relic);

            RelicCollectionView.Refresh();

            Save();
        }

        public void NewComponentObtained(ComponentModel newComponent)
        {
            foreach (var relic in _relics)
                foreach (var component in relic.Components)
                    if ((component.ItemName == newComponent.ItemName) &&
                        (component.ComponentName == newComponent.ComponentName))
                        component.Owned = newComponent.Owned;

            foreach (var relicModel in _relics)
                relicModel.UpdateRelicNeeded();

            foreach (var sellItemModel in _sellItems)
                foreach (var component in sellItemModel.Components)
                    if ((sellItemModel.ItemName == newComponent.ItemName) &&
                        (component.ItemPart == newComponent.ComponentName))
                        component.IsOwned = newComponent.Owned;

            Save();
        }

        public void Save()
        {
            _relicDataService.Save(_relics.Where(x => x.RelicType == RelicType.Lith).ToList(),
                _relics.Where(x => x.RelicType == RelicType.Meso).ToList(),
                _relics.Where(x => x.RelicType == RelicType.Neo).ToList(),
                _relics.Where(x => x.RelicType == RelicType.Axi).ToList(),
                _sellItems,
                "default");
        }

        private bool RelicFilter(object o)
        {
            var relic = o as RelicModel;

            if (relic == null)
                return false;

            if (IsNeededFilter && relic.IsNeeded)
                return false;

            if (RotationFilter && !relic.IsInRotation)
                return false;

            return (((relic.RelicType == RelicType.Lith) && LithFilter) ||
                    ((relic.RelicType == RelicType.Meso) && MesoFilter) ||
                    ((relic.RelicType == RelicType.Neo) && NeoFilter) ||
                    ((relic.RelicType == RelicType.Axi) && AxiFilter)) &&
                   relic.Search(SearchString);
        }

        private List<ItemModel> ExtrapolateItems()
        {
            var items = new List<ItemModel>();

            foreach (var relicModel in _relics)
                foreach (var component in relicModel.Components)
                {
                    if (component.ItemName == "Forma")
                        continue;

                    if (items.All(x => x.ItemName != component.ItemName))
                        items.Add(new ItemModel
                        {
                            ItemName = component.ItemName,
                            Components = {component}
                        });
                    else
                        foreach (var item in items.Where(x => x.ItemName == component.ItemName))
                            if (item.Components.All(x => x.ComponentName != component.ComponentName))
                                item.Components.Add(component);
                }

            items.Sort((x, y) => string.Compare(x.ItemName, y.ItemName, StringComparison.Ordinal));

            foreach (var item in items)
            {
                var sorted = item.Components.ToList();
                sorted.Sort((x, y) => string.Compare(x.ComponentName, y.ComponentName, StringComparison.Ordinal));
                item.Components = new ObservableCollection<ComponentModel>(sorted);
            }

            if (!File.Exists(@"C:\ProgramData\WarframeRelicTracker\default\sellItems.json"))
            {
                foreach (var itemModel in items)
                {
                    var sellItem = new SellItemModel
                    {
                        ItemName = itemModel.ItemName
                    };

                    foreach (var itemModelComponent in itemModel.Components)
                    {
                        sellItem.Components.Add(new SellComponentModel
                        {
                            IsOwned = itemModelComponent.Owned,
                            ItemPart = itemModelComponent.ComponentName,
                            ItemCount = "0",
                            SellPrice = "0"
                        });
                    }

                    _sellItems.Add(sellItem);
                }
                File.WriteAllText(@"C:\ProgramData\WarframeRelicTracker\default\SellItems.json",
                    JsonConvert.SerializeObject(_sellItems, Formatting.Indented));

                File.WriteAllText(@"C:\ProgramData\WarframeRelicTracker\SellItems.json",
                    JsonConvert.SerializeObject(_sellItems, Formatting.Indented));

                MessageBox.Show("Created sell list.");
            }

            return items;
        }
    }
}