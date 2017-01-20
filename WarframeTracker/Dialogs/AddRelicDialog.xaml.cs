using System.Linq;
using System.Windows;
using WarframeTracker.Model;
using WarframeTracker.ViewModel;

namespace WarframeTracker.Dialogs
{
    public partial class AddRelicDialog
    {
        public AddRelicDialog()
        {
            InitializeComponent();
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            //((TextBox) sender).Focus();
        }

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        public RelicModel Relic => GetRelic();

        private RelicModel GetRelic()
        {
            var relic =  new RelicModel
            {
                RelicSuffix = ((AddRelicDialogViewModel)DataContext).RelicSuffix,
                RelicType = ((AddRelicDialogViewModel)DataContext).RelicType,
            };

            foreach (var component in ((AddRelicDialogViewModel)DataContext).Components.ToArray())
            {
                relic.Components.Add(new ComponentModel
                {
                    Item = component.Item,
                    Name = component.Name,
                    Owned = false
                });
            }

            return relic;
        }

    }
}
