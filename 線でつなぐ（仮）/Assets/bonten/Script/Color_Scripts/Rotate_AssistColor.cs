using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_AssistColor : Base_Color_Script
{
    //アシストする回転オブジェクト
    [SerializeField]
    private GameObject RotateObj;

    private GameObject ConductorObj;

    //導体と接触してるかの成否
    [SerializeField]
    private bool hitting_target;

    [SerializeField]
    private int cnt = 0;

    public bool GetHitTarget() => hitting_target;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "ColorOutput")
        {
            hitting_target = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "ColorOutput")
        {
            hitting_target = false;
        }
    }
}
