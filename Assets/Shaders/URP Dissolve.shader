Shader "Custom/DissolveTopDown"
{
    Properties
    {
        _BaseColor("Base Color", Color) = (1,1,1,1)
        _BaseMap("Base Map", 2D) = "white" {}
        _NoiseMap("Noise Map", 2D) = "white" {}
        _DissolveAmount("Dissolve Amount", Range(0, 1)) = 0
        _NoiseScale("Noise Scale", Float) = 1
        _EdgeWidth("Edge Width", Range(0, 0.2)) = 0.05
        _EdgeColor("Edge Color", Color) = (1,0,0,1)
        _EdgeIntensity("Edge Intensity", Float) = 2
        _MaxWorldHeight("Max World Height", Float) = 10
        _MinWorldHeight("Min World Height", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
            "Queue" = "Geometry"
        }

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                float3 normalOS : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 positionWS : TEXCOORD1;
                float3 normalWS : TEXCOORD2;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);
            TEXTURE2D(_NoiseMap);
            SAMPLER(sampler_NoiseMap);

            CBUFFER_START(UnityPerMaterial)
                float4 _BaseColor;
                float4 _BaseMap_ST;
                float4 _EdgeColor;
                float _DissolveAmount;
                float _MaxWorldHeight;
                float _MinWorldHeight;
                float _NoiseScale;
                float _EdgeWidth;
                float _EdgeIntensity;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                
                VertexPositionInputs positionInputs = GetVertexPositionInputs(IN.positionOS.xyz);
                OUT.positionHCS = positionInputs.positionCS;
                OUT.positionWS = positionInputs.positionWS;
                
                VertexNormalInputs normalInputs = GetVertexNormalInputs(IN.normalOS);
                OUT.normalWS = normalInputs.normalWS;
                
                OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
                
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                float heightRange = _MaxWorldHeight - _MinWorldHeight;
                if (abs(heightRange) < 0.001)
                {
                    heightRange = 1.0;
                }

                float worldHeight = IN.positionWS.y;
                float normalizedHeight = saturate((worldHeight - _MinWorldHeight) / heightRange);
                
                normalizedHeight = 1.0 - normalizedHeight;
                
                float2 noiseUV = IN.positionWS.xz * _NoiseScale;
                float noise = SAMPLE_TEXTURE2D(_NoiseMap, sampler_NoiseMap, noiseUV).r;
                
                float dissolveMask = normalizedHeight + noise * 0.2;
                
                float dissolve = _DissolveAmount;
                float edge = dissolve + _EdgeWidth;
                
                if (dissolveMask < dissolve)
                    discard;
                
                half4 baseColor = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, IN.uv) * _BaseColor;
                
                if (dissolveMask < edge)
                {
                    float edgeFactor = (edge - dissolveMask) / _EdgeWidth;
                    baseColor = lerp(baseColor, _EdgeColor * _EdgeIntensity, edgeFactor);
                }
                
                Light mainLight = GetMainLight();
                float3 lightColor = mainLight.color * mainLight.distanceAttenuation;
                float NdotL = saturate(dot(IN.normalWS, mainLight.direction));
                baseColor.rgb *= lightColor * NdotL;
                
                return baseColor;
            }
            ENDHLSL
        }
    }
}