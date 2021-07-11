using ShapeDrawer.Helpers;
using System.Collections.Generic;

namespace ShapeDrawer.Models
{
    public class IsoceleseTriangle : Polygon
    {
        public IsoceleseTriangle(string measurements)
        {

            var height = ShapeHelper.ParseMeasurementParameter(measurements, "height");

            var width = ShapeHelper.ParseMeasurementParameter(measurements, "width");

            this.Coordinates = new List<Coordinate>()
            {
                new Coordinate(width / 2, 0),
                new Coordinate(0, height),
                new Coordinate(width, height),
                new Coordinate(width / 2, 0)
            };
        }

        public new string Type { get { return this.GetType().BaseType.Name; } }
    }
}
