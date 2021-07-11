using ShapeDrawer.Helpers;
using System;
using System.Collections.Generic;

namespace ShapeDrawer.Models
{
    public class ScaleneTriangle : Polygon
    {
        public ScaleneTriangle(string measurements)
        {
            var firstSideLength = ShapeHelper.ParseMeasurementParameter(measurements, "side length");

            var secondSideLength = ShapeHelper.ParseMeasurementParameter(measurements, "another side length");

            var firstSecondSideAngle = ShapeHelper.ParseMeasurementParameter(measurements, "angle between them", " degrees");

            if (firstSecondSideAngle >= 180)
                throw new ArgumentException("Angle cannot be greater than 180 degrees.");

            var thirdSideLength = Convert.ToInt32(Math.Sqrt(
                Math.Pow(secondSideLength, 2) + Math.Pow(firstSideLength, 2) - (2 * secondSideLength * firstSideLength * Math.Cos(Math.PI * firstSecondSideAngle / 180))
                ));

            // reorganise triangle so longest side is on the bottom
            double baseSide;
            double leftSide;
            double rightSide;

            double leftAngle;
            double rightAngle;
            double topAngle;
            double topVertexXCoordinate;
            double height;

            if (firstSideLength > secondSideLength && firstSideLength > thirdSideLength)
            {
                baseSide = firstSideLength;
                rightSide = secondSideLength;
                rightAngle = firstSecondSideAngle;

                height = rightSide * Math.Sin(Math.PI * rightAngle / 180);

                topVertexXCoordinate = baseSide - (rightSide * Math.Sin(Math.PI * (90 - rightAngle) / 180));
            }
            else if (secondSideLength > firstSideLength && secondSideLength > thirdSideLength)
            {
                baseSide = secondSideLength;
                leftSide = firstSideLength;
                leftAngle = firstSecondSideAngle;

                height = leftSide * Math.Sin(Math.PI * leftAngle / 180);

                topVertexXCoordinate = leftSide * Math.Sin(Math.PI * (90 - leftAngle) / 180);
            }
            else
            {
                baseSide = thirdSideLength;
                rightSide = firstSideLength;
                leftSide = secondSideLength;
                topAngle = firstSecondSideAngle;

                leftAngle = Math.Asin(rightSide * Math.Sin(Math.PI * topAngle / 180) / baseSide);
                height = leftSide * Math.Sin(leftAngle);

                topVertexXCoordinate = leftSide * Math.Sin((Math.PI / 2) - leftAngle);
            }

            this.Coordinates = new List<Coordinate>()
            {
                new Coordinate(Convert.ToInt32(topVertexXCoordinate), 0),
                new Coordinate(Convert.ToInt32(baseSide), Convert.ToInt32(height)),
                new Coordinate(0, Convert.ToInt32(height)),
                new Coordinate(Convert.ToInt32(topVertexXCoordinate), 0)
            };
        }
    }
}
