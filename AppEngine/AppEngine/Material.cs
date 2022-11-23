using static OpenGL.Gl;
using System;
using System.IO;
using Maths;

namespace AppEngine;

public class Material
{
    private readonly uint _shaderProgram;

    public Color Color
    {
        set
        {
            int colorProperty = glGetUniformLocation(_shaderProgram, "_color");
            glUniform4f(colorProperty, value.Red, value.Green, value.Blue, value.Alpha);
        }
    }
    
    public unsafe Matrix Model
    {
        set
        {
            int matrixProperty = glGetUniformLocation(_shaderProgram, "_model");
            glUniformMatrix4fv(matrixProperty, 1, true, &value.m11);
        }
    }
    
    public unsafe Matrix View
    {
        set
        {
            int matrixProperty = glGetUniformLocation(_shaderProgram, "_view");
            glUniformMatrix4fv(matrixProperty, 1, true, &value.m11);
        }
    }

    public Texture Texture
    {
        set
        {
            glActiveTexture(GL_TEXTURE0);
            value.Use();
            int textureProperty = glGetUniformLocation(_shaderProgram, "_texture");
            glUniform1i(textureProperty,0);
        }
    }
    public Material()
    {
        // create vertex shader GLSL
        string vertexShaderCode = File.ReadAllText("resources/shaders/vertex/07-camera-uv-position.vert");

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
        string fragmentShaderCode = File.ReadAllText("resources/shaders/fragment/04-texture.frag");

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
        _shaderProgram = glCreateProgram();

        glAttachShader(_shaderProgram, vertexShader);
        glAttachShader(_shaderProgram, fragmentShader);
        glLinkProgram(_shaderProgram);
    }

    public void Use()
    {
        glUseProgram(_shaderProgram);
    }
}