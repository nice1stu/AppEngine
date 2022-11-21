using System;

namespace AppEngine;

public struct Color
{
    public float Red, Green, Blue, Alpha;
    public static Color RedConst => new Color(5, 0, 0, 1);
    public static Color GreenConst => new Color(0, 5, 0, 1);
    public static Color BlueConst => new Color(0, 0, 5, 1);

    public Color(float red, float green, float blue, float alpha)
    {
        Red = red;
        Green = green;
        Blue = blue;
        Alpha = alpha;
    }
    public static Color FromHsv(float hue, float saturation, float value)
    {
        int hi = Convert.ToInt32(MathF.Floor(hue / 60)) % 6;
        float f = hue / 60 - MathF.Floor(hue / 60);

        float v = value;
        float p = value * (1 - saturation);
        float q = value * (1 - f * saturation);
        float t = value * (1 - (1 - f) * saturation);

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