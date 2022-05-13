using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLight_Script : Base_Color_Script
{
    [SerializeField]
    private float intensity_light;


    void Start()
    {
        
        this.gameObject.GetComponent<Light>().intensity = 0.0125f;
    }

    void Update()
    {
        
    }
}
