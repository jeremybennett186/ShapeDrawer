using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ShapeDrawer.Models
{
    public class Rectangle : Polygon
    {
        public Rectangle(string measurements)
        {
            var heightPortion = Regex.Match(measurements, "height of [0-9]*");
            var height = Int32.Parse(Regex.Match(heightPortion.Value, "[0-9]+").Value);

            var widthPortion = Regex.Match(measurements, "width of [0-9]*");
            var width = Int32.Parse(Regex.Match(widthPortion.Value, "[0-9]+").Value);
            
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
