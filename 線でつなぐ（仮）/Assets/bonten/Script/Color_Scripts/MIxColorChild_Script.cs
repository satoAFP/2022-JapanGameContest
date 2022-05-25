using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIxColorChild_Script : Base_Color_Script
{
    //ColorJudgement_Script�ɐF�̒l��n�����Ɍ��Z�����Z�����f������p�ϐ�
    private short colculation = 0;

    [SerializeField]
    private GameObject parent;
    [SerializeField]
    private GameObject efflight;

    public void SetColCulation(short col)
    {
        colculation = col;
    }
    public void SetColCulation(short col, int[] decolor)
    {
        colculation = col;
        SetColor(decolor, colculation);
    }

    // Update is called once per frame
    void Update()
    {
        if (colculation == ADDITION)
        {
            Debug.Log(parent.gameObject.name + "�F"+parent.gameObject.GetComponent<MixColor_Script>().GetColorBlue());
            SetColor(parent.gameObject.GetComponent<MixColor_Script>().GetColor());
            Debug.Log(parent.gameObject.name+"�F"+color[COLOR_BLUE]);
            colorchange_signal = true;
            colculation = NONE_COL;
        }
        else if (colculation == SUBTRACTION)
        {
            efflight.GetComponent<ColorLight_Script1>().SetLight(color);
            colorchange_signal = true;
            colculation = NONE_COL;
        }
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Power_Supply" && colorchange_signal == true)
        {
            //���C�gobj�ɐF�𑗐M
            collision.gameObject.GetComponent<ColorJudgment_Sctipt>().SetColor(color);
            collision.gameObject.GetComponent<ColorJudgment_Sctipt>().SetColorChange(colorchange_signal);

            colorchange_signal = false;
        }
    }
}
