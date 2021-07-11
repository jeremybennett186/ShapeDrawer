using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ShapeDrawer.Models
{
    public class Parallelogram : Polygon
    {
        public Parallelogram(string measurements)
        {
            var topPortion = Regex.Match(measurements, "top side length of [0-9]*");
            var topSideLength = Int32.Parse(Regex.Match(topPortion.Value, "[0-9]+").Value);

            var diagonalPortion = Regex.Match(measurements, "diagonal side length of [0-9]*");
            var diagonalSideLength = Int32.Parse(Regex.Match(diagonalPortion.Value, "[0-9]+").Value);

            var topLeftAnglePortion = Regex.Match(measurements, "top left corner angle of [0-9]* degrees");
            var topLeftAngle = Int32.Parse(Regex.Match(topLeftAnglePortion.Value, "[0-9]+").Value);

            if (topLeftAngle >= 180)
                throw new ArgumentException("Angle cannot be greater than 180 degrees.");

            var height = Convert.ToInt32(diagonalSideLength * Math.Sin(Math.PI * topLeftAngle / 180));
            var extraLength = Convert.ToInt32(Math.Sqrt(Math.Pow(diagonalSideLength, 2) - Math.Pow(height, 2)));

            if (topLeftAngle <= 90)
            {
                this.Coordinates = new List<Coordinate>() {
                    new Coordinate(0, 0),
                    new Coordinate(topSideLength, 0),
                    new Coordinate(topSideLength + extraLength, height),
                    new Coordinate(extraLength, height),
                    new Coordinate(0, 0)
                };
            }
            else
            {
                this.Coordinates = new List<Coordinate>() {
                    new Coordinate(0, height),
                    new Coordinate(topSideLength, height),
                    new Coordinate(topSideLength + extraLength, 0),
                    new Coordinate(extraLength, 0),
                    new Coordinate(0, height),
                };
            }
        }
    }
}
