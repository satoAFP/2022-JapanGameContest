using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class landing_check : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        GameObject.Find("player").GetComponent<player>().Ground_check = true;
    }

    private void OnTriggerExit(Collider col)
    {
        GameObject.Find("player").GetComponent<player>().Ground_check = false;
    }
}
