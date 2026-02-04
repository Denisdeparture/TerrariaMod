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
float2 hash(float2 x, float flow)
{
    float2 k = float2(0.3183099, 0.3678794);
    x = x * k + k.yx;
    float h = frac(16.0 * k.x * frac(x.x * x.y * (x.x + x.y)));
    float a = 6.2831 * h + flow;
    return float2(cos(a), sin(a));
}

float3 noise2d(float2 p, float flow)
{
    float2 i = floor(p);
    float2 f = frac(p);

    float2 u = f * f * (3.0 - 2.0 * f);
    float2 du = 6.0 * f * (1.0 - f);

    float2 ga = hash(i + float2(0.0, 0.0), flow);
    float2 gb = hash(i + float2(1.0, 0.0), flow);
    float2 gc = hash(i + float2(0.0, 1.0), flow);
    float2 gd = hash(i + float2(1.0, 1.0), flow);

    float va = dot(ga, f - float2(0.0, 0.0));
    float vb = dot(gb, f - float2(1.0, 0.0));
    float vc = dot(gc, f - float2(0.0, 1.0));
    float vd = dot(gd, f - float2(1.0, 1.0));

    float2 u_smooth = u;

    float val = lerp(lerp(va, vb, u_smooth.x), lerp(vc, vd, u_smooth.x), u_smooth.y);
    float2 grad = ga + u_smooth.x * (gb - ga) + u_smooth.y * (gc - ga) + u_smooth.x * u_smooth.y * (gd - gb - gc + ga);
    float2 derivs = du * ((va - vb - vc + vd));

    return float3(val, grad.x + derivs.x, grad.y + derivs.y);
}

// Основная функция шумов
float4 noise2d_full(float2 x, float2 stretch, float2 dir, float2 offset, float freq, int octaves, float rough, float distort, float droop, float spread, float flow)
{
    float a = 0.0;
    float b = 1.0;
    float2 d = float2(0.0, 0.0);
    float3 n = float3(0.0, 0.0, 0.0);
    float2 warp = float2(0.0, 0.0);
    float rot = 0.0;

    for (int i = 0; i < octaves; i++)
    {
        float2 p = x * freq * stretch + dir + offset * (n.x * droop) + warp * distort;
        n = noise2d(p, flow);

        // обновляем stretch, x, a, b, d, flow
        stretch = lerp(stretch, float2(1, 1), float(i) / float(octaves));
        x += float2(1.0, 0.0);
        a += n.x * b;
        b *= rough;
        d += n.yz * sqrt(b) * stretch;
        droop *= rough;
        freq *= 2.0;
        warp = d;
        flow *= sqrt(b);
        rot += radians(spread);
        float c = cos(rot);
        float s = sin(rot);
        float2 rotDir = float2(c, s);
        dir = float2(dir.x * c - dir.y * s, dir.x * s + dir.y * c);
    }

    a *= 1.0 * 0.8;
    d *= 1.0;

    return float4(a, normalize(float3(d.x, 1.0, d.y)));
}

// Pixel Shader
float4 Bankai(float2 fragCoord : TEXCOORD) : SV_Target
{
    float time = uTime * 1.5;

    float2 p = (-uScreenResolution.xy + 2.0 * fragCoord) / uScreenResolution.y;

    float ramp = distance(fragCoord, uTargetPosition) * 0.0025;
    float fitramp = smoothstep(0.618, 1.0, ramp);
    float distort = clamp(lerp(-1.0, 1.0, fitramp), -1.0, 1.0);

    p *= 2.0;

    float4 n2d = noise2d_full(
        p,
        float2(1.0, 1.0),
        float2(0.0, 1.0) * -uTime * 0.5,
        float2(0.0, 1.0),
        1.0,
        6,
        0.56,
        0.5 * -distort,
        0.0,
        20.0,
        ((-uTime * 5.0) + (ramp * 10.0))
    );

    float3 nx = n2d.yzw;
    float nf = n2d.x;
    nf = pow(nf, 2.0);

    float3 col = (p.x > 10.5) ? (0.5 + 0.5 * -nx.yzx) :
                                float3(nf * nf * 0.5, nf * nf * 0.7, nf);

    return float4(col, 1.0);
}

technique EffectTechnique
{
    pass Bankai
    {
        PixelShader = compile ps_3_0 Bankai();
    }
}

