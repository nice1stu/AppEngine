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

// create vertex shader GLSL
const string vertexShaderCode = @"
#version 330 core

layout (location = 0) in vec3 position;

void main()
{
    gl_position = vec4(position.x, position.y, position.z, 1.0);
";

uint vertexShader = glCreateShader(GL_VERTEX_SHADER);
glShaderSource(vertexShader, vertexShaderCode);
glCompileShader(vertexShader);

// create fragment shader (pixel shader)
const string fragmentShaderCode = @"
#version 330 core

out vec4 color;

void main()
{
    color = vec4(1.0f, 0.5f, 0.2f, 1.0f);
}
";

uint fragmentShader = glCreateShader(GL_FRAGMENT_SHADER);
glShaderSource(fragmentShader, fragmentShaderCode);
glCompileShader(fragmentShader);

// create & use render pipeline
uint shaderProgram = glCreateProgram();

// Todo: attach shaders
glAttachShader(shaderProgram, vertexShader);
glAttachShader(shaderProgram, fragmentShader);

glLinkProgram(shaderProgram);
glUseProgram(shaderProgram);

float[] vertices =
{
    -.5f, -.5f, 0f,
    +.5f, -.5f, 0f,
    0f, +.5f, 0f
};
// create & use a cache for vertex buffer
uint vertexArrayObject = glGenVertexArray();
glBindVertexArray(vertexArrayObject);
// create & use a buffer for vertex array data
uint vertexBufferObject = glGenBuffer();
glBindBuffer(GL_ARRAY_BUFFER, vertexBufferObject);
unsafe
{
    fixed(float* vertex = &vertices[0])
    {
        // the c++ to copy 9 flats from the address of any 
        glBufferData(GL_ARRAY_BUFFER, sizeof(float)*vertices.Length, vertex, GL_STATIC_DRAW);
    }
}
glVertexAttribPointer(0, 3, GL_FLOAT, false, 3 * sizeof(float), IntPtr.Zero);
glEnableVertexAttribArray(0);

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
    // draw whatever vertices are currently bound
    glDrawArrays(GL_TRIANGLES, 0, 3);
    
    //glFlush();
    Glfw.SwapBuffers(window);
}
Glfw.Terminate();