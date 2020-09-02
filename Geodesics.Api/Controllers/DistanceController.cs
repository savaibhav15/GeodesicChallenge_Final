using System.ComponentModel.DataAnnotations;
using Geodesics.Api.Application;
using Geodesics.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Geodesics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistanceController : ControllerBase
    {
        private readonly DistanceCalculatorFactory _distanceCalculatorFactory;
        
        /// <summary>
        /// Constructor which initializes the factory being used to return the service for distance method calculation.
        /// </summary>
        /// <param name="distanceCalculatorFactory"></param>
        public DistanceController(DistanceCalculatorFactory distanceCalculatorFactory)
        {
            _distanceCalculatorFactory = distanceCalculatorFactory;
        }

        /// <summary>
        /// Gets the response of the calculated distance between the points.
        /// </summary>
        /// <param name="distanceMethod"> The type of distance calculation method</param>
        /// <param name="measureUnit"> Measurement unit used </param>
        /// <param name="point1Latitude"> Latitude of the first point, ranges between -90 to 90</param>
        /// <param name="point1Longitude"> Longitude of the first point, ranges between -180 to 180</param>
        /// <param name="point2Latitude"> Latitude of the second point, ranges between -90 to 90</param>
        /// <param name="point2Longitude"> Longitude of the second point, ranges between -180 to 180</param>
        /// <returns>
        /// DistanceResponse containing the distance value calculated.
        /// </returns>
        /// <response code="200">Distance successfully calculated.</response>
        /// <response code="400">Latitude or longitude of any of the given points is out of range.</response>
        /// <response code="500">Unexpected server exception.</response>

        [HttpGet]
        [Route("{distanceMethod}/{measureUnit}")]
        public ActionResult<DistanceResponse> Get([EnumDataType(typeof(DistanceMethod))] DistanceMethod distanceMethod, [EnumDataType(typeof(MeasureUnit))] MeasureUnit measureUnit,
            [FromQuery][Required][Range(-90, 90)] double point1Latitude, [FromQuery][Required][Range(-180, 180)] double point1Longitude,
            [FromQuery][Required][Range(-90, 90)] double point2Latitude, [FromQuery][Required][Range(-180, 180)] double point2Longitude)
        {
            //Validates the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Initialises the distance point values.
            var point1 = new DistancePoint(point1Latitude, point1Longitude);
            var point2 = new DistancePoint(point2Latitude, point2Longitude);


            //Gets the type of distance calculation method service from the factory
            var distanceCalculationMethod = _distanceCalculatorFactory.GetDistanceCalculationStrategy(distanceMethod);

            //Returns the response
            return new ActionResult<DistanceResponse>(new DistanceResponse
            {
                Distance = distanceCalculationMethod.CalculateDistance(point1, point2, measureUnit)
            });

        }


    }
}
