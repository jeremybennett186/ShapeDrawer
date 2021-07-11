using System;
using System.Text.RegularExpressions;

namespace ShapeDrawer.Models
{
    public class Cube : Prism
    {
        public Cube(string measurements)
        {
            var sideLengthPortion = Regex.Match(measurements, "side length of [0-9]*");
            this.Width = this.Height = this.Length = Int32.Parse(Regex.Match(sideLengthPortion.Value, "[0-9]+").Value);
        }
    }
}
