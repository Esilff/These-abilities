void TritanopiaTransform_float(float3 RGB, out float3 TritanopiaColor)
{
    // Tritanopia color transformation matrix
    float3x3 tritanopiaMatrix = float3x3(
        1.0, 0.0, 0.0, // Red channel
        0.0, 1.0, 0.7, // Green channel
        0.0, 0.0, 1.0 // Blue channel
    );

    // Apply the matrix to the input color
    TritanopiaColor = mul(tritanopiaMatrix, RGB);

    // Optional: Shift blues towards pinks or purples
    //TritanopiaColor.b = lerp(TritanopiaColor.b, TritanopiaColor.r, 0.3); // Shift blues towards reds
}