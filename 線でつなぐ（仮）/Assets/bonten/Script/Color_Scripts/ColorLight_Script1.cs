using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLight_Script1 : Base_Color_Script
{
    ParticleSystem.MainModule par;

    public void SetLight(int[] color)
    {
        par.startColor = new Color((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200);
    }
    // Start is called before the first frame update
    void Start()
    {
        par = GetComponent<ParticleSystem>().main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
