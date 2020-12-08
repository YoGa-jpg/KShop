using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KShop
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public User(string Login, string Password, string Email)
        {
            this.Login = Login;
            this.Password = Password;
            this.Email = Email;
        }

        public User(string Login, string Password)
        {
            this.Login = Login;
            this.Password = Password;
        }

        public User() { }
    }
}
