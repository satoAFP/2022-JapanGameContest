using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputColor_Script : MonoBehaviour
{

    //カラーの数
    private const int COLOR_RED   = 0;
    private const int COLOR_BRUE  = 1;
    private const int COLOR_GREEN = 2;
    private const int COLOR_MAX   = 3;

    [NamedArrayAttribute(new string[] { "RED", "BRUE", "GREEN" })][SerializeField]
    private int[] color = new int[COLOR_MAX];


    //色のセッター
    void SetColorRed(int red)
    {
        color[COLOR_RED] = red;
    }
    void SetColorBrue(int brue)
    {
        color[COLOR_BRUE] = brue;
    }
    void SetColorGreen(int green)
    {
        color[COLOR_GREEN] = green;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], 255, 255, 1);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.color = new Color32(255, 255, 255, 1);
    }
}
