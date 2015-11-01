using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamousQuoteQuiz.Common
{
    public class RandomGenerator
    {
        private static Random rng = new Random();

        public void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public int GetRandomIndex(ICollection sequence, int id)
        {
            var exclude = new HashSet<int> {(int) id};
            var range = Enumerable.Range(1, sequence.Count).Where(i => !exclude.Contains(i));
            int index = rng.Next(1, sequence.Count - exclude.Count);
            var newIndex = range.ElementAt(index);

            return newIndex;
        }
    }
}
