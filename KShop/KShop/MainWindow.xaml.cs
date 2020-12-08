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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KShop.Goods;
using KShop.Сontext;

namespace KShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public User loggedUser;

        public Basket basket;

        public MainWindow()
        {
            InitializeComponent();

            basket = new Basket();
        }

        private void LaptopButton_Click(object sender, RoutedEventArgs e)
        {
            HideBorders();

            LaptopFrontPolyline.BeginStoryboard(this.FindResource("GradientChaser") as Storyboard);

            LaptopBackPolyline.BeginStoryboard(this.FindResource("GradientChaser") as Storyboard);

            TextPrinter(FrontSelectionBlock, "Ноутбуки");
            TextPrinter(BackSelectionBlock, "Ноутбуки");

            GoodsFrame.Content = new LaptopPage(this);
        }

        private void ComputerButton_Click(object sender, RoutedEventArgs e)
        {
            HideBorders();

            ComputerFrontPolyline.BeginStoryboard(this.FindResource("GradientChaser") as Storyboard);

            ComputerBackPolyline.BeginStoryboard(this.FindResource("GradientChaser") as Storyboard);

            TextPrinter(FrontSelectionBlock, "Компьютеры");
            TextPrinter(BackSelectionBlock, "Компьютеры");

            GoodsFrame.Content = new ComputerPage(this);
        }

        private void PhoneButton_Click(object sender, RoutedEventArgs e)
        {
            HideBorders();

            PhoneFrontPolyline.BeginStoryboard(this.FindResource("GradientChaser") as Storyboard);

            PhoneBackPolyline.BeginStoryboard(this.FindResource("GradientChaser") as Storyboard);

            TextPrinter(FrontSelectionBlock, "Телефоны");

            TextPrinter(BackSelectionBlock, "Телефоны");

            GoodsFrame.Content = new PhonePage(this);
        }

        private void HideBorders()
        {
            Storyboard board = (this.FindResource("ReverseGradientChaser") as Storyboard).Clone();

            MainGrid.Children.OfType<Polyline>().ToList()
                .ForEach(line => Dispatcher.Invoke(() => line.Stroke = new LinearGradientBrush()
                {
                    EndPoint = new Point(0.5, 1),
                    StartPoint = new Point(0.5, 0),
                    GradientStops = new GradientStopCollection(new GradientStop[]
                    {
                        new GradientStop() {Color = (Color)ColorConverter.ConvertFromString("#BE1ABE")/*Colors.Red*/, Offset = 0},
                        new GradientStop() {Color = Colors.Transparent, Offset = 0.1}
                    })
                }));
        }

        public async void TextPrinter(TextBlock Source, object Content)
        {
            string stringSource = string.Empty;

            for (int i = 0; i < Content.ToString().Length; i++)
            {
                stringSource = stringSource.Insert(i, Content.ToString()[i].ToString());

                Source.Text = stringSource;

                await Task.Delay(20);
            }
        }

        public void LogAccount(User loggedUser)
        {
            this.loggedUser = loggedUser;

            UserLogin.Text = $"Пользователь: {this.loggedUser.Login}";

            BasketButton.IsEnabled = true;
        }

        private void EnterButton_OnClick(object sender, RoutedEventArgs e)
        {
            new SignWindow(this).Show();
        }

        private void BasketButton_OnClick(object sender, RoutedEventArgs e)
        {
            basket.computres = basket.computres.Distinct().ToList();

            basket.laptops = basket.laptops.Distinct().ToList();

            basket.phones = basket.phones.Distinct().ToList();

            new BasketWindow(this).Show();
        }
    }
}
