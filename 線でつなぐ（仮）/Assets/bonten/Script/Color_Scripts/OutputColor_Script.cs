using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputColor_Script : Base_Color_Script
{
    bool color_change;

    public void SetMixColor(bool change)
    {
        color_change = change;
    }

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
        if (collision.gameObject.tag == "ColorMix")
            Debug.Log("���������������������ӂ�������");
    }

    private void OnCollisionExit(Collision collision)
    {
        //MixColor����F���̂Ă�
        if (collision.gameObject.tag == "ColorMix")
        {
            color[COLOR_RED]   -= collision.gameObject.GetComponent<MixColor_Script>().GetColorRed();
            color[COLOR_GREEN] -= collision.gameObject.GetComponent<MixColor_Script>().GetColorGreen();
            color[COLOR_BLUE]  -= collision.gameObject.GetComponent<MixColor_Script>().GetColorBlue();
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        //MixColor����F���擾
        if (collision.gameObject.tag == "ColorMix" && color_change == true)
        {
            color[COLOR_RED]     += collision.gameObject.GetComponent<MixColor_Script>().GetColorRed();
            color[COLOR_GREEN]   += collision.gameObject.GetComponent<MixColor_Script>().GetColorGreen();
            color[COLOR_BLUE]    += collision.gameObject.GetComponent<MixColor_Script>().GetColorBlue();
            color_change = false;
            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);
        }
    }
}
