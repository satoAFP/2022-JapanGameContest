using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCastRayTest : MonoBehaviour
{

    [SerializeField]
    private Transform targetTra;
    //�@�^�[�Q�b�g�Ƃ̋���
    private float distanceFromTargetObj;

    // Update is called once per frame
    void Update()
    {

        //�@�^�[�Q�b�g�Ƃ̋���
        distanceFromTargetObj = Vector3.Distance(transform.position, targetTra.position);

        RaycastHit hit;
        //�@Cube�̃��C���΂��^�[�Q�b�g�ƐڐG���Ă��邩����
        if (Physics.BoxCast(transform.position, Vector3.one * 0.5f, transform.forward, out hit, Quaternion.identity, 100f, LayerMask.GetMask("Target")))
        {
            Debug.Log(hit.transform.name);
        }
    }

    void OnDrawGizmos()
    {
        //�@Cube�̃��C���^���I�Ɏ��o��
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + transform.forward * distanceFromTargetObj, Vector3.one);
    }
}