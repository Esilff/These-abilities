void DeuteranopiaTransform_float(float3 RGB, out float3 DeuteranopiaColor)
{
    // Deuteranopia color transformation matrix
    float3x3 deuteranopiaMatrix = float3x3(
        1.0, 0.0, 0.0, // Red channel
        0.7, 1.0, 0.0, // Green channel
        0.0, 0.0, 1.0 // Blue channel
    );

    // Apply the matrix to the input color
    DeuteranopiaColor = mul(deuteranopiaMatrix, RGB);

    // Optional: Shift greens towards yellows or blues
    //DeuteranopiaColor.g = lerp(DeuteranopiaColor.g, DeuteranopiaColor.b, 0.5); // Shift greens towards blues
}