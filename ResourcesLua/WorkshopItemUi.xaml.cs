using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Text.RegularExpressions;

namespace ResourcesLua
{
    /// <summary>
    /// Interaktionslogik für WorkshopItemUi.xaml
    /// </summary>
    public partial class WorkshopItemUi : UserControl
    {
        string workshopId;
        private string workshopTitle;
        private string workshopDescription;
        private string workshopThumbnailUrl;
        private Brush previousColor;

        public BitmapImage thumbnail {
            get { return (BitmapImage)GetValue(thumbnailProperty); }
            set { SetValue(thumbnailProperty, value); }
        }

        // Using a DependencyProperty as the backing store for thumbnail.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty thumbnailProperty =
            DependencyProperty.Register("thumbnail", typeof(BitmapImage), typeof(WorkshopItemUi), new PropertyMetadata(null));
        private HttpClient client;

        public bool IncludeInLUA {
            get {
                return checkBox.IsChecked == true;
            }
        }

        public WorkshopItemUi()
        {
            InitializeComponent();
            thumbnail = new BitmapImage(new Uri("pack://application:,,,/ResourcesLua;component/Resources/placeholder.png"));
        }

        public WorkshopItemUi(string id, HttpClient httpclient)
        {
            InitializeComponent();
            thumbnail = new BitmapImage(new Uri("pack://application:,,,/ResourcesLua;component/Resources/placeholder.png"));
            text.Text = id;
            workshopId = id;
        }

        public WorkshopItemUi(string id, string title, string description, string thumb, HttpClient client, BitmapImage placeholder)
        {
            InitializeComponent();

            thumbnail = placeholder;

            this.workshopId = id;
            this.workshopTitle = title;
            this.workshopDescription = description;
            this.workshopThumbnailUrl = thumb;
            text.Text = title + "  [ID:" + id + "]";
            this.client = client;
            //this.ToolTip = description;
            thumbnail = new BitmapImage(new Uri(workshopThumbnailUrl));
            image.ToolTip = workshopDescription;
        }

        public string getResourceString()
        {
            return "resource.AddWorkshop( \"" + workshopId + "\" ) -- " + RemoveSpecialCharacters(workshopTitle);
        }

        private static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9]+", "", RegexOptions.Compiled);
        }

        private void userControl_MouseEnter(object sender, MouseEventArgs e)
        {
            previousColor = this.Background;
            this.Background = Brushes.AliceBlue;
        }

        private void userControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Background = previousColor;
        }

        private void userControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            checkBox.IsChecked = !checkBox.IsChecked;
        }
    }
}
