using System;
using System.Runtime.InteropServices;
using Maths;
using  static OpenGL.Gl;

namespace AppEngine;

public class Camera
{
    public Transform Transform = new Transform();
    private readonly Material _material;

    public Camera(Material material)
    {
        _material = material;
    }

    public void Render()
    {
        _material.View = Transform.Matrix;
    }
}