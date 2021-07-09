using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeDrawer.Models
{
    public class Polygon : Shape
    {
        public List<Coordinate> Coordinates { get; set; }
    }
}
