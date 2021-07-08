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
                case "isocelese triangle":
                    return DrawIsoceleseTriangle(commandMeasurements);
                case "square":
                    return DrawSquare(commandMeasurements);
                case "scalene triangle":
                    return DrawScaleneTriangle(commandMeasurements);
                case "parallelogram":
                    return DrawParallelogram(commandMeasurements);
                case "equilateral triangle":
                    return DrawEquilateralTriangle(commandMeasurements);
                case "pentagon":
                    return DrawPentagon(commandMeasurements);
                case "rectangle":
                    return DrawRectangle(commandMeasurements);
                case "hexagon":
                    return DrawHexagon(commandMeasurements);
                case "heptagon":
                    return DrawHeptagon(commandMeasurements);
                case "optagon":
                    return DrawOptagon(commandMeasurements);
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
            throw new NotImplementedException();
        }

        private Shape DrawIsoceleseTriangle(string commandMeasurements)
        {
            throw new NotImplementedException();
        }

        private Shape DrawSquare(string commandMeasurements)
        {
            var square = new Polygon();
            int sideLength = 0;
            if (commandMeasurements.Contains("side length of "))
            {
                string sideLengthString = Regex.Replace(commandMeasurements, @"[^\d]", "");
                sideLength = Int32.Parse(sideLengthString);
            }

            square.Coordinates = new List<Coordinate>()
            {
                new Coordinate(0, 0),
                new Coordinate(0, sideLength),
                new Coordinate(sideLength, sideLength),
                new Coordinate(sideLength, 0),
                new Coordinate(0, 0)
            };

            return square;
        }

        private Shape DrawScaleneTriangle(string commandMeasurements)
        {
            throw new NotImplementedException();
        }

        private Shape DrawParallelogram(string commandMeasurements)
        {
            throw new NotImplementedException();
        }

        private Shape DrawEquilateralTriangle(string commandMeasurements)
        {
            throw new NotImplementedException();
        }

        private Shape DrawPentagon(string commandMeasurements)
        {
            throw new NotImplementedException();
        }

        private Shape DrawRectangle(string commandMeasurements)
        {
            var rectangle = new Polygon();
            int sideLength = 0;
            if (commandMeasurements.Contains("length of "))
            {
                string sideLengthString = Regex.Replace(commandMeasurements, @"[^\d]", "");
                sideLength = Int32.Parse(sideLengthString);
            }

            rectangle.Coordinates = new List<Coordinate>()
            {
                new Coordinate(0, 0),
                new Coordinate(0, sideLength),
                new Coordinate(sideLength, sideLength),
                new Coordinate(sideLength, 0),
                new Coordinate(0, 0)
            };

            return rectangle;
        }

        private Shape DrawHexagon(string commandMeasurements)
        {
            throw new NotImplementedException();
        }

        private Shape DrawHeptagon(string commandMeasurements)
        {
            throw new NotImplementedException();
        }

        private Shape DrawOptagon(string commandMeasurements)
        {
            throw new NotImplementedException();
        }

        private Shape DrawOval(string commandMeasurements)
        {
            throw new NotImplementedException();
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
*/