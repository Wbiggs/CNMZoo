Shader "Custom/CausticsGlass"
{
    Properties
    {
        [Header(Caustics)]
        _Caustics("Caustics Texture", 2D) = "white" {}
        _CausticsLuminanceMaskStrength("Shadow Fade", Float) = 1
        _CausticsFadeRadius("Edge Fade Radius", Float) = 1
        _CausticsFadeStrength("Edge Fade Strength", Float) = 1
        _Speed("Speed", Float) = 0.5
        _Tiling("Tiling", Float) = 10 
        _RGBSplit("RGB Split", Float) = 0.5
        _Intensity("Intensity", Float) = 1
    }

    SubShader
    {
        ZWrite On
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 300

        Tags
        {
            "Queue" = "Transparent" 
            "RenderType" = "Transparent" 
            "RenderPipeline" = "UniversalRenderPipeline"
        }

        Pass
        {
            Name "Base"
            Tags { "LightMode" = "UniversalForward"}
            Cull Back

            HLSLPROGRAM

            #pragma target 5.0

            #pragma vertex vert
            #pragma fragment frag
            
            #pragma multi_compile_fog
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile_fragment _ _SHADOWS_SOFT

            #define _SPECULAR_COLOR

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareOpaqueTexture.hlsl"

            #include "/Noise.hlsl"
            #include "/Caustics.hlsl"

            // the original vertex struct
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;

                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;

                float2 uv : TEXCOORD0;

                float3 positionWS : TEXCOORD1;

                float4 screenPos : TEXCOORD2;
            };

            CBUFFER_START(UnityPerMaterial)
                half4 _Caustics_ST;
            CBUFFER_END

            // sampler2D _Caustics;

            half4x4 _MainLightDirection;
            float _Tiling, _Speed, _RGBSplit, _Intensity, _CausticsLuminanceMaskStrength, _CausticsFadeRadius, _CausticsFadeStrength;

            v2f vert (appdata v)
            {
                v2f o;

                VertexPositionInputs posnInputs = GetVertexPositionInputs(v.vertex);
                VertexNormalInputs normInputs = GetVertexNormalInputs(v.normal);

                //Output
                o.screenPos = ComputeScreenPos(posnInputs.positionCS);
            
                o.vertex =  posnInputs.positionCS;

                o.positionWS = posnInputs.positionWS;

                o.normal = normInputs.normalWS;

                o.uv = TRANSFORM_TEX(v.uv, _Caustics);

                return o;
            }

            real ShadowAten(real3 worldPosition)
            {
                return MainLightRealtimeShadow(TransformWorldToShadowCoord(worldPosition));
            }

            float4 frag(v2f i) : SV_TARGET
            {
                //Screen
                float2 screenUV = (i.screenPos.xy) / i.screenPos.w;

                float2 positionNDC = i.vertex.xy / _ScaledScreenParams.xy;

                // sample scene depth using screen-space coordinates
                #if UNITY_REVERSED_Z
                    real depth = SampleSceneDepth(positionNDC);
                #else
                    real depth = lerp(UNITY_NEAR_CLIP_VALUE, 1, SampleSceneDepth(UV));
                #endif

                float3 positionWS = ComputeWorldSpacePosition(positionNDC, depth, UNITY_MATRIX_I_VP);
                float3 positionOS = TransformWorldToObject(positionWS);

                float boundingBoxMask = all(step(positionOS, 0.5) * (1 - step(positionOS, -0.5)));

                half2 uv = mul(positionWS, _MainLightDirection).xy;
 
                half2 uv1 = Panner(uv, _Speed, 1 / _Tiling);
                half2 uv2 = Panner(uv, 1 * _Speed, -1 / _Tiling);

                half4 tex1 = SampleCaustics(uv1, _RGBSplit / 100);
                half4 tex2 = SampleCaustics(uv2, _RGBSplit / 100);

                half sceneLuminance = ShadowAten(positionWS);
                half luminanceMask = lerp(1, sceneLuminance, _CausticsLuminanceMaskStrength);

                half edgeFadeMask = 1 - saturate((distance(positionOS, 0) - _CausticsFadeRadius) / (1 - _CausticsFadeStrength));

                half4 caustics = min(tex1, tex2) * _Intensity;

                return caustics * boundingBoxMask * luminanceMask * edgeFadeMask;

                // return ShadowAten(positionWS);
            }
            ENDHLSL
        }

    }
}
