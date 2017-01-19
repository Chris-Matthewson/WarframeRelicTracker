/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:WarframeTracker.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using WarframeTracker.DataService;
using WarframeTracker.Design;

namespace WarframeTracker.ViewModel
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IRelicService, DesignRelicService>();
            }
            else
            {
                SimpleIoc.Default.Register<IRelicService, RelicDataService>();
            }
            
            SimpleIoc.Default.Register<MainViewModel>();
        }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance","CA1822:MarkMembersAsStatic",Justification = "This non-static member is needed for data binding purposes.")]
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "This non-static member is needed for data binding purposes.")]
        public AddRelicDialogViewModel AddRelicDialog => new AddRelicDialogViewModel();

        public static void Cleanup()
        {
        }
    }
}