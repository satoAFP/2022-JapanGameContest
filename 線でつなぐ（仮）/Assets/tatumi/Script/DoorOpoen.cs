using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoorOpoen : MonoBehaviour
{
    public bool[] ClearTaskflag;

    
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //全要素一致か判断
        IEnumerable<bool> results = (IEnumerable<bool>)ClearTaskflag.Distinct();
        //bool型で判定できるように変換
        foreach (bool Check in results)
           
        //要素のタイプと一致かどうかの判定
        if (results.Count() == 1 && Check == true)
        {
           anim.SetBool("Open", true);
           Debug.Log("true");
        }
        else 
        {
            anim.SetBool("Open", false);
            Debug.Log("false");
        }

        

    }

    public void RayOpenDoor()
    {

    }
}
