#version 330 core

in vec4 vertexColor;
in vec2 texCoord;

out vec4 color;

uniform vec4 _color;
uniform sampler2D _texture;

void main()
{
    color = texture(_texture, texCoord);
}