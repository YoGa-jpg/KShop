using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KShop
{
    class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public string Phone { get; set; }

        public string PhoneIDs { get; set; }

        public string ComputerIDs { get; set; }

        public string LaptopIDs { get; set; }

        public Order(User user, string Phone, Basket basket)
        {
            UserId = user.UserId;

            this.Phone = Phone;
            try
            {
                this.PhoneIDs = basket.phones.Select(item => item.PhoneId.ToString())
                                .Aggregate((x, y) => x.ToString() + "-" + y.ToString() + "-");
            }
            catch (Exception e) { }

            try
            {
                this.ComputerIDs = basket.computres.Select(item => item.ComputerId.ToString())
                    .Aggregate((x, y) => x.ToString() + "-" + y.ToString() + "-");
            }
            catch (Exception e) { }

            try
            {
                this.LaptopIDs = basket.laptops.Select(item => item.LaptopId.ToString())
                    .Aggregate((x, y) => x.ToString() + "-" + y.ToString() + "-");
            }
            catch (Exception e) { }
        }

        public Order() { }
    }
}
