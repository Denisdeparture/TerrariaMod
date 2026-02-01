sampler uImage0 : register(s0);
sampler uImage1 : register(s1);
sampler uImage2 : register(s2);
sampler uImage3 : register(s3);
float3 uColor;
float3 uSecondaryColor;
float2 uScreenResolution;
float2 uScreenPosition;
float2 uTargetPosition;
float2 uDirection;
float uOpacity;
float uTime;
float uIntensity;
float uProgress;
float2 uImageSize1;
float2 uImageSize2;
float2 uImageSize3;
float2 uImageOffset;
float uSaturation;
float4 uSourceRect;
float2 uZoom;
float4 Bankai(float2 texCoord : TEXCOORD0) : COLOR
{
    float fadeDuration = 2.0;
    float2 center = float2(0.5, 0.5);
    float dist = distance(texCoord, center);

    float fadeProgress = saturate(uTime / fadeDuration);
    float fadeAlpha = 1.0 - fadeProgress;

    float radius;
    if (fadeProgress >= 1.0)
    {
        radius = lerp(0.0, 0.5, (uTime - fadeDuration) * 2.0);
    }
    else
    {
        radius = 0.0;
    }

    float circleMask = smoothstep(radius, radius + 0.01, dist);

    float alpha = fadeAlpha * circleMask;

    float4 texColor = tex2D(uImage0, texCoord);

    float4 color = float4(0, 0, 0, alpha);

    return color;
}
technique EffectTechnique
{
    pass Bankai
    {
        PixelShader = compile ps_2_0 Bankai();
    }
}

