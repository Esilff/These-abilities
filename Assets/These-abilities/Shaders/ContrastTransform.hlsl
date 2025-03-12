void ContrastTransform_float(float3 RGB, out float3 ContrastColor)
{
    // Contrast color transformation matrix
    float3x3 contrastMatrix = float3x3(
        1.5, 0.0, 0.0, // Red channel
        0.0, 1.5, 0.0, // Green channel
        0.0, 0.0, 1.5 // Blue channel
    );
    
    // Apply the matrix to the input color
    ContrastColor = mul(contrastMatrix, RGB);
}