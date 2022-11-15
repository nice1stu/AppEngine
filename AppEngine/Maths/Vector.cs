namespace Maths;

public struct Vector
{
    public int x, y, z;
    public Vector(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static Vector Zero
    {
        get => new Vector(0, 0, 0);
    }

    public static Vector Inverse
    {
        get => new Vector(1, 0, 0);
    }
}