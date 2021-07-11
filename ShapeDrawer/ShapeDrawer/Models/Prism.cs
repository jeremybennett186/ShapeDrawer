using System;
using System.Text.RegularExpressions;

namespace ShapeDrawer.Models
{
    public class Prism : IShape
    {
        public int Width { get; set; }
        public int Length { get; set; }
        public int Height { get; set; }

        public Prism(string measurements)
        {
            var heightPortion = Regex.Match(measurements, "height of [0-9]*");
            this.Height = Int32.Parse(Regex.Match(heightPortion.Value, "[0-9]+").Value);

            var widthPortion = Regex.Match(measurements, "width of [0-9]*");
            this.Width = Int32.Parse(Regex.Match(widthPortion.Value, "[0-9]+").Value);

            var lengthPortion = Regex.Match(measurements, "length of [0-9]*");
            this.Length = Int32.Parse(Regex.Match(lengthPortion.Value, "[0-9]+").Value);
        }

        public Prism() { }

        public string Type { get { return typeof(Prism).Name; } }
    }
}
