//2d Textur, die auf der CPU-Seite gesetzt wurde (macht das Sprite automatisch)
uniform sampler2D texture;

//float der auf der CPU-Seite gesetzt wurde (siehe Update in Game.cs)
uniform float time;

//Mainmethode des Shaders
void main(void)
{
	//lese die Texturkoordinate des aktuellen Pixels aus (wird benötigt, um die Farbe aus der Textur zu fischen, dem sich der Pixel befindet)
	vec2 texCoord = gl_TexCoord[0].xy;
	
	//lese die Farbe aus der Textur aus, an dem sich dieser Pixel befindet:
	vec4 color = texture2D(texture, texCoord).rgba;

	color.r = sin(time);
	color.g = cos(time);
	color.b = sin(time) + 3.1415;

	//interpoliere abhängig von t zwischen der normalen Farbe und der ausgegrauten Farbe
	gl_FragColor = color;
}