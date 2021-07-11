using System;
using System.Text.RegularExpressions;

namespace ShapeDrawer.Models
{
    public class Circle : Oval
    {
        public Circle(string measurements)
        {
            var radiusPortion = Regex.Match(measurements, "radius of [0-9]*");
            this.Width = this.Height = Int32.Parse(Regex.Match(radiusPortion.Value, "[0-9]+").Value) * 2;
        }
    }
}
