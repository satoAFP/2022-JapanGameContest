using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//設定された色に変えるだけ
public class InputMusic_Script : Base_Enegization
{
    [SerializeField, Header("SE番号セット")]
    private int SE_set;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int REset_num()
    {
        return SE_set;
    }
}
