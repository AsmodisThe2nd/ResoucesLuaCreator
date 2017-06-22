using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;

namespace ResourcesLua
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string steamApiKey;
        private string collectionID = "249554398";
        private string steamApiUrl = "https://api.steampowered.com";
        HttpClient client;
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(string steamApiKey, string collectionID)
        {
            this.collectionID = collectionID;

            InitializeComponent();
            this.steamApiKey = steamApiKey;
            client = new HttpClient();
            var postParameters = new Dictionary<string, string>
            {
                { "format", "json" },
                {"key", steamApiKey },
                {"publishedfileids[0]",  collectionID},
                {"collectioncount", "1" }
            };
            var content = new FormUrlEncodedContent(postParameters);
            var response = client.PostAsync(steamApiUrl + "/ISteamRemoteStorage/GetCollectionDetails/v0001/", content).Result;
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Api Key might be invalid!");
            }
            var responseString = response.Content.ReadAsStringAsync().Result;
            MessageBox.Show(responseString);
        }
    }
}
