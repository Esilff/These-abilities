void ProtanopiaTransform_float(float3 RGB, out float3 ProtanopiaColor)
{
    // Protanopia color transformation matrix (adjusted for yellowish reds)
    float3x3 protanopiaMatrix = float3x3(
        1.0, 0.7, 0.0, // Red channel
        0.0, 1.0, 0.0, // Green channel
        0.0, 0.0, 1.0 // Blue channel
    );

    // Apply the matrix to the input color
    ProtanopiaColor = mul(protanopiaMatrix, RGB);

    // Optional: Boost blues and yellows for better distinction
    //ProtanopiaColor.b *= 1.2; // Increase blue intensity
    //ProtanopiaColor.g = lerp(ProtanopiaColor.g, ProtanopiaColor.b, 0.3); // Shift greens towards blues
}