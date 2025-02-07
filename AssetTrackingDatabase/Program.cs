using Microsoft.EntityFrameworkCore;

namespace AssetTrackingDatabase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("A Simple Asset Tracker Database \n");
            MyDBContext db = new MyDBContext();

                // Add new assets so that the database is not empty at start
                if (!db.Laptops.Any() && !db.Mobiles.Any())
                {
                    db.Laptops.AddRange(
                        new Laptop("MacBook", new DateTime(2023, 1, 1), 1200),
                        new Laptop("Lenovo", new DateTime(2022, 5, 10), 800),
                        new Laptop("Asus", new DateTime(2021, 8, 15), 700)
                    );

                    db.Mobiles.AddRange(
                        new Mobile("iPhone", new DateTime(2023, 3, 20), 999),
                        new Mobile("Samsung", new DateTime(2022, 6, 5), 850),
                        new Mobile("Nokia", new DateTime(2021, 9, 30), 300)
                    );

                    db.SaveChanges();
                }

                // Display menu options
                while (true)
                {
                    Console.WriteLine("\nChoose an option:");
                    Console.WriteLine("1. Add a laptop asset");
                    Console.WriteLine("2. Add a mobile asset");
                    Console.WriteLine("3. View all assets");
                    Console.WriteLine("4. Update an asset");
                    Console.WriteLine("5. Delete an asset");
                    Console.WriteLine("6. Exit");

                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            AddLaptop(db);
                            break;
                        case "2":
                            AddMobile(db);
                            break;
                        case "3":
                            ViewAssets(db);
                            break;
                        case "4":
                            UpdateAsset(db);
                            break;
                        case "5":
                            DeleteAsset(db);
                            break;
                        case "6":
                            return;
                    }
                } 
        }

        // Function to read a date price input from user and prompt again if wrong
        static DateTime ReadDate()
        {
            while (true)
            {
                Console.Write("Enter Purchase Date (yyyy-mm-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
                {
                    return date;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date format. Please try again.");
                    Console.ResetColor();
                }
            }
        }

        // Function to read a decimal price input from user and prompt again if wrong
        static decimal ReadPrice()
        {
            while (true)
            {
                Console.Write("Enter Price: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal price))
                {
                    return price;
                }
                else 
                { 
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid price format. Please enter a valid number.");
                    Console.ResetColor();
                }
            }
        }

        // Function to read a valid asset ID from user input and prompt again if wrong
        static int ReadAssetId(MyDBContext db)
        {
            while (true)
            {
                Console.Write("Enter asset ID: ");
                if (int.TryParse(Console.ReadLine(), out int id) && (db.Laptops.Any(l => l.Id == id) || db.Mobiles.Any(m => m.Id == id)))
                {
                    return id;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid ID. Please enter a valid asset ID.");
                    Console.ResetColor();
                }
            }
        }

        // Function to add a new Laptop asset
        static void AddLaptop(MyDBContext db)
        {
            Console.Write("\nEnter Model Name: ");
            string modelName = Console.ReadLine();
            DateTime purchaseDate = ReadDate();
            decimal price = ReadPrice();

            db.Laptops.Add(new Laptop(modelName, purchaseDate, price));
            db.SaveChanges();
        }

        // Function to add a new Mobile asset
        static void AddMobile(MyDBContext db)
        {
            Console.Write("\nEnter Model Name: ");
            string modelName = Console.ReadLine();
            DateTime purchaseDate = ReadDate();
            decimal price = ReadPrice();

            db.Mobiles.Add(new Mobile(modelName, purchaseDate, price));
            db.SaveChanges();
        }

        // Function to display all assets sorted by type (class) and then purchase date
        static void ViewAssets(MyDBContext db)
        {
            DateTime warningThreshold = DateTime.Now.AddMonths(-33);
            Console.WriteLine("\nLaptops:");
            foreach (var laptop in db.Laptops.OrderBy(l => l.PurchaseDate))
            {
                if (laptop.PurchaseDate <= warningThreshold)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.WriteLine($"ID: {laptop.Id, -5}| Model: {laptop.ModelName, -10}| Purchase Date: {laptop.PurchaseDate.ToShortDateString(), -10}| Price: ${laptop.Price}");
                Console.ResetColor();
            }

            Console.WriteLine("\nMobiles:");
            foreach (var mobile in db.Mobiles.OrderBy(m => m.PurchaseDate))
            {
                if (mobile.PurchaseDate <= warningThreshold)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.WriteLine($"ID: {mobile.Id,-5}| Model: {mobile.ModelName,-10}| Purchase Date: {mobile.PurchaseDate.ToShortDateString(),-10}| Price: ${mobile.Price}");
                Console.ResetColor();
            }
        }

        // Function to update an existing asset
        static void UpdateAsset(MyDBContext db)
        {
            ViewAssets(db);
            Console.Write("\nChoose wich kind of asset to update, choose L for Laptop or M for Mobile: ");
            string letter = Console.ReadLine().ToLower();
            if (letter == "l")
            {
                int id = ReadAssetId(db);
                var asset = db.Laptops.Find(id) as Asset;
                if (asset != null)
                {
                    Console.Write("Enter New Model Name: ");
                    asset.ModelName = Console.ReadLine();
                    asset.PurchaseDate = ReadDate();
                    asset.Price = ReadPrice();
                    db.SaveChanges();
                }
            }
            else if (letter == "m")
            {
                int id = ReadAssetId(db);
                var asset = db.Mobiles.Find(id) as Asset;
                if (asset != null)
                {
                    Console.Write("Enter New Model Name: ");
                    asset.ModelName = Console.ReadLine();
                    asset.PurchaseDate = ReadDate();
                    asset.Price = ReadPrice();
                    db.SaveChanges();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter L or M.");
                Console.ResetColor();
                UpdateAsset(db);
            }
        }

        // Function to delete an asset
        static void DeleteAsset(MyDBContext db)
        {
            ViewAssets(db);
            Console.Write("\nChoose wich kind of asset to delete, choose L for Laptop or M for Mobile: ");
            string letter = Console.ReadLine().ToLower();
            if (letter == "l")
            {
                int id = ReadAssetId(db);
                var asset = db.Laptops.Find(id) as Asset;
                if (asset != null)
                {
                    db.Remove(asset);
                    db.SaveChanges();
                }
            }
            else if (letter == "m")
            {
                int id = ReadAssetId(db);
                var asset = db.Mobiles.Find(id) as Asset;
                if (asset != null)
                {
                    db.Remove(asset);
                    db.SaveChanges();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter L or M.");
                Console.ResetColor();
                DeleteAsset(db);
            }

        }
    }
}

