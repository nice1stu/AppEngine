using System;
using GLFW;
using static OpenGL.Gl;
using Window = AppEngine.Window;

Console.WriteLine("Starting engine...");
Window window = new Window();

// create vertex shader GLSL
const string vertexShaderCode = @"
#version 330 core

layout (location = 0) in vec3 position;

void main()
{
    gl_Position = vec4(position.x, position.y, position.z, 1.0);
}
";

uint vertexShader = glCreateShader(GL_VERTEX_SHADER);
glShaderSource(vertexShader, vertexShaderCode);
glCompileShader(vertexShader);

unsafe
{
    int success;
    glGetShaderiv(vertexShader, GL_COMPILE_STATUS, &success);
    if (success == GL_FALSE)
    {
        Console.WriteLine("Error compiling vertex Shader:"+glGetShaderInfoLog(vertexShader));
    }
}

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

unsafe
{
    int success;
    glGetShaderiv(fragmentShader, GL_COMPILE_STATUS, &success);
    if (success == GL_FALSE)
    {
        Console.WriteLine("Error compiling fragment Shader:"+glGetShaderInfoLog(fragmentShader));
    }
}

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

while (!window.ShouldClose)
{
    window.BeginRender();
    // draw whatever vertices are currently bound
    glDrawArrays(GL_TRIANGLES, 0, 3);
    
    window.EndRender();
}
Glfw.Terminate();