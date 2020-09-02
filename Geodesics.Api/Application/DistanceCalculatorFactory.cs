using Geodesics.Api.Models;
using Geodesics.Api.Service;
using System;

namespace Geodesics.Api.Application
{
    /// <summary>
    /// The factory to configure the type of distance calculation strategy to be used
    /// </summary>
    public class DistanceCalculatorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes the service provider
        /// </summary>
        /// <param name="serviceProvider">
        /// Mechanism for retrieving the service object
        /// </param>
        public DistanceCalculatorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        /// <summary>
        /// Gets the concrete type of Calculation strategy being used
        /// </summary>
        /// <param name="distanceMethod"></param>
        /// <returns>
        /// The type of Calculation strategy being used
        /// </returns>
        public IDistanceCalculationStrategy GetDistanceCalculationStrategy(DistanceMethod distanceMethod)
        {
            switch (distanceMethod)
            {
                case DistanceMethod.GeodesicCurve:
                    return (IDistanceCalculationStrategy)_serviceProvider.GetService(typeof(GeodesicCurveDistanceCalculationStrategy));
                case DistanceMethod.Pythagoras:
                    var result = (IDistanceCalculationStrategy)_serviceProvider.GetService(typeof(PythagorasDistanceCalculationStrategy));
                    return result;
                default:
                    throw new ArgumentOutOfRangeException(nameof(distanceMethod), distanceMethod, null);
            }

        }
    }
}