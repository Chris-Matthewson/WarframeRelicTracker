using System;
using System.IO;
using WarframeTracker.ViewModel;

namespace WarframeTracker
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            string filePath;
            string _relicDirectory = @"C:\ProgramData\WarframeRelicTracker\default";
            
            if (!Directory.Exists(_relicDirectory))
            {
                Directory.CreateDirectory(_relicDirectory);
            }

            filePath = _relicDirectory +  @"\Lith.json";
            if (!File.Exists(filePath) && File.Exists(Environment.CurrentDirectory + @"\Relics\Lith.json"))
            {
                File.Copy(Environment.CurrentDirectory + @"\Relics\Lith.json", filePath);
            }

            filePath = _relicDirectory +  @"\Meso.json";
            if (!File.Exists(filePath) && File.Exists(Environment.CurrentDirectory + @"\Relics\Meso.json"))
            {
                File.Copy(Environment.CurrentDirectory + @"\Relics\Meso.json", filePath);
            }

            filePath = _relicDirectory +  @"\Neo.json";
            if (!File.Exists(filePath) && File.Exists(Environment.CurrentDirectory + @"\Relics\Neo.json"))
            {
                File.Copy(Environment.CurrentDirectory + @"\Relics\Neo.json", filePath);
            }

            filePath = _relicDirectory +  @"\Axi.json";
            if (!File.Exists(filePath) && File.Exists(Environment.CurrentDirectory + @"\Relics\Axi.json"))
            {
                File.Copy(Environment.CurrentDirectory + @"\Relics\Axi.json", filePath);
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