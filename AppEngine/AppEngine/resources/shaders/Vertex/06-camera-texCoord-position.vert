#version 330 core

//vertex attributes
layout (location = 0) in vec3 position;
layout (location = 1) in vec4 color;

//uniforms
uniform mat4 _model;
uniform mat4 _view;

//vertex outputs
out vec4 vertexColor;
out vec2 texCoord;

void main()
{
    gl_Position = _view * _model * vec4(position, 1.0);
    vertexColor = color;
    texCoord = vec2(position);
}