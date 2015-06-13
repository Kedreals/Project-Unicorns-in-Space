uniform sampler2D texture;

void main(void)
{
	vec2 texCoord = gl_TexCoord[0].xy;

	vec4 color = texture2D(texture, texCoord).rgba;

	vec4 help = vec4(1,1,1,0);

	gl_FragColor = (color + help) * (1.0/length(color + help));
}