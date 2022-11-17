using System;
using Maths;
using  static OpenGL.Gl;

namespace AppEngine;

public class MeshRenderer
{
    public Vector Position;
    public Vector Rotation;
    public Vector Scale;
    
    private Vector[] vertices =
    {
        new Vector(-.5f, -.9f, 0f),
        new Vector(+.5f, -.9f, 0f),
        new Vector(0f, 0f, 0f),
    
        new Vector(.5f, .9f, 0f),
        new Vector(-.5f, .9f, 0f),
        new Vector(0f, 0f, 0f)
    };

    private uint _vertexArrayObject;
    private uint _vertexBufferObject;
    private readonly Vector[] _vertexBuffer;

    public MeshRenderer()
    {
        Scale = new Vector(1, 1, 1);
        // create & use a cache for vertex buffer
        _vertexArrayObject = glGenVertexArray();
        glBindVertexArray(_vertexArrayObject);
        // create & use a buffer for vertex array data
        _vertexBufferObject = glGenBuffer();
        glBindBuffer(GL_ARRAY_BUFFER, _vertexBufferObject);
        _vertexBuffer = new Vector[vertices.Length];
    }
    
    public unsafe void Render()
    {
        Matrix matrix = Matrix.Translation(Position) * Matrix.Rotation(Rotation) * Matrix.Scale(Scale);
        for (var i = 0; i < vertices.Length; i++)
        {
            _vertexBuffer[i] = matrix * vertices[i];
        }
        fixed(Vector* vertex = &_vertexBuffer[0])
        {
            // the c++ to copy 9 flats from the address of any 
            glBufferData(GL_ARRAY_BUFFER, sizeof(Vector)*_vertexBuffer.Length, vertex, GL_STATIC_DRAW);
        }
        glVertexAttribPointer(0, 3, GL_FLOAT, false, sizeof(Vector), System.IntPtr.Zero);
        glEnableVertexAttribArray(0);
        glDrawArrays(GL_TRIANGLES, 0, 6);
    }
}