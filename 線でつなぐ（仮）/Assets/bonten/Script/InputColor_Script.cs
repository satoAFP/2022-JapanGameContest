using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputColor_Script : Base_Color_Script
{
    // Start is called before the first frame update
    void Start()
    {
        //0ÇÊÇËâ∫Ç…Ç»Ç¡ÇΩÇËÅA255ÇÊÇËè„Ç…Ç»Ç¡ÇΩÇËÇµÇ»Ç¢ÇÊÇ§Ç…Ç∑ÇÈ
        for(int i=0;i<COLOR_MAX;i++)
        {
            if(color[i]>COLOR_MAXNUM)
            {
                color[i] = COLOR_MAXNUM;
            }
            else if(color[i]<0)
            {
                color[i] = 0;
            }
        }
        GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);
    }
}
