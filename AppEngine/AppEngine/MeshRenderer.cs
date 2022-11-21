using System;
using System.Runtime.InteropServices;
using Maths;
using  static OpenGL.Gl;

namespace AppEngine;

public class MeshRenderer
{
    public Vector Position;
    public Vector Rotation;
    public Vector Scale;
    
    private Vertex[] vertices =
    {
        new Vertex (new Vector(1f, 0, 0), Color.RedConst),
        new Vertex (new Vector(0, 1f, 0), Color.GreenConst),
        new Vertex (new Vector(0, 0, 1f), Color.BlueConst)
    
        //new Vector(.5f, .9f, 0f),
        //new Vector(-.5f, .9f, 0f),
        //new Vector(0f, 0f, 0f)
    };

    private uint _vertexArrayObject;
    private uint _vertexBufferObject;


    public MeshRenderer()
    {
        Scale = new Vector(1, 1, 1);
        // create & use a cache for vertex buffer
        _vertexArrayObject = glGenVertexArray();
        glBindVertexArray(_vertexArrayObject);
        // create & use a buffer for vertex array data
        _vertexBufferObject = glGenBuffer();
        glBindBuffer(GL_ARRAY_BUFFER, _vertexBufferObject);

    }
    
    public unsafe void Render()
    {
        Matrix matrix = Matrix.Translation(Position) * Matrix.Rotation(Rotation) * Matrix.Scale(Scale);

            fixed(Vertex* vertex = &vertices[0])
        {
            // the c++ to copy 9 flats from the address of any 
            glBufferData(GL_ARRAY_BUFFER, sizeof(Vertex) * vertices.Length, vertex, GL_STATIC_DRAW);
        }
        glVertexAttribPointer(0, 3, GL_FLOAT, false, sizeof(Vertex), System.IntPtr.Zero);
        glVertexAttribPointer(1, 4, GL_FLOAT, false, sizeof(Vertex),Marshal.OffsetOf<Vertex>(nameof(Vertex.Color)));
        glEnableVertexAttribArray(0);
        glEnableVertexAttribArray(1);
        
        glDrawArrays(GL_TRIANGLES, 0, vertices.Length);
    }
}