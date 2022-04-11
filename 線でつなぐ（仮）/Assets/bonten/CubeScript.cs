using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour
{
    public
    bool nannka = false;
    void Update()
    {
        if (nannka == true)
        {
            GetComponent<Renderer>().material.color = Color.cyan;
        }
        else
        {
            Debug.Log("false");
        }
    }
    
    void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.tag == "Insulator")
        {
            nannka = GameObject.SphereScript.instance.sentence;
        }
    }
}
