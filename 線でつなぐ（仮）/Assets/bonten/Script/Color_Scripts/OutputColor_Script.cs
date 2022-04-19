using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputColor_Script : Base_Color_Script
{
    bool color_change = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ColorInput")
        {
            
            //ColorInputÇ©ÇÁêFÇéÊìæ
            color[COLOR_RED]    += collision.gameObject.GetComponent<InputColor_Script>().GetColorRed();
            color[COLOR_GREEN]  += collision.gameObject.GetComponent<InputColor_Script>().GetColorGreen();
            color[COLOR_BLUE]   += collision.gameObject.GetComponent<InputColor_Script>().GetColorBlue();

            Debug.Log(color);
            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //MixColorÇ©ÇÁêFÇéÃÇƒÇÈ
        if (collision.gameObject.tag == "ColorInput")
        {
            color[COLOR_RED]   -= collision.gameObject.GetComponent<InputColor_Script>().GetColorRed();
            color[COLOR_GREEN] -= collision.gameObject.GetComponent<InputColor_Script>().GetColorGreen();
            color[COLOR_BLUE]  -= collision.gameObject.GetComponent<InputColor_Script>().GetColorBlue();
            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);
        }
    }
}
