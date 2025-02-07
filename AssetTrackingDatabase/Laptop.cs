using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTrackingDatabase
{
    // Laptops class
    public class Laptop : Asset
    {
        public Laptop(string modelName, DateTime purchaseDate, decimal price)
        {
            ModelName = modelName;
            PurchaseDate = purchaseDate;
            Price = price;
        }
    }
}
