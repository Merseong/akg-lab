using System;
using System.Collections.Generic;
 
public static class ListExtensions
{
    private static Random rnd = new Random();
 
    public static T PickRandom<T>(this IList<T> source)
    {
        int randIndex = rnd.Next(source.Count);
        return source[randIndex];
    }

    public static void Shuffle<T>(this IList<T> values)
    {
        for (int i = values.Count - 1; i > 0; i--) {
            int k = rnd.Next(i + 1);
            T value = values[k];
            values[k] = values[i];
            values[i] = value;
        }
    }
}