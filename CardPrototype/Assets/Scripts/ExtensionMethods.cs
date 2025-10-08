using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static T Pop<T>(this List<T> list)
    {
        var r = list[0];
        list.RemoveAt(0);
        return r;
    }
}
