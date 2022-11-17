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
    Vector movement = Vector.Zero;
    if (window.GetKey(Keys.W))
        movement.Y += 0.01f;
    if (window.GetKey(Keys.S))
        movement.Y += -0.01f;
    if (window.GetKey(Keys.A))
        movement.X += -0.01f;
    if (window.GetKey(Keys.D))
        movement.X += 0.01f;
    triangle.Position = triangle.Position.Add(movement);

    window.BeginRender();
    triangle.Render();
    window.EndRender();
}
Glfw.Terminate();