using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor_Script : MonoBehaviour
{
    protected
        bool energization = false;
    public
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //
    void OnCollisionStay(Collision c)
    {
        //�d���ƐڐG���Ă�Ƃ�
        if(c.gameObject.tag == "Power_Supply")
        {
            //�ʓd�ϐ���true�ɂ��A�F��ԂɕύX
            energization = true;
            GetComponent<Renderer>().material.color = Color.red;

            Debug.Log("true");
        }
        //�d���ƐڐG�����ɓ��̂ƐڐG���Ă�Ƃ�
        else if(c.gameObject.tag == "Conductor")
        {
            //�≏�̂ƐڐG���Ă�Ƃ�
            if (c.gameObject.tag == "Insulator")
            {
                //�ʓd�ϐ���false�ɂ���
                energization = false;
            }
            else
            {
                //�ʓd�ϐ���true�ɂ��A�F��ԂɕύX
                energization = true;
                GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}
