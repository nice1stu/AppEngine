using System;
using System.Runtime.InteropServices;
using Maths;
using  static OpenGL.Gl;

namespace AppEngine;

public class MeshRenderer
{
    private readonly Material _material;
    public Transform Transform = new Transform();
    private Vertex[] vertices =
    {
        new Vertex (new Vector(-.5f, -.9f, 0f), Color.White),
        new Vertex (new Vector(+.5f, -.9f, 0f), Color.White),
        new Vertex (new Vector(0f, 0f, 0f), Color.White)
        
        //new Vector(.5f, .9f, 0f),
        //new Vector(-.5f, .9f, 0f),
        //new Vector(0f, 0f, 0f)
    };

    private uint _vertexArrayObject;
    private uint _vertexBufferObject;


    public MeshRenderer(Material material)
    {
        _material = material;
        // create & use a cache for vertex buffer
        _vertexArrayObject = glGenVertexArray();
        glBindVertexArray(_vertexArrayObject);
        // create & use a buffer for vertex array data
        _vertexBufferObject = glGenBuffer();
        glBindBuffer(GL_ARRAY_BUFFER, _vertexBufferObject);

    }
    
    public unsafe void Render()
    {
        _material.Model = Transform.Matrix;

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