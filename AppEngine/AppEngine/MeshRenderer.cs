using System.Runtime.InteropServices;
using static OpenGL.Gl;

namespace AppEngine;

public class MeshRenderer
{
    public Texture Texture;
    public readonly Transform Transform = new();


    private readonly Mesh.Mesh _mesh;
    private readonly Material _material;
    private uint _vertexArrayObject;
    private uint _vertexBufferObject;
    private uint _elementBufferObject;

    public MeshRenderer(Mesh.Mesh mesh, Material material)
    {
        _mesh = mesh;
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
        fixed (uint* index = &_mesh.Indices[0])
        {
            // tell c# to copy 9 floats from the address of the array to the buffer  bound above
            glBufferData(GL_ELEMENT_ARRAY_BUFFER, sizeof(uint) * _mesh.Indices.Length, index, GL_STATIC_DRAW);
        }
    }

    private unsafe void CreateVertexBuffer()
    {
        _vertexBufferObject = glGenBuffer();
        glBindBuffer(GL_ARRAY_BUFFER, _vertexBufferObject);
        fixed (Vertex* vertex = &_mesh.Vertices[0])
        {
            // tell c# to copy 9 floats from the address of the array to the buffer  bound above
            glBufferData(GL_ARRAY_BUFFER, sizeof(Vertex) * _mesh.Vertices.Length, vertex, GL_STATIC_DRAW);
        }
    }

    private void CreateVertexArray()
    {
        _vertexArrayObject = glGenVertexArray();
        glBindVertexArray(_vertexArrayObject);
    }

    public unsafe void Render()
    {
        Texture?.Use();
        _material.Use();
        _material.Model = Transform.Matrix;
        //draw whatever vertices are currently bound
        glBindVertexArray(_vertexArrayObject);
        glDrawElements(GL_TRIANGLES, _mesh.Indices.Length, GL_UNSIGNED_INT, null);
    }
    
}