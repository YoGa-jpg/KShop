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
using System.Windows.Navigation;
using System.Windows.Shapes;
using KShop.Goods;
using KShop.Сontext;

namespace KShop
{
    /// <summary>
    /// Interaction logic for ComputerPage.xaml
    /// </summary>
    public partial class ComputerPage : Page
    {
        private List<Computer> localGoodsList;

        private Computer SearchTemplate = new Computer();

        private MainWindow mainWindow;

        private string ROMMedium, PriceMedium;
        public ComputerPage(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;

            //Computer computer1 = new Computer("AVOCADO", "INTEL", "RADEON 2512X", @"C:\Users\Yoga\Desktop\КПиЯП\KShop\KShop\Res\Images\computer_1.jpeg", 8, 1024, 600, 500);
            //Computer computer2 = new Computer("LIMON", "AMD", "GeForce 1050 GTX", @"C:\Users\Yoga\Desktop\КПиЯП\KShop\KShop\Res\Images\computer_1.jpeg", 2, 512, 100, 2000);
            //Computer computer3 = new Computer("COCOS", "INTEL", "Palit 2080", @"C:\Users\Yoga\Desktop\КПиЯП\KShop\KShop\Res\Images\computer_1.jpeg", 8, 256, 300, 6000);

            //localGoodsList = new List<Computer>() { computer1, computer2, computer3 };

            using (DatabaseContext db = new DatabaseContext())
            {
                //db.Computer.AddRange(localGoodsList);

                //db.SaveChanges();

                localGoodsList = db.Computer.ToList();
            }

            GoodsViewer.ItemsSource = localGoodsList;
        }

        private void RAMBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchTemplate.RAM = double.Parse(((ComboBoxItem)e.AddedItems[0]).Content.ToString());

            FindSuitable();
        }

        private void ROMBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ROMMedium = ((ComboBoxItem)e.AddedItems[0]).Content.ToString();

            FindSuitable();
        }

        private void PriceBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PriceMedium = ((ComboBoxItem)e.AddedItems[0]).Content.ToString();

            FindSuitable();
        }

        private void FindSuitable()
        {
            List<Computer> LocalComputerList = localGoodsList;

            if (SearchTemplate.RAM != 0)
                LocalComputerList = LocalComputerList.Where(item => item.RAM.Equals(SearchTemplate.RAM)).ToList();

            if (ROMMedium != null)
                LocalComputerList = LocalComputerList.Where(item =>
                    item.ROM >= double.Parse(ROMMedium.Split('-')[0]) &
                    item.ROM <= double.Parse(ROMMedium.Split('-')[1])).ToList();

            if (PriceMedium != null)
                LocalComputerList = LocalComputerList.Where(item =>
                    item.Price >= double.Parse(PriceMedium.Split('-')[0]) &
                    item.Price <= double.Parse(PriceMedium.Split('-')[1])).ToList();

            GoodsViewer.ItemsSource = LocalComputerList;
        }

        private void Buy_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainWindow.basket.computres.Add(localGoodsList.Single(item => item.ComputerId == int.Parse((sender as Button).Tag.ToString())));
        }
    }
}
