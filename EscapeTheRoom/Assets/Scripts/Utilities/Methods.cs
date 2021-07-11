using System.Collections.Generic;
using System;


namespace EscapeTheRoom.Utilities.Methods
{
    public static class OwnList
    {
        public static void Shuffle<T>(this IList<T> list, int seed)
        {
            Random rng = new Random(seed);

            int n = list.Count;
            while (n > 1) {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

    public class UI
    {
        public static string GetFormattedTime(float time)
        {
            var milliseconds = (time * 1000) % 1000;

            var seconds = (int)time;
            var minutes = seconds / 60;
            seconds = (minutes == 0)? seconds : (seconds % (minutes * 60));

            return $"{minutes:00}:{seconds:00}:{milliseconds:000}";
        }
    }
}