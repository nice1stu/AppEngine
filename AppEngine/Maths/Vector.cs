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
            return (((enemyPosition.x-this.x)*(enemyPosition.x-this.x)) + ((enemyPosition.y-this.y)*(enemyPosition.y-this.y)) + ((enemyPosition.z-this.z)*(enemyPosition.z-this.z)));
        }
    }

    public Vector Normalized(Vector enemyDirection)
    {
        float magnitude = MathF.Sqrt((x * x) + (y * y) + (z * z));
        return new Vector (x / magnitude, y / magnitude, z / magnitude);
    }

    public Vector IsUnitVector()
    {
        return new Vector(x, y, z);
        
    }

    public float Dot(Vector b)
    {
        return (this.x * b.x) + (this.y * b.y) + (this.z * b.z);
    }

    public static float AngleBetweenRad(Vector a, Vector b)
    {
        float lengthA;
        float lengthB;
        lengthA = MathF.Sqrt((a.x * a.x) + (a.y * a.y) + (a.z * a.z));
        lengthB = MathF.Sqrt((b.x * b.x) + (b.y * b.y) + (b.z * b.z));
        return (MathF.Acos((float)((a.x * b.x) + (a.y * b.y) + (a.z * b.z) / (lengthA*lengthB) * 180/Math.PI)));
    }
    
    
}