using System.Numerics;

namespace Maths;

public record struct Vector
{
    public float X, Y, Z;
    
    public Vector(float x, float y, float z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }

    public static Vector Zero
    {
        get { return new Vector(0, 0, 0); }
    }



    public Vector Inverse()
    {
        return new(-X, -Y, -Z);
    }

    public Vector MultiplyWith(float k)
    {
        return new( X*k,  Y*k,  Z*k);
    }

    public Vector DivideBy(float k)
    {
        return new( X/k,  Y/k,  Z/k);
    }

    public Vector Add(Vector v)
    {
        return new Vector(X + v.X, Y + v.Y, Z + v.Z);
    }
    
    public Vector Subtract(Vector v)
    {
        return new Vector(X - v.X, Y - v.Y, Z - v.Z);
    }

    public float Magnitude
    {
        get
        {
         return MathF.Sqrt((X * X) + (Y * Y) + (Z * Z));
        }
    }
    
    public float SquareMagnitude
    {
        get
        {
            return (X * X) + (Y * Y) + (Z * Z);
        }
    }
    
    public float DistanceTo (Vector enemyPosition)
    {
        {
            return MathF.Sqrt(((enemyPosition.X-this.X)*(enemyPosition.X-this.X)) + ((enemyPosition.Y-this.Y)*(enemyPosition.Y-this.Y)) + ((enemyPosition.Z-this.Z)*(enemyPosition.Z-this.Z)));
        }
    }
    
    public float SquareDistanceTo (Vector enemyPosition)
    {
        {
            return (((enemyPosition.X-this.X)*(enemyPosition.X-this.X)) + ((enemyPosition.Y-this.Y)*(enemyPosition.Y-this.Y)) + ((enemyPosition.Z-this.Z)*(enemyPosition.Z-this.Z)));
        }
    }

    public Vector Normalized()
    {
        float magnitude = Magnitude;
        return new Vector (X / magnitude, Y / magnitude, Z / magnitude);
    }

    public bool IsUnitVector()
    {
        float magnitude = Magnitude;
        if (magnitude == 1)
        {
            return true;
        }
        return false;
    }

    public float Dot(Vector b)
    {
        return (this.X * b.X) + (this.Y * b.Y) + (this.Z * b.Z);
    }

    public static float AngleBetweenRad(Vector a, Vector b)
    {
        float lengthA;
        float lengthB;
        lengthA = MathF.Sqrt((a.X * a.X) + (a.Y * a.Y) + (a.Z * a.Z));
        lengthB = MathF.Sqrt((b.X * b.X) + (b.Y * b.Y) + (b.Z * b.Z));
        return (MathF.Acos((float)((a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z) / (lengthA*lengthB) * 180/Math.PI)));
    }

    public static float AngleBetweenDeg(Vector a, Vector b)
    {
        float numerator;
        float denominatorA;
        float denominatorB;
        numerator = (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
        denominatorA = ((a.X * a.X) + (a.Y * a.Y) + (a.Z * a.Z));
        denominatorB = ((b.X * b.X) + (b.Y * b.Y) + (b.Z * b.Z));
        return (numerator / MathF.Sqrt(denominatorA * denominatorB));
    }

    public Vector Cross(Vector right, Vector up)
    {
        return new Vector(((right.Y * up.Z) - (up.Y * right.Z)), ((right.X * up.Z) - (up.X * right.Z) * -1), ((right.X * up.Y) - (up.X * right.Y)));
    }

    public static Vector Max(Vector a, Vector b)
    {
        Vector result;
        result.X = MathF.Max(a.X, b.X);
        result.Y = MathF.Max(a.Y, b.Y);
        result.Z = MathF.Max(a.Z, b.Z);
        //return new Vector(MathF.Max(a.x, b.x), MathF.Max(a.y, b.y), MathF.Max(a.z, b.z));
        return result;
    }

    public static Vector Min(Vector a, Vector b)
    {
        Vector result;
        result.X = MathF.Min(a.X, b.X);
        result.Y = MathF.Min(a.Y, b.Y);
        result.Z = MathF.Min(a.Z, b.Z);
        return result;
    }
}