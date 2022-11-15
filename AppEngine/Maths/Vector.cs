namespace Maths;

public struct Vector
{
    public float x, y, z;

    public static Vector Zero => new Vector(0, 0, 0);

    public Vector Inverse()
    {
        return new(-x, -y, -z);
    }

    public Vector(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }
}