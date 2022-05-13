using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorJudgment_Sctipt : Base_Color_Script
{
    //�萔-----------------------------------------------------------------------------
    

    //---------------------------------------------------------------------------------


    //�Q�[���N���A�ƂȂ�J���[
    [NamedArrayAttribute(new string[] { "RED",  "GREEN", "BRUE" })]
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

        OnLight = transform.Find("OnLight").gameObject;
        OffLight = transform.Find("OffLight").gameObject;

        Debug.Log(OnLight.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(colorchange_signal==true)
        {
            Debug.Log("�z�������΂�");
            
            OnLight.SetActive(true);
            OffLight.SetActive(false);
            OnLight.GetComponent<Base_Color_Script>().SetColor(color);
            OnLight.GetComponent<Base_Color_Script>().SetColorChange(true);

            if (color[COLOR_RED] == 0 && color[COLOR_GREEN] == 0 && color[COLOR_BLUE] == 0)
            {
                OnLight.SetActive(false);
                OffLight.SetActive(true);
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
