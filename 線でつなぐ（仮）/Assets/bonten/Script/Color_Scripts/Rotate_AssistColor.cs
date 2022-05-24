using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_AssistColor : Base_Color_Script
{
    //�A�V�X�g�����]�I�u�W�F�N�g
    [SerializeField]
    private GameObject AssistingObj;

    [SerializeField]
    private string AssistTag;

    private void Start()
    {
        AssistTag = AssistingObj.tag;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "ColorOutput")
        {
            Debug.Log("���O����}�W�Ō����Ă�H");
            AssistingObj.GetComponent<Rotate_OutputColor>().SetCheckRight(true);
            AssistingObj.GetComponent<Rotate_OutputColor>().SetCheckLeft(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "ColorOutput")
        {
            AssistingObj.GetComponent<Rotate_OutputColor>().SetCheckRight(false);
            AssistingObj.GetComponent<Rotate_OutputColor>().SetCheckLeft(false);
        }
    }
}
