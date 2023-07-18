using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static T GrabRandom<T>(this IList<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    public static T GrabRandom<T>(this T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
}
