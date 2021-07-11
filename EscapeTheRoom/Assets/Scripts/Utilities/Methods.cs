using System.Collections.Generic;
using System;
using System.Security.Cryptography;

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

    /// <summary>
    /// Performs md5 hashing of string.
    /// </summary>
    public static class Hasher
    {
        /// <summary>
        /// Time reference point.
        /// </summary>
        private static readonly System.DateTime Jan1st1970 = new System.DateTime
            (1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);

        /// <summary>
        /// Generates md5 hash based on current time + guid object.
        /// </summary>
        /// <returns>String containing md5 hash.</returns>
        public static string GenerateHash()
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(
                System.Text.Encoding.Default.GetBytes(GenerateString())
            );

            return System.BitConverter.ToString(data);
        }

        /// <summary>
        /// Generates string based on guid object + current time in milliseconds.
        /// </summary>
        /// <returns>String containing random number.</returns>
        public static string GenerateString()
        {
            return System.Guid.NewGuid().ToString() + CurrentTimeMillis();
        }

        /// <summary>
        /// Gets current time in milliseconds.
        /// </summary>
        /// <returns>Current time in milliseconds.</returns>
        public static long CurrentTimeMillis()
        {
            return (long)(System.DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        /// <summary>
        /// Generate md5 hash as byte array.
        /// </summary>
        /// <returns>Byte array, represents md5 hash.</returns>
        public static byte[] GenerateHashArray()
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(
                System.Text.Encoding.Default.GetBytes(GenerateString())
            );

            return data;
        }

        /// <summary>
        /// Generates md5 hash based on input string.
        /// </summary>
        /// <param name="input">String, used as buffer.</param>
        /// <returns>String containing md5 hash.</returns>
        public static string GenerateHash(string input)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(
                System.Text.Encoding.Default.GetBytes(input)
            );

            return System.BitConverter.ToString(data);
        }

        /// <summary>
        /// Generates md5 hash as byte array, based on input string.
        /// </summary>
        /// <param name="input">String, used as buffer.</param>
        /// <returns>Byte array, represents md5 hash.</returns>
        public static byte[] GenerateHashArray(string input)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(
                System.Text.Encoding.Default.GetBytes(input)
            );

            return data;
        }

    }
}