using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climbing_check : MonoBehaviour
{
    public bool check = false;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<BlockCheck>() != null)
            if(col.gameObject.GetComponent<BlockCheck>().block==true)
                check = true;
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponent<BlockCheck>() != null)
            if (col.gameObject.GetComponent<BlockCheck>().block == true)
                check = false;
    }
}
