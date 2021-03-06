﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using WarframeTracker.Enums;
using WarframeTracker.Model;

namespace WarframeTracker.ViewModel
{
    public class AddRelicDialogViewModel : ViewModelBase
    {
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
                RaisePropertyChanged();
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
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<ComponentModel> _components = new ObservableCollection<ComponentModel>
        {
            new ComponentModel { Owned = false},
            new ComponentModel { Owned = false},
            new ComponentModel { Owned = false},
            new ComponentModel { Owned = false},
            new ComponentModel { Owned = false},
            new ComponentModel { Owned = false}
        };

        public ObservableCollection<ComponentModel> Components
        {
            get
            {
                return _components;
            }
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

        private RelayCommand _addComponentCommand;
        public ICommand AddComponentCommand =>  _addComponentCommand ?? ( _addComponentCommand = new RelayCommand(AddComponent));
        protected void AddComponent()
        {
            Components.Add(new ComponentModel {Owned = false});
        }

        #region RemoveComponentCommand
        private RelayCommand<ComponentModel> _removeComponentCommand;
        public ICommand RemoveComponentCommand => _removeComponentCommand ?? (_removeComponentCommand = new RelayCommand<ComponentModel>(RemoveComponent));
        protected void RemoveComponent(ComponentModel componentModel)
        {
            Components.Remove(componentModel);
        }
        #endregion
    }
}