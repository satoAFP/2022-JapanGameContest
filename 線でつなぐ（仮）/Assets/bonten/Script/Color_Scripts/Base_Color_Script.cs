using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�F�̊�ՃX�N���v�g
public class Base_Color_Script : MonoBehaviour
{
    //�萔-------------------------------------------------------
    //�F�̒l�̍ő�l
    protected const int COLOR_MAXNUM = 255;
    //�J���[�̐�
    protected const int COLOR_RED = 0;
    protected const int COLOR_GREEN = 1;
    protected const int COLOR_BLUE = 2;
    protected const int COLOR_MAX = 3;

    protected const bool ADDITION = true;               //���Z
    protected const bool SUBTRACTION = false;           //���Z

    //-----------------------------------------------------------

    [SerializeField]
    protected bool colorchange_signal = false;

    //�v�f�̖��O��ύX
    [NamedArrayAttribute(new string[] { "RED", "BRUE", "GREEN" })]
    [SerializeField]
    protected int[] color = new int[COLOR_MAX];

    //�Q�[���I�u�W�F�N�g�������Ƃ��Ď����A���̃I�u�W�F�N�g�̐F�����g�̐F�ɉe��������֐�
    //����1 -> �Q�[���I�u�W�F�N�g�^�̈���
    //����2 -> ���Z���(true)���A���Z���(false)�����߂�
    public void SetColor(GameObject obj, bool col)
    {
        if(col == ADDITION)
        {
            color[COLOR_RED]   += obj.gameObject.GetComponent<Base_Color_Script>().GetColorRed();
            color[COLOR_GREEN] += obj.gameObject.GetComponent<Base_Color_Script>().GetColorGreen();
            color[COLOR_BLUE]  += obj.gameObject.GetComponent<Base_Color_Script>().GetColorBlue();
        }
        else if(col == SUBTRACTION)
        {
            color[COLOR_RED]   -= obj.gameObject.GetComponent<Base_Color_Script>().GetColorRed();
            color[COLOR_GREEN] -= obj.gameObject.GetComponent<Base_Color_Script>().GetColorGreen();
            color[COLOR_BLUE] -= obj.gameObject.GetComponent<Base_Color_Script>().GetColorBlue();
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
}
