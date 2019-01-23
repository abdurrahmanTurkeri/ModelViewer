Shader "Custom/OutlineShader" {
	Properties {

		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_OutlineWidth("Outline Width", Range(1.0, 5.0)
	}

	CGINCLUDE
	#include "UnityCG.cginc"

	struct appdata{
		float4 vertex: POSITION;
		float3 normal: NORMAL;
	};

	struct v2f {
		float4 position: POSITION;
		float4 color: COLOR;
		float3 normal: NORMAL;
	};

	float _OutlineWidth;
	float4 _OutlineColor;

	v2f vertexShader(appdata i){

		i.vertex.xyz *= _OutlineWidth;
		v2f o;
		o.position = UnityObjectToClipPos(i.vertex);
		o.color = _OutlineColor;
		return o;
	};

	ENDCG
	SubShader {
		Tags { "Queue"="Transparent" }
		LOD 200

		Pass{

			Zwrite off

			CGPROGRAM

				#pragma vertex vertexShader
				#pragma fragment fragShader

				half4 fragShader(v2f i):COLOR{
					return i.color;
				}

			ENDCG
		}

	}
	FallBack "Diffuse"
}
