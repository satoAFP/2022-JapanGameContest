using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ê›íËÇ≥ÇÍÇΩêFÇ…ïœÇ¶ÇÈÇæÇØ
public class InputColor_Script : Base_Color_Script
{

    // Start is called before the first frame update
    void Start()
    {
        if(energization == true) GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
