using System.Collections.Generic;

namespace ShapeDrawer.Models
{
    public class Polygon : IShape
    {
        public List<Coordinate> Coordinates { get; set; }

        public string Type { get { return typeof(Polygon).Name; } }
    }
}
