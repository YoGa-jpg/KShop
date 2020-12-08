using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KShop.Goods;

namespace KShop
{
    public class Basket
    {
        public List<Phone> phones;

        public List<Laptop> laptops;

        public List<Computer> computres;

        public Basket()
        {
            phones = new List<Phone>();

            laptops = new List<Laptop>();

            computres = new List<Computer>();
        }
    }
}
