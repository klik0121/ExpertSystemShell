using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FuzzyLogicRedactor
{
    public static class RandomColor
    {
        private static KnownColor[] colorNames = (KnownColor[])Enum.GetValues(typeof(KnownColor));
        private static Random rnd = new Random();

        public static Color Get()
        {
            Color result;
            lock(rnd)
            {
                result = Color.FromKnownColor(colorNames[rnd.Next(colorNames.Length)]);
            }
            return result;
        }
    }
}
