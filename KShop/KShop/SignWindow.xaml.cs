using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using KShop.Сontext;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using TextBox = System.Windows.Controls.TextBox;

namespace KShop
{
    /// <summary>
    /// Interaction logic for SignWindow.xaml
    /// </summary>
    public partial class SignWindow : Window
    {
        private MainWindow mainWindow;

        private string RussianString = "йцукенгшщзхъэждлорпавыфячсмитьбю";

        public SignWindow(MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;

        }

        private void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (SignUpLogin.Text == "Admin" & SignUpPass.Password == "123456789")
            {
                new Admin().Show();
            }
            try
            {
                User user = new User(SignUpLogin.Text, SignUpPass.Password);

                using (DatabaseContext db = new DatabaseContext())
                {
                    User logged = db.User.Single(item => item.Login == user.Login & item.Password == user.Password);

                    mainWindow.LogAccount(logged);

                    this.Close();
                }
            }
            catch (InvalidOperationException exception)
            {
                ResultBlock.Text = "Неверный логин или пароль";
            }
        }

        private void RegButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(SignInEmail.Text == string.Empty | SignInLogin.Text == string.Empty |
                  SignInPass.Password == string.Empty))
            {
                if (RussianNotCotains(SignInEmail.Text) & RussianNotCotains(SignInLogin.Text) &
                    RussianNotCotains(SignInPass.Password))
                {
                    if (IsEmailValid(SignInEmail.Text))
                    {
                        User user = new User(SignInLogin.Text, SignInPass.Password, SignInEmail.Text);

                        try
                        {
                            using (DatabaseContext db = new DatabaseContext())
                            {
                                if (db.User.Count(item => item.Login == user.Login) == 0)
                                {
                                    db.User.Add(user);

                                    db.SaveChanges();

                                    ResultBlock.Text = "Регистрация прошла успешно";
                                }
                                else
                                {
                                    ResultBlock.Text = "Пользователь с таким логином уже существует";
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ResultBlock.Text = $"Произошла непредвиденная ошибка\n{ex.Message}";
                        }
                    }
                    else
                    {
                        ResultBlock.Text = "Неверный формат почты";
                    }
                }
                else
                {
                    ResultBlock.Text = "Содержит русские символы";
                }
            }
            else
            {
                ResultBlock.Text = "Одно из полей пустое";
            }
        }

        private void CloseImage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private bool IsEmailValid(string email)
        {
            try
            {
                MailAddress adress = new MailAddress(email);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private bool RussianNotCotains(string text)
        {
            foreach (var q in RussianString)
            {
                if (text.Contains(q))
                    return false;
            }

            return true;
        }
    }
}
