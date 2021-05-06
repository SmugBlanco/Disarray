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
float2 point1;
float2 point2;
float2 point3;

bool PointInTriangle(float2 A, float2 B, float2 C, float2 P)
{
    double s1 = C.y - A.y;
    double s2 = C.x - A.x;
    double s3 = B.y - A.y;
    double s4 = P.y - A.y;
    
    if (s1 == 0)
    {
        s1 = 1;
    }

    double w1 = (A.x * s1 + s4 * s2 - P.x * s1) / (s3 * s2 - (B.x - A.x) * s1);
    double w2 = (s4 - w1 * s3) / s1;
    
    return w1 >= 0 && w2 >= 0 && (w1 + w2) <= 1;
}

float4 ProtectiveSights(float4 position : SV_POSITION, float2 coords : TEXCOORD0) : COLOR0
{
    float4 currentPixel = tex2D(uImage0, coords);
    
    if (!active)
    {
        return currentPixel;
    }
    
    float2 widthRelativeToHeight = uScreenResolution.x / uScreenResolution.y;
    float2 currentCoordinate = coords * widthRelativeToHeight;
    float middle = 0.5 * widthRelativeToHeight;
    float2 coordsRelativeToScreenSize = coords * uScreenResolution;

    if (PointInTriangle(point1, point2, point3, coordsRelativeToScreenSize))
    {
        return currentPixel;
    }
    
    return float4(0, 0, 0, 1);
}

technique Technique1
{
    pass ProtectiveSights
    {
        PixelShader = compile ps_2_0 ProtectiveSights();
    }
}