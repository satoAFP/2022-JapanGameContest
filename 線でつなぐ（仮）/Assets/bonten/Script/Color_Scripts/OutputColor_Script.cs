using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputColor_Script : Base_Color_Script
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
        //if(collision.gameObject.tag == "ColorMix")
        //{
        //    if (energization == false && colorchange_signal == true)
        //    {
        //        Debug.Log("haire");
        //        collision.gameObject.GetComponent<MixColor_Script>().Decolorization(my_color, this.gameObject);
        //        colorchange_signal = false;
        //    }
        //}
        if (collision.gameObject.tag == "ColorInput")
        {
            //�d�C���ʂ��Ă���Ȃ炱��InputColor�̐F���擾���A���̃I�u�W�F�N�g���̂̐F���ύX
            if (collision.gameObject.GetComponent<InputColor_Script>().GetEnergization() == true && energization == false)
            {
                energization = true;
                colorchange_signal = true;
                //ColorInput����F���擾
                SetColor(collision.gameObject, ADDITION);
                GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 1);
            }
            else if (collision.gameObject.GetComponent<InputColor_Script>().GetEnergization() == false && energization == true)
            {
                MixObj.GetComponent<MixColor_Script>().Decolorization(color, this.gameObject);
                //���L��MixColorObj��E�F�ł���悤��true�ɂ���
                energization = false;
                //ColorInput����F���擾
                SetColor(collision.gameObject, SUBTRACTION);
                GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 1);
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //OutputColor����F���̂Ă�
        if (collision.gameObject.tag == "ColorInput")
        {
            energization = false;
            SetColor(collision.gameObject, SUBTRACTION);
            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 1);
        }
    }
}
