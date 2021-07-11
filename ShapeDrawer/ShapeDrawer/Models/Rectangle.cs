using ShapeDrawer.Helpers;
using System.Collections.Generic;

namespace ShapeDrawer.Models
{
    public class Rectangle : Polygon
    {
        public Rectangle(string measurements)
        {
            var height = ShapeHelper.ParseMeasurementParameter(measurements, "height");

            var width = ShapeHelper.ParseMeasurementParameter(measurements, "width");

            this.Coordinates = new List<Coordinate>()
            {
                new Coordinate(0, 0),
                new Coordinate(0, height),
                new Coordinate(width, height),
                new Coordinate(width, 0),
                new Coordinate(0, 0)
            };
        }
    }
}
