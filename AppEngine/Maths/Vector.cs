namespace Maths;

public struct Vector
{
    public float x, y, z;
    
    public Vector(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public static Vector Zero
    {
        get { return new Vector(0, 0, 0); }
    }

    public Vector Inverse()
    {
        return new(-x, -y, -z);
    }

    public Vector MultiplyWith(float k)
    {
        return new( x,  y,  z);
    }
    
    public Vector DivideBy(float d)
    {
        return new( x,  y,  z);
    }

    public Vector Add(Vector movement)
    {
        return new Vector(x, y, z);
    }
    
    public Vector Subtract(Vector playerPosition)
    {
        return new Vector(x, y, z);
    }
    public static Vector Magnitude
    {
        get { return Magnitude; }
    }

    public static Vector SquareMagnitude
    {
        get { return SquareMagnitude; }
    }
}