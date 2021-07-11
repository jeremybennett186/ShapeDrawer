using ShapeDrawer.Helpers;
using System;
using System.Collections.Generic;

namespace ShapeDrawer.Models
{
    public class Parallelogram : Polygon
    {
        public Parallelogram(string measurements)
        {
            var topSideLength = ShapeHelper.ParseMeasurementParameter(measurements, "top side length");

            var diagonalSideLength = ShapeHelper.ParseMeasurementParameter(measurements, "diagonal side length");

            var topLeftAngle = ShapeHelper.ParseMeasurementParameter(measurements, "top left corner angle", " degrees");

            if (topLeftAngle >= 180)
                throw new ArgumentException("Top left angle cannot be greater than 180 degrees.");

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
