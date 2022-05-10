using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorJudgment_Sctipt : Base_Color_Script
{
    //�Q�[���N���A�ƂȂ�J���[
    [NamedArrayAttribute(new string[] { "RED",  "GREEN", "BRUE" })]
    [SerializeField]
    private int[] clearColor = new int[COLOR_MAX];

    [SerializeField]
    private GameObject[] objs;

    GameObject OffLight;
    GameObject OnLight;

    private void Start()
    {
        OffLight = transform.Find("OffLight").gameObject;
        OnLight = transform.Find("OnLight").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        //�J���[�`�F���W�̐M��������ꂽ��F��ς���B
        if(colorchange_signal==true)
        {
            if (color[COLOR_RED] == 0 && color[COLOR_GREEN] == 0 && color[COLOR_BLUE]==0)
            {
                //����Obj���Active�ɂ��A�����obj��Active�ɂ���
                OffLight.SetActive(true);
                OnLight.SetActive(false);
            }
            else
            {
                //����Obj��Active�ɂ��A�����obj���Active�ɂ���
                OffLight.SetActive(false);
                OnLight.SetActive(true);
                //����Obj�ɐF�����
                OnLight.GetComponent<Base_Color_Script>().SetColor(color);
               
                colorchange_signal = false;
            }
            
        }

        if (clearColor[COLOR_RED] == color[COLOR_RED] && clearColor[COLOR_GREEN] == color[COLOR_GREEN] && clearColor[COLOR_BLUE] == color[COLOR_BLUE])
        {
            //�����ɃN���A�̏ؓI�ȃR�[�h
            objs[0].SetActive(false);
            objs[1].SetActive(true);
        }
    }


}
