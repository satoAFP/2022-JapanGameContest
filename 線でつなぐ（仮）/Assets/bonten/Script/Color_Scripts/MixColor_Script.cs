using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixColor_Script : Base_Color_Script
{
   
    // Start is called before the first frame update
    void Start()
    {
        colorchange_signal = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ColorOutput")
        {
            color[COLOR_RED]    += collision.gameObject.GetComponent<OutputColor_Script>().GetColorRed();
            color[COLOR_BLUE]   += collision.gameObject.GetComponent<OutputColor_Script>().GetColorBlue();
            color[COLOR_GREEN]  += collision.gameObject.GetComponent<OutputColor_Script>().GetColorGreen();
            for (int i = 0; i < COLOR_MAX; i++)
            {
                //色の値の最大値を超えてたらCOLOR_MAXNUM(255)に固定
                if (color[i] > COLOR_MAXNUM)
                {
                    color[i] = COLOR_MAXNUM;
                }
            }
            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);
            
            colorchange_signal = true;
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "ColorOutput")
        {
            //contact_colorinput--;//接触しているColorInputの保存数をはなれた数だけ減らす

            //その後、離れたColorInputが持っている色の値を今これが持ってる色の値から減らす
            color[COLOR_RED]    -= collision.gameObject.GetComponent<OutputColor_Script>().GetColorRed();
            color[COLOR_BLUE]   -= collision.gameObject.GetComponent<OutputColor_Script>().GetColorBlue();
            color[COLOR_GREEN]  -= collision.gameObject.GetComponent<OutputColor_Script>().GetColorGreen();

            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);
        }
    }

    public void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag == "ColorOutput" && colorchange_signal == true)
        {
            colorchange_signal = false;
        }
    }
}
