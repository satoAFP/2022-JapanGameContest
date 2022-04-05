using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObj : MonoBehaviour
{
    //public GameObject Ray;
    //BoxCastRayTest script;//スクリプトの宣言

    public bool move;//移動許可フラグ

    // Start is called before the first frame update
    void Start()
    {
        //script = Ray.GetComponent<BoxCastRayTest>();//スクリプトを代入
        move = false;//初期化
    }

    // Update is called once per frame
    void Update()
    {
        if(move == true)
        {
            Debug.Log("受け渡し");
        }
        else
        {
            Debug.Log("???");
        }
    }
}
