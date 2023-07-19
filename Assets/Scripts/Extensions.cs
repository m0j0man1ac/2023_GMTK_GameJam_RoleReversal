using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using DG.Tweening;

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

    public static void MyDOColor(this TMP_Text _obj, Color toColor, float duration, Ease ease)
    {
        //var color = _obj.color;
        //var alpha = _obj.color.a;
        //color.a = 0;

        DOVirtual.Color(_obj.color, toColor, duration, (value) => { _obj.color = value; }).SetEase(ease);
    }

    public static void MyDOColorPunch(this TMP_Text _obj, Color toColor, float duration, Ease inEase, Ease outEase)
    {
        var ogColor = _obj.color;
        DOVirtual.Color(_obj.color, toColor, duration*.5f, (value) => { _obj.color = value; }).SetEase(inEase)
            .OnComplete(() => {_obj.MyDOColor(ogColor, duration*.5f, outEase); });
    }
}
