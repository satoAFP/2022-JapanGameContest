using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MIxColorChild_Script : Base_Color_Script
{
    //ColorJudgement_Scriptに色の値を渡す時に減算か加算か判断させる用変数
    private short colculation;

    [SerializeField]
    private GameObject parent;

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
            Debug.Log("Take Off");
            SetColor(parent.gameObject.GetComponent<Base_Color_Script>().GetColor());
            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)128);
            colorchange_signal = true;
            colculation = NONE_COL;
        }
        else if (colculation == SUBTRACTION)
        {
            Debug.Log("勝利の大凱旋");
            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], (byte)128);
            colorchange_signal = true;
            colculation = NONE_COL;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Power_Supply" && colorchange_signal == true)
        {
            //ライトobjに色を送信
            collision.gameObject.GetComponent<ColorJudgment_Sctipt>().SetColor(color);
            collision.gameObject.GetComponent<ColorJudgment_Sctipt>().SetColorChange(colorchange_signal);

            colorchange_signal = false;
        }
    }
}
