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
    /// Interaktionslogik für resultWindow.xaml
    /// </summary>
    public partial class resultWindow : Window
    {
        public resultWindow()
        {
            InitializeComponent();
        }

        public resultWindow(string result)
        {
            InitializeComponent();
            textBox.Text = result;
        }

        internal void setText(string result)
        {
            textBox.Text = result;
        }
    }
}
