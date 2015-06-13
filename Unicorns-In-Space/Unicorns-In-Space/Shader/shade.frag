uniform sampler2D texture;
uniform vec4 addedColor;
uniform float percent;

void main(void)
{
	vec2 texCoord = gl_TexCoord[0].xy;

	vec4 color = texture2D(texture, texCoord).rgba;

	float f = percent/100.0f;

	gl_FragColor = (1-f)color + f*addedColor;
}