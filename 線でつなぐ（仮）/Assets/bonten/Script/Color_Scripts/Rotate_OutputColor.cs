using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_OutputColor : Base_Color_Script
{

    private const int RIGHT = 0;         //���̃I�u�W�F�N�g
    private const int LEFT = 1;     //����ȊO�̃I�u�W�F�N�g

    [SerializeField]
    [NamedArrayAttribute(new string[] { "right", "left" })]
    GameObject[] AssistObj = new GameObject[2];

    private int cnt = 0;


    //�A�N�Z�T�[
    public int GetPrecedence()
    {
        return cnt;
    }
    public void SetPrecedence(int num)
    {
        cnt = num;
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "ColorOutput")
        {
            if (AssistObj[RIGHT].GetComponent<Rotate_AssistColor>().GetHitTarget() && AssistObj[LEFT].GetComponent<Rotate_AssistColor>().GetHitTarget())
            {
                //�d�C���ʂ��Ă��邩�ǂ����m�F�B
                if ((collision.gameObject.GetComponent<OutputColor_Script>().GetEnergization() == false && energization == true))
                {
                    //�������Ă���Obj�̗D��x(cnt�ϐ�)��0(0�Ȃ炷�łɒE�F����Ă�)�łȂ��A����Obj��菬�����Ȃ�Aenergization�͓r�؂�Ă�̂ŐF��j������B
                    if (collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence() != 0 && cnt > collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence())
                    {
                        energization = false;
                        //ColorInput����F���擾
                        SetColor(collision.gameObject.GetComponent<OutputColor_Script>().GetColor());
                        GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200);
                    }
                }
                else if (collision.gameObject.GetComponent<OutputColor_Script>().GetEnergization() == true && energization == false)
                {
                    //�D��x(cnt�ϐ�)��0(0�Ȃ�E�F����Ă�)�łȂ��A����Obj��菬�����Ȃ炻��Obj�̐F���擾����B
                    if ((collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence() != 0 || cnt < collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence()))
                    {
                        //�ڐG���Ă�RelayColor�̃J�E���g���1�傫���l���擾����i����ʍs�ɂ��邽�߁j
                        cnt = collision.gameObject.GetComponent<OutputColor_Script>().GetPrecedence() + 1;
                        energization = true;
                        colorchange_signal = true;
                        //ColorInput����F���擾
                        SetColor(collision.gameObject, ADDITION);
                        GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200);
                    }
                }
            }
            //�A�V�X�gobj���ق���RelayColor�ƐڐG���Ă��Ȃ���ΒE�F����B
            else if (energization == true)
            {
                Debug.Log("1");
                energization = false;
                //ColorInput����F���擾
                SetColor(color,SUBTRACTION);
                GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)200);
            }
        }
    }
}

