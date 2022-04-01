using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conductor_Script : MonoBehaviour
{
    protected
        bool energization = false;
    public
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //
    void OnCollisionStay(Collider c)
    {
        //電源と接触してるとき
        if(c.CompareTag("Power_Supply"))
        {
            //通電変数をtrueにし、色を赤に変更
            energization = true;
            GetComponent<Renderer>().material.color = Color.red;
        }
        //電源と接触せずに導体と接触してるとき
        else if(c.CompareTag("Conductor"))
        {
            //絶縁体と接触してるとき
            if (c.CompareTag("Insulator"))
            {
                //通電変数をfalseにする
                energization = false;
            }
            else
            {
                //通電変数をtrueにし、色を赤に変更
                energization = true;
                GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }
}
