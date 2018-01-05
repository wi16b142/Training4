using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training4.Model
{
    public class Product
    {
        public static string[] type = new String[] {"Engine", "Gears", "Body" };

        public Product(string iD, string name, int price, string type)
        {
            ID = iD;
            Name = name;
            Price = price;
            Type = type;
        }

        public string ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string  Type { get; set; }

    }
}
