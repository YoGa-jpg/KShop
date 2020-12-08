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
    /// Interaction logic for PhonePage.xaml
    /// </summary>
    public partial class PhonePage : Page
    {
        private List<Phone> localGoodsList;

        private Phone SearchTemplate = new Phone();

        private MainWindow mainWindow;

        private string ROMMedium, PriceMedium, PixelsMedium;
        public PhonePage(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;

            Phone phone1 = new Phone(48, 2, 4000, "Apple Iphone 6S", "iOS", "Ips", "Kiri 8185K", @"\Res\Images\phone_1.jpeg", 2, 8, 879);
            Phone phone2 = new Phone(24, 2, 3600, "Xiaomi Mi 9T", "Android", "AMOLED", "Snapdragon 730", @"\Res\Images\phone_1.jpeg", 6, 64, 680);
            Phone phone3 = new Phone(12, 2, 4600, "Samsung S10", "Android", "AMOLED", "Kirin 10205K", @"\Res\Images\phone_1.jpeg", 6, 32, 787);

            localGoodsList = new List<Phone>() { phone1, phone2, phone3 };

            using (DatabaseContext db = new DatabaseContext())
            {
                //db.Phone.AddRange(localGoodsList);

                //db.SaveChanges();

                localGoodsList = db.Phone.ToList();
            }

            GoodsViewer.ItemsSource = localGoodsList;
        }

        private void DeveloperBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchTemplate.PhoneName = ((ComboBoxItem)e.AddedItems[0]).Content.ToString();

            FindSuitable();
        }

        private void OSBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchTemplate.OS = ((ComboBoxItem)e.AddedItems[0]).Content.ToString();

            FindSuitable();
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

        private void ScreenBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchTemplate.ScreenType = ((ComboBoxItem) e.AddedItems[0]).Content.ToString();

            FindSuitable();
        }

        private void PixelsBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PixelsMedium = ((ComboBoxItem) e.AddedItems[0]).Content.ToString();

            FindSuitable();
        }

        private void FindSuitable()
        {
            List<Phone> LocalPhoneList = localGoodsList;

            if (SearchTemplate.PhoneName != null)
                LocalPhoneList = LocalPhoneList.Where(item => item.PhoneName.Contains(SearchTemplate.PhoneName))
                    .ToList();

            if (SearchTemplate.OS != null)
                LocalPhoneList = LocalPhoneList.Where(item => item.OS == SearchTemplate.OS).ToList();

            if (SearchTemplate.ScreenType != null)
                LocalPhoneList = LocalPhoneList.Where(item => item.ScreenType == SearchTemplate.ScreenType).ToList();

            if (SearchTemplate.RAM != 0)
                LocalPhoneList = LocalPhoneList.Where(item => item.RAM.Equals(SearchTemplate.RAM)).ToList();

            if (ROMMedium != null)
                LocalPhoneList = LocalPhoneList.Where(item =>
                    item.ROM >= double.Parse(ROMMedium.Split('-')[0]) &
                    item.ROM <= double.Parse(ROMMedium.Split('-')[1])).ToList();

            if (PixelsMedium != null)
                LocalPhoneList = LocalPhoneList.Where(item =>
                    item.CameraQuality >= double.Parse(PixelsMedium.Split('-')[0]) &
                    item.CameraQuality <= double.Parse(PixelsMedium.Split('-')[1])).ToList();

            if (PriceMedium != null)
                LocalPhoneList = LocalPhoneList.Where(item =>
                    item.Price >= double.Parse(PriceMedium.Split('-')[0]) &
                    item.Price <= double.Parse(PriceMedium.Split('-')[1])).ToList();

            GoodsViewer.ItemsSource = LocalPhoneList;
        }

        private void Buy_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            mainWindow.basket.phones.Add(localGoodsList.Single(item => item.PhoneId == int.Parse((sender as Button).Tag.ToString())));
        }
    }
}
