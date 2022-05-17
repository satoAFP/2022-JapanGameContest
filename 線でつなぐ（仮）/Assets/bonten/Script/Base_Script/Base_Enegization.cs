using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Enegization : MonoBehaviour
{
    [SerializeField]
    protected bool energization=false;        //電気が通ってるかどうか


    //energizationのセッター。
    public bool GetEnergization()
    {
        return energization;
    }
    public void SetEnergization(bool electoric)
    {
        energization = electoric;

    }
}
