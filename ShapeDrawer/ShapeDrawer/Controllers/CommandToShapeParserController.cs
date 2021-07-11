using Microsoft.AspNetCore.Mvc;
using ShapeDrawer.Models;
using System;
using System.Text.RegularExpressions;

namespace ShapeDrawer.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class CommandToShapeParserController : ControllerBase
    {
        [HttpGet("{command}")]
        public ActionResult<IShape> Get(string command)
        {
            try
            {
                // parse command
                command = command.ToLower();

                command = Regex.Replace(command, "^(draw a|draw an)\\s+", "");

                string shapeType = Regex.Match(command, "^.*(?=(\\ with))").Value;

                string shapeMeasurements = Regex.Replace(command, string.Format("^({0} with a|with an)\\s+", shapeType), "");

                if (String.IsNullOrWhiteSpace(command) || String.IsNullOrWhiteSpace(shapeType) || String.IsNullOrWhiteSpace(shapeMeasurements))
                    throw new ArgumentException("Unable to parse input string.");

                // get shape
                try
                {
                    switch (shapeType)
                    {
                        case "circle":
                            return new Circle(shapeMeasurements);
                        case "isosceles triangle":
                            return new IsoceleseTriangle(shapeMeasurements);
                        case "square":
                            return new Square(shapeMeasurements);
                        case "scalene triangle":
                            return new ScaleneTriangle(shapeMeasurements);
                        case "parallelogram":
                            return new Parallelogram(shapeMeasurements);
                        case "rectangle":
                            return new Rectangle(shapeMeasurements);
                        case "oval":
                            return new Oval(shapeMeasurements);
                        case "prism":
                            return new Prism(shapeMeasurements);
                        case "cube":
                            return new Cube(shapeMeasurements);
                        default:
                            if (RegularPolygon.RegularPolygons.ContainsKey(shapeType))
                                return new RegularPolygon(RegularPolygon.RegularPolygons[shapeType], shapeMeasurements);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException(String.Format("Unable to generate shape given the measurements. Error: {0}", ex.Message));
                }

                throw new NotSupportedException("Unsupported shape type.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse(ex.Message));
            }
        }
    }
}