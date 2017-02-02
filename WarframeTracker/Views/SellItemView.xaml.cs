using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using WarframeTracker.Model;
using WarframeTracker.ViewModel;

namespace WarframeTracker.Views
{
    public partial class SellItemView
    {
        public SellItemView()
        {
            InitializeComponent();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var s = (FrameworkElement)sender;
            var dc = (SellComponentModel)s.DataContext;
            var dc2 = (SellItemModel)DataContext;
            var comp = new ComponentModel
            {
                ComponentName = dc.ItemPart,
                ItemName = dc2.ItemName,
                Owned = dc.IsOwned
            };
            SimpleIoc.Default.GetInstance<MainViewModel>().NewComponentObtained(comp);
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            /*
            var s = (FrameworkElement)sender;
            var dc = (SellComponentModel)s.DataContext;
            var dc2 = (SellItemModel) DataContext;
            var comp = new ComponentModel
            {
                ComponentName = dc.ItemPart,
                ItemName = dc2.ItemName,
                Owned = dc.IsOwned
            };
            SimpleIoc.Default.GetInstance<MainViewModel>().NewComponentObtained(comp);
            */

            SimpleIoc.Default.GetInstance<MainViewModel>().Save();
        }
    }
}
