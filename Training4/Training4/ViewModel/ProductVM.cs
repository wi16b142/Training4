using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training4.Model
{
    public class ProductVM:ViewModelBase
    {
        private static string[] types = new String[] {"Engine", "Gears", "Body" };
        
        public ProductVM(string iD, string name, int price, string type)
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
        public static String[]  Types { get { return types; } }
    }
}
