using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace KShop.Goods
{
    [Table("Computer")]
    public class Computer
    {
        public int ComputerId { get; set; }

        public string ComputerName { get; set; }

        public string CpuName { get; set; }

        public string GpuName { get; set; }

        public string ImageSource { get; set; }

        public double RAM { get; set; }

        public double ROM { get; set; }

        public double Voltage { get; set; }

        public double Price { get; set; }

        public Computer(string ComputerName, string CpuName, string GpuName, string ImageSource, double RAM, double ROM, double Voltage,
            double Price)
        {
            this.ComputerName = ComputerName;

            this.CpuName = CpuName;

            this.GpuName = GpuName;

            this.ImageSource = ImageSource;

            this.RAM = RAM;

            this.ROM = ROM;

            this.Voltage = Voltage;

            this.Price = Price;
        }

        public Computer() { }
    }
}
