//Terraria already provides these parameters for us, otherwise we can just set them when using the shaders

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

float4 Tint(float4 oldColor, float4 refColor, float tintProgress)
{
    float4 colorDifference = (refColor - oldColor) * tintProgress;
    return oldColor + colorDifference;
}

float DistanceSQ(float2 position, float2 positionToCompare)
{
    return pow(positionToCompare.x - position.x, 2) + pow(positionToCompare.y - position.y, 2);
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

float4 Pestillence(float4 position : SV_POSITION, float2 coords : TEXCOORD0) : COLOR0
{
    //float2 targetCoords = (uTargetPosition - uScreenPosition) / uScreenResolution - float2(0.5, 0.5);
    float4 currentPixel = tex2D(uImage0, coords);
    float multiplierBasedOnDistance = Clamp(DistanceSQ(coords, float2(0.5, 0.5)) * 2, 0, 1);
    float tintMultiplier = uProgress * (1 - uIntensity) * multiplierBasedOnDistance;
    return Tint(currentPixel, float4(0.63, 0.78, 0.16, 1), tintMultiplier);
}

technique Technique1
{
	pass Pestillence
	{
		PixelShader = compile ps_2_0 Pestillence();
	}
}