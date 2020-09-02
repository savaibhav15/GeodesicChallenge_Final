namespace Geodesics.Api.Models
{
    public class DistancePoint
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DistancePoint"/> class.
        /// </summary>
        /// <param name="latitude">
        /// The latitude of the point.
        /// </param>
        /// <param name="longitude">
        /// The longitude of the point.
        /// </param>
        public DistancePoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Using a private setter to prevent value from being modified elsewhere.
        /// </summary>
        public double Latitude { get; private set; }

        /// <summary>
        /// Using a private setter to prevent value from being modified elsewhere.
        /// </summary>
        public double Longitude { get; private set; }
    }
}