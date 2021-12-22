// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "YOS/MAGIC CIRCLE 1/BI VER2"
{
	Properties
	{
		_NOISE("NOISE", 2D) = "white" {}
		_VECLtextura("VECLtextura", Vector) = (1,0,0,0)
		_VECLnoiseuv("VECLnoiseuv", Vector) = (0,-0.2,0,0)
		_VECVORONOI("VECVORONOI", Vector) = (0,-0.2,0,0)
		_GRAD("GRAD", 2D) = "white" {}
		[HDR]_Color0("Color 0", Color) = (1,1,1,0)
		_lerpuvnoise("lerp uv noise", Float) = 0
		_scalenoiseuv("scalenoiseuv", Float) = 1
		_powertexturenoise("power texture noise", Float) = 1
		_multiplytexturenoise("multiply texture noise", Float) = 1
		_mask("mask", 2D) = "white" {}
		_SCALEVORONIO("SCALE VORONIO", Float) = 4.12
		_DISTORTVORONIO("DISTORT VORONIO", Float) = 1
		_OPACITY1("OPACITY", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit alpha:fade keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _GRAD;
		uniform sampler2D _NOISE;
		uniform float2 _VECLtextura;
		uniform float4 _NOISE_ST;
		uniform float2 _VECLnoiseuv;
		uniform float _scalenoiseuv;
		uniform float _SCALEVORONIO;
		uniform float2 _VECVORONOI;
		uniform float _DISTORTVORONIO;
		uniform float _lerpuvnoise;
		uniform float _powertexturenoise;
		uniform float _multiplytexturenoise;
		uniform sampler2D _mask;
		uniform float4 _mask_ST;
		uniform float4 _Color0;
		uniform float _OPACITY1;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		float2 voronoihash43( float2 p )
		{
			
			p = float2( dot( p, float2( 127.1, 311.7 ) ), dot( p, float2( 269.5, 183.3 ) ) );
			return frac( sin( p ) *43758.5453);
		}


		float voronoi43( float2 v, float time, inout float2 id, float smoothness )
		{
			float2 n = floor( v );
			float2 f = frac( v );
			float F1 = 8.0;
			float F2 = 8.0; float2 mr = 0; float2 mg = 0;
			for ( int j = -1; j <= 1; j++ )
			{
				for ( int i = -1; i <= 1; i++ )
			 	{
			 		float2 g = float2( i, j );
			 		float2 o = voronoihash43( n + g );
					o = ( sin( time + o * 6.2831 ) * 0.5 + 0.5 ); float2 r = g - f + o;
					float d = 0.5 * dot( r, r );
			 		if( d<F1 ) {
			 			F2 = F1;
			 			F1 = d; mg = g; mr = r; id = o;
			 		} else if( d<F2 ) {
			 			F2 = d;
			 		}
			 	}
			}
			return F1;
		}


		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv0_NOISE = i.uv_texcoord * _NOISE_ST.xy + _NOISE_ST.zw;
			float2 panner14 = ( 1.0 * _Time.y * _VECLtextura + uv0_NOISE);
			float2 panner31 = ( 1.0 * _Time.y * _VECLnoiseuv + uv0_NOISE);
			float simplePerlin2D27 = snoise( panner31*_scalenoiseuv );
			simplePerlin2D27 = simplePerlin2D27*0.5 + 0.5;
			float time43 = 0.0;
			float2 panner45 = ( 1.0 * _Time.y * _VECVORONOI + uv0_NOISE);
			float2 coords43 = ( panner45 + ( simplePerlin2D27 * _DISTORTVORONIO ) ) * _SCALEVORONIO;
			float2 id43 = 0;
			float voroi43 = voronoi43( coords43, time43,id43, 0 );
			float2 temp_cast_0 = (( simplePerlin2D27 * voroi43 )).xx;
			float2 lerpResult32 = lerp( panner14 , temp_cast_0 , _lerpuvnoise);
			float clampResult39 = clamp( ( ( pow( tex2D( _NOISE, lerpResult32 ).r , _powertexturenoise ) * voroi43 ) * _multiplytexturenoise ) , 0.0 , 1.0 );
			float2 uv_mask = i.uv_texcoord * _mask_ST.xy + _mask_ST.zw;
			float temp_output_41_0 = ( clampResult39 * tex2D( _mask, uv_mask ).r );
			float2 temp_cast_1 = (temp_output_41_0).xx;
			o.Emission = ( tex2D( _GRAD, temp_cast_1 ) * _Color0 ).rgb;
			float clampResult57 = clamp( ( temp_output_41_0 * _OPACITY1 ) , 0.0 , 1.0 );
			o.Alpha = clampResult57;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18100
249;242;999;769;-965.6165;247.2332;1;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;29;-1112.72,-523.7939;Inherit;False;0;11;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;30;-977.232,-366.587;Inherit;False;Property;_VECLnoiseuv;VECLnoiseuv;2;0;Create;True;0;0;False;0;False;0,-0.2;1,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PannerNode;31;-688.2318,-458.3804;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-808.5197,-10.10388;Inherit;False;Property;_scalenoiseuv;scalenoiseuv;7;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;47;-963.5651,158.8915;Inherit;False;Property;_VECVORONOI;VECVORONOI;3;0;Create;True;0;0;False;0;False;0,-0.2;1,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;52;-704.2986,16.14984;Inherit;False;Property;_DISTORTVORONIO;DISTORT VORONIO;12;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;27;-645.0278,-251.2567;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;51;-553.2986,-15.85016;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;45;-737.5566,131.4414;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;50;-405.2998,23.65367;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;44;-751.9652,260.2907;Inherit;False;Property;_SCALEVORONIO;SCALE VORONIO;11;0;Create;True;0;0;False;0;False;4.12;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;15;-805.2753,-632.6041;Inherit;False;Property;_VECLtextura;VECLtextura;1;0;Create;True;0;0;False;0;False;1,0;1,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;12;-940.7628,-791.0177;Inherit;False;0;11;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VoronoiNode;43;-363.5379,242.9264;Inherit;True;0;0;1;0;1;False;1;False;False;4;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;4;False;3;FLOAT;0;False;2;FLOAT;0;FLOAT;1
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-291.653,-134.709;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-413.9735,-332.8538;Inherit;False;Property;_lerpuvnoise;lerp uv noise;6;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;14;-516.275,-725.6041;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;11;-325.8078,-924.4075;Inherit;True;Property;_NOISE;NOISE;0;0;Create;True;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.LerpOp;32;-210.8412,-357.0092;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;10;-37.57447,-503.4159;Inherit;True;Property;_TextureSample1;Texture Sample 1;2;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;36;50.88123,-98.40553;Inherit;False;Property;_powertexturenoise;power texture noise;8;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;35;266.4037,-187.903;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;37;146.1244,121.6293;Inherit;False;Property;_multiplytexturenoise;multiply texture noise;9;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;-79.3046,147.8148;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;432.0297,49.31207;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;40;670.7449,198.547;Inherit;True;Property;_mask;mask;10;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;39;620.4612,30.48274;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;58;1309.171,244.7172;Inherit;False;Property;_OPACITY1;OPACITY;13;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;1216.345,118.5471;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;16;804.5709,-620.1165;Inherit;True;Property;_GRAD;GRAD;4;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;1478.009,89.41827;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;55;1532.5,359.681;Inherit;False;Constant;_Float5;Float 2;12;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;56;1474.5,265.681;Inherit;False;Constant;_Float4;Float 2;12;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;19;912.0051,-338.0689;Inherit;False;Property;_Color0;Color 0;5;1;[HDR];Create;True;0;0;False;0;False;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;1146.571,-393.0941;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;57;1657.5,191.681;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;53;1865.446,-251.4003;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;YOS/MAGIC CIRCLE 1/BI VER2;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;31;0;29;0
WireConnection;31;2;30;0
WireConnection;27;0;31;0
WireConnection;27;1;34;0
WireConnection;51;0;27;0
WireConnection;51;1;52;0
WireConnection;45;0;29;0
WireConnection;45;2;47;0
WireConnection;50;0;45;0
WireConnection;50;1;51;0
WireConnection;43;0;50;0
WireConnection;43;2;44;0
WireConnection;48;0;27;0
WireConnection;48;1;43;0
WireConnection;14;0;12;0
WireConnection;14;2;15;0
WireConnection;32;0;14;0
WireConnection;32;1;48;0
WireConnection;32;2;33;0
WireConnection;10;0;11;0
WireConnection;10;1;32;0
WireConnection;35;0;10;1
WireConnection;35;1;36;0
WireConnection;49;0;35;0
WireConnection;49;1;43;0
WireConnection;38;0;49;0
WireConnection;38;1;37;0
WireConnection;39;0;38;0
WireConnection;41;0;39;0
WireConnection;41;1;40;1
WireConnection;16;1;41;0
WireConnection;54;0;41;0
WireConnection;54;1;58;0
WireConnection;18;0;16;0
WireConnection;18;1;19;0
WireConnection;57;0;54;0
WireConnection;57;1;56;0
WireConnection;57;2;55;0
WireConnection;53;2;18;0
WireConnection;53;9;57;0
ASEEND*/
//CHKSM=E29382A448219A299A9D8D77C87D370D267206CC