using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Paragraph = System.Windows.Documents.Paragraph;
using Path = System.Windows.Shapes.Path;

namespace KShop
{
    /// <summary>
    /// Interaction logic for BuyWindow.xaml
    /// </summary>
    public partial class BuyWindow : Window
    {
        public BuyWindow()
        {
            InitializeComponent();
        }


        private void BuyWindow_OnDeactivated(object sender, EventArgs e)
        {
            Thread.Sleep(500);

            this.Close();
        }
    }
}
