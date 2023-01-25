using System;
using AppEngine;
using AppEngine.Mesh;
using GLFW;
using Maths;

using Window = AppEngine.Window;

Console.WriteLine("Starting engine...");
Window window = new Window();
Material material = new Material();
Texture wall = new Texture("resources/textures/wall.jpg");
Texture gamer = new Texture("resources/textures/Gamer.jpg");
Texture texture = new Texture("resources/textures/Texture03.jpg");

MeshRenderer plane = new MeshRenderer(new PlaneMesh(), material);
plane.Texture = texture;

plane.Transform.Position = new Vector(0, -2f, 0);

MeshRenderer box1 = new MeshRenderer(new PyramidMesh(), material);
box1.Texture = wall;
box1.Transform.Scale = Vector.One.DivideBy(2);
box1.Transform.Rotation = new Vector(-.7f, .7f, 0f);

MeshRenderer box2 = new MeshRenderer(new BoxMesh(), material);
box2.Texture = gamer;
box2.Transform.Position = new Vector(-2, -2, 0);
box2.Transform.Scale = Vector.One.DivideBy(2);

MeshRenderer focusCube = new MeshRenderer(new BoxMesh(), material);
focusCube.Transform.Position = new Vector(0, 0, 0);

Camera camera = new Camera(material, window);
camera.Transform.Position = new Vector(0, 0, 3);

float lastFrameTime = (float)Glfw.Time;
window.GetCursorPosition(out float cursorX, out float cursorY);
bool isFKeyHeld = false;
while (!window.ShouldClose)
{
    float deltaTime = (float)Glfw.Time - lastFrameTime;
    lastFrameTime = (float)Glfw.Time;
    
    window.GetCursorPosition(out float newCursorX, out float newCursorY);
    float cursorDeltaX = newCursorX - cursorX;
    float cursorDeltaY = newCursorY - cursorY;
    cursorX = newCursorX;
    cursorY = newCursorY;

    //Disco color
    material.Color = Color.FromHsv(lastFrameTime * 60, 0.8f, 0.6f);
    material.Color = Color.White;
    
    box1.Transform.Rotation = box1.Transform.Rotation.Add(new Vector(deltaTime * 0.5f, deltaTime, 0f));

    isFKeyHeld = window.GetKey(Keys.F);
    Move(camera.Transform, deltaTime, cursorDeltaX, cursorDeltaY, isFKeyHeld, focusCube);
    camera.Transform.LookAt(focusCube.Transform.Position);

    //Move(camera.Transform, deltaTime, cursorDeltaX, cursorDeltaY);
    
    //render
    window.BeginRender();
    camera.Render();
    box1.Render();
    box2.Render();
    focusCube.Render();
    plane.Render();
    
    window.EndRender();
}

Glfw.Terminate();

void Move(Transform transform,float deltaTime, float deltaCursorX, float deltaCursorY, bool isFKeyHeld, MeshRenderer focusCube)
{
    const float mouseSensitivity = 0.001f;
    transform.Rotation = transform.Rotation.Add(new Vector(deltaCursorY, deltaCursorX, 0f).MultiplyWith(-mouseSensitivity));
    
    Vector movement = Vector.Zero;
    if (isFKeyHeld)
    {
        if (window.GetKey(Keys.W))
            movement.Y += 1f;
        if (window.GetKey(Keys.S))
            movement.Y -= 1f;
        if (window.GetKey(Keys.A))
            movement.X -= 1f;
        if (window.GetKey(Keys.D))
            movement.X += 1f;
    }
    else
    {
        if (window.GetKey(Keys.W))
            movement.Z -= 1f;
        if (window.GetKey(Keys.S))
            movement.Z += 1f;
        if (window.GetKey(Keys.A))
            movement.X -= 1f;
        if (window.GetKey(Keys.D))
            movement.X += 1f;
    }
    transform.MoveLocal(movement.MultiplyWith(deltaTime));
}