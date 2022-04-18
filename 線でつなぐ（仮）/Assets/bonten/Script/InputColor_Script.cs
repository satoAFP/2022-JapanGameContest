using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputColor_Script : MonoBehaviour
{
    //�F�̒l�̍ő�l
    private const int COLOR_MAXNUM = 255;
    //�J���[�̐�
    private const int COLOR_RED   = 0;
    private const int COLOR_BRUE  = 1;
    private const int COLOR_GREEN = 2;
    private const int COLOR_MAX   = 3;

    [NamedArrayAttribute(new string[] { "RED", "BRUE", "GREEN" })][SerializeField]
    private int[] color = new int[COLOR_MAX];


    //�F�̃Z�b�^�[
    public void SetColorRed(int red)
    {
        color[COLOR_RED] = red;
    }
    public void SetColorBlue(int brue)
    {
        color[COLOR_BRUE] = brue;
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
        return color[COLOR_BRUE];
    }
    public int GetColorGreen()
    {
        return color[COLOR_GREEN];
    }
    //public int GetColor(int element)
    //{
    //    if (element < 0 || element > 2)
    //    {
    //        return 0;//�v�f�O�̏ꍇ��0��Ԃ�
    //    }
    //    else
    //    {
    //        return color[element];
    //    }
    //}

    public void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BRUE], (byte)color[COLOR_GREEN], 1);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(color[COLOR_RED]);
    }
}
