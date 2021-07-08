using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeDrawer.Models
{
    public class Cube : Shape
    {
        public List<Coordinate> Coordinates { get; set; }

        public List<Coordinate> GetCoordinates()
        {
            return new List<Coordinate>();
        }
    }
}
