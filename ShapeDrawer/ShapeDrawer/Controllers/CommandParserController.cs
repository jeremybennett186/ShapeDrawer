using Microsoft.AspNetCore.Mvc;
using ShapeDrawer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
                case "equilateral triangle":
                    return DrawRegularPolygon(commandMeasurements, 3);
                case "pentagon":
                    return DrawRegularPolygon(commandMeasurements, 5);
                case "rectangle":
                    return DrawRectangle(commandMeasurements);
                case "hexagon":
                    return DrawRegularPolygon(commandMeasurements, 6);
                case "heptagon":
                    return DrawRegularPolygon(commandMeasurements, 7);
                case "octagon":
                    return DrawRegularPolygon(commandMeasurements, 8);
                case "oval":
                    return DrawOval(commandMeasurements);
                case "cube":
                    return DrawCube(commandMeasurements);
            }
            throw new Exception ("Unsupported shape type");
        }

        // use polymorphism for shapes
        private Shape DrawCircle(string commandMeasurements)
        {
            var circle = new Oval();

            var radiusPortion = Regex.Matches(commandMeasurements, "radius of [0-9]*");
            circle.Width = circle.Height = Int32.Parse(Regex.Matches(radiusPortion.FirstOrDefault().Value, "[0-9]+").FirstOrDefault().Value) * 2;

            return circle;
        }

        private Shape DrawIsoceleseTriangle(string commandMeasurements)
        {
            var heightPortion = Regex.Matches(commandMeasurements, "height of [0-9]*");
            var height = Int32.Parse(Regex.Matches(heightPortion.FirstOrDefault().Value, "[0-9]+").FirstOrDefault().Value);

            var widthPortion = Regex.Matches(commandMeasurements, "width of [0-9]*");
            var width = Int32.Parse(Regex.Matches(widthPortion.FirstOrDefault().Value, "[0-9]+").FirstOrDefault().Value);

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
            var sideLengthPortion = Regex.Matches(commandMeasurements, "side length of [0-9]*");
            int sideLength = Int32.Parse(Regex.Matches(sideLengthPortion.FirstOrDefault().Value, "[0-9]+").FirstOrDefault().Value);

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
            // base, height, small angle
            throw new NotImplementedException();
        }

        private Shape DrawParallelogram(string commandMeasurements)
        {
            // base, height, small angle
            throw new NotImplementedException();
        }

        private Shape DrawRectangle(string commandMeasurements)
        {
            var rectangle = new Polygon();

            var heightPortion = Regex.Matches(commandMeasurements, "height of [0-9]*");
            var height = Int32.Parse(Regex.Matches(heightPortion.FirstOrDefault().Value, "[0-9]+").FirstOrDefault().Value);

            var widthPortion = Regex.Matches(commandMeasurements, "width of [0-9]*");
            var width = Int32.Parse(Regex.Matches(widthPortion.FirstOrDefault().Value, "[0-9]+").FirstOrDefault().Value);

            
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
            var sideLengthPortion = Regex.Matches(commandMeasurements, "side length of [0-9]*");
            int sideLength = Int32.Parse(Regex.Matches(sideLengthPortion.FirstOrDefault().Value, "[0-9]+").FirstOrDefault().Value);

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

            // ensure shape is flush with canvas
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

            var heightPortion = Regex.Matches(commandMeasurements, "height of [0-9]*");
            oval.Height = Int32.Parse(Regex.Matches(heightPortion.FirstOrDefault().Value, "[0-9]+").FirstOrDefault().Value);

            var widthPortion = Regex.Matches(commandMeasurements, "width of [0-9]*");
            oval.Width = Int32.Parse(Regex.Matches(widthPortion.FirstOrDefault().Value, "[0-9]+").FirstOrDefault().Value);

            return oval;
        }

        private Shape DrawCube(string sideLength)
        {
            throw new NotImplementedException();
        }
    }
}


/*
 * if (command == "draw an oval") { 
    return new Oval()
    {
        Height = 100,
        Width = 300
    };
}
else
{
    return new Polygon()
    {
        Coordinates = new List<Coordinate>()
        {
            new Coordinate()
            {
                X = 0,
                Y = 0
            },
            new Coordinate()
            {
                X = 100, Y = 0
            },
            new Coordinate()
            {
                X = 100, Y = 100
            },
            new Coordinate()
            {
                X = 0, Y = 100
            },
            new Coordinate()
            {
                X = 0, Y = 0
            }
        }
    };
}





            /*var square = new Polygon();
            int sideLength = 0;
            if (commandMeasurements.Contains("side length of "))
            {
                string sideLengthString = Regex.Replace(commandMeasurements, @"[^\d]", "");
                sideLength = Int32.Parse(sideLengthString);
            }
*/