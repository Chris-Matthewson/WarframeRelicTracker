using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using WarframeTracker.Annotations;

namespace WarframeTracker.Model
{
    public class ItemModel : INotifyPropertyChanged
    {
        private string _itemName = "Paris Prime";

        public string ItemName
        {
            get { return _itemName; }
            set
            {
                if (Equals(_itemName, value))
                {
                    return;
                }
                _itemName = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ComponentModel> Components { get; set; } = new ObservableCollection<ComponentModel>();

        public bool Owned => Components.All(component => component.Owned);
        
        public override string ToString()
        {
            return ItemName + (Owned ? "Owned" : "Not Owned") +
                   "(" + string.Join(Environment.NewLine, Components.Select(x => x.ComponentName)) + ")";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            //Debug.WriteLine("Prop changed in ItemModel: " + propertyName);
        }

        public bool Search(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
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
