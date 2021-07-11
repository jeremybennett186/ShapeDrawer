using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ShapeDrawer.Models
{
    public class RegularPolygon : Polygon
    {
        public static Dictionary<string, int> SupportedRegularPolygons = new Dictionary<string, int>() {
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

        public RegularPolygon (int numSides, string measurements)
        {
            var sideLengthPortion = Regex.Match(measurements, "side length of [0-9]*");
            int sideLength = Int32.Parse(Regex.Match(sideLengthPortion.Value, "[0-9]+").Value);

            this.Coordinates = GetRegularPolygonCoordinates(numSides, sideLength);
        }

        public static List<Coordinate> GetRegularPolygonCoordinates(int numSides, int sidelength)
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
        public static List<Coordinate> AlignCoordinatesWithCanvas(List<Coordinate> coordinates)
        {
            var minX = coordinates.Min(c => c.X);
            var minY = coordinates.Min(c => c.Y);
            if (minX > 0)
                coordinates = coordinates.Select(c => { c.X = c.X - minX; return c; }).ToList();
            if (minY > 0)
                coordinates = coordinates.Select(c => { c.Y = c.Y - minY; return c; }).ToList();
            return coordinates;
        }
    }
}
