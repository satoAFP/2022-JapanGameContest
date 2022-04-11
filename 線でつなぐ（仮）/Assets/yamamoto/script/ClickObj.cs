using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObj : MonoBehaviour
{
    //public GameObject Ray;
    //BoxCastRayTest script;//スクリプトの宣言

    public bool move;//移動許可フラグ

    public bool grab;//掴みフラグ

    // Start is called before the first frame update
    void Start()
    {
        //script = Ray.GetComponent<BoxCastRayTest>();//スクリプトを代入
        move = false;//初期化
        grab = false;//初期化
    }

    // Update is called once per frame
    void Update()
    {
        if(move == true)
        {
            Debug.Log("受け渡し");
            //左クリックを受け付ける&掴んでいない状態
            if (Input.GetMouseButtonDown(0)&&grab==false)
            {
                Debug.Log("掴む");
                grab = true;//掴みフラグをtrue
            }
            //左クリックを受け付ける&掴んでいる状態
            else if (Input.GetMouseButtonDown(0) && grab == true)
            {
                Debug.Log("離す");

                grab = false;//掴みフラグをfalse

            }
        }
        else
        {
            Debug.Log("???");
        }       
    }
}
