using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaithConnect.Application.Utilities
{
    public static class GeoHelper
    {
        private const double EarthRadiusMeters = 6371000;

        public static double CalculateDistance(
            double latitude1,
            double longitude1,
            double latitude2,
            double longitude2)
        {
            var lat1Rad = ToRadians(latitude1);
            var lon1Rad = ToRadians(longitude1);

            var lat2Rad = ToRadians(latitude2);
            var lon2Rad = ToRadians(longitude2);

            var deltaLat = lat2Rad - lat1Rad;
            var deltaLon = lon2Rad - lon1Rad;

            var a =
                Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                Math.Cos(lat1Rad) *
                Math.Cos(lat2Rad) *
                Math.Sin(deltaLon / 2) *
                Math.Sin(deltaLon / 2);

            var c = 2 * Math.Atan2(
                Math.Sqrt(a),
                Math.Sqrt(1 - a));

            return EarthRadiusMeters * c;
        }

        private static double ToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
    }
    }
