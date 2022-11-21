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
    
    //Disco color
    material.Color = Color.FromHsv(lastFrameTime * 60, 0.8f, 0.6f);
    
    Vector movement = Vector.Zero;
    if (window.GetKey(Keys.W))
        movement.Y += 1f;
    if (window.GetKey(Keys.S))
        movement.Y += -1f;
    if (window.GetKey(Keys.A))
        movement.X += -1f;
    if (window.GetKey(Keys.D))
        movement.X += 1f;
    if (window.GetKey(Keys.G))
        triangle.Scale = triangle.Scale.DivideBy(1+1*deltaTime);
    if (window.GetKey(Keys.H))
        triangle.Scale = triangle.Scale.MultiplyWith(1+1*deltaTime);
    if (window.GetKey(Keys.Q))
        triangle.Rotation = triangle.Rotation.Add(new Vector(0, 0, deltaTime));
    if (window.GetKey(Keys.E))
        triangle.Rotation = triangle.Rotation.Subtract(new Vector(0, 0, deltaTime));
       /* if (window.GetKey(Keys.Q))
            triangle.Rotation.Z += deltaTime;*/

        triangle.Position = triangle.Position.Add(movement.MultiplyWith(deltaTime));

    //render
    window.BeginRender();
    triangle.Render();
    window.EndRender();
}
Glfw.Terminate();