shader_type spatial;

render_mode unshaded, cull_disabled;

uniform bool active = false;
uniform vec4 flash_color: source_color = vec4(1.0, 1.0, 1.0, 1.0);
uniform sampler2D tex: source_color;

void fragment()
{
	// laod the origial color of each pixel (from the sprite texture)
	vec4 originalColor = texture(tex, UV);
	// set the alpha the same, without this all the invisible bg turns flash_color
	ALPHA = originalColor.a;
	
	// set the color 
	vec4 color = active ? flash_color : originalColor;
	ALBEDO = vec3(color.r, color.g, color.b);
}