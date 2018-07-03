Shader "Unlit/Water"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_ScreenWidth ("_ScreenWidth", Range(0,1920)) = 1920
		_Color ("_Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			float _ScreenWidth;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			#define PI 3.14159265358979323846
			float rand(float2 c){
				return frac(sin(dot(c.xy ,float2(12.9898,78.233))) * 43758.5453);
			}

			float noise(float2 p, float freq ){
				float unit = _ScreenWidth/freq;
				float2 ij = floor(p/unit);
				float2 xy = fmod(p,unit)/unit;
				//xy = 3.*xy*xy-2.*xy*xy*xy;
				xy = .5*(1.-cos(PI*xy));
				float a = rand((ij+float2(0.,0.)));
				float b = rand((ij+float2(1.,0.)));
				float c = rand((ij+float2(0.,1.)));
				float d = rand((ij+float2(1.,1.)));
				float x1 = lerp(a, b, xy.x);
				float x2 = lerp(c, d, xy.x);
				return lerp(x1, x2, xy.y);
			}

			float pNoise(float2 p, int res){
				float persistance = .5;
				float n = 0.;
				float normK = 0.;
				float f = 4.;
				float amp = 1.;
				int iCount = 0;
				for (int i = 0; i<50; i++){
				n+=amp*noise(p, f);
				f*=2.;
				normK+=amp;
				amp*=persistance;
				if (iCount == res) break;
				iCount++;
				}
				float nf = n/normK;
				return nf*nf*nf*nf;
			}

			// Simplex 2D noise
			//
			float3 permute(float3 x) { 
				return fmod(((x*34.0)+1.0)*x, 289.0); 
			}

			float snoise(float2 v) {
			  const float4 C = float4(0.211324865405187, 0.366025403784439,
			           -0.577350269189626, 0.024390243902439);
			  float2 i  = floor(v + dot(v, C.yy) );
			  float2 x0 = v -   i + dot(i, C.xx);
			  float2 i1;
			  i1 = (x0.x > x0.y) ? float2(1.0, 0.0) : float2(0.0, 1.0);
			  float4 x12 = x0.xyxy + C.xxzz;
			  x12.xy -= i1;
			  i = fmod(i, 289.0);
			  float3 p = permute( permute( i.y + float3(0.0, i1.y, 1.0 ))
			  + i.x + float3(0.0, i1.x, 1.0 ));
			  float3 m = max(0.5 - float3(dot(x0,x0), dot(x12.xy,x12.xy),
			    dot(x12.zw,x12.zw)), 0.0);
			  m = m*m ;
			  m = m*m ;
			  float3 x = 2.0 * frac(p * C.www) - 1.0;
			  float3 h = abs(x) - 0.5;
			  float3 ox = floor(x + 0.5);
			  float3 a0 = x - ox;
			  m *= 1.79284291400159 - 0.85373472095314 * ( a0*a0 + h*h );
			  float3 g;
			  g.x  = a0.x  * x0.x  + h.x  * x0.y;
			  g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			  return 130.0 * dot(m, g);
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv)*_Color;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				float2 rotation = float2(_SinTime[0],_CosTime[0])/_ScreenWidth;
				return col*noise(i.uv+rotation,1920);
			}
			ENDCG
		}
	}
}
