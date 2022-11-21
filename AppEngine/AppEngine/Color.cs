using System;

namespace AppEngine;

public struct Color
{
    public float Red, Green, Blue, Alpha;

    public Color(float red, float green, float blue, float alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }
    public static Color FromHSV(float hue, float saturation, float value)
    {
        int hi = Convert.ToInt32(MathF.Floor(hue / 60)) % 6;
        float f = hue / 60 - MathF.Floor(hue / 60);

        int v = Convert.ToInt32(value);
        int p = Convert.ToInt32(value * (1 - saturation));
        int q = Convert.ToInt32(value * (1 - f * saturation));
        int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

        return hi switch
        {
            0 => new Color(v, t, p, 1),
            1 => new Color(q, v, p, 1),
            2 => new Color(p, v, t, 1),
            3 => new Color(p, q, v, 1),
            4 => new Color(t, p, v, 1),
            _ => new Color(v, p, q, 1)
        };
    }
}