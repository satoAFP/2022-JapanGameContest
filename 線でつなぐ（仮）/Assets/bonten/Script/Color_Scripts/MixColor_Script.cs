using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixColor_Script : Base_Color_Script
{

    //�q�I�u�W�F�N�g�擾�p
    [SerializeField]
    private GameObject child;
    private bool decolor = false;

    //�E�F����
    public void Decolorization(int[] decolor)
    {
        //�q�E�F�I�u�W�F�N�g�ɐF���o���w�߂��o��
        child.GetComponent<MIxColorChild_Script>().SetColCulation(SUBTRACTION, decolor);

        color[COLOR_RED] -= decolor[COLOR_RED];
        color[COLOR_GREEN] -= decolor[COLOR_GREEN];
        color[COLOR_BLUE] -= decolor[COLOR_BLUE];


        for (short i = 0; i < COLOR_MAX; i++)
        {
            if (color[i] < 0)
            {
                color[i] = 0;
            }
        }

        GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        //�^�OColorOutput�I�u�W�F�N�g����F���擾���A���̐F�ɕύX
        if (collision.gameObject.tag == "ColorOutput")
        {
            //ColorOutoput��energization��true�Ȃ炱���ɓ���
            if (collision.gameObject.GetComponent<Base_Enegization>().GetEnergization() && colorchange_signal)
            {
                colorchange_signal = false;

                GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200);
                //�q��obj�ɐF����͂���M�����o��
                child.GetComponent<MIxColorChild_Script>().SetColCulation(ADDITION);
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //�^�OColorOutput�I�u�W�F�N�g����F���擾���A�ݒ肳�ꂽ�l�����̃I�u�W�F�N�g�̕ϐ�����������ƂŒE�F
        if (collision.gameObject.tag == "ColorOutput")
        {
            //���̌�A���ꂽColorInput�������Ă���F�̒l�������ꂪ�����Ă�F�̒l���猸�炷
            SetColor(collision.gameObject, SUBTRACTION);
            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200);
            //�q�I�u�W�F�N�g�ɐF�������w�߂��o��
            child.GetComponent<MIxColorChild_Script>().SetColCulation(SUBTRACTION);
        }
    }
}
