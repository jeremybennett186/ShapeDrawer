using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ShapeDrawer.Models
{
    public class Square : Polygon
    {
        public Square(string measurements)
        {
            var sideLengthPortion = Regex.Match(measurements, "side length of [0-9]*");
            int sideLength = Int32.Parse(Regex.Match(sideLengthPortion.Value, "[0-9]+").Value);

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
