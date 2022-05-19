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
    private GameObject OnLight;         //�������������̂ق���obj
    [SerializeField]
    private GameObject OffLight;        //����������������Ȃ��ق���obj

    [SerializeField]
    private bool MCmode;

    private void Start()
    {
        //���������̃I�u�W�F�N�g���擾
        //OnLight = transform.Find("OnLight").gameObject;
        //OffLight = transform.Find("OffLight").gameObject;
        OffLight.GetComponent<Renderer>().material.color=new Color32((byte)(clearColor[COLOR_RED]/5), (byte)(clearColor[COLOR_GREEN]/5), (byte)(clearColor[COLOR_BLUE]/5), 255);
    }

    // Update is called once per frame
    void Update()
    {
        //�F�ύX�̐M�����󂯎���
        if (colorchange_signal == true)
        {
            //�F�ύX������
            OnLight.GetComponent<Base_Color_Script>().SetColor(color);
            OnLight.GetComponent<Base_Color_Script>().SetColorChange(true);

            //�O�E��0�Ȃ����
            if (color[COLOR_RED] == 0 && color[COLOR_GREEN] == 0 && color[COLOR_BLUE] == 0)
            {
                OnLight.SetActive(false);
                OffLight.SetActive(true);
            }
            else
            {
                OnLight.SetActive(true);
                OffLight.SetActive(false);
            }

            colorchange_signal = false;
        }

        if (MCmode == false)
        {
            if (clearColor[COLOR_RED] == color[COLOR_RED] && clearColor[COLOR_GREEN] == color[COLOR_GREEN] && clearColor[COLOR_BLUE] == color[COLOR_BLUE])
            {
                Debug.Log("���肠�[");
                //�����ɃN���A�̏ؓI�ȃR�[�h
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
