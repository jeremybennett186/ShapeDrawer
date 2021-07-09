using Microsoft.AspNetCore.Mvc;
using ShapeDrawer.Helpers;
using ShapeDrawer.Models;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ShapeDrawer.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class CommandToShapeParserController : ControllerBase
    {
        [HttpGet("{command}")]
        public ActionResult<Shape> Get(string command)
        {
            try
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


                switch (shapeType)
                {
                    case "circle":
                        return ShapeHelper.DrawCircle(commandMeasurements);
                    case "isosceles triangle":
                        return ShapeHelper.DrawIsoceleseTriangle(commandMeasurements);
                    case "square":
                        return ShapeHelper.DrawSquare(commandMeasurements);
                    case "scalene triangle":
                        return ShapeHelper.DrawScaleneTriangle(commandMeasurements);
                    case "parallelogram":
                        return ShapeHelper.DrawParallelogram(commandMeasurements);
                    case "rectangle":
                        return ShapeHelper.DrawRectangle(commandMeasurements);
                    case "oval":
                        return ShapeHelper.DrawOval(commandMeasurements);
                    case "cube":
                        return ShapeHelper.DrawCube(commandMeasurements);
                    default:
                        if (ShapeHelper.RegularPolygons.ContainsKey(shapeType))
                            return ShapeHelper.DrawRegularPolygon(commandMeasurements, ShapeHelper.RegularPolygons[shapeType]);
                        break;
                }

                throw new Exception("Unsupported shape type");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ErrorResponse(ex.Message));
            }
        }
    }
}