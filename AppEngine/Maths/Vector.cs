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
        return new( x*k,  y*k,  z*k);
    }

    public Vector DivideBy(float k)
    {
        return new( x/k,  y/k,  z/k);
    }

    public Vector Add(Vector v)
    {
        return new Vector(x + v.x, y + v.y, z + v.z);
    }
    
    public Vector Subtract(Vector v)
    {
        return new Vector(x - v.x, y - v.y, z - v.z);
    }

    public float Magnitude
    {
        get
        {
         return MathF.Sqrt((x * x) + (y * y) + (z * z));
        }
    }
    
    public float SquareMagnitude
    {
        get
        {
            return (x * x) + (y * y) + (z * z);
        }
    }
    
    public float DistanceTo (Vector enemyPosition)
    {
        {
            return MathF.Sqrt(((enemyPosition.x-this.x)*(enemyPosition.x-this.x)) + ((enemyPosition.y-this.y)*(enemyPosition.y-this.y)) + ((enemyPosition.z-this.z)*(enemyPosition.z-this.z)));
        }
    }
    
    public float SquareDistanceTo (Vector enemyPosition)
    {
        {
            return MathF.Sqrt(((enemyPosition.x-this.x)*(enemyPosition.x-this.x)) + ((enemyPosition.y-this.y)*(enemyPosition.y-this.y)) + ((enemyPosition.z-this.z)*(enemyPosition.z-this.z)));
        }
    }
}