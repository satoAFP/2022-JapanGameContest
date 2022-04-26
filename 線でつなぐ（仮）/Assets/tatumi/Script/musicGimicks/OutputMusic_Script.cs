using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputMusic_Script : Base_Color_Script
{
    private int[] my_color = new int[3];
    GameObject MixObj;
    private void Start()
    {
        colorchange_signal = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="ColorMix")
        {
            MixObj = collision.gameObject;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ColorInput")
        {
            //電気は電源なので必ず通るので取得＆自身に代入
            collision.gameObject.GetComponent<InputMusic_Script>().
            
              
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //OutputColorから色を捨てる
        if (collision.gameObject.tag == "ColorInput")
        {
            energization = false;
            SetColor(collision.gameObject, SUBTRACTION);
            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 1);
        }
    }
}
