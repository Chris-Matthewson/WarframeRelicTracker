using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using WarframeTracker.Model;

namespace WarframeTracker.ViewModel
{
    public class ComponentViewModel : ViewModelBase
    {
        private readonly ComponentModel _component;
        public ComponentViewModel(ComponentModel component)
        {
            _component = component;
            _relics = new ObservableCollection<RelicModel>(_component.Relics);
        }

        public string ItemName
        {
            get { return _component.ItemName; }
            set
            {
                if (Equals(_component.ItemName, value))
                {
                    return;
                }
                _component.ItemName = value;
                RaisePropertyChanged();
            }
        }
        

        public string ComponentName
        {
            get { return _component.ComponentName; }
            set
            {
                if (Equals(_component.ComponentName, value))
                {
                    return;
                }
                _component.ComponentName = value;
                RaisePropertyChanged();
            }
        }
        
        public bool Owned
        {
            get { return _component.Owned; }
            set
            {
                if (Equals(_component.Owned, value))
                {
                    return;
                }
                _component.Owned = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<RelicModel> _relics;

        public ObservableCollection<RelicModel> Relics
        {
            get { return _relics; }
            set
            {
                if (Equals(_relics, value))
                {
                    return;
                }
                _relics = value;
                RaisePropertyChanged();
            }
        }
    }
}