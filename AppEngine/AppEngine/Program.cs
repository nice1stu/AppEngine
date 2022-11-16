﻿using System;
using AppEngine;
using GLFW;
using Maths;
using static OpenGL.Gl;
using Window = AppEngine.Window;

Console.WriteLine("Starting engine...");
Window window = new Window();
new Material();
Material material = new Material();
material.Use();

Vector[] vertices =
{
    new Vector(-.5f, -.5f, 0f),
    new Vector(+.5f, -.5f, 0f),
    new Vector(0f, +.5f, 0f)
}; 
    // create & use a cache for vertex buffer
uint vertexArrayObject = glGenVertexArray();
glBindVertexArray(vertexArrayObject);
    // create & use a buffer for vertex array data
uint vertexBufferObject = glGenBuffer();
glBindBuffer(GL_ARRAY_BUFFER, vertexBufferObject);

while (!window.ShouldClose)
{
    unsafe
    {
        fixed(Vector* vertex = &vertices[0])
        {
            // the c++ to copy 9 flats from the address of any 
            glBufferData(GL_ARRAY_BUFFER, sizeof(Vector)*vertices.Length, vertex, GL_STATIC_DRAW);
        }
        glVertexAttribPointer(0, 3, GL_FLOAT, false, sizeof(Vector), IntPtr.Zero);
    }
    glEnableVertexAttribArray(0);
    window.BeginRender();
        // draw whatever vertices are currently bound
    glDrawArrays(GL_TRIANGLES, 0, 3);

    if (window.GetKey(Keys.H))
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = vertices[i].MultiplyWith(1.01f);
        }
    }
    
    window.EndRender();
}
Glfw.Terminate();