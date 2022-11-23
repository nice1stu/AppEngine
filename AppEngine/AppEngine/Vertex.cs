using Maths;

namespace AppEngine;

public struct Vertex
{
    public Vector Position;
    public Color Color;
    public float U, V;

    public Vertex(Vector position, Color color, float u, float v)
    {
        Position = position;
        Color = color;
        U = u;
        V = v;
    }
}