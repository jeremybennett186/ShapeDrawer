using ShapeDrawer.Helpers;
using System.Collections.Generic;

namespace ShapeDrawer.Models
{
    public class Square : Polygon
    {
        // regular polygon function is not used for squares because it draws the square rotated by 45 degrees, which is correct but looks weird
        public Square(string measurements)
        {
            int sideLength = ShapeHelper.ParseMeasurementParameter(measurements, "side length");

            this.Coordinates = new List<Coordinate>()
            {
                new Coordinate(0, 0),
                new Coordinate(0, sideLength),
                new Coordinate(sideLength, sideLength),
                new Coordinate(sideLength, 0),
                new Coordinate(0, 0)
            };
        }
    } 
}
