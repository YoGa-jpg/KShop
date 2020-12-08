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
using KShop.Сontext;

namespace KShop
{
    /// <summary>
    /// Interaction logic for LaptopPage.xaml
    /// </summary>
    public partial class LaptopPage : Page
    {
        private List<Laptop> localGoodsList;

        private Laptop SearchTemplate = new Laptop();

        private MainWindow mainWindow;

        private string ROMMedium, PriceMedium;

        public LaptopPage(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;

            //Laptop laptop1 = new Laptop("Xiaomi MI 2", "AMD", "GTX 9999", "Черный", "Windows", @"/Res/Images/notebook.jpeg", "15.6", 16, 1024, 2000);
            //Laptop laptop4 = new Laptop("Xiaomi MI 3", "Intel", "GTX 9999", "Черный", "Linux", @"/Res/Images/notebook.jpeg", "15.6", 8, 512, 5000);
            //Laptop laptop5 = new Laptop("Xiaomi MI 4", "Intel", "GTX 9999", "Черный", "None", @"/Res/Images/notebook.jpeg", "15.6", 2, 256, 500);
            //Laptop laptop2 = new Laptop("Dell D12512", "AMD", "GTX 9999", "Черный", "iOS", @"/Res/Images/notebook.jpeg", "13.4", 2, 512, 1300);
            //Laptop laptop3 = new Laptop("ASUS ROG 253UA", "AMD", "GTX 9999", "Черный", "Linux", @"/Res/Images/notebook.jpeg", "15.6", 8, 1024, 3600);

            //localGoodsList = new List<Laptop>() { laptop1, laptop2, laptop3, laptop4, laptop5 };

            using (DatabaseContext db = new DatabaseContext())
            {
                //db.Laptop.AddRange(localGoodsList);

                //db.SaveChanges();

                localGoodsList = db.Laptop.ToList();
            }

            GoodsViewer.ItemsSource = localGoodsList;
        }

        private void DeveloperBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchTemplate.LaptopName = ((ComboBoxItem) e.AddedItems[0]).Content.ToString();

            FindSuitable();
        }

        private void OSBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchTemplate.OS = ((ComboBoxItem) e.AddedItems[0]).Content.ToString();

            FindSuitable();
        }


        private void DiagonalBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchTemplate.Diagonal = ((ComboBoxItem) e.AddedItems[0]).Content.ToString();

            FindSuitable();
        }

        private void RAMBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchTemplate.RAM = double.Parse(((ComboBoxItem) e.AddedItems[0]).Content.ToString());

            FindSuitable();
        }

        private void ROMBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ROMMedium = ((ComboBoxItem) e.AddedItems[0]).Content.ToString();

            FindSuitable();
        }

        private void PriceBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PriceMedium = ((ComboBoxItem) e.AddedItems[0]).Content.ToString();

            FindSuitable();
        }

        private void FindSuitable()
        {
            List<Laptop> LocalLaptopList = localGoodsList;

            if (SearchTemplate.LaptopName != null)
                LocalLaptopList = LocalLaptopList.Where(item => item.LaptopName.Contains(SearchTemplate.LaptopName))
                    .ToList();

            if (SearchTemplate.OS != null)
                LocalLaptopList = LocalLaptopList.Where(item => item.OS == SearchTemplate.OS).ToList();

            if (SearchTemplate.Diagonal != null)
                LocalLaptopList = LocalLaptopList.Where(item => item.Diagonal == SearchTemplate.Diagonal).ToList();

            if (SearchTemplate.RAM != 0)
                LocalLaptopList = LocalLaptopList.Where(item => item.RAM.Equals(SearchTemplate.RAM)).ToList();

            if (ROMMedium != null)
                LocalLaptopList = LocalLaptopList.Where(item =>
                    item.ROM >= double.Parse(ROMMedium.Split('-')[0]) &
                    item.ROM <= double.Parse(ROMMedium.Split('-')[1])).ToList();

            if (PriceMedium != null)
                LocalLaptopList = LocalLaptopList.Where(item =>
                    item.Price >= double.Parse(PriceMedium.Split('-')[0]) &
                    item.Price <= double.Parse(PriceMedium.Split('-')[1])).ToList();

            GoodsViewer.ItemsSource = LocalLaptopList;
        }

        private void Buy_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            mainWindow.basket.laptops.Add(localGoodsList.Single(item => item.LaptopId == int.Parse((sender as Button).Tag.ToString())));
        }
    }
}
