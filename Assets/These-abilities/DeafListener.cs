
using System.Collections.Generic;
using UnityEngine;

public class DeafListener : MonoBehaviour
{
    private List<Quaternion> _emitters;

    public List<Quaternion> Emitters { get => _emitters; set => _emitters = value; }

    private Texture2D _dataTexture;

    private void Awake()
    {
        _emitters = new List<Quaternion>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _dataTexture = new Texture2D(256, 256, TextureFormat.RGBA32, false);
        Color[] colors = new Color[256 * 256];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.black;
        }
        _dataTexture.SetPixels(colors);
        _dataTexture.Apply();
    }

    private void FixedUpdate()
    {
        //Debug.Log(Emitters.Count);
    }

    private void LateUpdate()
    {
        _emitters.Clear();
    }

    private Vector2 EmitterToSpherical(Quaternion _emitterDirection)
    {
        var direction = _emitterDirection * Vector3.forward;

        var theta = Mathf.Acos(direction.z); // Polar angle (θ)
        var phi = Mathf.Atan2(direction.y, direction.x); // Azimuthal angle (φ)

        var u = (phi + Mathf.PI) / (2 * Mathf.PI); // Map φ to [0, 1]
        var v = theta / Mathf.PI; // Map θ to [0, 1]

        return new Vector2(u, v);    
    }

    public Texture2D EmitterDataToTexture()
    {
        for(int i = 0; i < _emitters.Count; i++)
        {
            var sphericalCoordinate = EmitterToSpherical(_emitters[i]);
            Debug.Log(sphericalCoordinate);
            float r = sphericalCoordinate.x;
            float g = sphericalCoordinate.y;
            _dataTexture.SetPixel(i,0, new Color(r, g, 0, 1));
        }
        _dataTexture.Apply();
        return _dataTexture;
    }
}
