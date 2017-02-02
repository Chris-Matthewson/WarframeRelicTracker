using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using WarframeTracker.Annotations;

namespace WarframeTracker.Model
{
    public class SellItemModel : INotifyPropertyChanged
    {
        private string _itemName;

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

        private ObservableCollection<SellComponentModel> _components = new ObservableCollection<SellComponentModel>();

        public ObservableCollection<SellComponentModel> Components
        {
            get { return _components; }
            set
            {
                if (Equals(_components, value))
                {
                    return;
                }
                _components = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Search(string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                return true;
            }
            if (Components.Any(relicModelComponent => relicModelComponent.ItemPart.ToLower().Contains(searchString.ToLower())) ||
                ItemName.ToLower().Contains(searchString.ToLower()))
            {
                return true;
            }

            return false;
        }
    }
}
