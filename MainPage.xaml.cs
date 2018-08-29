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
using System.Reflection;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TheImmortalTrail
{
    public sealed partial class MainPage : Page
    {
        public static Player player1 = new Player();
        public Story s = new Story();
        public Player p = new Player();

        public MainPage()
        {
            this.InitializeComponent();

            OpenGameLogic();

        }

        public async void OpenGameLogic()
        {

            StartGameIntro();

        }

        public void NewGamePlay()
        {

            PlayStory();

        }

        public async void ContinueGamePlay()
        {

            player1 = await LoadPlayerSaveFile();

            PlayStory();

        }

        public void GamePlay()
        {

            PlayStory();

        }

        public void StartGameIntro()
        {
            Canvasation.Background = new SolidColorBrush(Colors.Green);

            Button startButton = new Button();
            startButton.Height = 60;
            startButton.Width = 120;
            startButton.HorizontalAlignment = HorizontalAlignment.Center;
            startButton.Margin = new Thickness (0, 600, 200, 0);
            startButton.Tapped += new TappedEventHandler(Button_StartTappedEvent);
            startButton.Content = "New Game";

            Button continueButton = new Button();
            continueButton.Height = 60;
            continueButton.Width = 120;
            continueButton.HorizontalAlignment = HorizontalAlignment.Center;
            continueButton.Margin = new Thickness(200, 600, 0, 0);
            continueButton.Tapped += new TappedEventHandler(Button_ContinuedTappedEvent);
            continueButton.Content = "Continue";

            ButtonGrid.Children.Add(startButton);
            ButtonGrid.Children.Add(continueButton);

        }

        public void Button_StartTappedEvent(object sender, TappedRoutedEventArgs e)
        {
            ButtonGrid.Children.Clear();
            NewGamePlay();
        }

        public void Button_ContinuedTappedEvent(object sender, TappedRoutedEventArgs e)
        {
            ButtonGrid.Children.Clear();
            ContinueGamePlay();
        }

        public void SaveButton()
        {
            Button save = new Button();
            save.Content = "Save";
            save.Height = 40;
            save.Width = 80;
            save.VerticalAlignment = VerticalAlignment.Top;
            save.HorizontalAlignment = HorizontalAlignment.Left;
            save.Tapped += new TappedEventHandler(Button_SaveButtonTapped);

            ButtonGrid.Children.Add(save);
        }

        public void Button_SaveButtonTapped(object sender, TappedRoutedEventArgs e)
        {
            SavePlayerFile(player1);
        }

        public void NextScene(int score, string saveKey)
        {
            SaveButton();
            string buttonOrStory = saveKey.Remove(0, 1);
            int tempKey = Int32.Parse(buttonOrStory);
            if (s.GetType().GetProperty($"S{tempKey}")==null && s.GetType().GetProperty($"Q{tempKey}") == null)
            {
                SavePlayerFile(player1);
                StartGameIntro();
            }
            else if (s.GetType().GetProperty($"B{tempKey}1") == null)
            {
                #region NewSaveKey2

                string newSaveKey = player1.SaveKey.Remove(0, 1);
                int saveKeyNum = Int32.Parse(newSaveKey);
                saveKeyNum += 1;

                player1.SaveKey = $"S{saveKeyNum}";

                #endregion
                PlayStory();
            }
            else if (s[$"B{buttonOrStory}1"].Equals(s[$"B{buttonOrStory}1"]) && s[$"Q{tempKey}"].Equals(s[$"Q{tempKey}"]))
            {
                TextBlock tb = new TextBlock();
                tb.Height = 350;
                tb.Width = 800;
                tb.HorizontalAlignment = HorizontalAlignment.Center;
                tb.VerticalAlignment = VerticalAlignment.Bottom;
                tb.Margin = new Thickness(0, 0, 0, 80);
                tb.Text = $"{s[$"Q{player1.SaveKey.Remove(0, 1)}"]}";
                tb.TextWrapping = TextWrapping.Wrap;

                Button A1 = new Button();
                A1.Name = "A1";
                A1.Height = 80;
                A1.Width = 600;
                A1.HorizontalAlignment = HorizontalAlignment.Left;
                A1.Margin = new Thickness(80, 400, 200, 0);
                A1.Tapped += new TappedEventHandler(Button_NextStory);
                String[] a1BtnSetup = $"{s[$"B{buttonOrStory}1"]}".Split(':');
                String[] a1Button = a1BtnSetup[0].Split('|');
                //int[] a1Ints =
                //{
                //    Int32.Parse(a1Button[0]),
                //    Int32.Parse(a1Button[1])
                //};
                A1.Content = $"{a1BtnSetup[1]}";

                Button A2 = new Button();
                A2.Name = "A2";
                A2.Height = 80;
                A2.Width = 600;
                A2.HorizontalAlignment = HorizontalAlignment.Right;
                A2.Margin = new Thickness(200, 400, 80, 0);
                A2.Tapped += new TappedEventHandler(Button_NextStory);
                String[] a2BtnSetup = $"{s[$"B{buttonOrStory}2"]}".Split(':');
                //String[] a2Button = a2BtnSetup[0].Split('|');
                //int[] a2Ints =
                //{
                //    Int32.Parse(a2Button[0]),
                //    Int32.Parse(a2Button[1])
                //};
                A2.Content = $"{a2BtnSetup[1]}";

                Button A3 = new Button();
                A3.Name = "A3";
                A3.Height = 80;
                A3.Width = 600;
                A3.HorizontalAlignment = HorizontalAlignment.Left;
                A3.Margin = new Thickness(80, 600, 200, 0);
                A3.Tapped += new TappedEventHandler(Button_NextStory);
                String[] a3BtnSetup = $"{s[$"B{buttonOrStory}3"]}".Split(':');
                //String[] a3Button = a3BtnSetup[0].Split('|');
                //int[] a3Ints =
                //{
                //    Int32.Parse(a3Button[0]),
                //    Int32.Parse(a3Button[1])
                //};
                A3.Content = $"{a3BtnSetup[1]}";

                Button A4 = new Button();
                A4.Name = "A4";
                A4.Height = 80;
                A4.Width = 600;
                A4.HorizontalAlignment = HorizontalAlignment.Right;
                A4.Margin = new Thickness(200, 600, 80, 0);
                A4.Tapped += new TappedEventHandler(Button_NextStory);
                String[] a4BtnSetup = $"{s[$"B{buttonOrStory}4"]}".Split(':');
                //String[] a4Button = a4BtnSetup[0].Split('|');
                //int[] a4Ints =
                //{
                //    Int32.Parse(a4Button[0]),
                //    Int32.Parse(a4Button[1])
                //};
                A4.Content = $"{a4BtnSetup[1]}";

                ButtonGrid.Children.Add(tb);
                ButtonGrid.Children.Add(A1);
                ButtonGrid.Children.Add(A2);
                ButtonGrid.Children.Add(A3);
                ButtonGrid.Children.Add(A4);

            }
            
        }

        public void Button_NextStory(object sender, TappedRoutedEventArgs e)
        {
            Button thisButton = sender as Button;
            string name = thisButton.Name.Remove(0, 1);
            string buttonOrStory = player1.SaveKey.Remove(0, 1);

            int[] a4Ints = new int[2];

            String[] a4BtnSetup = $"{s[$"B{buttonOrStory}{name}"]}".Split(':');
            String[] a4Button = a4BtnSetup[0].Split('|');
            if (a4Button.Count().Equals(2))
            {
                a4Ints[0] = Int32.Parse(a4Button[0]);
                a4Ints[1] = Int32.Parse(a4Button[1]);
                player1.Score += a4Ints[0];
                player1.MemeCount += a4Ints[1];
            }
            else
            {
                a4Ints[0] = Int32.Parse(a4Button[0]);
                player1.Score += a4Ints[0];
            }

            PlayNextStory(name);

            #region New SaveKey

            string newSaveKey = player1.SaveKey.Remove(0, 1);
            int saveKeyNum = Int32.Parse(newSaveKey);
            saveKeyNum += 1;

            player1.SaveKey = $"S{saveKeyNum}";

            #endregion


        }

        public void PlayNextStory(string ButtonChoice)
        {
            ButtonGrid.Children.Clear();

            TextBlock tb = new TextBlock();
            tb.Height = 350;
            tb.Width = 800;
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            tb.VerticalAlignment = VerticalAlignment.Bottom;
            tb.Margin = new Thickness(0, 0, 0, 80);
            tb.Text = $"{s[$"S{player1.SaveKey.Remove(0,1)}{ButtonChoice}"]}";
            tb.TextWrapping = TextWrapping.Wrap;
            tb.Tapped += new TappedEventHandler(Button_NextStoryTapped);

            ButtonGrid.Children.Add(tb);

        }
        public void PlayStory()
        {
            ButtonGrid.Children.Clear();

            TextBlock tb = new TextBlock();
            tb.Height = 350;
            tb.Width = 800;
            tb.HorizontalAlignment = HorizontalAlignment.Center;
            tb.VerticalAlignment = VerticalAlignment.Bottom;
            tb.Margin = new Thickness(0, 0, 0, 80);
            tb.Text = $"{s[$"S{player1.SaveKey.Remove(0, 1)}"]}";
            tb.TextWrapping = TextWrapping.Wrap;
            tb.Tapped += new TappedEventHandler(Button_NextStoryTapped);

            ButtonGrid.Children.Add(tb);

        }

        public void Button_NextStoryTapped(object sender, TappedRoutedEventArgs e)
        {
            ButtonGrid.Children.Clear();
            NextScene(player1.Score, player1.SaveKey);
        }

        

        public async void SavePlayerFile(Player p)
        {
            var savePicker = new FileSavePicker();
            savePicker.FileTypeChoices.Add("Immortal Trail", new List<String> { ".tit" });

            StorageFile file = await savePicker.PickSaveFileAsync();

            if (file != null)
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(Player));
                string contents = "";

                using (var stream = new MemoryStream())
                {
                    ser.WriteObject(stream, p);
                    stream.Seek(0, SeekOrigin.Begin);

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

            Player p = null;

            var filePicker = new FileOpenPicker();

            filePicker.FileTypeFilter.Add(".tit");

            var file = await filePicker.PickSingleFileAsync();

            if (file != null)
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
