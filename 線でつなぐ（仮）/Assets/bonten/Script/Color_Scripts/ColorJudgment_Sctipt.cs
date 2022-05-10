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

    // Update is called once per frame
    void Update()
    {
        //�J���[�`�F���W�̐M��������ꂽ��F��ς���B
        if(colorchange_signal==true)
        {
            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED],  (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 1);
            colorchange_signal = false;
        }

        if (clearColor[COLOR_RED] == color[COLOR_RED] && clearColor[COLOR_GREEN] == color[COLOR_GREEN] && clearColor[COLOR_BLUE] == color[COLOR_BLUE])
        {
            //�����ɃN���A�̏ؓI�ȃR�[�h
            objs[0].SetActive(false);
            objs[1].SetActive(true);
        }
    }


}
