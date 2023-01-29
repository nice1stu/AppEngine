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
Texture Gamer = new Texture("resources/textures/Gamer.jpg");
Texture texture = new Texture("resources/textures/Texture03.jpg");

MeshRenderer plane = new MeshRenderer(new PlaneMesh(), material);
plane.Texture = texture;

plane.Transform.Position = new Vector(0, -2f, 0);

MeshRenderer box1 = new MeshRenderer(new PyramidMesh(), material);
box1.Texture = wall;
box1.Transform.Scale = Vector.One.DivideBy(2);
//box1.Transform.Rotation = new Vector(-.7f, .7f, 0f);
box1.Transform.Rotation = Matrix.Rotation(new Vector(-0.7f, 0.7f, 0f)) * box1.Transform.Rotation;

MeshRenderer box2 = new MeshRenderer(new BoxMesh(), material);
box2.Texture = Gamer;
box2.Transform.Position = new Vector(-2, -2, 0);
box2.Transform.Scale = Vector.One.DivideBy(2);
Camera camera = new Camera(material, window);
camera.Transform.Position = new Vector(0, 0, 3);

float lastFrameTime = (float)Glfw.Time;
window.GetCursorPosition(out float cursorX, out float cursorY);
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
    
    box1.Transform.Rotation = Matrix.Rotation(new Vector(deltaTime * 0.5f, deltaTime, 0f)) * box1.Transform.Rotation;

    // float velocity
    //if F key is not pressed
    // we know plane is at Y zero
    // if camera.Tramsform.position.Y > 0
    // simulate falling
    // else
    // if player press space simulate jumping

    Move(camera.Transform, deltaTime, cursorDeltaX, cursorDeltaY);
    
    //render
    window.BeginRender();
    camera.Render();
    box1.Render();
    box2.Render();
    plane.Render();
    
    window.EndRender();
}

Glfw.Terminate();

void Move(Transform transform, float deltaTime, float deltaCursorX, float deltaCursorY)
{
    const float mouseSensitivity = 0.001f;
    const float movementFactor = 0.005f;

    //if F key is pressed:
    //  focus on box2 (use cross product to calculate rotation and assign it to transform.Rotation
    // WS moves Up/Down & AD Left/Right
    if (window.GetKey(Keys.F))
    {
        Vector up = new Vector(0, 1, 0);
        Vector deltaPosition = box2.Transform.Position.Subtract(transform.Position).Normalize();
        Vector right = Vector.Cross(up, deltaPosition).Normalize();
        Vector forward = Vector.Cross(up, right).Normalize();
        transform.Rotation = Matrix.FromBaseVectors(right, up, forward);
        
        if (window.GetKey(Keys.W))
        {
            Vector forwardOrbit = forward.MultiplyWith(movementFactor);
            transform.MoveLocal(forwardOrbit);
        }
        if (window.GetKey(Keys.S))
        {
            Vector backOrbit = forward.MultiplyWith(-movementFactor);
            transform.MoveLocal(backOrbit);
        }
        if (window.GetKey(Keys.A))
        {
            Vector leftOrbit = right.MultiplyWith(-movementFactor);
            transform.MoveLocal(leftOrbit);
        }
        if (window.GetKey(Keys.D))
        {
            Vector rightOrbit = right.MultiplyWith(movementFactor);
            transform.MoveLocal(rightOrbit);
        }
    }
    else
    {
        // if F key is not pressed (else)
    // use deltaCursor X & Y to rotate the camera
    // WS moves Forwad/Back & AD Strafe Left/Right

    // F key not pressed
    Matrix xRotation = Matrix.Rotation(new Vector(0, deltaCursorX*mouseSensitivity*-1, 0));
    Matrix yRotation = Matrix.Rotation(new Vector(deltaCursorY*mouseSensitivity*-1, 0, 0));
    transform.Rotation = xRotation * transform.Rotation * yRotation;

    Vector movement = Vector.Zero;
    if (window.GetKey(Keys.W))
        movement.Z -= 1f;
    if (window.GetKey(Keys.S))
        movement.Z += 1f;
    if (window.GetKey(Keys.A))
        movement.X -= 1f;
    if (window.GetKey(Keys.D))
        movement.X += 1f;
    
    transform.MoveLocal(movement.MultiplyWith(deltaTime)); 
    }
}