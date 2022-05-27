using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_music_Assist : MonoBehaviour
{
    //�A�V�X�g�����]�I�u�W�F�N�g
    [SerializeField]
    private GameObject AssistingObj;

    [SerializeField]
    private string AssistTag;

    //���O�ꕔ�擾�i������肠����̂͂��ׂĎ擾,�������s�H�j
    private string OutColor_name;

    private void Start()
    {
        AssistTag = AssistingObj.tag;
        //���O
        OutColor_name = "OutM&C";
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "MusicOutput")
        {
            AssistingObj.GetComponent<Rotate_music>().SetCheckRight(true);
            AssistingObj.GetComponent<Rotate_music>().SetCheckLeft(true);
        }
        else if(collider.gameObject.name.Contains(OutColor_name) == true)
        {
            AssistingObj.GetComponent<Rotate_music>().SetCheckRight(true);
            AssistingObj.GetComponent<Rotate_music>().SetCheckLeft(true);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "MusicOutput")
        {
            AssistingObj.GetComponent<Rotate_music>().SetCheckRight(false);
            AssistingObj.GetComponent<Rotate_music>().SetCheckLeft(false);
        }
        else if (collider.gameObject.name.Contains(OutColor_name) == true)
        {
            AssistingObj.GetComponent<Rotate_music>().SetCheckRight(true);
            AssistingObj.GetComponent<Rotate_music>().SetCheckLeft(true);
        }
    }
}
