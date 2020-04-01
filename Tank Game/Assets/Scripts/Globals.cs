using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals
{
    public static Vector2 resolution;

    public static Color Invert(Color rgbColor)
    {
        return new Color(1.0f - rgbColor.r, 1.0f - rgbColor.g, 1.0f - rgbColor.b);
    }

    public static float GetScale(int width, int height, Vector2 scalerReferenceResolution, float scalerMatchWidthOrHeight)
    {
        return Mathf.Pow(width / scalerReferenceResolution.x, 1f - scalerMatchWidthOrHeight) *
               Mathf.Pow(height / scalerReferenceResolution.y, scalerMatchWidthOrHeight);
    }
}
