using System;
using AppEngine;
using GLFW;
using Maths;
//using static OpenGL.Gl;
using Window = AppEngine.Window;

Console.WriteLine("Starting engine...");
Window window = new Window();
new Material();
Material material = new Material();
material.Use();
Texture wall = new Texture("resources/textures/wall.jpg");

MeshRenderer triangle = new MeshRenderer(material);

float lastFrameTime = (float)Glfw.Time;
while (!window.ShouldClose)
{
    float deltaTime = (float)Glfw.Time - lastFrameTime;
    lastFrameTime = (float)Glfw.Time;
    
    //Disco color
    material.Color = Color.FromHsv(lastFrameTime * 60, 0.8f, 0.6f);
    material.Color = Color.White;
    //material.T = lastFrameTime;

    UpdateTrianglePosition(deltaTime);
    
    //render
    window.BeginRender();
    triangle.Render();
    window.EndRender();
}

Glfw.Terminate();

void UpdateTrianglePosition(float deltaTime)
{
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
        triangle.Transform.Scale = triangle.Transform.Scale.DivideBy(1+1*deltaTime);
    if (window.GetKey(Keys.H))
        triangle.Transform.Scale = triangle.Transform.Scale.MultiplyWith(1+1*deltaTime);
    if (window.GetKey(Keys.Q))
        triangle.Transform.Rotation = triangle.Transform.Rotation.Add(new Vector(0, 0, deltaTime));
    if (window.GetKey(Keys.E))
        triangle.Transform.Rotation = triangle.Transform.Rotation.Subtract(new Vector(0, 0, deltaTime));

    triangle.Transform.Position = triangle.Transform.Position.Add(movement.MultiplyWith(deltaTime));
}