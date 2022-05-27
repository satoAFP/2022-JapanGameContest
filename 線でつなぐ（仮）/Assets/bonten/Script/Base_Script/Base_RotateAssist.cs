using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_RotateAssist : Base_Enegization
{
    protected const int RIGHT = 0;    //右
    protected const int LEFT = 1;     //左

    [SerializeField]
    [NamedArrayAttribute(new string[] { "right", "left" })]
    protected GameObject[] AssistObj = new GameObject[2];

    [SerializeField]
    [NamedArrayAttribute(new string[] { "right", "left" })]
    protected bool[] hit_check = new bool[2];         //アシストOBJ(両端の球)が当たってるかどうかチェックするよう

    public void SetCheckRight(bool success)
    {
        hit_check[RIGHT] = success;
    }
    public void SetCheckLeft(bool success)
    {
        hit_check[LEFT] = success;
    }
}
