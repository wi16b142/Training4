using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training4.Model;

namespace Training4.ViewModel
{
    public class ProductVM:ViewModelBase
    {
        Product product;
        public static String[] Types
        {
            get {return Product.type; }
        }

        public ProductVM(Product product)
        {
            this.product = product;
        }

        public ProductVM(string iD, string name, int price, string type)
        {
            ID = iD;
            Name = name;
            Price = price;
            Type = type;
        }

        #region Properties
        private string iD;

        public string ID
        {
            get { return iD; }
            set { iD = value; }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int price;

        public int Price
        {
            get { return price; }
            set { price = value; }
        }

        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        #endregion


    }
}
