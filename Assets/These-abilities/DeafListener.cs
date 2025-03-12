using System;
using System.Collections.Generic;
using UnityEngine;

public class DeafListener : MonoBehaviour
{
    private List<Quaternion> _emitters;

    public List<Quaternion> Emitters { get => _emitters; set => _emitters = value; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _emitters = new List<Quaternion>();
    }

    private void FixedUpdate()
    {
        Debug.Log($"Emitters count: {_emitters.Count}");
        _emitters.Clear();
    }
}
