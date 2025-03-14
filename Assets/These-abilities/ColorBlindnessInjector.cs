using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ColorBlindnessInjector : MonoBehaviour
{
    [SerializeField] private UniversalRendererData urd;
    
    [SerializeField] private ColorBlindnessOptions options;

    private FullScreenPassRendererFeature _colorBlindnessFullScreenPass;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Awake()
    // {
    // }

    void OnDestroy()
    {
        urd.rendererFeatures.Remove(_colorBlindnessFullScreenPass);
    }
    

    void CreateColorBlindnessPass(Material colorBlindnessMat)
    {
        if (_colorBlindnessFullScreenPass != null)
        {
            urd.rendererFeatures.Remove(_colorBlindnessFullScreenPass);
        }
        _colorBlindnessFullScreenPass = ScriptableObject.CreateInstance<FullScreenPassRendererFeature>();
        _colorBlindnessFullScreenPass.name = "ColorBlindness Full Screen Pass";
        _colorBlindnessFullScreenPass.fetchColorBuffer = true;
        _colorBlindnessFullScreenPass.injectionPoint =
            FullScreenPassRendererFeature.InjectionPoint.BeforeRenderingPostProcessing;
        _colorBlindnessFullScreenPass.requirements = ScriptableRenderPassInput.None;
        _colorBlindnessFullScreenPass.passMaterial = colorBlindnessMat;

        urd.rendererFeatures.Add(_colorBlindnessFullScreenPass);
        urd.SetDirty();
    }

    public List<string> GetOptions()
    {
        return options.GetColorblindnessOptions();
    }

    public void SetColorblindnessMode(string mode)
    {
        if (mode == "None" && _colorBlindnessFullScreenPass != null)
        {
            urd.rendererFeatures.Remove(_colorBlindnessFullScreenPass);
            urd.SetDirty();
            
        }

        if (mode == "Protanopia")
        {
            CreateColorBlindnessPass(options.ProtanopiaMaterial);
        }
        if (mode == "Deuteranopia")
        {
            CreateColorBlindnessPass(options.DeuteranopiaMaterial);
        }
        if (mode == "Tritanopia")
        {
            CreateColorBlindnessPass(options.TritanopiaMaterial);
        }
        if (mode == "Achromia")
        {
            CreateColorBlindnessPass(options.AchromiaMaterial);
        }
        if (mode == "Contrast")
        {
            CreateColorBlindnessPass(options.ContrastMaterial);
        }
    }
}
