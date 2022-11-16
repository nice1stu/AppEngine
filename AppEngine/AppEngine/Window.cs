using GLFW;
using static OpenGL.Gl;

namespace AppEngine;

public class Window
{
    public bool ShouldClose => Glfw.WindowShouldClose(window);
    private GLFW.Window window;
    public Window()
    {
        Glfw.Init();
        Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
        Glfw.WindowHint(Hint.ContextVersionMajor, 3);
        Glfw.WindowHint(Hint.ContextVersionMinor, 3);
        Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
        Glfw.WindowHint(Hint.OpenglForwardCompatible, Constants.True);
        Glfw.WindowHint(Hint.Doublebuffer, Constants.True);

        window = Glfw.CreateWindow(800, 600, "AppEngine", Monitor.None, GLFW.Window.None);
        Glfw.MakeContextCurrent(window);
        Import(Glfw.GetProcAddress);
        
        Glfw.SetKeyCallback(window, OnKeyCallback);
    }

    public bool GetKey(Keys key)
    {
        return  Glfw.GetKey(window, key) != InputState.Release;
    }

    public void BeginRender()
    {
        // update input
        Glfw.PollEvents();
        bool isSpacePressed = GetKey(Keys.Space);
    
        // update your game
    
        // render
        if (isSpacePressed)
        {
            glClearColor(.2f, .05f,.2f, 1f);
        }else
            glClearColor(0, 0,0, 1);
        glClear(GL_COLOR_BUFFER_BIT);
    }

    public void EndRender()
    {
        Glfw.SwapBuffers(window);
    }
    void OnKeyCallback(System.IntPtr window, Keys key, int scanCode, InputState state, ModifierKeys mods) //key events
    {
        if (key == Keys.Escape && state == InputState.Press)
        {
            Glfw.SetWindowShouldClose(Glfw.CurrentContext, true);
        }
    }
}