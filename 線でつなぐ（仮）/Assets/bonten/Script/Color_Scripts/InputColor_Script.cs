using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ê›íËÇ≥ÇÍÇΩêFÇ…ïœÇ¶ÇÈÇæÇØ
public class InputColor_Script : Base_Color_Script
{
    public void Start()
    {
        energization = true;
    }
    // Update is called once per frame
    public void Update()
    {
        if (energization == true) GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200);
    }
}
