using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor_Script : MonoBehaviour
{
    protected
        bool energization = false;
    //List<GameObject> colList = new List<GameObject>();//�R���C�_�[���X�g



    public

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    //�����ŃQ�[���I�u�W�F�N�g���X�g�̒��ɓ���̃^�O���������āA����ɉ�������������������
    //    foreach (GameObject Obj in colList)
    //    {
    //        if (Obj.Find("Power_Supply"))
    //        {
    //            energization = true;
    //            GetComponent<Renderer>().material.color = Color.cyan;
    //        }
    //        else if (Obj.Find("Conductor"))
    //        {
    //            energization = true;
    //            GetComponent<Renderer>().material.color = Color.cyan;
    //        }
    //        else if (Obj.Find("Insulator"))
    //        {
    //            energization = false;
    //            GetComponent<Renderer>().material.color = Color.gray;
    //        }

    //    }

    //}

    ////�����ŐG��Ă�I�u�W�F�N�g�����X�g�ɂԂ�����
    //void OnCollisionEnter(Collision c)
    //{
    //    colList.Add(c.gameObject);
    //}

    ////���ꂽ�痣�ꂽ�I�u�W�F�N�g�̃��X�g���폜
    //void OnCollisionExit(Collision c)
    //{
    //    colList.Remove(c.gameObject);
    //}


    void OnCollisionStay(Collision c)
    {
        //�d���ƐڐG���Ă�Ƃ�
        if (c.gameObject.tag == "Power_Supply")
        {
            //�ʓd�ϐ���true�ɂ��A�F�𐅐F�ɕύX
            energization = true;
            GetComponent<Renderer>().material.color = Color.cyan;

            Debug.Log("true");
        }
        //�d���ƐڐG�����ɓ��̂ƐڐG���Ă�Ƃ�!
        else if (c.gameObject.tag == "Conductor")
        {
            //�≏�̂ƐڐG���Ă�Ƃ�
            if (c.gameObject.tag == "Insulator")
            {
                //�ʓd�ϐ���false�ɂ���
                energization = false;
                GetComponent<Renderer>().material.color = Color.gray;
            }
            else
            {
                //�ʓd�ϐ���true�ɂ��A�F�𐅐F�ɕύX
                energization = true;
                GetComponent<Renderer>().material.color = Color.cyan;
            }
        }
    }
}
