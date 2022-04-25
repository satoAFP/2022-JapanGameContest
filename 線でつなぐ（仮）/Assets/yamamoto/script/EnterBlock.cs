using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBlock : MonoBehaviour
{
    [SerializeField]
    private GameObject MapcipSlect;//マップチップスクリプト

    //マップチップの上に他のオブジェクトが乗っているとき
    private void OnTriggerEnter(Collider other)
    {
        //レイヤー番号10：Target
        if(other.gameObject.layer == 10)
        {
            MapcipSlect.GetComponent<MapcipSlect>().Onblock = true;
        }
    }

    //マップチップの上に他のオブジェクトが乗っているとき
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            MapcipSlect.GetComponent<MapcipSlect>().Onblock = false;
        }
    }
}
