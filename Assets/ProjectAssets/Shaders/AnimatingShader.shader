
Shader "Custom/AnimatingShader" {
	SubShader{
		Pass{
		CGPROGRAM

#pragma vertex vert
#pragma fragment frag
#pragma target 3.0

#include "UnityCG.cginc"

	float4 vert(appdata_base v) : POSITION
	{
		return UnityObjectToClipPos(v.vertex);
	}

	fixed4 frag(float4 sp:WPOS) : COLOR
	{
		const float PI = 3.14159;
		const float redOffset = 0;
		const float greenOffset = PI / 3;
		const float blueOffset = PI / 3 * 2;
		const float redSpeed = 2;
		const float greenSpeed = 3;
		const float blueSpeed = 4;

		fixed4 screenCoord = sp - _ScreenParams / 2;
		fixed angle = acos(screenCoord.x / sqrt(screenCoord.x*screenCoord.x + screenCoord.y*screenCoord.y));
		fixed lerpFactor = angle / (PI);

		fixed red = cos(angle + _Time.y * redSpeed + redOffset);
		fixed green = cos(angle + _Time.y * greenSpeed + greenOffset);
		fixed blue = cos(angle + _Time.y * blueSpeed + blueOffset);
		return fixed4(red, green, blue, 1.0);
	}

		ENDCG
	}
	}
}