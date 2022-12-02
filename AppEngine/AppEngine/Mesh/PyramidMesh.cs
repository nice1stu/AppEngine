using System.Security.Cryptography.X509Certificates;
using Maths;

namespace AppEngine.Mesh;

public class PyramidMesh: Mesh
{
    public override Vertex[] Vertices => vertices;
    public override uint[] Indices => indices;
    
    private  static readonly Vertex[] vertices =
    {
        new Vertex(new Vector(-0.5f, -0.5f, 0.5f), Color.White, 0f,0f), // 0: bottom-left
        new Vertex(new Vector(0,0.5f, 0), Color.White, 0f,1f), // 1: top-left
        new Vertex(new Vector(0, 0.5f, 0), Color.White, 1f, 1f), // 2: top-right
        new Vertex(new Vector(0.5f, -0.5f, 0.5f), Color.White, 1f ,0f), // 3: bottom-right
        
        new Vertex(new Vector(-0.5f, -0.5f, -0.5f), Color.White, 1f,0f), // 4: bottom-left
        new Vertex(new Vector(0,0.5f, 0), Color.White, 1f,1f), // 5: top-left
        new Vertex(new Vector(0, 0.5f, 0), Color.White, 0f, 1f), // 6: top-right
        new Vertex(new Vector(0.5f, -0.5f, -0.5f), Color.White, 0f ,0f), // 7: bottom-right

        new Vertex(new Vector(-0.5f,-0.5f, 0.5f), Color.White, 0f,0f), // 12: bottom-left
        new Vertex(new Vector(0.5f, -0.5f, 0.5f), Color.White, 1f, 0f), // 13: bottom-right
        new Vertex(new Vector(0.5f,-0.5f, -0.5f), Color.White, 1f,1f), // 14: bottom-right back
        new Vertex(new Vector(-0.5f, -0.5f, -0.5f), Color.White, 0f, 1f), // 15: bottom-left back
    };

    private static readonly uint[] indices =
    {
        //front
        0, 3, 2,
        //back
        4, 6, 7,
        //bottom
        12,13,14,
        12,15,14,
        //right
        0,1,4,
        //side
        3, 6, 7
    };
}