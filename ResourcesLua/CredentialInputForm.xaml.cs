using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace ResourcesLua
{
    /// <summary>
    /// Interaktionslogik für CredentialInputForm.xaml
    /// </summary>
    public partial class CredentialInputForm : Window
    {

        string apiEnterMessage = "Enter API key here";
        string workshopIdEnterMessage = "Enter collection ID here";

        public CredentialInputForm()
        {
            InitializeComponent();
        }

        public CredentialInputForm(string key, string id)
        {
            InitializeComponent();
            textBox.Text = key;
            textBox_Copy.Text = id;
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                ((TextBox)sender).Text = apiEnterMessage;
            }
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == apiEnterMessage)
            {
                ((TextBox)sender).Text = "";
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.AppStarting;
            MainWindow m = new MainWindow(textBox.Text, textBox_Copy.Text);
            this.Cursor = Cursors.Arrow;
            var propertyInfo = typeof(Window).GetProperty("IsDisposed", BindingFlags.NonPublic | BindingFlags.Instance);

            if (!(bool)propertyInfo.GetValue(m))
            {
                m.Show();
            }
            this.Close();
        }

        private void textBox1_LostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                ((TextBox)sender).Text = workshopIdEnterMessage;
            }
        }

        private void textBox1_GotFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == workshopIdEnterMessage)
            {
                ((TextBox)sender).Text = "";
            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }
    }
}
