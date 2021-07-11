using ShapeDrawer.Helpers;

namespace ShapeDrawer.Models
{
    public class Prism : IShape
    {
        public int Width { get; set; }
        public int Length { get; set; }
        public int Height { get; set; }

        public Prism(string measurements)
        {
            this.Height = ShapeHelper.ParseMeasurementParameter(measurements, "height");

            this.Width = ShapeHelper.ParseMeasurementParameter(measurements, "width");

            this.Length = ShapeHelper.ParseMeasurementParameter(measurements, "length");
        }

        public Prism() { }

        public string Type { get { return typeof(Prism).Name; } }
    }
}
