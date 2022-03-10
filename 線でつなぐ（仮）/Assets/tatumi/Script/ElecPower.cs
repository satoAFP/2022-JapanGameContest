using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElecPower : MonoBehaviour
{
    public int mynumber;
    public GameObject maneger;
    public bool flag,change;

    ElecPower_maneger SC_EPmane;

    // Start is called before the first frame update
    void Start()
    {
        flag = false;
        change = false;

        SC_EPmane = maneger.GetComponent<ElecPower_maneger>();
    }

    // Update is called once per frame
    void FiexdUpdate()
    {
        //関数にしておくこと
        if(flag != change)
        {
            change = flag;
            //マテリアル変更
            if(change==true)
            {
                SC_EPmane.flags[mynumber] = flag;
                SC_EPmane.Change(flag, mynumber);
            }
            else
            {

            }
        }
        
    }
}
