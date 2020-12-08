using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using KShop.Сontext;

namespace KShop
{
    /// <summary>
    /// Interaction logic for BasketWindow.xaml
    /// </summary>
    public partial class BasketWindow : Window
    {
        private MainWindow mainWindow;
        public BasketWindow(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;

            LaptopsViewer.ItemsSource = this.mainWindow.basket.laptops;

            ComputersViewer.ItemsSource = this.mainWindow.basket.computres;

            PhonesViewer.ItemsSource = this.mainWindow.basket.phones;
        }

        private void RemoveLaptop_OnClick(object sender, RoutedEventArgs e)
        {
            mainWindow.basket.laptops = mainWindow.basket.laptops
                .Where(item => item.LaptopId.ToString() != (sender as Button).Tag.ToString()).ToList();

            LaptopsViewer.ItemsSource = mainWindow.basket.laptops;
        }

        private void RemoveComputer_OnClick(object sender, RoutedEventArgs e)
        {
            mainWindow.basket.computres = mainWindow.basket.computres
                .Where(item => item.ComputerId.ToString() != (sender as Button).Tag.ToString()).ToList();

            ComputersViewer.ItemsSource = mainWindow.basket.computres;
        }


        private void RemovePhone_OnClick(object sender, RoutedEventArgs e)
        {
            mainWindow.basket.phones = mainWindow.basket.phones
                .Where(item => item.PhoneId.ToString() != (sender as Button).Tag.ToString()).ToList();

            PhonesViewer.ItemsSource = mainWindow.basket.phones;
        }

        private void OrderButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (new Regex(@"\(?\d{2}\)?-? *\d{3}-? *-?\d{4}").IsMatch(PhoneBox.Text))
            {
                if (!(mainWindow.basket.computres.Count == 0 & mainWindow.basket.phones.Count == 0 & mainWindow.basket.laptops.Count == 0))
                {
                    using (DatabaseContext db = new DatabaseContext())
                    {
                        Order order = new Order(mainWindow.loggedUser, PhoneBox.Text, mainWindow.basket);

                        db.Order.Add(order);

                        db.SaveChanges();
                    }

                    new BuyWindow().Show();

                    WritepPDF();
                }
                else
                {
                    ResultBox.Text = "Отсутствуют товары для заказа";
                }
            }
            else
            {
                ResultBox.Text = "Неверный формат телефона";
                Thread.Sleep(500);
            }
        }

        private void WritepPDF()
        {
            Document document = new Document(PageSize.A4, 50, 50, 50, 50);

            var PDFfont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 24);

            var PDFfontBase = PDFfont.GetCalculatedBaseFont(false);

            string UserInforamtion = $"User: {mainWindow.loggedUser.Login}\nPhone Number: {PhoneBox.Text}\n\n\n";

            string BasketInformation =
                $"{(mainWindow.basket.laptops.Count != 0 ? $"Laptops: {mainWindow.basket.laptops.Select(item => item.LaptopName).Aggregate((x, y) => x + ", " + y)}" : string.Empty)}\n" +
                $"{(mainWindow.basket.computres.Count != 0 ? $"Computers: {mainWindow.basket.computres.Select(item => item.ComputerName).Aggregate((x, y) => x + ", " + y)}" : string.Empty)}\n" +
                $"{(mainWindow.basket.phones.Count != 0 ? $"Phones: {mainWindow.basket.phones.Select(item => item.PhoneName).Aggregate((x, y) => x + ", " + y)}" : string.Empty)}\n";

            using (var writer = PdfWriter.GetInstance(document, new FileStream("Check.pdf", FileMode.Create)))
            {
                document.Open();

                writer.DirectContent.SetColorStroke(new BaseColor(255, 0, 0));

                writer.DirectContent.SetLineWidth(2.0f);

                writer.DirectContent.MoveTo(document.Left - 25, document.Top);
                writer.DirectContent.LineTo(document.Right - 25, document.Top);
                writer.DirectContent.Stroke();

                writer.DirectContent.MoveTo(document.Left - 25, document.Top);
                writer.DirectContent.LineTo(document.Left - 25, document.Bottom + 100);
                writer.DirectContent.Stroke();

                writer.DirectContent.MoveTo(document.Right - 25, document.Top);
                writer.DirectContent.LineTo(document.Right - 25, document.Bottom + 100);
                writer.DirectContent.Stroke();

                writer.DirectContent.MoveTo(document.Left - 25, document.Bottom + 100);
                writer.DirectContent.LineTo(document.Right - 25, document.Bottom + 100);
                writer.DirectContent.Stroke();

                writer.DirectContent.BeginText();

                writer.DirectContent.SetFontAndSize(PDFfontBase, 24f);

                document.Add(new iTextSharp.text.Paragraph("Check"));

                document.Add(new iTextSharp.text.Paragraph(UserInforamtion + BasketInformation));

                document.Add(new iTextSharp.text.Paragraph($"\n{DateTime.Now}"));

                writer.DirectContent.EndText();

                document.Close();

                writer.Close();
            }
        }
    }
}
