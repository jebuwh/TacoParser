using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        //<summary>
        //Locates two farthest Taco Bell locations based on TacoBell-US-AL.csv file data
        //</summary>

        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
          logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath);

            logger.LogInfo($"Lines: {lines[0]}");

           
            var parser = new TacoParser();

            
            var locations = lines.Select(parser.Parse).ToArray();

           
            ITrackable locationA = new TacoBell();
            ITrackable locationB = new TacoBell();
            locationA = null;
            locationB= null;
            ITrackable locA = null;
            ITrackable locB = null;
            
            var distance = 0.00;
           
            GeoCoordinate geo = new GeoCoordinate();
            
            for (int i = 0; i < locations.Length; i++)
            {
                locationA = locations[i];
                var corA = new GeoCoordinate();
                corA.Latitude = locationA.Location.Latitude;
                corA.Longitude = locationA.Location.Longitude;

                for (int j = 0; j < locations.Length; j++)
                {
                    locationB = locations[j];
                    var corB = new GeoCoordinate();
                    corB.Latitude = locationB.Location.Latitude;
                    corB.Longitude = locationB.Location.Longitude;

                    
                    if(corA.GetDistanceTo(corB) > distance)
                    {
                       distance = corA.GetDistanceTo(corB);
                        locA = locationA;
                        locB = locationB;

                    }
                   

                    
                }

                
            }
            double miles = Math.Round(distance * 0.00062137, 2);
            Console.WriteLine($"{locA.Name} and {locB.Name} are {miles} miles apart.");
           
        }
    }
}
