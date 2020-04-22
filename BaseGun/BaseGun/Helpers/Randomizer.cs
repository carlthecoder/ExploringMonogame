using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseGun.Helpers
{
    public static class Randomizer
    {
        private static readonly Random _rand = new Random();

        public static int GetRandomNumber(int minValue, int maxValue)
        {
            var value = _rand.Next(minValue, maxValue);

            return value;
        }
    }
}
