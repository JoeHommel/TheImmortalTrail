using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TheImmortalTrail.Models;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.Storage.Pickers;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using Windows.UI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TheImmortalTrail
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            StartGameLogic();

        }

        public async void StartGameLogic()
        {
            Player p = new Player();
            p.saveKey = "S4";
            p.Score = 85;
            //SaveState(p);
            p = await LoadPlayerSaveFile();

            if (p != null)
            {
                Canvasation.Background = new SolidColorBrush(Colors.Green);
                Button button = new Button();
                button.Content = $"{p.Score}";
                Canvasation.Children.Add(button);
            }
            else
            {
                Canvasation.Background = new SolidColorBrush(Colors.Red);
            }
            

        }

        public string NextScene(int score, string saveKey)
        {
            string newSaveKey = "";

             

            return newSaveKey;
        }

        public async void SaveState(Player p)
        {
            var savePicker = new FileSavePicker();
            savePicker.FileTypeChoices.Add("Immortal Trail", new List<String> { ".tit" });

            StorageFile file = await savePicker.PickSaveFileAsync();

            if(file != null)
            {
                DataContractSerializer ser = new DataContractSerializer(typeof (Player));
                string contents = "";

                using (var stream = new MemoryStream())
                {
                    ser.WriteObject(stream, p);
                    
                    using (var streamReader = new StreamReader(stream))
                    {
                        contents = streamReader.ReadToEnd();
                    }

                }
                await FileIO.WriteTextAsync(file, contents);
            }
        }

        public async Task<Player> LoadPlayerSaveFile()
        {
            Player p = new Player();

            var filePicker = new FileOpenPicker();

            filePicker.FileTypeFilter.Add(".tit");

            var file = await filePicker.PickSingleFileAsync();

            if(file != null)
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(Player));

                using (Stream stream = await file.OpenStreamForReadAsync())
                {
                    var loadedPlayer = ser.ReadObject(stream) as Player;

                    if (loadedPlayer != null)
                    {
                        p = loadedPlayer;
                    }
                }
            }


            return p;
        }

    }

     
}
