using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetTrackingDatabase
{
    // Mobiles class
    public class Mobile : Asset
    {
        public Mobile(string modelName, DateTime purchaseDate, decimal price)
        {
            ModelName = modelName;
            PurchaseDate = purchaseDate;
            Price = price;
        }
    }
}
