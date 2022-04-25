using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coloer : MonoBehaviour
{
    public int a=0;
    private bool A;

    [SerializeField]
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
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

      

        image.color = new Color32(255,255,255,(byte) a);
        
    }
}
