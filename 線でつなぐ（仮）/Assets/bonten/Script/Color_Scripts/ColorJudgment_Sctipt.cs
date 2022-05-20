using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorJudgment_Sctipt : Base_Color_Script
{
    //�萔-----------------------------------------------------------------------------


    //---------------------------------------------------------------------------------


    //�Q�[���N���A�ƂȂ�J���[
    [NamedArrayAttribute(new string[] { "RED", "GREEN", "BRUE" })]
    [SerializeField]
    private int[] clearColor = new int[COLOR_MAX];

    //�����n�̎q�I�u�W�F�N�g���擾
    [SerializeField]
    private GameObject Liting;         //�������������̂ق���obj
    [SerializeField]
    private GameObject Nonliting;        //����������������Ȃ��ق���obj

    [SerializeField]
    private bool MCmode;

    // Update is called once per frame
    void Update()
    {
        //�F�ύX�̐M�����󂯎���
        if (colorchange_signal == true)
        {
            //�F�ύX������
            Liting.GetComponent<Base_Color_Script>().SetColorChange(true);
            Liting.GetComponent<Base_Color_Script>().SetColor(color);

            //�O�E��0�Ȃ����
            if (color[COLOR_RED] == 0 && color[COLOR_GREEN] == 0 && color[COLOR_BLUE] == 0)
            {
                //�ʓd�Ȃ�OFF��
                if (Liting.activeSelf == true)
                {
                    Liting.SetActive(false);
                    Nonliting.SetActive(true);


                    this.gameObject.GetComponent<TurnonPower_Script>().else_Switch_off();
                }
            }
          
            colorchange_signal = false;
        }

        if (MCmode == false)
        {
            if (clearColor[COLOR_RED] == color[COLOR_RED] && clearColor[COLOR_GREEN] == color[COLOR_GREEN] && clearColor[COLOR_BLUE] == color[COLOR_BLUE])
            {
                //�ʓd�Ȃ�ON��
                if (Liting.activeSelf == false)
                {
                    Liting.SetActive(true);
                    Nonliting.SetActive(false);
                    this.gameObject.GetComponent<TurnonPower_Script>().else_Switch_on();
                }
            }
        }
        else
        {
            if (clearColor[COLOR_RED] == color[COLOR_RED] && clearColor[COLOR_GREEN] == color[COLOR_GREEN] && clearColor[COLOR_BLUE] == color[COLOR_BLUE])
            {
                //�����ɃN���A�̏ؓI�ȃR�[�h
                energization = true;
            }
            else
            {
                energization = false;
            }
        }
    }
}
