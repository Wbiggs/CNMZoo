#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

TEXTURE2D(_Caustics);
SAMPLER(sampler_Caustics);

float invLerp(float from, float to, float value)
{
    return (value - from) / (to - from);
}

float remap(float origFrom, float origTo, float targetFrom, float targetTo, float value)
{
    float rel = invLerp(origFrom, origTo, value);
    return lerp(targetFrom, targetTo, rel);
}

half2 Panner(half2 uv, half speed, half tiling)
{
    return (half2(1, 0) * _Time.y * speed) + (uv * tiling);
}

half4 SampleCaustics(half2 uv, half split)
{
    half2 uv1 = uv + half2(split, split);
    half2 uv2 = uv + half2(split, -split);
    half2 uv3 = uv + half2(-split, -split);

    half r = remap(-0.1, 1, 0, 1, SAMPLE_TEXTURE2D(_Caustics, sampler_Caustics, uv1).r);
    half g = remap(-0.1, 1, 0, 1, SAMPLE_TEXTURE2D(_Caustics, sampler_Caustics, uv2).r);
    half b = remap(-0.1, 1, 0, 1, SAMPLE_TEXTURE2D(_Caustics, sampler_Caustics, uv3).r);

    half a = SAMPLE_TEXTURE2D(_Caustics, sampler_Caustics, uv1);

    return half4(r, g, b, a);
}