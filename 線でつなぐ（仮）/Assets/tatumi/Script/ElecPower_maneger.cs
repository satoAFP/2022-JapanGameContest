using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecPower_maneger : MonoBehaviour
{
    //��Ƃ����L�ł���z��
    public bool[] flags = new bool[10];
    public GameObject[] gimicks=new GameObject[10];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Change(bool a,int number)
    {
        if (a == true)
        {
            //���ז�����
            if (gimicks[2].GetComponent<ElecPower>().flag == true)
            {

            }
            //�����Ƃ��i�����H�j
            else
            {
                gimicks[number + 1].GetComponent<ElecPower>().flag = true;
            }
        }
        //�t�p�^�[���i�����H�j
        else
        {
            gimicks[number - 1].GetComponent<ElecPower>().flag = false;
        }

        return true;
    }
}
