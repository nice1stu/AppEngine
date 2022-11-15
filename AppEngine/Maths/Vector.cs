namespace Maths;

public struct Vector
{
    public float X, Y, Z;


    public Vector(float x, float y, float z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }

    private static Vector Zero { get; }
    
    private static Vector Inverse { get; }

    public float MultiplyWith()
    {
        return;
    }
    
}