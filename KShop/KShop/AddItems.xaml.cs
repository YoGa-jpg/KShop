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
using KShop.Goods;
using KShop.Сontext;

namespace KShop
{
    /// <summary>
    /// Interaction logic for AddItems.xaml
    /// </summary>
    public partial class AddItems : Window
    {
        private Admin admin;

        public AddItems(Admin admin)
        {
            InitializeComponent();

            this.admin = admin;
        }

        private void AddComputer_Click(object sender, RoutedEventArgs e)
        {
            Computer computer = new Computer(ComputerName.Text, ComputerCpu.Text, ComputerGpu.Text, ComputerImage.Text,
                double.Parse(ComputerRAM.Text), double.Parse(ComputerROM.Text), double.Parse(ComputerVoltage.Text),
                double.Parse(ComputerPrice.Text));

            using (DatabaseContext db = new DatabaseContext())
            {
                db.Computer.Add(computer);

                db.SaveChanges();
            }

            admin.InitializeGrids();
        }

        private void AddLaptop_Click(object sender, RoutedEventArgs e)
        {
            Laptop laptop = new Laptop(LaptopName.Text, LaptopCpu.Text, LaptopGpu.Text, LaptopColor.Text, LaptopOS.Text,
                LaptopImage.Text, LaptopDiagonal.Text, double.Parse(LaptopRAM.Text), double.Parse(LaptopROM.Text),
                double.Parse(LaptopPrice.Text));

            using (DatabaseContext db = new DatabaseContext())
            {
                db.Laptop.Add(laptop);

                db.SaveChanges();
            }

            admin.InitializeGrids();
        }

        private void AddPhone_Click(object sender, RoutedEventArgs e)
        {
            Phone phone = new Phone(int.Parse(PhoneCamera.Text), int.Parse(PhoneSIM.Text), int.Parse(PhoneAccum.Text),
                PhoneName.Text, PhoneOS.Text, PhoneScreen.Text, PhoneCpu.Text, PhoneImage.Text,
                double.Parse(PhoneRAM.Text), double.Parse(PhoneROM.Text), double.Parse(PhonePrice.Text));

            using (DatabaseContext db = new DatabaseContext())
            {
                db.Phone.Add(phone);

                db.SaveChanges();
            }

            admin.InitializeGrids();
        }
    }
}
