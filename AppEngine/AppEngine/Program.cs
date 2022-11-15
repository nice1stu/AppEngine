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

Window window = Glfw.CreateWindow(800, 600, "AppEngine", Monitor.None, Window.None);

Glfw.MakeContextCurrent(window);

Import(Glfw.GetProcAddress);

while (true)
{
    Glfw.PollEvents();
}