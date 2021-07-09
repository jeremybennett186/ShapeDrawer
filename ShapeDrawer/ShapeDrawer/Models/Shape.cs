using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShapeDrawer.Models
{
    public abstract class Shape
    {
        public string Type { get { return this.GetType().Name; } }
    }
}
