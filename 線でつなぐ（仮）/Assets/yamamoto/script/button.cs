using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class button : MonoBehaviour
{

    public Animator anim;

    public Material[] mat = new Material[1];//�ύX�������}�e���A�����Z�b�g
    Material[] mats;

    [SerializeField]
    private float waittime;//�{�^���N���b�N��̑ҋ@����

    // Start is called before the first frame update
    void Start()
    {
        mats = GetComponent<Renderer>().materials;
    }

    // Update is called once per frame
    void Update()
    {
        //�߂�

        mats[0] = mat[1];

        GetComponent<Renderer>().materials = mats;
    }

    public void RayPushButton()
    {
        anim.SetBool("Push", true);
        StartCoroutine("ButtonPushed");
    }

    IEnumerator ButtonPushed()
    {
        Debug.Log("�͂�");
        //1�b��~
        yield return new WaitForSeconds(waittime);
        anim.SetBool("Push", false);
    }

    public void RayTargetButton()
    {
        //�}�e���A���ύX(1=����0=����)

        mats[0] = mat[0];

        GetComponent<Renderer>().materials = mats;

        Debug.Log("�~�X�^�[k");

    }
}
