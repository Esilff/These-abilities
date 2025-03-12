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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            CreateColorBlindnessPass(options.ProtanopiaMaterial);
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            CreateColorBlindnessPass(options.DeuteranopiaMaterial);
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            CreateColorBlindnessPass(options.TritanopiaMaterial);
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            CreateColorBlindnessPass(options.AchromiaMaterial);
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            CreateColorBlindnessPass(options.ContrastMaterial);
        }
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
}
