using System.Runtime.InteropServices;
using Maths;
using static OpenGL.Gl;

namespace AppEngine;

public class MeshRenderer
{
    public readonly Transform Transform = new();
    private  static readonly Vertex[] vertices =
    {
        new Vertex(new Vector(-0.5f, -0.5f, 0.5f), Color.White, 0f,0f), // 0: bottom-left
        new Vertex(new Vector(-0.5f,0.5f, 0.5f), Color.White, 0f,1f), // 1: top-left
        new Vertex(new Vector(0.5f, 0.5f, 0.5f), Color.White, 1f, 1f), // 2: top-right
        new Vertex(new Vector(0.5f, -0.5f, 0.5f), Color.White, 1f ,0f), // 3: bottom-right
        
        new Vertex(new Vector(-0.5f, -0.5f, -0.5f), Color.White, 0f,0f), // 0: bottom-left
        new Vertex(new Vector(-0.5f,0.5f, -0.5f), Color.White, 0f,1f), // 1: top-left
        new Vertex(new Vector(0.5f, 0.5f, -0.5f), Color.White, 1f, 1f), // 2: top-right
        new Vertex(new Vector(0.5f, -0.5f, -0.5f), Color.White, 1f ,0f), // 3: bottom-right
    };

    private static readonly uint[] indices =
    {
        0, 1, 2,
        0, 3, 2,
        7, 6, 5,
        7, 4, 5,
     
        0, 3, 4,
        3, 4, 7,
        1, 2, 6,
        1, 5, 6,
        
        0, 1, 5,
        0, 4, 5,
        
        2, 3, 6,
        3, 6, 7
    };
        
    private readonly Material _material;
    private uint _vertexArrayObject;
    private uint _vertexBufferObject;
    private uint _elementBufferObject;

    public MeshRenderer(Material material)
    {
        _material = material;
        // the vertex array stores the following configuration and buffers
        CreateVertexArray();
        // the vertex buffer holds all vertices on the GPU
        CreateVertexBuffer();
        // the element buffer holds all vertex indices on the GPU
        CreateElementBuffer();
               // the vertex attributes tell the Vertex Shader, in which order the attributes like Position, Color, ... arrive
        ConfigureVertexAttributes();
    }

    private static unsafe void ConfigureVertexAttributes()
    {
        glVertexAttribPointer(0, 3, GL_FLOAT, false, sizeof(Vertex), System.IntPtr.Zero);
        glVertexAttribPointer(1, 4, GL_FLOAT, false, sizeof(Vertex), Marshal.OffsetOf<Vertex>(nameof(Vertex.Color)));
        glVertexAttribPointer(2, 2, GL_FLOAT, false, sizeof(Vertex), Marshal.OffsetOf<Vertex>(nameof(Vertex.U)));
        glEnableVertexAttribArray(0);
        glEnableVertexAttribArray(1);
        glEnableVertexAttribArray(2);
    }

    private unsafe void CreateElementBuffer()
    {
        _elementBufferObject = glGenBuffer();
        glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, _elementBufferObject);
        fixed (uint* index = &indices[0])
        {
            // tell c++ to copy 9 floats from the address of the array to the buffer  bound above
            glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(uint) * indices.Length, index, GL_STATIC_DRAW);
        }
    }

    private unsafe void CreateVertexBuffer()
    {
        _vertexBufferObject = glGenBuffer();
        glBindBuffer(GL_ARRAY_BUFFER, _vertexBufferObject);
        fixed (Vertex* vertex = &vertices[0])
        {
            // tell c++ to copy 9 floats from the address of the array to the buffer  bound above
            glBufferData(GL_ARRAY_BUFFER, sizeof(Vertex) * vertices.Length, vertex, GL_STATIC_DRAW);
        }
    }

    private void CreateVertexArray()
    {
        _vertexArrayObject = glGenVertexArray();
        glBindVertexArray(_vertexArrayObject);
    }

    public unsafe void Render()
    {
        _material.Use();
        _material.Model = Transform.Matrix;
        //draw whatever vertices are currently bound
        glDrawElements(GL_TRIANGLES, indices.Length, GL_UNSIGNED_INT, null);
    }
    
}