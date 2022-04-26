using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�F�̊�ՃX�N���v�g
public class Base_Color_Script : Base_Enegization
{
    //�萔-------------------------------------------------------
    //�F�̒l�̍ő�l
    protected const int COLOR_MAXNUM = 255;
    protected const int COLOR_MINNUM = 0;
    //�J���[�̐�
    protected const int COLOR_RED = 0;
    protected const int COLOR_GREEN = 1;
    protected const int COLOR_BLUE = 2;
    protected const int COLOR_MAX = 3;

    protected const short NONE_COL = 2;             //�v�Z�Ȃ�
    protected const short ADDITION = 1;             //���Z
    protected const short SUBTRACTION = 0;          //���Z

    //-----------------------------------------------------------

    [SerializeField]
    protected bool colorchange_signal = false;

    //�v�f�̖��O��ύX
    [NamedArrayAttribute(new string[] { "RED", "GREEN", "BRUE" })]
    [SerializeField]
    protected int[] color = new int[COLOR_MAX];

    //�Q�[���I�u�W�F�N�g�������Ƃ��Ď����A���̃I�u�W�F�N�g�̐F�����g�̐F�ɉe��������֐�
    //����1 -> �Q�[���I�u�W�F�N�g�^�̈���
    //����2 -> ���Z���(true)���A���Z���(false)�����߂�
    public void SetColor(GameObject obj, short  col)
    {
        if(col == ADDITION)
        {
            color[COLOR_RED]   += obj.gameObject.GetComponent<Base_Color_Script>().GetColorRed();
            color[COLOR_GREEN] += obj.gameObject.GetComponent<Base_Color_Script>().GetColorGreen();
            color[COLOR_BLUE]  += obj.gameObject.GetComponent<Base_Color_Script>().GetColorBlue();
            for (int i = 0; i < COLOR_MAX; i++)
            {
                if (color[i] > COLOR_MAXNUM)
                {
                    //�F�̒l�̍ő�l�𒴂��Ă���COLOR_MAXNUM(255)�ɌŒ�
                    color[i] = COLOR_MAXNUM;
                }
            }
        }
        else if(col == SUBTRACTION)
        {
            color[COLOR_RED]   = obj.gameObject.GetComponent<Base_Color_Script>().GetColorRed();
            color[COLOR_GREEN] = obj.gameObject.GetComponent<Base_Color_Script>().GetColorGreen();
            color[COLOR_BLUE]  = obj.gameObject.GetComponent<Base_Color_Script>().GetColorBlue();
            for (int i = 0; i < COLOR_MAX; i++)
            {
                if (color[i] < 0)
                {
                    //�F�̒l�̍ŏ��l��������Ă���0�ɂ���
                    color[i] = 0;
                }
            }
        }
    }

    public void SetColor(int[] _color, short col)
    {
        if (col == ADDITION)
        {
            color[COLOR_RED]   += _color[COLOR_RED];
            color[COLOR_GREEN] += _color[COLOR_GREEN];
            color[COLOR_BLUE]  += _color[COLOR_BLUE];
            for (int i = 0; i < COLOR_MAX; i++)
            {
                if (color[i] > COLOR_MAXNUM)
                {
                    //�F�̒l�̍ő�l�𒴂��Ă���COLOR_MAXNUM(255)�ɌŒ�
                    color[i] = COLOR_MAXNUM;
                }
            }
        }
        else if (col == SUBTRACTION)
        {
            color[COLOR_RED]   -= _color[COLOR_RED];
            color[COLOR_GREEN] -= _color[COLOR_GREEN];
            color[COLOR_BLUE]  -= _color[COLOR_BLUE];
            for (int i = 0; i < COLOR_MAX; i++)
            {
                if (color[i] < 0)
                {
                    //�F�̒l�̍ŏ��l��������Ă���0�ɂ���
                    color[i] = 0;
                }
            }
        }
    }
    public void SetColorChange(bool change) => colorchange_signal = change;

    //�F�̃Z�b�^�[
    public void SetColorRed(int red) => color[COLOR_RED] = red;
    public void SetColorBlue(int brue) => color[COLOR_BLUE] = brue;
    public void SetColorGreen(int green) => color[COLOR_GREEN] = green;

    //�F�̃Q�b�^�[�B�����ŗv�f�����w�肵�Ď擾

    public int GetColorRed() => color[COLOR_RED];

    public int GetColorBlue() => color[COLOR_BLUE];

    public int GetColorGreen() => color[COLOR_GREEN];

    public int[] GetColor() => color;
}
