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

float lastFrameTime = (float)Glfw.Time;
while (!window.ShouldClose)
{
    float deltaTime = (float)Glfw.Time - lastFrameTime;
    lastFrameTime = (float)Glfw.Time;
    
    Vector movement = Vector.Zero;
    if (window.GetKey(Keys.W))
        movement.Y += 1f;
    if (window.GetKey(Keys.S))
        movement.Y += -1f;
    if (window.GetKey(Keys.A))
        movement.X += -1f;
    if (window.GetKey(Keys.D))
        movement.X += 1f;
    triangle.Position = triangle.Position.Add(movement.MultiplyWith(deltaTime));

    window.BeginRender();
    triangle.Render();
    window.EndRender();
}
Glfw.Terminate();