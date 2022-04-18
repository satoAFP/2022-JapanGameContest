using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixColor_Script : MonoBehaviour
{
    //�F�̒l�̍ő�l
    private const int COLOR_MAXNUM = 255;
    //�J���[�̐�
    private const int COLOR_RED = 0;    //��
    private const int COLOR_BRUE = 1;   //��
    private const int COLOR_GREEN = 2;  //��
    private const int COLOR_MAX = 3;    //�v�f��
    private int[] color = new int[COLOR_MAX];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name== "Cylinder")
        {
            color[COLOR_RED]   += collision.gameObject.GetComponent<InputColor_Script>().GetColorRed();
            color[COLOR_BRUE]  += collision.gameObject.GetComponent<InputColor_Script>().GetColorBlue();
            color[COLOR_GREEN] += collision.gameObject.GetComponent<InputColor_Script>().GetColorGreen();

            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BRUE], (byte)color[COLOR_GREEN], 1);
        }
    }
}
