using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using WarframeTracker.Enums;
using WarframeTracker.Model;

namespace WarframeTracker.ViewModel
{
    public class RelicViewModel : ViewModelBase
    {
        private readonly RelicModel _relic;
        public RelicViewModel(RelicModel relic)
        {
            _relic = relic;
            _components = new ObservableCollection<ComponentModel>(_relic.Components);
        }
        

        public RelicType RelicType
        {
            get { return _relic.RelicType; }
            set
            {
                if (Equals(_relic.RelicType, value))
                {
                    return;
                }
                _relic.RelicType = value;
                RaisePropertyChanged();
            }
        }
        

        public RelicSuffix RelicSuffix
        {
            get { return _relic.RelicSuffix; }
            set
            {
                if (Equals(_relic.RelicSuffix, value))
                {
                    return;
                }
                _relic.RelicSuffix = value;
                RaisePropertyChanged();
            }
        }
        

        public string RelicName => _relic.Name;


        public bool IsNeeded => _relic.IsNeeded;

        private ObservableCollection<ComponentModel> _components;

        public ObservableCollection<ComponentModel> Components
        {
            get { return _components; }
            set
            {
                if (Equals(_components, value))
                {
                    return;
                }
                _components = value;
                RaisePropertyChanged();
            }
        }
    }
}