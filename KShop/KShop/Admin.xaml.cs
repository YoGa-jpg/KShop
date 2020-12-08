using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KShop.Goods;
using KShop.Сontext;
using DataGrid = System.Windows.Controls.DataGrid;
using MessageBox = System.Windows.MessageBox;

namespace KShop
{
    /// <summary>
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        private List<DataGrid> DbGrids;

        public Admin()
        {
            InitializeComponent();

            DbGrids = new List<DataGrid>() {UserGrid, OrderGrid, LaptopGrid, ComputerGrid, PhoneGrid};

            InitializeGrids();
        }

        private void AddItem_OnClick(object sender, RoutedEventArgs e)
        {
            new AddItems(this).Show();
        }

        public void InitializeGrids()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                UserGrid.ItemsSource = db.User.ToList();

                OrderGrid.ItemsSource = db.Order.ToList();

                LaptopGrid.ItemsSource = db.Laptop.ToList();

                ComputerGrid.ItemsSource = db.Computer.ToList();

                PhoneGrid.ItemsSource = db.Phone.ToList();
            }
        }

        private void DeleteItem_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedGrid = DbGrids.First(item => item.SelectedItem != null);

            using (DatabaseContext db = new DatabaseContext())
            {
                if (selectedGrid.SelectedItem is User)
                    db.User.Remove(selectedGrid.SelectedItem as User);
                else if (selectedGrid.SelectedItem is Order)
                    db.Order.Remove(selectedGrid.SelectedItem as Order);
                else if (selectedGrid.SelectedItem is Laptop)
                    db.Laptop.Remove(selectedGrid.SelectedItem as Laptop);
                else if (selectedGrid.SelectedItem is Computer)
                    db.Computer.Remove(selectedGrid.SelectedItem as Computer);
                else if (selectedGrid.SelectedItem is Phone)
                    db.Phone.Remove(selectedGrid.SelectedItem as Phone);

                db.SaveChanges();

                InitializeGrids();
            }
        }
    }
}
