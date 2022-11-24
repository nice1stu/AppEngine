using Maths;

namespace AppEngine.Mesh;

public abstract class Mesh
{
    public abstract Vertex[] Vertices { get; }

    public abstract uint[] Indices { get; }
}