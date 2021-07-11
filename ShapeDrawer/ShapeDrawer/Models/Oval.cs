using ShapeDrawer.Helpers;

namespace ShapeDrawer.Models
{
    public class Oval : IShape
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public string Type { get { return typeof(Oval).Name; } }

        public Oval(string measurements)
        {

            this.Height = ShapeHelper.ParseMeasurementParameter(measurements, "height");

            this.Width = ShapeHelper.ParseMeasurementParameter(measurements, "width");
        }

        public Oval() { }
    }
}
