using Maths;

namespace AppEngine;

public class Transform
{
    private readonly Material _material;
    public Vector Position;
    public Vector Rotation;
    public Vector Scale = Vector.One;
    public Matrix Matrix => Matrix.Translation(Position) * Matrix.Rotation(Rotation) * Matrix.Scale(Scale);
    
}