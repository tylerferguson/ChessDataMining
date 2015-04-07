using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessDataMining.Facts;

namespace ChessDataMining
{
    public class TimeControlCategoriser : ITimeControlCategoriser
    {
        private Dictionary<int, int[]> boundaries = new Dictionary<int, int[]>()
            {
                { 0, new int[2] { 6, 15}},
                { 60, new int[2] { 4, 13}},
                { 120, new int[2] { 2, 11}},
                { 180, new int[2] { 0, 9}},
                { 240, new int[2] { 0, 7}},
                { 300, new int[2] { 0, 5}},
                { 360, new int[2] { 0, 3}},
                { 420, new int[2] { 0, 1}}
            };

        public string Categorise(string time)
        {
            if (time == "-")
            {
                return "None";
            }
            var times = time.Split('+');
            var sideTime = Convert.ToInt32(times[0]);
            var increment = Convert.ToInt32(times[1]);

            if (sideTime < 0 || increment < 0)
            {
                throw new ArgumentException();
            }
            else if (sideTime > 420)
            {
                return "Classic";
            }

            var incrementBounds = boundaries[sideTime];
            string value;
            if (increment < incrementBounds[0])
            {
                value = "Bullet";
            }
            else if (increment <= incrementBounds[1])
            {
                value = "Blitz";
            }
            else
            {
                value = "Classic";
            }
            return value;
        }
    }
}
