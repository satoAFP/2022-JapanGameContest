using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputColor_Script : Base_Color_Script
{
    private int[] my_color = new int[3];
    GameObject MixObj;
    [SerializeField]
    private bool mixObj_hit;
    [SerializeField]
    private int cnt;          // ����ʍs�̂��߂̗D��x

    //�A�N�Z�T�[
    public int GetPrecedence()
    {
        return cnt;
    }
    public void SetPrecedence(int num)
    {
        cnt = num;
    }

    private void Start()
    {
        colorchange_signal = false;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ColorMix")
        {
            MixObj = collision.gameObject;
            mixObj_hit = true;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ColorInput")
        {
            //ColorInput�����ꂽ��AColorInput�ɓd�C�������Ȃ��Ȃ������̏���
            if (collision.gameObject.GetComponent<Base_Enegization>().GetEnergization() == false && energization == true)
            {
                if (mixObj_hit == true)
                {
                    MixObj.GetComponent<MixColor_Script>().Decolorization(color);
                }
                //���L��MixColorObj��E�F�ł���悤��true�ɂ���
                energization = false;
                //ColorInput����F���擾
                SetColor(collision.gameObject.GetComponent<Base_Color_Script>().GetColor(), SUBTRACTION);
                GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 255);
            }
        }
        else if (collision.gameObject.tag == "ColorOutput")
        {
            //�d�C���ʂ��Ă��邩�ǂ����m�F�B
            if (collision.gameObject.GetComponent<OutputColor_Script>().GetEnergization() == false && energization == true)
            {
                //�������Ă���Obj�̗D��x(cnt�ϐ�)��0(0�Ȃ炷�łɒE�F����Ă�)�łȂ��A����Obj��菬�����Ȃ�Aenergization�͓r�؂�Ă�̂ŐF��j������B
                if (collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence() != 0 && cnt > collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence())
                {
                    energization = false;
                    if (mixObj_hit)
                    {
                        MixObj.GetComponent<MixColor_Script>().Decolorization(color);
                    }
                    //ColorInput����F���擾
                    SetColor(collision.gameObject.GetComponent<OutputColor_Script>().GetColor());
                    GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 255);

                }
            }
            else if (collision.gameObject.GetComponent<OutputColor_Script>().GetEnergization() == true && energization == false)
            {
                //�D��x(cnt�ϐ�)��0(0�Ȃ�E�F����Ă�)�łȂ��A����Obj��菬�����Ȃ炻��Obj�̐F���擾����B
                if ((collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence() != 0 || cnt < collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence()))
                {
                    cnt = collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence() + 1;
                    energization = true;
                    colorchange_signal = true;
                    //ColorInput����F���擾
                    SetColor(collision.gameObject, ADDITION);
                    GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 255);

                    if (mixObj_hit)
                    {
                        MixObj.GetComponent<Base_Color_Script>().SetColor(color, ADDITION);
                        MixObj.GetComponent<Base_Color_Script>().SetColorChange(true);
                    }
                }
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //OutputColor����F���̂Ă�
        if (collision.gameObject.tag == "ColorInput")
        {
            Debug.Log("�����Ƃ΂��[�I");
            if (mixObj_hit == true)
            {
                MixObj.GetComponent<MixColor_Script>().Decolorization(color);
            }
            energization = false;
            colorchange_signal = false;
            SetColor(collision.gameObject.GetComponent<Base_Color_Script>().GetColor(), SUBTRACTION);
            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 255);

        }
        else if (collision.gameObject.tag == "ColorMix")
        {
            collision.gameObject.GetComponent<MixColor_Script>().Decolorization(color);
        }
    }
}
