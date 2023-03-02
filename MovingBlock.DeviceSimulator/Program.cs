// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var point1 = new Location(17, 100);
var point2 = new Location(17, 100);

// 10 m Longitude change:
double longchangeonem = 0.0000093960227;
point2.Longitude = point1.Longitude + longchangeonem;
Console.WriteLine(CalculateDistance(point1, point2));
point2.Longitude = point1.Longitude + longchangeonem * 10;
Console.WriteLine(CalculateDistance(point1, point2));
point2.Longitude = point1.Longitude + longchangeonem * 100;
Console.WriteLine(CalculateDistance(point1, point2));
point2.Longitude = point1.Longitude + longchangeonem * 1000;
Console.WriteLine(CalculateDistance(point1, point2));
point2.Longitude = point1.Longitude + longchangeonem * 50000;
Console.WriteLine(CalculateDistance(point1, point2));

Console.WriteLine("---------------------------");
point2 = GetPoint2(point1, 1);
Console.WriteLine(CalculateDistance(point1, point2));
point2 = GetPoint2(point1, 10);
Console.WriteLine(CalculateDistance(point1, point2));
point2 = GetPoint2(point1, 100);
Console.WriteLine(CalculateDistance(point1, point2));
point2 = GetPoint2(point1, 1000);
Console.WriteLine(CalculateDistance(point1, point2));
point2 = GetPoint2(point1, 50000);
Console.WriteLine(CalculateDistance(point1, point2));

double CalculateDistance(Location point1, Location point2)
{
    var lat1 = point1.Latitude * (Math.PI / 180.0);
    var long1 = point1.Longitude * (Math.PI / 180.0);
    var lat2 = point2.Latitude * (Math.PI / 180.0);
    var long2 = point2.Longitude * (Math.PI / 180.0) - long1;
    var dist = Math.Pow(Math.Sin((lat2 - lat1) / 2.0), 2.0) +
             Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(long2 / 2.0), 2.0);
    return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(dist), Math.Sqrt(1.0 - dist)));
}

Location GetPoint2(Location point1, double dist)
{
    Location point2 = new Location(point1.Latitude, point1.Longitude);
    double longchangeonem = 0.0000093960227;
    point2.Longitude += longchangeonem * dist;
    return point2;
}

public class Location
{
    public Location(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public double Latitude { get; set; }
    public double Longitude { get; set; }
}