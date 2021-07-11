using System;
using System.Text.RegularExpressions;

namespace ShapeDrawer.Models
{
    public class Oval : IShape
    {
        public int Height { get; set; }
        public int Width { get; set; }

        public string Type { get { return typeof(Oval).Name; } }

        public Oval(string measurements)
        {
            var heightPortion = Regex.Match(measurements, "height of [0-9]*");
            this.Height = Int32.Parse(Regex.Match(heightPortion.Value, "[0-9]+").Value);

            var widthPortion = Regex.Match(measurements, "width of [0-9]*");
            this.Width = Int32.Parse(Regex.Match(widthPortion.Value, "[0-9]+").Value);
        }

        public Oval() { }
    }
}
