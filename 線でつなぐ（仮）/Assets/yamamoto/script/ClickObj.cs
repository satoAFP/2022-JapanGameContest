using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObj : MonoBehaviour
{
    //public GameObject Ray;
    //BoxCastRayTest script;//�X�N���v�g�̐錾

    public bool move;//�ړ����t���O

    // Start is called before the first frame update
    void Start()
    {
        //script = Ray.GetComponent<BoxCastRayTest>();//�X�N���v�g����
        move = false;//������
    }

    // Update is called once per frame
    void Update()
    {
        if(move == true)
        {
            Debug.Log("�󂯓n��");
        }
        else
        {
            Debug.Log("???");
        }
    }
}
