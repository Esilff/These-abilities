using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorBlindnessOptions", menuName = "These-abilities/ColorBlindnessOptions")]
public class ColorBlindnessOptions : ScriptableObject
{
    [SerializeField] private Material _protanopiaMaterial;
    [SerializeField] private Material _deuteranopiaMaterial;
    [SerializeField] private Material _tritanopiaMaterial;
    [SerializeField] private Material _achromiaMaterial;
    [SerializeField] private Material _contrastMaterial;

    public Material ProtanopiaMaterial => _protanopiaMaterial;
    public Material DeuteranopiaMaterial => _deuteranopiaMaterial;
    public Material TritanopiaMaterial => _tritanopiaMaterial;
    public Material AchromiaMaterial => _achromiaMaterial;
    public Material ContrastMaterial => _contrastMaterial;

    public List<string> GetColorblindnessOptions()
    {
        List<string> options = new List<string>();
        options.Add("None");
        if (_protanopiaMaterial)
        {
            options.Add("Protanopia");
        }

        if (_deuteranopiaMaterial)
        {
            options.Add("Deuteranopia");
        }

        if (_tritanopiaMaterial)
        {
            options.Add("Tritanopia");
        }

        if (_achromiaMaterial)
        {
            options.Add("Achromia");
        }

        if (_contrastMaterial)
        {
            options.Add("Contrast");
        }
        return options;
    }
}
