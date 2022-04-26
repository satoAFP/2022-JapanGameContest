using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicJudgment_Sctipt : Base_Color_Script
{
    //ゲームクリアとなるカラー
    [NamedArrayAttribute(new string[] { "RED", "BRUE", "GREEN" })]
    [SerializeField]
    private int[] clearColor = new int[COLOR_MAX];

    [SerializeField]
    private GameObject[] objs;

    // Update is called once per frame
    void Update()
    {
        if(colorchange_signal==true)
        {
            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);
            colorchange_signal = false;
        }

        objs[0].SetActive(true);
        objs[1].SetActive(false);

        if (clearColor[COLOR_RED] == color[COLOR_RED] && clearColor[COLOR_GREEN] == color[COLOR_GREEN] && clearColor[COLOR_BLUE] == color[COLOR_BLUE])
        {
            Debug.Log("くりあー");
            //ここにクリアの証的なコード
            objs[0].SetActive(false);
            objs[1].SetActive(true);
        }
    }


}
