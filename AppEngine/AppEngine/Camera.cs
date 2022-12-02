using System;
using System.Numerics;
using Maths;

namespace AppEngine;

public class Camera
{
    public Transform Transform = new Transform();
    public float FieldOfViewDegrees = 90f;
    public float NearPlaneDistance = 0.01f;
    public float FarPlaneDistance = 1000f;
    private readonly Material _material;
    private readonly Window _window;
    public float gravity = -9.8f;

    public Camera(Material material, Window window)
    {
        _material = material;
        _window = window;
    }

    public void Render()
    {
        _material.View = Transform.Matrix.Invert();
        _material.Projection = Matrix.Perspective(FieldOfViewDegrees * MathF.PI / 180, _window.AspectRatio, NearPlaneDistance, FarPlaneDistance);
    }

    public void Update()
    {
        
    }
}