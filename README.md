A simple asset tracking console application for storing some simple information about laptops and mobiles. The assets are stored in database using Entity Framework Core 
and incorporates Create, Read, Update and Delete functionalities. Assets that are three months away from being older than three years are marked as red when 
listed.

After downloading and setting up the environment with Visual Studio and SQL server managment studio you probably will have to change
the "DESKTOP-2SJCFFC" part of the codeline:

string connectionString = "Data Source=DESKTOP-2SJCFFC;Initial Catalog=AssetTracking;Integrated Security=True; TrustServerCertificate=True";

to point to your own database. You'll find this line of code in the beginning of class MyDBContext. You might also want to remove "TrustServerCertificate=True" at
the end of that line, I couldnt get It to run properly without It but It has something to do with my computer it seems.  
