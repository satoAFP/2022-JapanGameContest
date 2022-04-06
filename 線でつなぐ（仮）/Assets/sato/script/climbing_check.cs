using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climbing_check : MonoBehaviour
{
    public bool check = false;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Respawn")
        {
            check = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Respawn")
        {
            check = false;
        }
    }
}
