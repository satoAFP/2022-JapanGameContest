using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLight_Script : Base_Color_Script
{
    [SerializeField]
    private float intensity_light;


    void Start()
    {
        this.gameObject.GetComponent<Light>().color = new Color(255, 0, 0);
        this.gameObject.GetComponent<Light>().intensity = 0.05f;
    }

    void Update()
    {
        
    }
}
