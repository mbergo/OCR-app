using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeApp
{
    public class ProductMatcher
    {
        private Dictionary<string, string> _eanDatabase;

        public ProductMatcher()
        {
            // Initialize the EAN database with sample data
            _eanDatabase = new Dictionary<string, string>
            {
                { "1234567890", "Product A" },
                { "0987654321", "Product B" },
                { "1111111111", "Product C" }
            };
        }

        public string GetProductName(string barcode)
        {
            if (_eanDatabase.ContainsKey(barcode))
            {
                return _eanDatabase[barcode];
            }
            else
            {
                return "Product not found in database";
            }
        }
    }
}

