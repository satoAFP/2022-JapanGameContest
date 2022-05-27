using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_AssistColor : MonoBehaviour
{
    //アシストする回転オブジェクト
    [SerializeField]
    private GameObject AssistingObj;

    [SerializeField]
    private string AssistTag;

    private void Start()
    {
            Debug.Log(AssistTag);
        
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(AssistTag))
        {
            AssistingObj.GetComponent<Base_RotateAssist>().SetCheckRight(true);
            AssistingObj.GetComponent<Base_RotateAssist>().SetCheckLeft(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag(AssistTag))
        {
            AssistingObj.GetComponent<Base_RotateAssist>().SetCheckRight(false);
            AssistingObj.GetComponent<Base_RotateAssist>().SetCheckLeft(false);
        }
    }
}
