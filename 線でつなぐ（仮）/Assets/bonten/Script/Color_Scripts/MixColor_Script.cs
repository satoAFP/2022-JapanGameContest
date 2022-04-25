using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixColor_Script : Base_Color_Script
{
    
    //�q�I�u�W�F�N�g�擾�p
    private GameObject child;

    

    // Start is called before the first frame update
    void Start()
    {
        //�q�I�u�W�F�N�g���擾
        child = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public bool GetColorChange() => colorchange_signal;

    public void OnCollisionStay(Collision collision)
    {
        //�^�OColorOutput�I�u�W�F�N�g����F���擾���A���̐F�ɕύX
        if (collision.gameObject.tag == "ColorOutput")
        {
            if (collision.gameObject.GetComponent<Base_Color_Script>().GetEnergization() == true)
            {
                if (colorchange_signal == false)
                {
                    SetColor(collision.gameObject, ADDITION);
                  //GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);

                    //�q�I�u�W�F�N�g�ɐF���o���w�߂��o��
                    child.GetComponent<MIxColorChild_Script>().SetColCulation(ADDITION);
                }
            }
            else
            {
                SetColor(collision.gameObject, ADDITION);
               // GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);

                //�q�I�u�W�F�N�g�ɐF�������w�߂��o��
                child.GetComponent<MIxColorChild_Script>().SetColCulation(ADDITION);
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        //�^�OColorOutput�I�u�W�F�N�g����F���擾���A�ݒ肳�ꂽ�l�����̃I�u�W�F�N�g�̕ϐ�����������ƂŒE�F
        if (collision.gameObject.tag == "ColorOutput")
        {
            //���̌�A���ꂽColorInput�������Ă���F�̒l�������ꂪ�����Ă�F�̒l���猸�炷
            SetColor(collision.gameObject, SUBTRACTION);
          //  GetComponent<Renderer>().material.color = new Color32((byte)color[COLOR_RED], (byte)color[COLOR_BLUE], (byte)color[COLOR_GREEN], 1);
            //�q�I�u�W�F�N�g�ɐF�������w�߂��o��
            child.GetComponent<MIxColorChild_Script>().SetColCulation(SUBTRACTION);
        }
    }

    
}
