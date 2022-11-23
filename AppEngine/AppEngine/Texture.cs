using System;
using System.Buffers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using static OpenGL.Gl;

namespace AppEngine;

public class Texture
{
    /// <summary>
    /// Stores a reference to the GL Texture Object
    /// </summary>
    private uint textureObject;
    
    /// <summary>
    /// Prepares a Texture for rendering with OpenGL:
    /// - loads the texture to Memory
    /// - creates a GL Texture object
    /// - buffers the texture to the GPU
    /// - generates mip-map levels
    /// - clears up the texture from the RAM
    /// </summary>
    /// <param name="texturePath">The path from which to load the texture.</param>
    public unsafe Texture(string texturePath)
    {
        Configuration customConfig = Configuration.Default.Clone();
        customConfig.PreferContiguousImageBuffers = true;
        
        using Image<Rgb24> image = Image.Load<Rgb24>(customConfig, texturePath);
        image.Mutate(context => context.Flip(FlipMode.Vertical).Resize(512,512));
        textureObject = glGenTexture();
        glBindTexture(GL_TEXTURE_2D, textureObject);
        
        if (!image.DangerousTryGetSinglePixelMemory(out Memory<Rgb24> memory))
        {
            throw new Exception(
                "This can only happen with multi-GB images or when PreferContiguousImageBuffers is not set to true.");
        }

        using MemoryHandle pinHandle = memory.Pin();
        void* ptr = pinHandle.Pointer;
        glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, image.Width, image.Height, 0, GL_RGB, GL_UNSIGNED_BYTE, ptr);
        glGenerateMipmap(GL_TEXTURE_2D);
    }

    /// <summary>
    /// Binds the texture to the active GL Texture ren.
    /// Don't forget to use `glActiveTexture(GL_TEXTURE0)` before.
    /// </summary>
    public void Use()
    {
        glBindTexture(GL_TEXTURE_2D, textureObject);
    }
}