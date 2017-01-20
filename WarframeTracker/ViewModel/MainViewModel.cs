using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using WarframeTracker.DataService;
using WarframeTracker.Dialogs;
using WarframeTracker.Enums;
using WarframeTracker.Model;

namespace WarframeTracker.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IRelicService _relicDataService;
        private readonly List<RelicModel> _lithRelics;
        private readonly List<RelicModel> _mesoRelics;
        private readonly List<RelicModel> _neoRelics;
        private readonly List<RelicModel> _axiRelics;
        private readonly List<ItemModel> _items;

        private string _searchString = "";

        public string SearchString
        {
            get { return _searchString; }
            set
            {
                if (_searchString.Equals(value))
                {
                    return;
                }
                _searchString = value;
                FilterRelics();
                RaisePropertyChanged();
            }
        }

        private bool _lithFilter = true;

        public bool LithFilter
        {
            get { return _lithFilter; }
            set
            {
                if (_lithFilter.Equals(value))
                {
                    return;
                }
                _lithFilter = value;
                FilterRelics();
                RaisePropertyChanged();
            }
        }

        private bool _mesoFilter = true;

        public bool MesoFilter
        {
            get { return _mesoFilter; }
            set
            {
                if (_mesoFilter.Equals(value))
                {
                    return;
                }
                _mesoFilter = value;
                FilterRelics();
                RaisePropertyChanged();
            }
        }

        private bool _neoFilter = true;

        public bool NeoFilter
        {
            get { return _neoFilter; }
            set
            {
                if (_neoFilter.Equals(value))
                {
                    return;
                }
                _neoFilter = value;
                FilterRelics();
                RaisePropertyChanged();
            }
        }

        private bool _axiFilter = true;

        public bool AxiFilter
        {
            get { return _axiFilter; }
            set
            {
                if (_axiFilter.Equals(value))
                {
                    return;
                }
                _axiFilter = value;
                FilterRelics();
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<ItemModel> _filteredItemModels = new ObservableCollection<ItemModel>();

        public ObservableCollection<ItemModel> FilteredItemModels
        {
            get { return _filteredItemModels; }
            set
            {
                if (Equals(_filteredItemModels, value))
                {
                    return;
                }
                _filteredItemModels = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<RelicModel> _filteredRelics = new ObservableCollection<RelicModel>();

        public ObservableCollection<RelicModel> FilteredRelics
        {
            get { return _filteredRelics; }
            set
            {
                if (_filteredRelics.Equals(value))
                {
                    return;
                }
                _filteredRelics = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel(IRelicService relicDataService)
        {
            _relicDataService = relicDataService;
            _lithRelics = _relicDataService.GetRelics(RelicType.Lith, "default");
            _mesoRelics = _relicDataService.GetRelics(RelicType.Meso, "default");
            _neoRelics = _relicDataService.GetRelics(RelicType.Neo, "default");
            _axiRelics = _relicDataService.GetRelics(RelicType.Axi, "default");

            FilterRelics();

            _items = ExtrapolateItems();

            FilteredItemModels = new ObservableCollection<ItemModel>(_items);
        }
        
        private RelayCommand _addRelicCommand;
        public ICommand AddRelicCommand =>  _addRelicCommand ?? ( _addRelicCommand = new RelayCommand(AddRelic));
        protected void AddRelic()
        {
            var relicDialog = new AddRelicDialog();

            if (relicDialog.ShowDialog() != true)
            {
                return;
            }

            switch (relicDialog.Relic.RelicType)
            {
                case RelicType.Lith:
                    _lithRelics.Add(relicDialog.Relic);
                    break;
                case RelicType.Meso:
                    _mesoRelics.Add(relicDialog.Relic);
                    break;
                case RelicType.Neo:
                    _neoRelics.Add(relicDialog.Relic);
                    break;
                case RelicType.Axi:
                    _axiRelics.Add(relicDialog.Relic);
                    break;
            }

            FilterRelics();
            Save(null);
        }

        public void Save(ComponentModel item)
        {
            if (item != null)
            {
                foreach (var relic in new List<RelicModel>().Concat(_lithRelics)
                    .Concat(_mesoRelics)
                    .Concat(_neoRelics)
                    .Concat(_axiRelics).ToList())
                {
                    foreach (var relicComponent in relic.Components)
                    {
                        if (relicComponent.Name == item.Name &&
                            relicComponent.Item == item.Item)
                        {
                            relicComponent.Owned = item.Owned;
                        }
                    }
                }

                FilterRelics();
            }

            _relicDataService.Save(_lithRelics, _mesoRelics, _neoRelics, _axiRelics, "default");
        }

        private void FilterRelics()
        {
            var filteredList = new List<RelicModel>();

            if (_lithFilter)
            {
                filteredList.AddRange(SearchRelics(_lithRelics, SearchString));
            }

            if (_mesoFilter)
            {
                filteredList.AddRange(SearchRelics(_mesoRelics, SearchString));
            }

            if (_neoFilter)
            {
                filteredList.AddRange(SearchRelics(_neoRelics, SearchString));
            }

            if (_axiFilter)
            {
                filteredList.AddRange(SearchRelics(_axiRelics, SearchString));
            }

            FilteredRelics = new ObservableCollection<RelicModel>(filteredList);
        }

        private List<RelicModel> SearchRelics(List<RelicModel> relics, string searchString)
        {
            var results = new List<RelicModel>();

            foreach (var relicModel in relics)
            {
                if (string.IsNullOrWhiteSpace(searchString) ||
                    relicModel.RelicSuffix.ToString().ToLower().Contains(searchString.ToLower()) ||
                    relicModel.RelicType.ToString().ToLower().Contains(searchString.ToLower()))
                {
                    results.Add(relicModel);
                }
                else
                {
                    if (relicModel.Components.Any(relicModelComponent => relicModelComponent.Item.ToLower().Contains(searchString.ToLower()) ||
                                                                         relicModelComponent.Name.ToLower().Contains(searchString.ToLower())))
                    {
                        results.Add(relicModel);
                    }
                }
            }

            return results;
        }

        private List<ItemModel> ExtrapolateItems()
        {
            var allRelics = _lithRelics.Concat(_mesoRelics)
                                       .Concat(_neoRelics)
                                       .Concat(_axiRelics);
            var items = new List<ItemModel>();


            foreach (var relicModel in allRelics)
            {
                foreach (var component in relicModel.Components)
                {
                    //component.Relic = relicModel.RelicType + "-" + relicModel.RelicSuffix;

                    if (component.Item == "Forma")
                    {
                        continue;
                    }

                    if (items.All(x => x.Name != component.Item))
                    {
                        items.Add(new ItemModel
                        {
                            Name = component.Item,
                            Components = {component}
                        });
                    }
                    else
                    {
                        foreach (var item in items.Where(x => x.Name == component.Item))
                        {
                            item.Components.Add(component);
                        }
                        
                    }
                }
            }
            
            return items;
        }
    }
}