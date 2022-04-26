using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_or_Child_Script : Base_Color_Script
{
    //ColorJudgement_ScriptÇ…êFÇÃílÇìnÇ∑éûÇ…å∏éZÇ©â¡éZÇ©îªífÇ≥ÇπÇÈópïœêî
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (colculation == ADDITION)
        {
            SetColor(parent.gameObject, colculation);
            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED],  (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 1);
            parent.gameObject.GetComponent<MixColor_Script>().SetColorChange(false);
            colorchange_signal = true;
        }
        else if (colculation == SUBTRACTION)
        {
            GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_GREEN], (byte)color[COLOR_BLUE], 1);
            colorchange_signal = true;
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
            
            collision.gameObject.GetComponent<ColorJudgment_Sctipt>().SetColor(this.gameObject, colculation);
            collision.gameObject.GetComponent<ColorJudgment_Sctipt>().SetColorChange(colorchange_signal);
            
            colorchange_signal = false;
        }
    }
}
