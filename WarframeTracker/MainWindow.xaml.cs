using System;
using System.IO;
using System.Threading;
using System.Windows;
using WarframeTracker.ViewModel;

namespace WarframeTracker
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            string filePath;
            string _relicDirectory = @"C:\ProgramData\WarframeRelicTracker";
            
            if (Directory.Exists(Environment.CurrentDirectory + @"\Relics\"))
            {

                MessageBox.Show("Updated relics detected. Would you like to import?" + Environment.NewLine +
                                "NOTE: THIS WILL ERASE ALL CURRENT DATA!!!", "Update Relics", MessageBoxButton.YesNo);
                {
                    if (Directory.Exists(_relicDirectory))
                    {
                        Directory.Delete(_relicDirectory, true);
                    }

                    Thread.Sleep(100);

                    Directory.CreateDirectory(_relicDirectory + @"\default");


                    filePath = _relicDirectory + @"\Lith.json";
                    File.Copy(Environment.CurrentDirectory + @"\Relics\Lith.json", filePath);

                    filePath = _relicDirectory + @"\Meso.json";
                    File.Copy(Environment.CurrentDirectory + @"\Relics\Meso.json", filePath);

                    filePath = _relicDirectory + @"\Neo.json";
                    File.Copy(Environment.CurrentDirectory + @"\Relics\Neo.json", filePath);

                    filePath = _relicDirectory + @"\Axi.json";
                    File.Copy(Environment.CurrentDirectory + @"\Relics\Axi.json", filePath);



                    filePath = _relicDirectory + @"\default\Lith.json";
                    File.Copy(Environment.CurrentDirectory + @"\Relics\Lith.json", filePath);

                    filePath = _relicDirectory + @"\default\Meso.json";
                    File.Copy(Environment.CurrentDirectory + @"\Relics\Meso.json", filePath);

                    filePath = _relicDirectory + @"\default\Neo.json";
                    File.Copy(Environment.CurrentDirectory + @"\Relics\Neo.json", filePath);

                    filePath = _relicDirectory + @"\default\Axi.json";
                    File.Copy(Environment.CurrentDirectory + @"\Relics\Axi.json", filePath);
                    
                    Directory.Delete(Environment.CurrentDirectory + @"\Relics\", true);
                }
                
            }

            if (!Directory.Exists(_relicDirectory))
            {
                Directory.CreateDirectory(_relicDirectory + @"\default\");
            }

            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();

            /*
            var relics = new List<RelicModel>();

            var dialog = new AddRelicDialog();
            while (dialog.ShowDialog() == true)
            {
                relics.Add(dialog.Relic);

                dialog = new AddRelicDialog();
            }

            File.WriteAllText(@"C:\Users\Cmatt\Desktop\Axi.json", JsonConvert.SerializeObject(relics, Formatting.Indented));
            Environment.Exit(0);
            */


        }
    }
}