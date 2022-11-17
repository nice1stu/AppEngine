using System;
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
MeshRenderer triangle = new MeshRenderer();

while (!window.ShouldClose)
{
    window.BeginRender();
    triangle.Render();
    window.EndRender();
}
Glfw.Terminate();