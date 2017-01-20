using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using WarframeTracker.Annotations;
using WarframeTracker.Enums;

namespace WarframeTracker.Model
{
    public class RelicModel : INotifyPropertyChanged
    {
        public string Name => RelicType + " " + RelicSuffix;

        private RelicType _relicType = RelicType.Lith;

        public RelicType RelicType
        {
            get { return _relicType; }
            set
            {
                if (Equals(_relicType, value))
                {
                    return;
                }
                _relicType = value;
                OnPropertyChanged();
            }
        }

        private RelicSuffix _relicSuffix = RelicSuffix.A1;

        public RelicSuffix RelicSuffix
        {
            get { return _relicSuffix; }
            set
            {
                if (Equals(_relicSuffix, value))
                {
                    return;
                }
                _relicSuffix = value;
                OnPropertyChanged();
            }
        }
        
        public ObservableCollection<ComponentModel> Components { get; set; } = new ObservableCollection<ComponentModel>();

        public bool IsNeeded => Components.All(x => x.Owned);
        
        public override string ToString()
        {
            return Name + (IsNeeded ? "Is Needed" : "Not Needed") + 
                "(" + string.Join(Environment.NewLine, Components.Select(x => x.ItemName + " " + x.ComponentName)) + ")";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Debug.WriteLine("Prop changed in RelicModel: " + propertyName);
        }

        public bool Search(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString) ||
                    RelicSuffix.ToString().ToLower().Contains(searchString.ToLower()) ||
                    RelicType.ToString().ToLower().Contains(searchString.ToLower()))
            {
                return true;
            }
            else if (Components.Any(relicModelComponent => relicModelComponent.ItemName.ToLower().Contains(searchString.ToLower()) ||
                                                           relicModelComponent.ComponentName.ToLower().Contains(searchString.ToLower())))
            {
                return true;
            }

            return false;
        }
    }
}
