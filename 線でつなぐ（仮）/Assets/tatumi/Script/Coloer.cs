using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coloer : MonoBehaviour
{
    private int a,time=0;
    private bool A;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //time++;
        if (a < 1)
        {
            A = true;
        }
        else if (a > 255)
        {
            A = false;
        }
        

        if (A == true)
            a++;
        else
            a--;



        this.gameObject.GetComponent<Image>().color = new Color(255.0f,255.0f, 255.0f, a);
    }
}
