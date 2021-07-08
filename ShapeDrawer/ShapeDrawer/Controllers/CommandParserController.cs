﻿using Microsoft.AspNetCore.Mvc;
using ShapeDrawer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ShapeDrawer.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class CommandParserController : ControllerBase
    {
        static Dictionary<string, int> RegularPolygons = new Dictionary<string, int>() {
            { "equilateral triangle", 3 },
            { "pentagon", 5 },
            { "hexagon", 6 },
            { "heptagon", 7 },
            { "octagon", 8 },
            { "nonagon", 9 },
            { "decagon", 10 },
            { "hendecagon", 11 },
            { "dodecagon", 12 }
        };

        [HttpGet("{command}")]
        public Shape Get(string command)
        {
            command = command.ToLower();

            if (command.StartsWith("draw an "))
                command = command.Substring(8, command.Length - 8);
            else if (command.StartsWith("draw a "))
                command = command.Substring(7, command.Length - 7);

            string shapeType = command.Substring(0, command.IndexOf("with") - 1);

            string commandMeasurements = command.TrimStart(shapeType.ToCharArray());

            if (commandMeasurements.StartsWith(" with an "))
                commandMeasurements = commandMeasurements.Substring(9, commandMeasurements.Length - 9);
            else if (commandMeasurements.StartsWith(" with a "))
                commandMeasurements = commandMeasurements.Substring(8, commandMeasurements.Length - 8);


            switch (shapeType) {
                case "circle":
                    return DrawCircle(commandMeasurements);
                case "isosceles triangle":
                    return DrawIsoceleseTriangle(commandMeasurements);
                case "square":
                    return DrawSquare(commandMeasurements);
                case "scalene triangle":
                    return DrawScaleneTriangle(commandMeasurements);
                case "parallelogram":
                    return DrawParallelogram(commandMeasurements);
                case "rectangle":
                    return DrawRectangle(commandMeasurements);
                case "oval":
                    return DrawOval(commandMeasurements);
                case "cube":
                    return DrawCube(commandMeasurements);
                default:
                    if (RegularPolygons.ContainsKey(shapeType))
                        return DrawRegularPolygon(commandMeasurements, RegularPolygons[shapeType]);
                    break;
            }
            throw new Exception ("Unsupported shape type");
        }

        private Shape DrawCircle(string commandMeasurements)
        {
            var circle = new Oval();

            var radiusPortion = Regex.Match(commandMeasurements, "radius of [0-9]*");
            circle.Width = circle.Height = Int32.Parse(Regex.Match(radiusPortion.Value, "[0-9]+").Value) * 2;

            return circle;
        }

        private Shape DrawIsoceleseTriangle(string commandMeasurements)
        {
            var heightPortion = Regex.Match(commandMeasurements, "height of [0-9]*");
            var height = Int32.Parse(Regex.Match(heightPortion.Value, "[0-9]+").Value);

            var widthPortion = Regex.Match(commandMeasurements, "width of [0-9]*");
            var width = Int32.Parse(Regex.Match(widthPortion.Value, "[0-9]+").Value);

            return new Polygon()
            {
                Coordinates = new List<Coordinate>()
                {
                    new Coordinate(width / 2, 0),
                    new Coordinate(0, height),
                    new Coordinate(width, height),
                    new Coordinate(width / 2, 0)
                }
            };
        }

        // Don't use regular polygon function because it rotates the square (which is still correct but looks weird)
        private Shape DrawSquare(string commandMeasurements)
        {
            var sideLengthPortion = Regex.Match(commandMeasurements, "side length of [0-9]*");
            int sideLength = Int32.Parse(Regex.Match(sideLengthPortion.Value, "[0-9]+").Value);

            return new Polygon()
            {
                Coordinates = new List<Coordinate>()
                {
                    new Coordinate(0, 0),
                    new Coordinate(0, sideLength),
                    new Coordinate(sideLength, sideLength),
                    new Coordinate(sideLength, 0),
                    new Coordinate(0, 0)
                }
            };
        }

        private Shape DrawScaleneTriangle(string commandMeasurements)
        {
            var firstSidePortion = Regex.Match(commandMeasurements, "side length of [0-9]*");
            var firstSideLength = Int32.Parse(Regex.Match(firstSidePortion.Value, "[0-9]+").Value);

            var secondSidePortion = Regex.Match(commandMeasurements, "another side length of [0-9]*");
            var secondSideLength = Int32.Parse(Regex.Match(secondSidePortion.Value, "[0-9]+").Value);

            var firstSecondSideAnglePortion = Regex.Match(commandMeasurements, "angle between them of [0-9]*");
            var firstSecondSideAngle = Int32.Parse(Regex.Match(firstSecondSideAnglePortion.Value, "[0-9]+").Value);

            var thirdSideLength = Math.Sqrt(
                Math.Pow(secondSideLength, 2) + Math.Pow(firstSideLength, 2) - (2 * secondSideLength * firstSideLength * Math.Cos(Math.PI * firstSecondSideAngle / 180))
                );

            /*
            var bottomLeftAngle = Math.Acos((Math.Pow(firstSideLength, 2) + Math.Pow(thirdSideLength, 2) - Math.Pow(secondSideLength, 2)) / 2 * thirdSideLength);

            var startXPosition = thirdSideLength * Math.Sin(Math.PI / 4 - bottomLeftAngle);

            var height = Convert.ToInt32(Math.Sqrt(Math.Pow(thirdSideLength, 2) - Math.Pow(startXPosition, 2)));*/

            return new Polygon()
            {
                Coordinates = new List<Coordinate>()
                {
                    new Coordinate(Convert.ToInt32(startXPosition), 0),
                    new Coordinate(secondSideLength, height),
                    new Coordinate(0, height),
                    new Coordinate(Convert.ToInt32(startXPosition), 0)
                }
            };
        }

        private Shape DrawParallelogram(string commandMeasurements)
        {
            var topPortion = Regex.Match(commandMeasurements, "top side length of [0-9]*");
            var topSideLength = Int32.Parse(Regex.Match(topPortion.Value, "[0-9]+").Value);

            var diagonalPortion = Regex.Match(commandMeasurements, "diagonal side length of [0-9]*");
            var diagonalSideLength = Int32.Parse(Regex.Match(diagonalPortion.Value, "[0-9]+").Value);

            var topLeftAnglePortion = Regex.Match(commandMeasurements, "top left corner angle of [0-9]* degrees");
            var topLeftAngle = Int32.Parse(Regex.Match(topLeftAnglePortion.Value, "[0-9]+").Value);

            var height = Convert.ToInt32(diagonalSideLength * Math.Sin(Math.PI * topLeftAngle / 180));
            var extraLength = Convert.ToInt32(Math.Sqrt(Math.Pow(diagonalSideLength, 2) - Math.Pow(height, 2)));

            List<Coordinate> coordinates;

            if (topLeftAngle <= 90)
            {
                coordinates = new List<Coordinate>() {
                    new Coordinate(0, 0),
                    new Coordinate(topSideLength, 0),
                    new Coordinate(topSideLength + extraLength, height),
                    new Coordinate(extraLength, height),
                    new Coordinate(0, 0)
                };
            }
            else
            {
                coordinates = new List<Coordinate>() {
                    new Coordinate(0, height),
                    new Coordinate(topSideLength, height),
                    new Coordinate(topSideLength + extraLength, 0),
                    new Coordinate(extraLength, 0),
                    new Coordinate(0, height),
                };
            }

            return new Polygon()
            {
                Coordinates = coordinates
            };
        }

        private Shape DrawRectangle(string commandMeasurements)
        {
            var rectangle = new Polygon();

            var heightPortion = Regex.Match(commandMeasurements, "height of [0-9]*");
            var height = Int32.Parse(Regex.Match(heightPortion.Value, "[0-9]+").Value);

            var widthPortion = Regex.Match(commandMeasurements, "width of [0-9]*");
            var width = Int32.Parse(Regex.Match(widthPortion.Value, "[0-9]+").Value);

            
            rectangle.Coordinates = new List<Coordinate>()
            {
                new Coordinate(0, 0),
                new Coordinate(0, height),
                new Coordinate(width, height),
                new Coordinate(width, 0),
                new Coordinate(0, 0)
            };

            return rectangle;
        }

        private Polygon DrawRegularPolygon(string commandMeasurements, int numSides)
        {
            var sideLengthPortion = Regex.Match(commandMeasurements, "side length of [0-9]*");
            int sideLength = Int32.Parse(Regex.Match(sideLengthPortion.Value, "[0-9]+").Value);

            var shape = new Polygon()
            {
                Coordinates = GetEquilateralPolygonCoordinates(numSides, sideLength)
            };

            return shape;
        }

        private List<Coordinate> GetEquilateralPolygonCoordinates(int numSides, int sidelength)
        {
            var coordinates = new List<Coordinate>();

            // get radius of the circle that encompasses the polygon
            var radius = sidelength / (2 * Math.Sin(Math.PI / numSides));

            // get shape coordinates
            for (int i = 0; i < numSides; i++)
            {
                coordinates.Add(new Coordinate(
                    Convert.ToInt32(radius + radius * Math.Cos(2 * Math.PI * i / numSides)),
                    Convert.ToInt32(radius + radius * Math.Sin(2 * Math.PI * i / numSides))));
            }

            coordinates = AlignCoordinatesWithCanvas(coordinates);

            return coordinates;
        }

        // ensure shape is flush with canvas
        private static List<Coordinate> AlignCoordinatesWithCanvas(List<Coordinate> coordinates)
        {
            var minX = coordinates.Min(c => c.X);
            var minY = coordinates.Min(c => c.Y);
            if (minX > 0)
                coordinates = coordinates.Select(c => { c.X = c.X - minX; return c; }).ToList();
            if (minY > 0)
                coordinates = coordinates.Select(c => { c.Y = c.Y - minY; return c; }).ToList();
            return coordinates;
        }

        private Shape DrawOval(string commandMeasurements)
        {
            var oval = new Oval();

            var heightPortion = Regex.Match(commandMeasurements, "height of [0-9]*");
            oval.Height = Int32.Parse(Regex.Match(heightPortion.Value, "[0-9]+").Value);

            var widthPortion = Regex.Match(commandMeasurements, "width of [0-9]*");
            oval.Width = Int32.Parse(Regex.Match(widthPortion.Value, "[0-9]+").Value);

            return oval;
        }

        private Shape DrawCube(string sideLength)
        {
            throw new NotImplementedException();
        }
    }
}