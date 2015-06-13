uniform sampler2D texture;
uniform sampler2D overlay;

uniform float time;

void main(void)
{
	vec2 texCoord = gl_TexCoord[0].xy;

	float help = sin(time) * 0.5 + 0.5;
	
	gl_FragColor = (1-help) * texture2D(texture, texCoord).rgba + (help) * texture2D(overlay, texCoord).rgba;
}