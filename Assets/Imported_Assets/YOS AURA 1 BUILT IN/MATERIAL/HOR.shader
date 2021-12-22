// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "YOS/MAGIC CIRCLE 1/BI HOR"
{
	Properties
	{
		_mask("mask", 2D) = "white" {}
		_NOISE("NOISE", 2D) = "white" {}
		_pwermask("pwer mask", Float) = 1
		_GRAD("GRAD", 2D) = "white" {}
		[HDR]_Color0("Color 0", Color) = (1,1,1,0)
		_veltextura("vel textura", Vector) = (1,1,0,0)
		_velnoiseuv("vel noise uv", Vector) = (0.5,1,0,0)
		_scalepowermulnoise("scale power mul noise", Vector) = (2,1,0.2,0)
		_scaletexturex("scale texture x", Vector) = (1,1,0,0)
		_alpha("alpha", Float) = 1
		_scalexmask("scale x mask", Float) = 1
		_OPACITY1("OPACITY", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
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
		uniform float2 _scaletexturex;
		uniform float2 _veltextura;
		uniform float4 _Color0;
		uniform sampler2D _mask;
		uniform float3 _scalepowermulnoise;
		uniform float2 _velnoiseuv;
		uniform float _scalexmask;
		uniform float _pwermask;
		uniform float _alpha;
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


		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float temp_output_49_0 = ( _veltextura.x * _Time.y );
			float2 appendResult86 = (float2(temp_output_49_0 , 0.0));
			float2 uv_TexCoord61 = i.uv_texcoord * _scaletexturex + appendResult86;
			float2 appendResult40 = (float2(temp_output_49_0 , ( -_veltextura.y * _Time.y )));
			float2 uv_TexCoord21 = i.uv_texcoord + appendResult40;
			float2 appendResult35 = (float2(temp_output_49_0 , ( _veltextura.y * _Time.y )));
			float2 uv_TexCoord33 = i.uv_texcoord + appendResult35;
			float ifLocalVar23 = 0;
			if( i.uv_texcoord.y > 0.5 )
				ifLocalVar23 = frac( (0.0 + (uv_TexCoord21.y - 0.0) * (2.0 - 0.0) / (1.0 - 0.0)) );
			else if( i.uv_texcoord.y == 0.5 )
				ifLocalVar23 = 0.5;
			else if( i.uv_texcoord.y < 0.5 )
				ifLocalVar23 = frac( (2.0 + (uv_TexCoord33.y - 0.0) * (0.0 - 2.0) / (1.0 - 0.0)) );
			float2 appendResult46 = (float2(uv_TexCoord61.x , ifLocalVar23));
			float4 tex2DNode10 = tex2D( _NOISE, ( appendResult46 + float2( 0,0 ) ) );
			float2 temp_cast_0 = (tex2DNode10.r).xx;
			o.Emission = ( tex2D( _GRAD, temp_cast_0 ) * _Color0 ).rgb;
			float temp_output_64_0 = ( _velnoiseuv.x * _Time.y );
			float2 appendResult88 = (float2(temp_output_64_0 , 0.0));
			float2 uv_TexCoord78 = i.uv_texcoord + appendResult88;
			float2 appendResult67 = (float2(temp_output_64_0 , ( -_velnoiseuv.y * _Time.y )));
			float2 uv_TexCoord73 = i.uv_texcoord + appendResult67;
			float2 appendResult68 = (float2(temp_output_64_0 , ( _velnoiseuv.y * _Time.y )));
			float2 uv_TexCoord69 = i.uv_texcoord + appendResult68;
			float ifLocalVar76 = 0;
			if( uv_TexCoord78.y > 0.5 )
				ifLocalVar76 = frac( (0.0 + (uv_TexCoord73.y - 0.0) * (2.0 - 0.0) / (1.0 - 0.0)) );
			else if( uv_TexCoord78.y == 0.5 )
				ifLocalVar76 = 0.5;
			else if( uv_TexCoord78.y < 0.5 )
				ifLocalVar76 = frac( (2.0 + (uv_TexCoord69.y - 0.0) * (0.0 - 2.0) / (1.0 - 0.0)) );
			float4 appendResult79 = (float4(uv_TexCoord78.x , ifLocalVar76 , 0.0 , 0.0));
			float simplePerlin2D58 = snoise( appendResult79.xy*_scalepowermulnoise.x );
			simplePerlin2D58 = simplePerlin2D58*0.5 + 0.5;
			float2 appendResult93 = (float2(_scalexmask , 0.0));
			float2 uv_TexCoord91 = i.uv_texcoord * appendResult93;
			float2 uv_TexCoord55 = i.uv_texcoord + float2( 0,1 );
			float temp_output_53_0 = frac( (2.0 + (uv_TexCoord55.y - 0.0) * (0.0 - 2.0) / (1.0 - 0.0)) );
			float ifLocalVar56 = 0;
			if( i.uv_texcoord.y <= 0.5 )
				ifLocalVar56 = temp_output_53_0;
			else
				ifLocalVar56 = frac( (0.0 + (i.uv_texcoord.y - 0.0) * (2.0 - 0.0) / (1.0 - 0.0)) );
			float2 appendResult57 = (float2(uv_TexCoord91.x , ifLocalVar56));
			float4 temp_cast_3 = (_pwermask).xxxx;
			float4 temp_cast_4 = (0.0).xxxx;
			float4 temp_cast_5 = (1.0).xxxx;
			float4 clampResult99 = clamp( ( ( tex2DNode10.r * pow( tex2D( _mask, ( ( _scalepowermulnoise.z * pow( simplePerlin2D58 , _scalepowermulnoise.y ) ) + appendResult57 ) ) , temp_cast_3 ) * _alpha ) * _OPACITY1 ) , temp_cast_4 , temp_cast_5 );
			o.Alpha = clampResult99.r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18100
249;248;999;763;-984.6119;166.14;1.316108;True;False
Node;AmplifyShaderEditor.Vector2Node;77;-3035.259,-1021.961;Inherit;False;Property;_velnoiseuv;vel noise uv;6;0;Create;True;0;0;False;0;False;0.5,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.NegateNode;63;-2780.353,-1131.743;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;62;-2838.095,-808.6579;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;64;-2647.705,-989.2831;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;-2576.878,-815.5323;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;66;-2537.008,-1161.99;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;67;-2398.15,-1197.735;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;68;-2385.775,-917.2697;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;47;-2374.772,324.9297;Inherit;False;Property;_veltextura;vel textura;5;0;Create;True;0;0;False;0;False;1,1;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;73;-2156.962,-1257.693;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;69;-2207.048,-936.5174;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,1;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;70;-1855.09,-924.1441;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;2;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;72;-1776.239,-1222.799;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;38;-2389.608,550.2334;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NegateNode;37;-2119.865,215.1481;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;88;-1355.715,-1342.576;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;78;-1141.741,-1346.751;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FractNode;71;-1534.777,-940.4466;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;75;-2178.479,-1383.396;Inherit;False;Constant;_Float0;Float 0;6;0;Create;True;0;0;False;0;False;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;74;-1286,-1095.328;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;-1940.217,356.6081;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-1916.39,531.359;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;-1914.52,124.9017;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;76;-804.5162,-1106.911;Inherit;True;False;5;0;FLOAT;0;False;1;FLOAT;0.5;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;54;-302.0056,781.3355;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;55;-365.8897,1113.432;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,1;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;40;-1737.662,149.1561;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;35;-1725.287,429.6216;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;21;-1496.474,89.19826;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;94;368.7725,627.3492;Inherit;False;Property;_scalexmask;scale x mask;11;0;Create;True;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;79;-393.4138,-1171.184;Inherit;True;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TFHCRemapNode;51;-77.51988,822.476;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;82;-545.4731,-739.7864;Inherit;False;Property;_scalepowermulnoise;scale power mul noise;8;0;Create;True;0;0;False;0;False;2,1,0.2;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TFHCRemapNode;50;-13.93265,1125.806;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;2;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;33;-1546.56,410.374;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,1;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;58;-94.29615,-1046.785;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;52;182.6528,688.8457;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;43;-1530.526,-431.6194;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;45;-1517.991,-36.5043;Inherit;False;Constant;_Float1;Float 1;5;0;Create;True;0;0;False;0;False;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;93;541.1885,660.5062;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TFHCRemapNode;31;-1194.603,422.7472;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;2;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;29;-1115.752,124.0929;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;53;325.6494,1180.799;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;85;-515.0728,-539.9866;Inherit;False;Property;_scaletexturex;scale texture x;9;0;Create;True;0;0;False;0;False;1,1;1,3;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.FractNode;30;-702.0565,94.74203;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;86;-470.9611,-399.5533;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FractNode;32;-874.29,406.4447;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;81;189.0274,-936.0861;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;2.17;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;91;591.5867,392.5979;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ConditionalIfNode;56;138.1521,256.7116;Inherit;True;False;5;0;FLOAT;0;False;1;FLOAT;0.5;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;61;-251.9538,-578.9481;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ConditionalIfNode;23;-308.2816,-119.8822;Inherit;True;False;5;0;FLOAT;0;False;1;FLOAT;0.5;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;57;502.0247,45.06586;Inherit;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;83;347.0656,-646.3016;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;7;603.7108,-218.7658;Inherit;True;Property;_mask;mask;0;0;Create;True;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SimpleAddOpNode;90;762.0869,23.20272;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;46;197.1064,-363.6136;Inherit;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;84;673.7269,-465.1865;Inherit;True;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;11;555.1187,-884.403;Inherit;True;Property;_NOISE;NOISE;1;0;Create;True;0;0;False;0;False;None;None;False;white;Auto;Texture2D;-1;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.RangedFloatNode;9;1131.444,157.0332;Inherit;False;Property;_pwermask;pwer mask;2;0;Create;True;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;5;918.5309,-101.7311;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;8;1344.91,-8.616921;Inherit;False;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;89;1504.284,259.024;Inherit;False;Property;_alpha;alpha;10;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;10;723.1891,-675.6198;Inherit;True;Property;_TextureSample1;Texture Sample 1;2;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;1602.461,-49.77854;Inherit;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;100;1711.517,428.8282;Inherit;False;Property;_OPACITY1;OPACITY;12;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;16;1246.249,-670.0244;Inherit;True;Property;_GRAD;GRAD;3;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;96;1880.355,273.5293;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;97;1934.846,543.7921;Inherit;False;Constant;_Float5;Float 2;12;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;98;1876.846,449.7921;Inherit;False;Constant;_Float4;Float 2;12;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;19;1338.614,-449.1364;Inherit;False;Property;_Color0;Color 0;4;1;[HDR];Create;True;0;0;False;0;False;1,1,1,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;99;2059.846,375.792;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;80;-404.3306,-858.4202;Inherit;False;Property;_scalenoise;scale noise;7;0;Create;True;0;0;False;0;False;2.45;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;1616.649,-446.2242;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;95;1889,-182.6863;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;YOS/MAGIC CIRCLE 1/BI HOR;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;63;0;77;2
WireConnection;64;0;77;1
WireConnection;64;1;62;0
WireConnection;65;0;77;2
WireConnection;65;1;62;0
WireConnection;66;0;63;0
WireConnection;66;1;62;0
WireConnection;67;0;64;0
WireConnection;67;1;66;0
WireConnection;68;0;64;0
WireConnection;68;1;65;0
WireConnection;73;1;67;0
WireConnection;69;1;68;0
WireConnection;70;0;69;2
WireConnection;72;0;73;2
WireConnection;37;0;47;2
WireConnection;88;0;64;0
WireConnection;78;1;88;0
WireConnection;71;0;70;0
WireConnection;74;0;72;0
WireConnection;49;0;47;1
WireConnection;49;1;38;0
WireConnection;41;0;47;2
WireConnection;41;1;38;0
WireConnection;39;0;37;0
WireConnection;39;1;38;0
WireConnection;76;0;78;2
WireConnection;76;1;75;0
WireConnection;76;2;74;0
WireConnection;76;3;75;0
WireConnection;76;4;71;0
WireConnection;40;0;49;0
WireConnection;40;1;39;0
WireConnection;35;0;49;0
WireConnection;35;1;41;0
WireConnection;21;1;40;0
WireConnection;79;0;78;1
WireConnection;79;1;76;0
WireConnection;51;0;54;2
WireConnection;50;0;55;2
WireConnection;33;1;35;0
WireConnection;58;0;79;0
WireConnection;58;1;82;1
WireConnection;52;0;51;0
WireConnection;93;0;94;0
WireConnection;31;0;33;2
WireConnection;29;0;21;2
WireConnection;53;0;50;0
WireConnection;30;0;29;0
WireConnection;86;0;49;0
WireConnection;32;0;31;0
WireConnection;81;0;58;0
WireConnection;81;1;82;2
WireConnection;91;0;93;0
WireConnection;56;0;43;2
WireConnection;56;1;45;0
WireConnection;56;2;52;0
WireConnection;56;3;53;0
WireConnection;56;4;53;0
WireConnection;61;0;85;0
WireConnection;61;1;86;0
WireConnection;23;0;43;2
WireConnection;23;1;45;0
WireConnection;23;2;30;0
WireConnection;23;3;45;0
WireConnection;23;4;32;0
WireConnection;57;0;91;1
WireConnection;57;1;56;0
WireConnection;83;0;82;3
WireConnection;83;1;81;0
WireConnection;90;0;83;0
WireConnection;90;1;57;0
WireConnection;46;0;61;1
WireConnection;46;1;23;0
WireConnection;84;0;46;0
WireConnection;5;0;7;0
WireConnection;5;1;90;0
WireConnection;8;0;5;0
WireConnection;8;1;9;0
WireConnection;10;0;11;0
WireConnection;10;1;84;0
WireConnection;13;0;10;1
WireConnection;13;1;8;0
WireConnection;13;2;89;0
WireConnection;16;1;10;1
WireConnection;96;0;13;0
WireConnection;96;1;100;0
WireConnection;99;0;96;0
WireConnection;99;1;98;0
WireConnection;99;2;97;0
WireConnection;18;0;16;0
WireConnection;18;1;19;0
WireConnection;95;2;18;0
WireConnection;95;9;99;0
ASEEND*/
//CHKSM=2D65E6486A44BE29DF50E95D4836DD2B2C17FE3F