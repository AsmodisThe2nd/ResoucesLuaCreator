﻿using System;
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
using Newtonsoft.Json.Linq;
using System.IO;

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
        BitmapImage img;
        resultWindow r;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(string steamApiKey, string collectionID)
        {
            img = new BitmapImage(new Uri("pack://application:,,,/ResourcesLua;component/Resources/placeholder.png"));

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
            // MessageBox.Show(responseString);

            JToken collection = JObject.Parse(responseString)["response"]["collectiondetails"][0];

            IList<JToken> collChildren = collection["children"].Children().ToList();


            postParameters.Clear();
            postParameters.Add("itemcount", collChildren.Count.ToString());
            postParameters.Add("key", steamApiKey);
            postParameters.Add("format", "json");
            postParameters.Add("collectioncount", "1");

            int i = 0;
            foreach (JToken c in collChildren)
            {
                postParameters.Add("publishedfileids[" + i.ToString() + "]", c["publishedfileid"].ToString());
                i++;
            }
            content = new FormUrlEncodedContent(postParameters);
            response = client.PostAsync(steamApiUrl + "/ISteamRemoteStorage/GetPublishedFileDetails/v0001/", content).Result;
            if (!response.IsSuccessStatusCode)
            {
                MessageBox.Show("Api Key might be invalid!");
            }
            responseString = response.Content.ReadAsStringAsync().Result;
            //MessageBox.Show(responseString);
            IList<JToken> addons = JObject.Parse(responseString)["response"]["publishedfiledetails"].Children().ToList();
            i = 0;
            foreach (JToken addon in addons)
            {
                try
                {
                    WorkshopItemUi w = new WorkshopItemUi(
                       addon["publishedfileid"].ToString(),
                       addon["title"].ToString(),
                       addon["description"].ToString(),
                       addon["preview_url"].ToString(),
                        client,
                        img);
                    stackpanel.Children.Add(w);
                }
                catch (Exception ex) { i++; }
            }

            MessageBox.Show(i.ToString() + " Addon" + (i>1?"s":"") + " could not be loaded!");
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string result = "";
            foreach (WorkshopItemUi item in stackpanel.Children)
            {
                if (item.IncludeInLUA)
                {
                    result += item.getResourceString() + "\r\n";
                }
            }
            if (r == null)
            {
                r = new resultWindow(result);
            }
            else
            {
                r.setText(result);
            }
            r.Show();

        }
    }
}
