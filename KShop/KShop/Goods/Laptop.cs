using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KShop
{
    [Table("Laptop")]
    public class Laptop
    {
        public int LaptopId { get; set; }

        public string LaptopName { get; set; }

        public string CpuName { get; set; }

        public string GpuName { get; set; }

        public string Color { get; set; }

        public string OS { get; set; }

        public string ImageSource { get; set; }

        public string Diagonal { get; set; }

        public double RAM { get; set; }

        public double ROM { get; set; }

        public double Price { get; set; }

        public Laptop(string LaptopName, string CpuName, string GpuName, string Color, string OS, string ImageSource, string Diagonal,
            double RAM, double ROM, double Price)
        {
            this.LaptopName = LaptopName;

            this.CpuName = CpuName;

            this.GpuName = GpuName;

            this.Color = Color;

            this.OS = OS;

            this.ImageSource = ImageSource;

            this.Diagonal = Diagonal;

            this.RAM = RAM;

            this.ROM = ROM;

            this.Price = Price;
        }

        public Laptop() { }

    }
}
