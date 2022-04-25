using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputColor_Script : Base_Color_Script
{
    bool color_change = true;


    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ColorInput")
        {
            //電気が通っているならこのInputColorの色を取得し、このオブジェクト自体の色も変更
            if (collision.gameObject.GetComponent<InputColor_Script>().GetEnergization() == true)
            {
                energization = true;
                //ColorInputから色を取得
                SetColor(collision.gameObject, ADDITION);
               // GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);
            }
            else
            {
                energization = false;
                //ColorInputから色を取得
                SetColor(collision.gameObject, SUBTRACTION);
                //GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //OutputColorから色を捨てる
        if (collision.gameObject.tag == "ColorInput")
        {
            energization = false;
            SetColor(collision.gameObject, SUBTRACTION);
          //  GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);
        }
    }
}
