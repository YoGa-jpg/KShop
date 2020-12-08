using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KShop.Goods;
using Microsoft.EntityFrameworkCore;

namespace KShop.Сontext
{
    class DatabaseContext : DbContext
    {
        public DbSet<Laptop> Laptop { get; set; }

        public DbSet<Computer> Computer { get; set; }

        public DbSet<Phone> Phone { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<Order> Order { get; set; }

        public DatabaseContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Yoga\Desktop\КПиЯП\KShop\KShop\KShopDB.mdf;Integrated Security=True");
    }
}
