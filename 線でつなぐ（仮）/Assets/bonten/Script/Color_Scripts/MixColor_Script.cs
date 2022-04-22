using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixColor_Script : Base_Color_Script
{
    //�q�I�u�W�F�N�g�擾�p
    private GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        child = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public bool GetColorChange() => colorchange_signal;

    public void OnCollisionEnter(Collision collision)
    {
        //�^�OColorOutput�I�u�W�F�N�g����F���擾���A���̐F�ɕύX
        if (collision.gameObject.tag == "ColorOutput")
        {
            color[COLOR_RED]    += collision.gameObject.GetComponent<Base_Color_Script>().GetColorRed();
            color[COLOR_BLUE]   += collision.gameObject.GetComponent<Base_Color_Script>().GetColorBlue();
            color[COLOR_GREEN]  += collision.gameObject.GetComponent<Base_Color_Script>().GetColorGreen();
            for (int i = 0; i < COLOR_MAX; i++)
            {
                if (color[i] > COLOR_MAXNUM)
                {
                    //�F�̒l�̍ő�l�𒴂��Ă���COLOR_MAXNUM(255)�ɌŒ�
                    color[i] = COLOR_MAXNUM;
                }
                else if (color[i] < 0) 
                {
                    //�F�̒l�̍ŏ��l��������Ă���0�ɂ���
                    color[i] = 0;
                }
            }
            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);

            colorchange_signal = true;
            child.GetComponent<MIxColorChild_Script>().SetColCulation(ADDITION);
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //�^�OColorOutput�I�u�W�F�N�g����F���擾���A�ݒ肳�ꂽ�l�����̃I�u�W�F�N�g�̕ϐ�����������ƂŒE�F
        if (collision.gameObject.tag == "ColorOutput")
        {
            //���̌�A���ꂽColorInput�������Ă���F�̒l�������ꂪ�����Ă�F�̒l���猸�炷
            color[COLOR_RED]    -= collision.gameObject.GetComponent<OutputColor_Script>().GetColorRed();
            color[COLOR_BLUE]   -= collision.gameObject.GetComponent<OutputColor_Script>().GetColorBlue();
            color[COLOR_GREEN]  -= collision.gameObject.GetComponent<OutputColor_Script>().GetColorGreen();

            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);
            colorchange_signal = true;
            child.GetComponent<MIxColorChild_Script>().SetColCulation(SUBTRACTION);
        }
    }

    
}
