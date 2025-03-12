void AchromiaTransform_float(float3 RGB, out float3 AchromiaColor)
{
    // Achromia color transformation matrix
    float3x3 achromiaMatrix = float3x3(
        0.2126, 0.7152, 0.0722, // Red channel
        0.2126, 0.7152, 0.0722, // Green channel
        0.2126, 0.7152, 0.0722 // Blue channel
    );
    
    // Apply the matrix to the input color
    AchromiaColor = mul(achromiaMatrix, RGB);
}