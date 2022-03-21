using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecPower_maneger : MonoBehaviour
{
    //二つとも共有できる配列
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
            //お邪魔判定
            if (gimicks[2].GetComponent<ElecPower>().flag == true)
            {

            }
            //無いとき（いらん？）
            else
            {
                gimicks[number + 1].GetComponent<ElecPower>().flag = true;
            }
        }
        //逆パターン（いらん？）
        else
        {
            gimicks[number - 1].GetComponent<ElecPower>().flag = false;
        }

        return true;
    }
}
