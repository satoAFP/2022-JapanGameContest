//����񂭂ˁH����

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObj : MonoBehaviour
{
    public bool move;//�ړ����t���O

    public bool grab;//�͂݃t���O

    // Start is called before the first frame update
    void Start()
    {
        move = false;//������
        grab = false;//������
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(transform.position);

        if (move == true)
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
