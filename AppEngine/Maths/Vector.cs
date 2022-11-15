namespace Maths;

public struct Vector
{
    public float X, Y, Z;

    public static Vector Zero
    {
        get => new Vector(0, 0, 0);
    }

    public Vector(float x, float y, float z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }
}