using System;
using Maths;

namespace AppEngine;
using  static OpenGL.Gl;

public class MeshRenderer
{
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

    public MeshRenderer()
    {
        // create & use a cache for vertex buffer
        _vertexArrayObject = glGenVertexArray();
        glBindVertexArray(_vertexArrayObject);
        // create & use a buffer for vertex array data
        _vertexBufferObject = glGenBuffer();
        glBindBuffer(GL_ARRAY_BUFFER, _vertexBufferObject);
    }
    
    public unsafe void Render()
    {
        fixed(Vector* vertex = &vertices[0])
        {
            // the c++ to copy 9 flats from the address of any 
            glBufferData(GL_ARRAY_BUFFER, sizeof(Vector)*vertices.Length, vertex, GL_STATIC_DRAW);
        }
        glVertexAttribPointer(0, 3, GL_FLOAT, false, sizeof(Vector), System.IntPtr.Zero);
        glEnableVertexAttribArray(0);
        glDrawArrays(GL_TRIANGLES, 0, 6);
    }
}