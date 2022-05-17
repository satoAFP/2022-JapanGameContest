using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpos : MonoBehaviour
{
    private GameObject BoxCastRayTest;

    // Start is called before the first frame update
    void Start()
    {
        BoxCastRayTest = GameObject.Find("fps_camera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //線（シリンダー）に判定ブロックが当たっているときその上にブロックを置けなくする
        if(other.gameObject.tag=="Conductor"|| other.gameObject.tag == "ColorOutput")
        {
            Debug.Log("いい流れが出来てるよ！");
            BoxCastRayTest.GetComponent<BoxCastRayTest>().Existence_Check = true;
        }

        //電源オブジェクトに判定ブロックが当たっているときその上にブロックを置けなくする
        if (other.gameObject.tag == "Power_Supply")
        {
            Debug.Log("ティーダの〇〇〇気持ちよすぎだろ！");
            BoxCastRayTest.GetComponent<BoxCastRayTest>().NosetLight = true;
        }
        else
        {
            Debug.Log("ヒューズの〇〇〇気持ちよすぎだろ！");
            BoxCastRayTest.GetComponent<BoxCastRayTest>().NosetLight = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //電源オブジェクトに判定ブロックが当たっているときその上にブロックを置けなくする
        if (other.gameObject.tag == "Power_Supply")
        {
           // Debug.Log("ヒューズの〇〇〇気持ちよすぎだろ！");
            //BoxCastRayTest.GetComponent<BoxCastRayTest>().NosetLight = true;
        }
    }
}
