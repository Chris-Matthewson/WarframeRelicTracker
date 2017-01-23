using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using WarframeTracker.Annotations;

namespace WarframeTracker.Model
{
    public class ComponentModel : INotifyPropertyChanged
    {
        private string _itemName = "";

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

        private string _componentName = "";

        public string ComponentName
        {
            get { return _componentName; }
            set
            {
                if (Equals(_componentName, value))
                {
                    return;
                }
                _componentName = value;
                OnPropertyChanged();
            }
        }

        private bool _owned;

        public bool Owned
        {
            get { return _owned; }
            set
            {
                if (Equals(_owned, value))
                {
                    return;
                }
                _owned = value;
                OnPropertyChanged();
            }
        }
        

        private RelicModel _relic = new RelicModel();

        public RelicModel Relic
        {
            get { return _relic; }
            set
            {
                if (Equals(_relic, value))
                {
                    return;
                }
                _relic = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return ItemName + " " + ComponentName + 
                   "(" +  Relic.Name + ")" +
                   (Owned ? "Owned" : "NotOwned)");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            //Debug.WriteLine("Prop changed in ComponentModel " + propertyName);
        }
    }
}
