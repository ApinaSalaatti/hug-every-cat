Shader "Custom/TrapWarningArea" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB) Alpha (A)", 2D) = "white"
		_NoiseTex ("Noise texture", 2D) = "white"
	}
	SubShader {
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		//ZWrite On Lighting Off Cull Off Fog { Mode Off } Blend One Zero
		Blend SrcAlpha OneMinusSrcAlpha
		
		GrabPass { "_GrabTexture" }
	
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			
			sampler2D _GrabTexture;
			sampler2D _MainTex;
			sampler2D _NoiseTex;
			float4 _Color;
			
			struct vin {
				float4 vertex : POSITION;
				float4 color: COLOR;
				float2 texcoord: TEXCOORD0;
			};
			
			struct v2f {
				float4 vertex : POSITION;
				fixed4 color: COLOR;
				float2 texcoord : TEXCOORD0;
				float4 uvgrab : TEXCOORD1;
			};
			
			v2f vert(vin input) {
				v2f output;
				output.vertex = mul(UNITY_MATRIX_MVP, input.vertex);
				output.uvgrab = ComputeGrabScreenPos(output.vertex);
				output.color = input.color;
				output.texcoord = input.texcoord;
				return output;
			}
			
			half4 frag(v2f input) : COLOR {
				float time = _Time[1];
				float2 waveDisplacement = (-time, time);
				fixed4 col = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(input.uvgrab));
				
				input.texcoord += waveDisplacement;
				fixed4 noiseCol = tex2D(_MainTex, input.texcoord);
				
				return _Color * noiseCol;
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
