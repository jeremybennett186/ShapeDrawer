using System;
using System.Text.RegularExpressions;

namespace ShapeDrawer.Helpers
{
    public static class ShapeHelper
    {
        // read a measurement from string, i.e. extract height amount of 250 from "a width of 200 and a height of 250"
        // unitName can optionally be used to enforce unit name at end of input string
        public static int ParseMeasurementParameter(string input, string measurementName, string unitName = "")
        {
            var match = Regex.Match(input, String.Format("{0} of [0-9]*{1}", measurementName, unitName));

            if (!match.Success)
                throw new ArgumentException(String.Format("Unable to read {0}.", measurementName));

            return Int32.Parse(Regex.Match(match.Value, "[0-9]+").Value);
        }
    }
}
