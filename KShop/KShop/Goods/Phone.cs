using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace KShop.Goods
{
    [Table("Phone")]
    public class Phone
    {
        public int PhoneId { get; set; }

        public int CameraQuality { get; set; }

        public int SIMAmount { get; set; }

        public int AccumStorage { get; set; }

        public string PhoneName { get; set; }

        public string OS { get; set; }

        public string ScreenType { get; set; }

        public string CpuName { get; set; }

        public string ImageSource { get; set; }

        public double RAM { get; set; }

        public double ROM { get; set; }

        public double Price { get; set; }

        public Phone(int CameraQuality, int SIMAmount, int AccumStorage, string PhoneName, string OS, string ScreenType,
            string CpuName, string ImageSource, double RAM, double ROM, double Price)
        {
            this.CameraQuality = CameraQuality;

            this.SIMAmount = SIMAmount;

            this.AccumStorage = AccumStorage;

            this.PhoneName = PhoneName;

            this.OS = OS;

            this.ScreenType = ScreenType;

            this.CpuName = CpuName;

            this.ImageSource = ImageSource;

            this.RAM = RAM;

            this.ROM = ROM;

            this.Price = Price;
        }

        public Phone() { }
    }
}
