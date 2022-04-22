Shader "Black Hole" {
	Properties {
		_MainTex    ("Main", 2D) = "white" {}
		_Center     ("Center", Vector) = (0.5, 0.5, 0, 0)
		_Distortion ("Distortion", Float) = -2
		_DarkRange  ("Dark Range", Float) = 0.1
		_Warp       ("Warp", Float) = 30
	}
	CGINCLUDE
		#include "UnityCG.cginc"
		sampler2D _MainTex;
		float2 _Center;
		float _Distortion, _DarkRange, _Warp;
		float4 frag (v2f_img input) : SV_TARGET
		{
			float2 uv = input.uv;
			float2 center = _Center * _ScreenParams.xy;
			float dist = distance(center, uv * _ScreenParams.xy);

			float2 warp = normalize(_Center - uv) * pow(dist, _Distortion) * _Warp;
			warp.y = -warp.y;
			uv = uv + warp;

			float light = saturate(_DarkRange * dist - 1.5);
			return tex2D(_MainTex, uv) * light;
		}
	ENDCG
	SubShader {
		ZTest Off Cull Off ZWrite Off Blend Off Fog { Mode Off }
		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			ENDCG
		}
	}
	FallBack Off
}