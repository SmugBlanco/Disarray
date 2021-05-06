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
bool active;
float intensity;

float4 Tint(float4 oldColor, float4 refColor, float tintProgress)
{
    float4 colorDifference = (refColor - oldColor) * tintProgress;
    return oldColor + colorDifference;
}

float Clamp(float value, float minimum, float maximum)
{
    if (value < minimum)
    {
        return minimum;
    }
    
    if (value > maximum)
    {
        return maximum;
    }
    
    return value;
}

float4 Flashbang(float4 position : SV_POSITION, float2 coords : TEXCOORD0) : COLOR0
{
    float4 currentPixel = tex2D(uImage0, coords);
    
    if (!active)
    {
        return currentPixel;
    }
    
    float bangIntensity = Clamp(intensity, 0, 1);
    return Tint(currentPixel, float4(1, 1, 1, 0), bangIntensity);
}

technique Technique1
{
    pass Flashbang
    {
        PixelShader = compile ps_2_0 Flashbang();
    }
}