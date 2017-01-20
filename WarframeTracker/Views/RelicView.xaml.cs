using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using WarframeTracker.Model;
using WarframeTracker.ViewModel;

namespace WarframeTracker.Views
{
    public partial class RelicView
    {
        public RelicView()
        {
            InitializeComponent();
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().Save(((FrameworkElement)sender).DataContext as ComponentModel);
        }

        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            SimpleIoc.Default.GetInstance<MainViewModel>().Save(((FrameworkElement)sender).DataContext as ComponentModel);
        }
    }
}