uniform sampler2D texture;
uniform float time;

void main(void)
{
	vec2 texCoord = gl_TexCoord[0].xy;

	vec4 color = texture2D(texture, texCoord).rgba;

	float f = -10;
	float g = 10 + 1;
	float h = -abs(texCoord.x - texCoord.y);

	vec4 help = vec4(-cos(10*(f+g)*h),-cos(10*(f+g)*h),-cos(10*(f+g)*h),0);

	help = help * 10;

	gl_FragColor = color + help;
}