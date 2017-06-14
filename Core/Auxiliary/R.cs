using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class R
    {
        private static Random rgen = new Random();
        public static T GetRandom<T>(this IList<T> list)
        {
            return list[rgen.Next(0, list.Count)];
        }
        public static IEnumerable<T> GetRandoms<T>(this IList<T> list, int count)
        {
            List<int> indices = new List<int>();
            for (int i =0 ;i < count; i++)
            {
                int r = rgen.Next(0, list.Count);
                if (indices.Contains(r))
                {
                    i--;
                    continue;
                }
                indices.Add(r);
            }
            foreach(var ind in indices)
            {
                yield return list[ind];
            }
        }
    }
}
