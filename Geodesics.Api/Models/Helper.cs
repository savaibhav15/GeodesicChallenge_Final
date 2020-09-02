using System;

namespace Geodesics.Api.Controllers
{
    public static class Helper
    {
        private const double HalfCircleDegrees = 180.0;

        public static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / HalfCircleDegrees;
        }

        public static double RadiansToDegrees(double radians)
        {
            return radians * HalfCircleDegrees / Math.PI;
        }

        public static double GetEarthRadius(MeasureUnit measureUnit)
        {
            switch (measureUnit)
            {
                case MeasureUnit.Km:
                    return 6371;
                case MeasureUnit.Mile:
                    return 3959;
                default:
                    throw new ArgumentOutOfRangeException(nameof(measureUnit));
            }
        }

    }
}