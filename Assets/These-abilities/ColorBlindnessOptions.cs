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
}
