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
        //‘S—v‘fˆê’v‚©”»’f
        IEnumerable<bool> results = (IEnumerable<bool>)ClearTaskflag.Distinct();
        //boolŒ^‚Å”»’è‚Å‚«‚é‚æ‚¤‚É•ÏŠ·
        foreach (bool Check in results)
           
        //—v‘f‚Ìƒ^ƒCƒv‚Æˆê’v‚©‚Ç‚¤‚©‚Ì”»’è
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
}
