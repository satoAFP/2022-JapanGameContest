using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Color_Script : MonoBehaviour
{
    //�F�̒l�̍ő�l
    protected const int COLOR_MAXNUM = 255;
    //�J���[�̐�
    protected const int COLOR_RED = 0;
    protected const int COLOR_GREEN = 1;
    protected const int COLOR_BLUE = 2;
    protected const int COLOR_MAX = 3;

    [SerializeField]
    protected bool colorchange_signal = false;

    [NamedArrayAttribute(new string[] { "RED", "BRUE", "GREEN" })]
    [SerializeField]
    protected int[] color = new int[COLOR_MAX];


    //�F�̃Z�b�^�[
    public void SetColorRed(int red)
    {
        color[COLOR_RED] = red;
    }
    public void SetColorBlue(int brue)
    {
        color[COLOR_BLUE] = brue;
    }
    public void SetColorGreen(int green)
    {
        color[COLOR_GREEN] = green;
    }

    //�F�̃Q�b�^�[�B�����ŗv�f�����w�肵�Ď擾

    public int GetColorRed()
    {
        return color[COLOR_RED];
    }
    public int GetColorBlue()
    {
        return color[COLOR_BLUE];
    }
    public int GetColorGreen()
    {
        return color[COLOR_GREEN];
    }
}
