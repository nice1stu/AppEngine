using System;
using GLFW;
using static OpenGL.Gl;

Console.WriteLine("Starting engine...");
Glfw.Init();
Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
Glfw.WindowHint(Hint.ContextVersionMajor, 3);
Glfw.WindowHint(Hint.ContextVersionMinor, 3);
Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
Glfw.WindowHint(Hint.OpenglForwardCompatible, Constants.True);
Glfw.WindowHint(Hint.Doublebuffer, Constants.True);

Window window = Glfw.CreateWindow(800, 600, "AppEngine", Monitor.None, Window.None);
Glfw.MakeContextCurrent(window);
Import(Glfw.GetProcAddress);

Glfw.SetKeyCallback(window, OnKeyCallback);

void OnKeyCallback(IntPtr window, Keys key, int scanCode, InputState state, ModifierKeys mods) //key events
{
    if (key == Keys.Escape && state == InputState.Press)
    {
        Glfw.SetWindowShouldClose(Glfw.CurrentContext, true);
    }
}

float[] vertices =
{
    -.5f, -.5f, 0f,
    +.5f, -.5f, 0f,
    0f, +.5f, 0f
};

uint vertexarrayObject = glGenVertexArray();
glBindVertexArray(vertexarrayObject);
uint vertexBufferObject = glGenBuffer();
glBindBuffer(GL_ARRAY_BUFFER, vertexBufferObject);
unsafe
{
    fixed(float* vertex = &vertices[0])
    {
        glBufferData(GL_ARRAY_BUFFER, sizeof(float)*vertices.Length, vertex, GL_STATIC_DRAW);
    }
}

while (!Glfw.WindowShouldClose(window))
{
    // update input
    Glfw.PollEvents();
    bool isSpacePressed = Glfw.GetKey(window, Keys.Space) != InputState.Release;
    
    // update your game
    
    // render
    if (isSpacePressed)
    {
        glClearColor(.2f, .05f,.2f, 1);
    }else
        glClearColor(0, 0,0, 1);
    glClear(GL_COLOR_BUFFER_BIT);
    //glFlush();
    Glfw.SwapBuffers(window);
}

Glfw.Terminate();