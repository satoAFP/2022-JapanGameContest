using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObj : MonoBehaviour
{
    //public GameObject Ray;
    //BoxCastRayTest script;//�X�N���v�g�̐錾

    public bool move;//�ړ����t���O

    public bool grab;//�͂݃t���O

    // Start is called before the first frame update
    void Start()
    {
        //script = Ray.GetComponent<BoxCastRayTest>();//�X�N���v�g����
        move = false;//������
        grab = false;//������
    }

    // Update is called once per frame
    void Update()
    {
        if(move == true)
        {
            Debug.Log("�󂯓n��");
            //���N���b�N���󂯕t����&�͂�ł��Ȃ����
            if (Input.GetMouseButtonDown(0)&&grab==false)
            {
                Debug.Log("�͂�");
                grab = true;//�͂݃t���O��true
            }
            //���N���b�N���󂯕t����&�͂�ł�����
            else if (Input.GetMouseButtonDown(0) && grab == true)
            {
                Debug.Log("����");

                grab = false;//�͂݃t���O��false

            }
        }
        else
        {
            Debug.Log("???");
        }       
    }
}
