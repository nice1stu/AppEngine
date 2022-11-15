namespace Maths;

public struct Vector
{
    public float X = 5, Y = .23f, Z = -2.5f;

    Vector right = new Vector(1f, 0f, 0f);

    public Vector(float x, float y, float z) : this()
    {
        X = x;
        Y = y;
        Z = z;
    }
}