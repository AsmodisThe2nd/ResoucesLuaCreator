using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(((TextBox)sender).Text == "")
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
            MainWindow m = new MainWindow(textBox.Text, textBox_Copy.Text);
            m.Show();
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
    }
}
