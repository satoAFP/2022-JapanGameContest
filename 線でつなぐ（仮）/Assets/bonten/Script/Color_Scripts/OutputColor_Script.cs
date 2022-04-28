using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputColor_Script : Base_Color_Script
{
    private int[] my_color = new int[3];
    GameObject MixObj;
    private bool mixObj_hit; 

    private void Start()
    {
        colorchange_signal = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="ColorMix")
        {
            MixObj = collision.gameObject;
            mixObj_hit = true;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ColorInput")
        {
            //電気が通っているならこのInputColorの色を取得し、このオブジェクト自体の色も変更
            if (collision.gameObject.GetComponent<InputColor_Script>().GetEnergization() == true && energization == false)
            {
                energization = true;
                colorchange_signal = true;
                //ColorInputから色を取得
                SetColor(collision.gameObject, ADDITION);
                GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 1);
            }
            else if (collision.gameObject.GetComponent<InputColor_Script>().GetEnergization() == false && energization == true)
            {
                if (mixObj_hit == true)
                {
                    MixObj.GetComponent<MixColor_Script>().Decolorization(color, this.gameObject);
                }
                //下記のMixColorObjを脱色できるようにtrueにする
                energization = false;
                //ColorInputから色を取得
                SetColor(collision.gameObject.GetComponent<Base_Color_Script>().GetColor(), SUBTRACTION);
                GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 1);
            }
        }
        else if(collision.gameObject.tag == "ColorOutput")
        {
            //電気が通っているならこのInputColorの色を取得し、このオブジェクト自体の色も変更
            if (collision.gameObject.GetComponent<OutputColor_Script>().GetEnergization() == true && energization == false)
            {
                energization = true;
                colorchange_signal = true;
                //ColorInputから色を取得
                SetColor(collision.gameObject, ADDITION);
                GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 1);
            }
            else if (collision.gameObject.GetComponent<OutputColor_Script>().GetEnergization() == false && energization == true)
            {
                if (mixObj_hit == true)
                {
                    MixObj.GetComponent<MixColor_Script>().Decolorization(color, this.gameObject);
                }
                //下記のMixColorObjを脱色できるようにtrueにする
                energization = false;
                //ColorInputから色を取得
                SetColor(collision.gameObject, SUBTRACTION);
                GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 1);
            }
        }

    }

    public void OnCollisionExit(Collision collision)
    {
        //OutputColorから色を捨てる
        if (collision.gameObject.tag == "ColorInput")
        {
            MixObj.GetComponent<MixColor_Script>().Decolorization(color, this.gameObject);
            energization = false;
            SetColor(collision.gameObject.GetComponent<Base_Color_Script>().GetColor(), SUBTRACTION);
            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 1);
        }
    }
}
