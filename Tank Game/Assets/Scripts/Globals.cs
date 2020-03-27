using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static Color Invert(Color rgbColor)
    {
        return new Color(1.0f - rgbColor.r, 1.0f - rgbColor.g, 1.0f - rgbColor.b);
    }
}
