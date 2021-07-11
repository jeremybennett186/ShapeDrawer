using ShapeDrawer.Helpers;

namespace ShapeDrawer.Models
{
    public class Cube : Prism
    {
        public Cube(string measurements)
        {
            this.Width = this.Height = this.Length = ShapeHelper.ParseMeasurementParameter(measurements, "side length");
        }
    }
}
