using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using WarframeTracker.Annotations;
using WarframeTracker.ViewModel;

namespace WarframeTracker.Model
{
    public class SellComponentModel : INotifyPropertyChanged
    {
        private string _itemPart = "";

        public string ItemPart
        {
            get { return _itemPart; }
            set
            {
                if (Equals(_itemPart, value))
                {
                    return;
                }
                _itemPart = value;
                OnPropertyChanged();
            }
        }

        private string _itemCount;

        public string ItemCount
        {
            get { return _itemCount; }
            set
            {
                if (Equals(_itemCount, value))
                {
                    return;
                }
                _itemCount = value;
                OnPropertyChanged();
            }
        }

        private string _sellPrice;

        public string SellPrice
        {
            get { return _sellPrice; }
            set
            {
                if (Equals(_sellPrice, value))
                {
                    return;
                }
                _sellPrice = value;
                OnPropertyChanged();
            }
        }

        private bool _isOwned;

        public bool IsOwned
        {
            get { return _isOwned; }
            set
            {
                if (Equals(_isOwned, value))
                {
                    return;
                }
                _isOwned = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _incrementCountCommand;
        public ICommand IncrementCountCommand =>  _incrementCountCommand ?? ( _incrementCountCommand = new RelayCommand(IncrementCount));
        protected void IncrementCount()
        {
            int newCount;

            var valid = int.TryParse(ItemCount, out newCount);

            if (valid && newCount < 99)
            {
                newCount++;
                ItemCount = newCount.ToString();
                SimpleIoc.Default.GetInstance<MainViewModel>().Save();
            }
        }

        private RelayCommand _decrementCountCommand;
        public ICommand DecrementCountCommand =>  _decrementCountCommand ?? ( _decrementCountCommand = new RelayCommand(DecrementCount));
        protected void DecrementCount()
        {
            int newCount;

            var valid = int.TryParse(ItemCount, out newCount);

            if (valid && newCount > 0)
            {
                newCount--;
                ItemCount = newCount.ToString();
                SimpleIoc.Default.GetInstance<MainViewModel>().Save();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));  
        }
    }
}
