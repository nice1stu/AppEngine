using Maths;

namespace AppEngine;

public class Transform
{
    private readonly Material _material;
    public Vector Position;
    public Matrix Rotation = Matrix.Identity;
    public Vector Scale = Vector.One;
    public Matrix Matrix => Matrix.Translation(Position) * Rotation * Matrix.Scale(Scale);
    
    public void MoveLocal(Vector vector)
    {
        Position = Position.Add(Maths.Matrix.Transform(Matrix, vector, 0f));
    }
    
}