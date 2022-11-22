using System;
using AppEngine;
using GLFW;
using Maths;
//using static OpenGL.Gl;
using Window = AppEngine.Window;

Console.WriteLine("Starting engine...");
Window window = new Window();
Material material = new Material();
Texture wall = new Texture("resources/textures/wall.jpg");
MeshRenderer triangle = new MeshRenderer(material);
MeshRenderer triangle2 = new MeshRenderer(material);
triangle2.Transform.Position = new Vector(1, -0.5f, 0);
triangle2.Transform.Scale = Vector.One.DivideBy(2);
Camera camera = new Camera(material);

float lastFrameTime = (float)Glfw.Time;
while (!window.ShouldClose)
{
    float deltaTime = (float)Glfw.Time - lastFrameTime;
    lastFrameTime = (float)Glfw.Time;
    
    //Disco color
    material.Color = Color.FromHsv(lastFrameTime * 60, 0.8f, 0.6f);
    material.Color = Color.White;
    //material.T = lastFrameTime;

    UpdateTrianglePosition(camera.Transform, deltaTime);
    
    //render
    window.BeginRender();
    camera.Render();
    triangle.Render();
    triangle2.Render();
    //...
    window.EndRender();
}

Glfw.Terminate();

void UpdateTrianglePosition(Transform transform,float deltaTime)
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
        transform.Scale = transform.Scale.DivideBy(1+1*deltaTime);
    if (window.GetKey(Keys.H))
        transform.Scale = transform.Scale.MultiplyWith(1+1*deltaTime);
    if (window.GetKey(Keys.Q))
        transform.Rotation = transform.Rotation.Add(new Vector(0, 0, deltaTime));
    if (window.GetKey(Keys.E))
        transform.Rotation = transform.Rotation.Subtract(new Vector(0, 0, deltaTime));

    transform.Position = transform.Position.Add(movement.MultiplyWith(deltaTime));
}