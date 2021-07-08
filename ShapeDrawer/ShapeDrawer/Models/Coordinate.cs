using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeDrawer.Models
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}
