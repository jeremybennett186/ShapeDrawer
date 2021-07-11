using ShapeDrawer.Helpers;

namespace ShapeDrawer.Models
{
    public class Circle : Oval
    {
        public Circle(string measurements)
        {
            this.Width = this.Height = ShapeHelper.ParseMeasurementParameter(measurements, "radius");
        }
    }
}
