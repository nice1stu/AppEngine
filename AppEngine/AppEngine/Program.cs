using System;
using AppEngine;
using AppEngine.Mesh;
using GLFW;
using Maths;
using OpenGL;
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

MeshRenderer box2 = new MeshRenderer(new BoxMesh(), material);
box2.Texture = Gamer;
box2.Transform.Position = new Vector(-2, -2, 0);
box2.Transform.Scale = Vector.One.DivideBy(2);

Camera playerCam = new Camera(material, window);
playerCam.Transform.Position = new Vector(0, 0, 3);

//Gravity
float velocity = 0f;
float gravity = -98.0f;
bool isJumping = false;
float jumpVelocity = 0f;

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
    
    Move(playerCam.Transform, deltaTime, cursorDeltaX, cursorDeltaY);
    
    //render
    window.BeginRender();
    playerCam.Render();
    box1.Render();
    box2.Render();
    plane.Render();
    
    window.EndRender();
}

Glfw.Terminate();

void Move(Transform transform, float deltaTime, float deltaCursorX, float deltaCursorY)
{
    const float mouseSensitivity = 0.001f;
    const float movementFactor = 0.05f;
    
    // F key pressed
    if (window.GetKey(Keys.F))
    {
        Vector up = new Vector(0, 1, 0);
        Vector deltaPosition = box2.Transform.Position.Subtract(playerCam.Transform.Position).Normalize();
        Vector right = Vector.Cross(deltaPosition, up).Normalize();
        Vector forward = deltaPosition.MultiplyWith(-1);
        up = Vector.Cross(forward,right).Normalize();
        playerCam.Transform.Rotation = Matrix.FromBaseVectors(right, up,forward);
        
        if (window.GetKey(Keys.W))
        {
            Vector upward = new Vector(0, movementFactor, 0);
            transform.MoveLocal(upward);
        }
        if (window.GetKey(Keys.S))
        {
            Vector backward = new Vector(0, -movementFactor, 0);
            transform.MoveLocal(backward);
        }
        if (window.GetKey(Keys.A))
        {
            Vector leftside = new Vector(-movementFactor, 0, 0);
            transform.MoveLocal(leftside);
        }
        if (window.GetKey(Keys.D))
        {
            Vector rightside = new Vector(movementFactor, 0, 0);
            transform.MoveLocal(rightside);
        }
    }
    else
    {
        // F key not pressed
        Matrix xRotation = Matrix.Rotation(new Vector(0, deltaCursorX*mouseSensitivity*-1, 0)); 
        Matrix yRotation = Matrix.Rotation(new Vector(deltaCursorY*mouseSensitivity*1, 0, 0)); 
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
    
        //Add Gravity

        movement.Y += gravity * deltaTime;

        transform.MoveLocal(movement.MultiplyWith(deltaTime));
        transform.Position = new Vector(transform.Position.X, Math.Max(transform.Position.Y, -2f), transform.Position.Z);
    }

    
    if(!isJumping && window.GetKey(Keys.Space))
    {
        isJumping = true;
        jumpVelocity = 0.5f;
    }
    
    if(isJumping)
    {
        playerCam.Transform.Position = new Vector(transform.Position.X, transform.Position.Y + jumpVelocity, transform.Position.Z);
        jumpVelocity += gravity * deltaTime;
        
        if(playerCam.Transform.Position.Y <= 0)
        {
            playerCam.Transform.Position = new Vector(transform.Position.X, 0, transform.Position.Z);
            isJumping = false;
        }
    }
}