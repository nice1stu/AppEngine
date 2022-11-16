using static OpenGL.Gl;
using System;

namespace AppEngine;

public class Material
{
    private uint shaderProgram;
    public Material()
    {
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
            color = vec4(0.0f, 0.0f, 1.0f, 1.0f);
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
        shaderProgram = glCreateProgram();

        glAttachShader(shaderProgram, vertexShader);
        glAttachShader(shaderProgram, fragmentShader);
        glLinkProgram(shaderProgram);
    }

    public void Use()
    {
        glUseProgram(shaderProgram);
    }
}