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

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var s = (FrameworkElement)sender;
            var dc = (ComponentModel)s.DataContext;
            SimpleIoc.Default.GetInstance<MainViewModel>().NewComponentObtained(dc);
        }
    }
}