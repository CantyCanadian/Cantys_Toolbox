﻿///====================================================================================================
///
///     BlackWhite by
///     - CantyCanadian
///		- ellioman
///		- AlanZucconi
///
///====================================================================================================
Shader "Custom/PostProcess/BlackWhite"
{
	Properties
	{
		[HideInInspector]_MainTex ("Texture", 2D) = "white" {}
		_Blend ("Black & White Blend", Range (0, 1)) = 0
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float2 uv : TEXCOORD0;
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;

			float4 _MainTex_TexelSize;

			float _Blend;

			v2f vert (appdata v)
			{
				v2f o;

				o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;

				return o;
			}

			fixed4 frag (v2f i) : COLOR
			{
				float4 col = tex2D(_MainTex, i.uv);
						
				// The three magic numbers represent the sensitivity of the human eye to the R, G and B components.
				float luminosity = (col.r * 0.3f) + (col.g * 0.59f) + (col.b * 0.11f);
						
				float4 result = col;
				result.rgb = lerp(col.rgb, float3(luminosity, luminosity, luminosity), _Blend);
				return result;
			}
			ENDCG
		}
	}
}
