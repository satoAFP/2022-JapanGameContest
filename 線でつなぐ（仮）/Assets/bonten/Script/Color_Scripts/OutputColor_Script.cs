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
            //�d�C���ʂ��Ă���Ȃ炱��InputColor�̐F���擾���A���̃I�u�W�F�N�g���̂̐F���ύX
            if (collision.gameObject.GetComponent<InputColor_Script>().GetEnergization() == true)
            {
                energization = true;
                //ColorInput����F���擾
                SetColor(collision.gameObject, ADDITION);
               // GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);
            }
            else
            {
                energization = false;
                //ColorInput����F���擾
                SetColor(collision.gameObject, SUBTRACTION);
                //GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);
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
          //  GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);
        }
    }
}
