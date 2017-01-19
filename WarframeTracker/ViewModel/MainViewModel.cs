using System.Collections.ObjectModel;
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
        private IRelicService _relicDataService;
        public MainViewModel(IRelicService relicDataService)
        {
            _relicDataService = relicDataService;
            LithRelics = new ObservableCollection<RelicModel>(_relicDataService.GetRelics(RelicType.Lith, "default"));
            MesoRelics = new ObservableCollection<RelicModel>(_relicDataService.GetRelics(RelicType.Meso, "default"));
            NeoRelics = new ObservableCollection<RelicModel>(_relicDataService.GetRelics(RelicType.Neo, "default"));
            AxiRelics = new ObservableCollection<RelicModel>(_relicDataService.GetRelics(RelicType.Axi, "default"));
        }

        private ObservableCollection<RelicModel> _lithRelics = new ObservableCollection<RelicModel>();

        public ObservableCollection<RelicModel> LithRelics
        {
            get { return _lithRelics; }
            set
            {
                if (Equals(_lithRelics, value))
                {
                    return;
                }
                _lithRelics = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<RelicModel> _mesoRelics = new ObservableCollection<RelicModel>();

        public ObservableCollection<RelicModel> MesoRelics
        {
            get { return _mesoRelics; }
            set
            {
                if (Equals(_mesoRelics, value))
                {
                    return;
                }
                _mesoRelics = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<RelicModel> _neoRelics = new ObservableCollection<RelicModel>();

        public ObservableCollection<RelicModel> NeoRelics
        {
            get { return _neoRelics; }
            set
            {
                if (Equals(_neoRelics, value))
                {
                    return;
                }
                _neoRelics = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<RelicModel> _axiRelics = new ObservableCollection<RelicModel>();

        public ObservableCollection<RelicModel> AxiRelics
        {
            get { return _axiRelics; }
            set
            {
                if (Equals(_axiRelics, value))
                {
                    return;
                }
                _axiRelics = value;
                RaisePropertyChanged();
            }
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
                    LithRelics.Add(relicDialog.Relic);
                    break;
                case RelicType.Meso:
                    MesoRelics.Add(relicDialog.Relic);
                    break;
                case RelicType.Neo:
                    NeoRelics.Add(relicDialog.Relic);
                    break;
                case RelicType.Axi:
                    AxiRelics.Add(relicDialog.Relic);
                    break;
            }

            Save();
        }

        public void Save()
        {
            _relicDataService.Save(LithRelics.ToList(), MesoRelics.ToList(), NeoRelics.ToList(), AxiRelics.ToList(), "default");
        }
    }
}